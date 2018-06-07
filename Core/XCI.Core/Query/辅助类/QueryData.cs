using System;
using System.Collections;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 表示查询数据
    /// </summary>
    [Serializable]
    public class QueryData
    {
        /// <summary>
        /// 获取或者设置表名称
        /// </summary>
        public string TableName;

        /// <summary>
        /// 获取或者设置取前几条记录
        /// </summary>
        public int Top;

        /// <summary>
        /// 自定义查询字符串
        /// </summary>
        public string QueryString;

        /// <summary>
        /// 获取或者设置插入字段列表
        /// </summary>
        public readonly XCIList<InsertField> InsertFieldList = new XCIList<InsertField>();

        /// <summary>
        /// 获取或者设置更新字段列表
        /// </summary>
        public readonly XCIList<UpdateField> UpdateFieldList = new XCIList<UpdateField>();

        /// <summary>
        /// 获取或者设置查询字段列表
        /// </summary>
        public readonly XCIList<SelectField> SelectFieldList = new XCIList<SelectField>();

        /// <summary>
        /// 获取或者设置分组字段列表
        /// </summary>
        public readonly XCIList<GroupField> GroupFieldList = new XCIList<GroupField>();

        /// <summary>
        /// 获取或者设置排序字段列表
        /// </summary>
        public readonly XCIList<OrderByClause> OrderByList = new XCIList<OrderByClause>();
        
        /// <summary>
        /// 获取或者设置查询条件列表
        /// </summary>
        public readonly XCIList<Condition> ConditionList = new XCIList<Condition>();

    }

    /// <summary>
    /// 表示查询字段
    /// </summary>
    [Serializable]
    public class SelectField
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 默认构造
        /// </summary>
        public SelectField()
        {

        }

        /// <summary>
        /// 使用字段名称构造
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public SelectField(string fieldName)
        {
            FieldName = fieldName;
        }
    }

    /// <summary>
    /// 表示分组字段
    /// </summary>
    [Serializable]
    public class GroupField
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 默认构造
        /// </summary>
        public GroupField()
        {

        }

        /// <summary>
        /// 使用字段名称构造
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public GroupField(string fieldName)
        {
            FieldName = fieldName;
        }
    }

    /// <summary>
    /// 表示插入字段
    /// </summary>
    [Serializable]
    public class InsertField
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        
        /// <summary>
        /// 字段值
        /// </summary>
        public object FieldValue { get; set; }

        /// <summary>
        /// 默认构造
        /// </summary>
        public InsertField()
        {

        }

        /// <summary>
        /// 使用字段名称字段值构造
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        public InsertField(string fieldName, object fieldValue)
        {
            this.FieldName = fieldName;
            this.FieldValue = fieldValue;
        }
    }

    /// <summary>
    /// 表示更新字段
    /// </summary>
    [Serializable]
    public class UpdateField : InsertField
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        public UpdateField()
        {

        }

        /// <summary>
        /// 使用字段名称字段值构造
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        public UpdateField(string fieldName, object fieldValue)
        {
            this.FieldName = fieldName;
            this.FieldValue = fieldValue;
        }
    }

    /// <summary>
    /// 排序语句
    /// </summary>
    [Serializable]
    public class OrderByClause
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName;
        
        /// <summary>
        /// 排序类型
        /// </summary>
        public OrderByType Ordering;

        /// <summary>
        /// 默认构造
        /// </summary>
        public OrderByClause()
        {

        }

        /// <summary>
        /// 使用字段名称排序类型构造
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="ordering">排序类型</param>
        public OrderByClause(string fieldName, OrderByType ordering)
        {
            this.FieldName = fieldName;
            this.Ordering = ordering;
        }
    }
    
    /// <summary>
    /// 表示查询条件关系
    /// </summary>
    public enum ConditionType
    {
        /// <summary>
        /// And
        /// </summary>
        And,


        /// <summary>
        /// Or
        /// </summary>
        Or,


        /// <summary>
        /// 没有类型
        /// </summary>
        None
    }

    /// <summary>
    /// SQL 比较操作符
    /// </summary>
    public enum ComparisonType
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equals,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEquals,
        /// <summary>
        /// 模糊查询Like
        /// </summary>
        Like,
        /// <summary>
        /// 模糊查询Like相反
        /// </summary>
        NotLike,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterOrEquals,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessOrEquals,
        /// <summary>
        /// Is
        /// </summary>
        Is,
        /// <summary>
        /// IsNot
        /// </summary>
        IsNot,
        /// <summary>
        /// In
        /// </summary>
        In,
        /// <summary>
        /// NotIn
        /// </summary>
        NotIn,
        /// <summary>
        /// 之间Between
        /// </summary>
        BetweenAnd
    }

    /// <summary>
    /// SQL 聚合函数枚举
    /// </summary>
    public enum AggregateFunction
    {
        /// <summary>
        /// 返回组中的项数
        /// </summary>
        Count,
        /// <summary>
        /// 返回表达式中所有值的和或仅非重复值的和。SUM 只能用于数字列。空值将被忽略
        /// </summary>
        Sum,
        /// <summary>
        /// 返回组中各值的平均值。将忽略空值
        /// </summary>
        Avg,
        /// <summary>
        /// 返回表达式中的最小值
        /// </summary>
        Min,
        /// <summary>
        /// 返回表达式的最大值
        /// </summary>
        Max
    }

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum OrderByType
    {

        /// <summary>
        /// 顺序
        /// </summary>
        Asc,


        /// <summary>
        /// 倒序
        /// </summary>
        Desc
    }

    /// <summary>
    /// 表示条件语句
    /// </summary>
    [Serializable]
    public class Condition
    {
        /// <summary>
        /// 条件关系
        /// </summary>
        public ConditionType CondTypeType;
        
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName;
        
        /// <summary>
        /// SQL比较符
        /// </summary>
        public ComparisonType Comparison;
        
        /// <summary>
        /// 字段值
        /// </summary>
        public object FieldValue;

        /// <summary>
        /// 是否开始括号
        /// </summary>
        public bool IsBeginBracket;

        /// <summary>
        /// 是否结束括号
        /// </summary>
        public bool IsEndBracket;

        /// <summary>
        /// Like前通配符
        /// </summary>
        public string LikePrefix;

        /// <summary>
        /// Like后通配符
        /// </summary>
        public string LikeSuffix;

        /// <summary>
        /// Between开始值
        /// </summary>
        public object StartValue;

        /// <summary>
        /// Between结束值
        /// </summary>
        public object EndValue;

        /// <summary>
        /// 设置In值集合
        /// </summary>
        public IList InValues;

        /// <summary>
        /// 默认构造
        /// </summary>
        public Condition()
        {

        }

        /// <summary>
        /// 使用条件关系构造
        /// </summary>
        /// <param name="condType">条件关系</param>
        public Condition(ConditionType condType)
        {
            CondTypeType = condType;
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="condType">条件关系</param>
        /// <param name="comparison">SQL比较符</param>
        public Condition(string fieldName, object fieldValue, ConditionType condType, ComparisonType comparison)
        {
            CondTypeType = condType;
            FieldName = fieldName;
            Comparison = comparison;
            FieldValue = fieldValue;
        }
    }

    /// <summary>
    /// SQL原样输出
    /// </summary>
    [Serializable]
    public class SQLValue
    {
        /// <summary>
        /// 字段值
        /// </summary>
        public object FieldValue { get; set; }

        /// <summary>
        /// 字段值构造
        /// </summary>
        /// <param name="val">字段值</param>
        public SQLValue(object val)
        {
            this.FieldValue = val;
        }

        /// <summary>
        /// 返回一个SQLValue对象
        /// </summary>
        /// <param name="val">字段值</param>
        public static SQLValue Value(object val)
        {
            return new SQLValue(val);
        }
    }
}