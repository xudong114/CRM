﻿using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class F_NewsRepository : EntityFrameworkRepository<F_News>, IF_NewsRepository
    {
        public F_NewsRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_News> GetAll(ISpecification<F_News> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_News.Where(spec.GetExpression());
        }

    }
}
