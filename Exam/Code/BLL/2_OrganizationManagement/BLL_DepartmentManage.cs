using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL;
using COMMON.Logs;

namespace BLL.OrganizationManagement
{
    public class BLL_DepartmentManage : BLL_Base
    {
        public ResultInfo<object> AddDepartment(T_DEPARTMENT dept)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                result = base.dbContext.AddEntity(dept);
            }
            catch (Exception ex)
            {
                Log.WriteException("添加部门异常", ex);
            }

            return result;
        }

        public ResultInfo<object> UpdateDepartment(T_DEPARTMENT dept)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                var data = base.dbContext.QueryEntitys<T_DEPARTMENT>(x => x.ID == dept.ID).FirstOrDefault();

                data.PARENT_ID = dept.PARENT_ID;
                data.SEQUENCE = dept.SEQUENCE;
                data.DEPARTMENT_NAME = dept.DEPARTMENT_NAME;
                data.DEPARTMENT_DESC = dept.DEPARTMENT_DESC;

                result = base.dbContext.UpdateEntitys();

            }
            catch (Exception ex)
            {
                Log.WriteException("修改部门异常", ex);
            }

            return result;
        }

        public bool QueryDeletable(string deptID)
        {
            int count = 0;

            try
            {
                var dID = Guid.Parse(deptID);
                count = base.T_DEPARTMENT.Where(x => x.PARENT_ID == dID).Count();
            }
            catch (Exception ex)
            {
                Log.WriteException("查询是否可以删除部门异常", ex);
            }

            return count > 0 ? false : true;
        }

        public ResultInfo<object> DeleteDepartment(string deptID)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            var dID = Guid.Parse(deptID);
            try
            {
                var data = base.dbContext.QueryEntitys<T_DEPARTMENT>(x => x.ID == dID).FirstOrDefault();

                result = base.dbContext.DeleteEntity<T_DEPARTMENT>(data);
            }
            catch (Exception ex)
            {
                Log.WriteException("删除部门异常", ex);
            }

            return result;
        }

        public DepartmentInfo QueryDepartmentByName(string name)
        {
            DepartmentInfo result = new DepartmentInfo();

            try
            {
                var data = (from a in base.T_DEPARTMENT
                               where a.DEPARTMENT_NAME == name
                               select a).FirstOrDefault();

                result.ID = data.ID;
                result.PARENT_ID = data.PARENT_ID;
                result.SEQUENCE = data.SEQUENCE;
                result.DEPARTMENT_NAME = data.DEPARTMENT_NAME;
                result.DEPARTMENT_DESC = data.DEPARTMENT_DESC;
                result.FullPath = result.DEPARTMENT_NAME;

                var pareant = (from a in base.T_DEPARTMENT
                               where a.ID == data.PARENT_ID
                               select a).FirstOrDefault();

                if (pareant != null)
                {
                    result.ParentName = pareant.DEPARTMENT_NAME;
                    GetFullPath(result, data, base.T_DEPARTMENT);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException("根据Name查询部门异常", ex);
            }

            return result;
        }

        public DepartmentInfo QueryDepartmentByID(string deptID)
        {
            DepartmentInfo result = new DepartmentInfo();
            var dID = Guid.Parse(deptID);

            try
            {
                var data = (from a in base.T_DEPARTMENT
                            where a.ID == dID
                            select a).FirstOrDefault();

                result.ID = data.ID;
                result.PARENT_ID = data.PARENT_ID;
                result.SEQUENCE = data.SEQUENCE;
                result.DEPARTMENT_NAME = data.DEPARTMENT_NAME;
                result.DEPARTMENT_DESC = data.DEPARTMENT_DESC;
                result.FullPath = result.DEPARTMENT_NAME;

                var pareant = (from a in base.T_DEPARTMENT
                               where a.ID == data.PARENT_ID
                               select a).FirstOrDefault();

                if (pareant != null)
                {
                    result.ParentName = pareant.DEPARTMENT_NAME;
                    GetFullPath(result, data, base.T_DEPARTMENT);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException("根据ID查询部门异常", ex);
            }

            return result;
        }

        private void GetFullPath(DepartmentInfo data, T_DEPARTMENT current, System.Data.Entity.DbSet<T_DEPARTMENT> all)
        {
            var parent = all.Where(a => a.ID == current.PARENT_ID).FirstOrDefault();

            if (parent != null)
            {
                data.FullPath += "->" + parent.DEPARTMENT_NAME;
                GetFullPath(data, parent, all);
            }
        }
    }

    public class DepartmentInfo : T_DEPARTMENT
    {
        public string ParentName { get; set; }
        public string FullPath { get; set; }
    }
}
