using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data
{
    /// <summary>
    /// 分页接口
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// 页面索引
        /// </summary>
        int PageIndex
        {
            get;
        }
        /// <summary>
        /// 分页记录条数
        /// </summary>
        int PageSize
        {
            get;
        }
        /// <summary>
        /// 总记录条数
        /// </summary>
        int TotalCount
        {
            get;
        }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages
        {
            get;
        }
        /// <summary>
        /// 是否有前一页
        /// </summary>
        bool HasPreviousPage
        {
            get;
        }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        bool HasNextPage
        {
            get;
        }
    }
}
