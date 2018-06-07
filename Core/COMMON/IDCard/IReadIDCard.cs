using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDCardRead
{
    /// <summary>
    /// 读取身份证信息接口
    /// </summary>
    public interface IReadIDCard
    {
        /// <summary>
        /// 读取身份信息
        /// </summary>
        /// <returns></returns>
        PersonInfo ReadPersonInfo();
    }
}
