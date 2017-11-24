﻿using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Infrastructure;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class G_EntityRepository : EntityFrameworkRepository<G_Entity>, IG_EntityRepository
    {
        public G_EntityRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public PagedResult<G_Entity> GetAll(int pageIndex, int pageSize, ISpecification<G_Entity> spec, string sort = "createddate_desc")
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            var query = from o in context.G_Entities.Where(spec.GetExpression())
                        select o;

            switch (sort)
            {
                case "createddate_desc":
                    {
                        query = query.OrderByDescending(item => item.CreatedDate);
                    }
                    break;
                case "createddate":
                    {
                        query = query.OrderBy(item => item.CreatedDate);
                    }
                    break;
                default:
                    {
                        query = query.OrderByDescending(item => item.CreatedDate);
                    }
                    break;
            }

            int skip;
            try
            {
                skip = checked((pageIndex - 1) * pageSize);
            }
            catch (OverflowException)
            {
                skip = 0;
            }

            int take = pageSize;
            var pagedQuery = query.Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
            if (pagedQuery == null)
                return new PagedResult<G_Entity>();
            return new PagedResult<G_Entity>(pagedQuery.Key.Total, (pagedQuery.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedQuery.Select(p => p).ToList());
        }

        public string GetMaxCode()
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            return context.G_Entities.Max(item => item.Code);
        }

    }
}
