using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Data
{
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>, IPage, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int TotalCount
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int TotalPages
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return this.PageIndex > 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return this.PageIndex + 1 < this.TotalPages;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public PagedList()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int num = source.Count<T>();
            this.TotalCount = num;
            this.TotalPages = num / pageSize;
            if (num % pageSize > 0)
            {
                this.TotalPages++;
            }
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            var skipPageCount = pageIndex - 1;
            if (source.Count() <= pageSize)
            {
                base.AddRange(source.ToList());
                pageIndex = 1;
            }
            else
            {
                base.AddRange(source.Skip(skipPageCount * pageSize).Take(pageSize).ToList<T>());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            this.TotalCount = source.Count<T>();
            this.TotalPages = this.TotalCount / pageSize;
            if (this.TotalCount % pageSize > 0)
            {
                this.TotalPages++;
            }
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            var skipPageCount = pageIndex - 1;
            if (source.Count() <= pageSize)
            {
                base.AddRange(source.ToList());
                pageIndex = 1;
            }
            else
            {
                base.AddRange(source.Skip(skipPageCount * pageSize).Take(pageSize).ToList<T>());
            }
            //base.AddRange(source.Skip(skipPageCount * pageSize).Take(pageSize).ToList<T>());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            this.TotalCount = totalCount;
            this.TotalPages = this.TotalCount / pageSize;
            if (this.TotalCount % pageSize > 0)
            {
                this.TotalPages++;
            }
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            base.AddRange(source);
        }
        public List<T> ToList()
        {
            return new List<T>(this);
        }
    }
}
