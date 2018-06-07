using System;
using DataAccess.ADO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
namespace BLL.TreeData
{
    public class BLL_QueryTreeData:BLL_Base
    {
        /// <summary>
        /// 查询树形结构的表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <returns></returns>
        public IEnumerable<TreeObject> QueryTreeDataList<T>(string displayMember, string valueMember,string parentIDMember) where T:class
        {
            Func<T, TreeObject> exp = (x) => new TreeObject() { 
                Text = x.GetType().GetProperty(displayMember).GetValue(x, null).ToString(), 
                ID = x.GetType().GetProperty(valueMember).GetValue(x, null).ToString(), 
                ParentID = x.GetType().GetProperty(parentIDMember).GetValue(x, null) == null ? null : x.GetType().GetProperty(parentIDMember).GetValue(x, null).ToString(),
                SEQUENCE = x.GetType().GetProperty("SEQUENCE").GetValue(x, null)==null?null:x.GetType().GetProperty("SEQUENCE").GetValue(x, null).ToString()
            };
            return base.Set<T>().Select(exp).OrderBy(x=>x.SEQUENCE).ToList();
        }
        /// <summary>
        /// 查询树形结构的表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <returns></returns>
        public IEnumerable<TreeObject> QueryTreeDataList(string tableName,string displayMember, string valueMember,string parentMember)
        {
            throw new Exception("未实现!");
        }
    }
}
