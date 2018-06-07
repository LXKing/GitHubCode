using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL;
namespace BLL.Default
{
    public class BLL_InitOperation:BLL_Base
    {
        public List<T_OPERATION> GetOperationByRoleID(string roleID)
        {
            var rolID = Guid.Parse(roleID);

            var result = (from a in base.T_OPERATION
                          join b in base.T_OP_REF_ROLE
                          on a.ID equals b.OP_ID
                          where b.ROLE_ID == rolID
                          select a).ToList();

            return result;
        }

        public V_USER_INFO GetUserInforByID(string userID)
        {
            var uID = Guid.Parse(userID);

            var result = (from a in base.V_USER_INFO
                          where a.ID == uID
                          select a).FirstOrDefault();

            return result;
        }
    }
}
