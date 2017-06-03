using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure
{
    public class PagedResult<T> : ICollection<T>
    {
        public static readonly PagedResult<T> Empty = new PagedResult<T>(0, 0, 0, 0, new List<T>());

        public PagedResult()
        {
            this.Rows = new List<T>();
        }

        public PagedResult(int pageSize, int pageIndex, int totalRecords, int totalPages, List<T> rows)
        {
            this.PageSize = pageSize;
            this.PageIndex = PageIndex;
            this.TotalRecords = totalRecords;
            this.TotalPages = totalPages;
            this.Rows = rows;
        }

        /// <summary>
        /// 当前页面数据
        /// </summary>
        public List<T> Rows { get; set; }
        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// 记录总页数
        /// </summary>
        public int TotalPages { get; set; }




        public void Add(T item)
        {
            this.Rows.Add(item);
        }

        public void Clear()
        {
            this.Rows.Clear();
        }

        public bool Contains(T item)
        {
            return this.Rows.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.Rows.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return this.Rows.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Rows.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Rows.GetEnumerator();
        }
    }
}
