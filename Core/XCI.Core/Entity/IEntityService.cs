using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// ʵ������ӿ�
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    public interface IEntityService<T> where T : EntityBase
    {
        /// <summary>
        /// ���ݿ����
        /// </summary>
        IDatabase Database{get;}
        /// <summary>
        /// ������ѯ���� ����Ѿ����ڲ�ѯ�����������������Ժ󷵻�
        /// </summary>
        /// <param name="alreadyQuery">�Ѵ��ڵĲ�ѯ����</param>
        Query<T> CreateQuery(Query alreadyQuery = null);

        /// <summary>
        /// ���ݱ�תΪʵ���б�
        /// </summary>
        /// <param name="table">���ݱ�</param>
        XCIList<T> ConvertToEntityList(DataTable table);

        /// <summary>
        /// ӳ�������� ��DataTableһ��ӳ��Ϊһ��ʵ�����
        /// </summary>
        /// <param name="dataRow">������</param>
        T MapToEntity(DataRow dataRow);

        /// <summary>
        /// ӳ��ʵ�� ��ʵ�����תΪDataTableһ��
        /// </summary>
        /// <param name="table">���ݱ���� ��Ҫ���ڴ����ж���</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns>������</returns>
        DataRow MapToDataRow(DataTable table, T entity);

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="row">�����ж���</param>
        /// <param name="entity">ʵ�����</param>
        void UpdateDataRow(DataRow row, T entity);

        /// <summary>
        /// ���Զ����Ƿ����
        /// </summary>
        /// <param name="query">��ѯ����</param>
        /// <returns>������ڷ���True</returns>
        bool Exists(Query query);

        /// <summary>
        /// ���Զ����Ƿ����
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <param name="propertyNames">������������</param>
        /// <returns>������ڷ���True</returns>
        bool Exists(T entity, params string[] propertyNames);

        /// <summary>
        /// ���Զ����Ƿ����
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <param name="exps">�����������Ʊ��ʽ</param>
        /// <returns>������ڷ���True</returns>
        bool Exists(T entity, params Expression<Func<T, object>>[] exps);

        /// <summary>
        /// ���ʵ�����
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        void Create(T entity);

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        void Update(T entity);

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="query">��ѯ����</param>
        void Update(Query query);

        /// <summary>
        /// ���ʵ��汾
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        void CheckVersion(T entity);

        /// <summary>
        /// ���������ֶ� 
        /// </summary>
        /// <param name="fieldDic">�ı�������ֵ� (���� (���� ��ֵ))</param>
        void BatchUpdateField(Dictionary<object, Dictionary<string, object>> fieldDic);

        /// <summary>
        /// ���������ֶ� 
        /// </summary>
        /// <param name="fieldList">�ı������ (���� ���� ��ֵ)</param>
        void BatchUpdateField(IList<Tuple<object,string,object>> fieldList);

        /// <summary>
        /// ɾ��ʵ�����(�������ɾ���ֶ� �����ɾ���ֶ�ֵ)
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        void Delete(T entity);

        /// <summary>
        /// ɾ��ʵ����� ����ʵ������
        /// </summary>
        /// <param name="ID">ʵ������</param>
        void Delete(int ID);

        /// <summary>
        /// ɾ��ʵ����� ����ʵ����������
        /// </summary>
        /// <param name="IDs">ʵ����������</param>
        void Delete(int[] IDs);

        /// <summary>
        /// ɾ��ʵ��
        /// </summary>
        /// <param name="query">��ѯ����</param>
        void Delete(Query query);

        /// <summary>
        /// ɾ��ȫ��ʵ��
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// ��ȡʵ�����
        /// </summary>
        /// <param name="ID">ʵ��ID</param>
        T Get(int ID);

        /// <summary>
        /// ��ȡʵ�����
        /// </summary>
        /// <param name="query">��ѯ����</param>
        T Get(Query query);

        /// <summary>
        /// ��ȡ���ݱ�(ָ���Ƿ�������û���ɾ������)
        /// </summary>
        /// <param name="isContainValid">�Ƿ������������</param>
        /// <param name="isContainDelete">�Ƿ����ɾ������</param>
        /// <param name="query">��ѯ����</param>
        DataTable GetTable(bool isContainValid, bool isContainDelete, Query query);

        /// <summary>
        /// ��ȡ���ݱ����ָ���Ĳ�ѯ����(���Ҫ��ȡȫ������ ��ѯ���󴫿ռ���)
        /// </summary>
        /// <param name="query">��ѯ����</param>
        DataTable GetTable(Query query);

        /// <summary>
        /// ��ȡ��Ч���ݱ�(ֻ����û��ɾ�� û�н��õļ�¼)
        /// </summary>
        DataTable GetTable();

        /// <summary>
        /// ��ȡɾ�������ݱ�
        /// </summary>
        DataTable GetDeleteTable();

        /// <summary>
        /// ��ȡ�����б�(ָ���Ƿ�������û���ɾ������)
        /// </summary>
        /// <param name="isContainValid">�Ƿ������������</param>
        /// <param name="isContainDelete">�Ƿ����ɾ������</param>
        /// <param name="query">��ѯ����</param>
        XCIList<T> GetList(bool isContainValid, bool isContainDelete, Query query);

        /// <summary>
        /// ��ȡ�����б����ָ���Ĳ�ѯ����(���Ҫ��ȡȫ������ ��ѯ���󴫿ռ���)
        /// </summary>
        /// <param name="query">��ѯ����</param>
        XCIList<T> GetList(Query query);

        /// <summary>
        /// ��ȡ��Ч�����б�(ֻ����û��ɾ�� û�н��õļ�¼)
        /// </summary>
        XCIList<T> GetList();

        /// <summary>
        /// ��ȡɾ���������б�
        /// </summary>
        XCIList<T> GetDeleteList();
        
        /// <summary>
        /// ʵ���Ƿ�����༭
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <returns>�������༭ ����True</returns>
        bool IsAllowEdit(T entity);

        /// <summary>
        /// ʵ���Ƿ�����ɾ��
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <returns>�������༭ ����True</returns>
        bool IsAllowDelete(T entity);
    }
}