using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.EntityFramework
{
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        DbContext Context { get;}
    }
}
