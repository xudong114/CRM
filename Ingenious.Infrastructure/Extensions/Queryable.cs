using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Extensions
{
    public static class Queryable
    {
        //
        // Summary:
        //     安全执行计算，如数据源为null则返回目标类型的默认值。
        //     Invokes a projection function on each element of a generic System.Linq.IQueryable<T>
        //     and returns the maximum resulting value.
        //
        // Parameters:
        //   source:
        //     A sequence of values to determine the maximum of.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Type parameters:
        //   TSource:
        //     The type of the elements of source.
        //
        //   TResult:
        //     The type of the value returned by the function represented by selector.
        //
        // Returns:
        //     The maximum value in the sequence.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     source or selector is null.
        public static TResult SafeMax<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            if (source == null || source.LongCount() == 0)
            {
                return default(TResult);
            }

            if (source == null)
            {
                throw new ArgumentNullException("null");
            }
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return source.Provider.Execute<TResult>(Expression.Call(null, ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(new Type[] { typeof(TSource), typeof(TResult) }), new Expression[] { source.Expression, Expression.Quote(selector) }));
        }
       
    }
}
