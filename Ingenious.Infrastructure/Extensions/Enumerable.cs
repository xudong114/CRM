using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Extensions
{
    public static class Enumerable
    {
        /// <summary>
        /// 序列化数据集为以“,”分割的字符串。
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="outer"></param>
        /// <returns></returns>
        public static string Join<TResult>(this IEnumerable<TResult> outer)
        {
            var sb = new StringBuilder();
            foreach (var item in outer)
            {
                sb.Append(item.ToString() + ",");
            }
            return sb.ToString().Substring(0, sb.Length - 2);
        }

    }
}
