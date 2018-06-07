using System;

namespace XCI.Component
{
    [Serializable]
    public class AppUpdateEntity
    {
        /// <summary>
        /// 版本号
        /// </summary>		
        public int Version { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 文件名称 不包含路径
        /// </summary>		
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>		
        public long FileSize { get; set; }

        /// <summary>
        /// 更新内容
        /// </summary>		
        public string Content { get; set; }
        
        /// <summary>
        /// 是否重启
        /// </summary>		
        public bool IsRestart { get; set; }


        public override bool Equals(object obj)
        {
            return this.Version == ((AppUpdateEntity)obj).Version;
        }

        public override int GetHashCode()
        {
            return this.Version.GetHashCode();
        }
    }
}