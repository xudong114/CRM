using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Extensions
{
    public class EqualityComparer<TSource> : IEqualityComparer<TSource>
    {
        private Func<TSource, string> Expression;
        public EqualityComparer()
        { }
        public EqualityComparer(Func<TSource, string> func)
        {
            this.Expression = func;
        }

        public bool Equals(TSource x, TSource y)
        {
            if (Expression == null)
                return x.Equals(y);
            return Expression(x) == Expression(y);
        }

        public int GetHashCode(TSource obj)
        {
            if (obj == null || Expression == null)
                return 0;
            return Expression.Invoke(obj).GetHashCode();
        }

    }
}
