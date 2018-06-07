using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess
{
    interface IEFDataAccess : IDisposable
    {
        ResultInfo<object> AddEntity<TEntity>(TEntity entity) where TEntity : class,new();
        ResultInfo<object> AddEntitys<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class,new();
        ResultInfo<object> DeleteEntity<TEntity>(TEntity entity) where TEntity : class,new();
        ResultInfo<object> DeleteEntitys<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class,new();
        IEnumerable<TEntity> QueryEntitys<TEntity>(Expression<Func<TEntity, bool>> where = null) where TEntity : class,new();
        //IEnumerable<TEntity> SqlQueryEntitys<TEntity>(string sql) where TEntity : new();
        IEnumerable<TEntity> SqlQueryEntitys<TEntity>(string sql, IEnumerable<DbParameter> pams=null) where TEntity : class,new();
        ResultInfo<object> UpdateEntitys();
        ResultInfo<object> UpdateEntitys<TEntity, TKey>(IEnumerable<TEntity> entitysList, TKey key) where TEntity : class,new();
    }
}
