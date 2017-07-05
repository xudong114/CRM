using Ingenious.Domain.Models;
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

    public class DictionaryRepository : EntityFrameworkRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Dictionary> GetAll(ISpecification<Dictionary> spec)
        {
            
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Dictionaries.Where(spec.GetExpression());
        }


        public Dictionary GetByCode(string code)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Dictionaries.Where(item => item.Code.Equals(code)).FirstOrDefault();
        }

        public Dictionary GetByName(string name)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Dictionaries.Where(item => item.Name.Equals(name)).FirstOrDefault();
        }

    }
}
