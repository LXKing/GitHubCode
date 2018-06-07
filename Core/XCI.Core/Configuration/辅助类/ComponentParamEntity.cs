using System;

namespace XCI.Component
{
    /// <summary>
    /// ����ʵ��
    /// </summary>
    [Serializable]
    public class ComponentParamEntity
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ����ֵ
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Comment { get; set; }

        public override bool Equals(object obj)
        {
            return this.Name.Equals(((ComponentParamEntity) obj).Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}