using MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BLL_RoleManage:BLL_Base
    {
        public IEnumerable<T_ROLE> QueryAllRole()
        {
            return base.T_ROLE.ToList();
        }
    }
}
