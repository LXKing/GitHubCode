using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DataAccess.ADO
{
    public abstract class ADODataAccessBase:IADODataAccess
    {
        #region 私有变量
        protected DbConnection _connection;
        protected DbCommand _cmd;
        protected DbDataAdapter _dataAdapter;
        protected DbTransaction _transation; 
        #endregion

        #region 构造函数
        public ADODataAccessBase(string connString)
        {

        }
        public ADODataAccessBase(DbConnection connection)
        {
            _connection = connection;
        } 
        #endregion

        #region 开启事务
        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            this.Open();
            _transation = _connection.BeginTransaction();
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="level">事务锁定行为(System.Data.IsolationLevel枚举)</param>
        public void BeginTransaction(System.Data.IsolationLevel level)
        {
            this.Open();
            _transation = _connection.BeginTransaction(level);
        }
        #endregion

        #region 提交事务

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (_transation != null)
                {
                    _transation.Commit();
                    _transation.Dispose();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollBackTransaction()
        {
            try
            {
                if (_transation != null)
                {
                    _transation.Rollback();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Connection的Open、Close以及Dispose
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 关闭数据库连接，并释放cmd,_transation,_dataAdapter资源;
        /// </summary>
        public void Close()
        {
            try
            {
                if (_cmd != null)
                    _cmd.Dispose();
                if (_transation != null)
                    _transation.Dispose();
                if (_dataAdapter != null)
                    (_dataAdapter as DbDataAdapter).Dispose();
                    //_dataAdapter.Dispose();
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 释放所有数据库连接相关资源(连接、数据库命令、事务、以及context对象)
        /// </summary>
        public void Dispose()
        {
            Close();
        }
        #endregion

        #region SQL增删查改
        public virtual ResultInfo<object> DBExecuteNonQuery(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                if (_transation != null)
                {
                    throw new Exception("该方法不适合执行挂起事务的操作!");
                }
                this.Open();
                _cmd = _connection.CreateCommand();
                _cmd.CommandText = sql;
                _cmd.CommandType = cmdType;
                if (parmCollection != null)
                {
                    parmCollection.ToList().ForEach(x =>
                    {
                        _cmd.Parameters.Add(x);
                    });
                    //_cmd.Parameters.AddRange(parmCollection.ToArray());
                }
                int count = _cmd.ExecuteNonQuery();
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            finally
            {
                this.Close();
            }
            return result;
        }
        /// <summary>
        /// 事务执行参数化SQl语句(执行完必须提交或者回滚)(插入、更新和删除)
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="parmCollection">执行的参数,如果无参可为空</param>
        /// <param name="successAutoCommit">成功时自动提交事务(默认false,表示可继续使用当前开启的事务,使用完需要手动调用CommitTransaction方法)</param>
        /// <returns></returns>
        public virtual ResultInfo<object> DBExecuteNonQueryAsTran(string sql, IEnumerable<DbParameter> parmCollection = null,CommandType cmdType=CommandType.Text, bool successAutoCommit = false)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                if (_transation == null)
                    throw new Exception("该方法需要先开始事务，请执行BeginTransaction相关的方法!");
                this.Open();
                _cmd = _connection.CreateCommand();
                _cmd.CommandText = sql;
                _cmd.CommandType = cmdType;
                if (parmCollection != null)
                {
                    parmCollection.ToList().ForEach(x => {
                        _cmd.Parameters.Add(x);
                    });
                    //_cmd.Parameters.AddRange(parmCollection.ToArray());
                }
                _cmd.Transaction = this._transation;
                int count = _cmd.ExecuteNonQuery();
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            finally
            {
                if (result.Success && successAutoCommit)
                {
                    this.CommitTransaction();
                }
            }
            return result;
        }
        /// <summary>
        /// 参数化执行查询，返回查询结果集中的第一行第一列的值(其他行列忽略)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual T DBExecuteScalar<T>(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            try
            {
                this.Open();
                _cmd = _connection.CreateCommand();
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = sql;
                _cmd.CommandType = cmdType;

                if (parmCollection != null)
                {
                    parmCollection.ToList().ForEach(x =>
                    {
                        _cmd.Parameters.Add(x);
                    });
                    //_cmd.Parameters.AddRange(parmCollection.ToArray());
                }
                object result = _cmd.ExecuteScalar();
                return ConverTo<T>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Close();
            }
        }
        /// <summary>
        /// 参数化查询返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parmCollection"></param>
        /// <returns></returns>
        public abstract DataSet DBExecuteAsDataSet(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text);
        /// <summary>
        /// 参数化SQL查询返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parmCollection"></param>
        /// <returns></returns>
        public virtual DataTable DBExecuteAsDataTable(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            try
            {
                DataSet ds = this.DBExecuteAsDataSet(sql, parmCollection,cmdType);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return new DataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 参数化SQL查询返回可枚举泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">参数化SQL语句</param>
        /// <param name="parmCollection">参数集合</param>
        /// <returns></returns>
        public virtual IEnumerable<T> DBExecuteAsIEnumerable<T>(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text) where T : new()
        {
            try
            {
                var table = this.DBExecuteAsDataTable(sql, parmCollection,cmdType);
                List<T> list = table.FillModel_DA<T>();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 转为带json名字的json串
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parmCollection"></param>
        /// <param name="cmdType"></param>
        /// <param name="jsonname"></param>
        /// <returns></returns>
        public virtual string DBExecuteDataTableAsJson(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text, string jsonname=null)
        {
            try
            {
                var table = this.DBExecuteAsDataTable(sql, parmCollection, cmdType);
                if(jsonname==null)
                    return table.DataTableToJson();
                else
                    return table.DataTableToJson(jsonname);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="TResult">支持int16 32 64 decimal double byte char string这些类型</typeparam>
        /// <param name="valueInput"></param>
        /// <returns></returns>
        private TResult ConverTo<TResult>(object valueInput)
        {
            try
            {
                Converter<object, TResult> convertResult = (object o) =>
                {
                    object res = default(TResult);
                    if (typeof(TResult) == typeof(int) || typeof(TResult) == typeof(Int32))
                    {
                        res = Convert.ToInt32(o);
                    }
                    if (typeof(TResult) == typeof(Int16))
                    {
                        res = Convert.ToInt16(o);
                    }
                    if (typeof(TResult) == typeof(Int64))
                    {
                        res = Convert.ToInt64(o);
                    }
                    if (typeof(TResult) == typeof(decimal))
                    {
                        res = Convert.ToDecimal(o);
                    }
                    if (typeof(TResult) == typeof(double))
                    {
                        res = Convert.ToDouble(o);
                    }
                    if (typeof(TResult) == typeof(byte))
                    {
                        res = Convert.ToByte(o);
                    }
                    if (typeof(TResult) == typeof(char))
                    {
                        res = Convert.ToChar(o);
                    }
                    if (typeof(TResult) == typeof(string))
                    {
                        res = Convert.ToString(o);
                    }
                    if (typeof(TResult) == typeof(byte[]))
                    {
                        res = (byte[])o;
                    }
                    if (typeof(TResult) == typeof(object))
                    {

                    }
                    return (TResult)res;
                };
                return convertResult(valueInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        
        #endregion
    }
}
