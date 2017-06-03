using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain
{
    public interface IUnitOfWork
    {
        bool Committed { get; }

        void Commit();

        void Rollbakc();
    }
}
