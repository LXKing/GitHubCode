using System.Text;

namespace XCI.Core
{
    /// <summary>
    /// 定义可设置或检索的键/值对
    /// </summary>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    public class XCIKeyValuePair<TKey, TValue>
    {
        private TKey key;
        private TValue value;

        /// <summary>
        /// 用指定的键和值初始化新实例
        /// </summary>
        /// <param name="key">每个键/值对中定义的对象</param>
        /// <param name="value">与 key 相关联的定义</param>
        public XCIKeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        /// <summary>
        /// 获取键/值对中的键
        /// </summary>
        public TKey Key
        {
            get { return key; }
        }

        /// <summary>
        /// 获取键/值对中的值
        /// </summary>
        public TValue Value
        {
            get { return value; }
        }

        /// <summary>
        /// 使用键和值的字符串表示形式返回实例的字符串表示形式
        /// </summary>
        /// <returns>它包括键和值的字符串表示形式</returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append('[');
            if (Key != null)
            {
                s.Append(Key.ToString());
            }
            s.Append(", ");
            if (Value != null)
            {
                s.Append(Value.ToString());
            }
            s.Append(']');
            return s.ToString();
        }
    }
}