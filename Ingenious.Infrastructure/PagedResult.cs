using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure
{
    /// <summary>
    /// 分页基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T> : ICollection<T>,PagedList.IPagedList
    {
        public static readonly PagedResult<T> Empty = new PagedResult<T>(0, 0, 0, 0, new List<T>());

        public PagedResult()
        {
            this.Rows = new List<T>();
            this.Data = new List<T>();
        }

        public PagedResult(int totalRecords, int totalPages, int pageSize, int pageIndex, List<T> rows)
        {
            this.PageSize = pageSize;
            this.PageIndex = PageIndex;
            this.TotalRecords = totalRecords;
            this.TotalPages = totalPages;
            this.Rows = rows;
            this.Data = rows;
        }

        /// <summary>
        /// 当前页面数据
        /// </summary>
        public List<T> Data { get; set; }

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

        public int FirstItemOnPage
        {
            get { return 1; }
        }

        public bool HasNextPage
        {
            get { return this.PageIndex < this.TotalPages; }
        }

        public bool HasPreviousPage
        {
            get { return this.PageIndex > 1 && this.PageIndex < this.TotalPages; }
        }

        public bool IsFirstPage
        {
            get { return this.PageIndex == 1; }
        }

        public bool IsLastPage
        {
            get { return this.PageIndex == this.TotalPages; }
        }

        public int LastItemOnPage
        {
            get { return this.TotalPages; }
        }

        public int PageCount
        {
            get { return this.TotalPages; }
        }

        public int PageNumber
        {
            get { return this.PageIndex; }
        }

        public int TotalItemCount
        {
            get { return this.TotalRecords; }
        }
    }
}
