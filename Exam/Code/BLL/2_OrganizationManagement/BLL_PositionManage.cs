using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL;
using COMMON.Logs;

namespace BLL.OrganizationManagement
{
    public class BLL_PositionManage : BLL_Base
    {
        public ResultInfo<object> AddPosition(T_POSITION pos)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                result = base.dbContext.AddEntity(pos);
            }
            catch (Exception ex)
            {
                Log.WriteException("添加岗位异常", ex);
            }

            return result;
        }

        public ResultInfo<object> UpdatePosition(T_POSITION pos)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                var data = base.dbContext.QueryEntitys<T_POSITION>(x => x.ID == pos.ID).FirstOrDefault();

                data.PARENT_ID = pos.PARENT_ID;
                data.SEQUENCE = pos.SEQUENCE;
                data.POSITION_NAME = pos.POSITION_NAME;
                data.POSITION_DESC = pos.POSITION_DESC;

                result = base.dbContext.UpdateEntitys();

            }
            catch (Exception ex)
            {
                Log.WriteException("修改岗位异常", ex);
            }

            return result;
        }

        public bool QueryDeletable(string posID)
        {
            int count = 0;

            try
            {
                var pID = Guid.Parse(posID);
                count = base.T_POSITION.Where(x => x.PARENT_ID == pID).Count();
            }
            catch (Exception ex)
            {
                Log.WriteException("查询是否可以删除职位异常", ex);
            }

            return count > 0 ? false : true;
        }

        public ResultInfo<object> DeletePosition(string posID)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            var pID = Guid.Parse(posID);

            try
            {
                var data = base.dbContext.QueryEntitys<T_POSITION>(x => x.ID == pID).FirstOrDefault();

                result = base.dbContext.DeleteEntity<T_POSITION>(data);
            }
            catch (Exception ex)
            {
                Log.WriteException("删除岗位异常", ex);
            }

            return result;
        }

        public PositionInfo QueryPositionByName(string name)
        {
            PositionInfo result = new PositionInfo();

            try
            {
                var data = (from a in base.T_POSITION
                            where a.POSITION_NAME == name
                            select a).FirstOrDefault();

                result.ID = data.ID;
                result.PARENT_ID = data.PARENT_ID;
                result.SEQUENCE = data.SEQUENCE;
                result.POSITION_NAME = data.POSITION_NAME;
                result.POSITION_DESC = data.POSITION_DESC;
                result.FullPath = result.POSITION_NAME;

                var pareant = (from a in base.T_POSITION
                               where a.ID == data.PARENT_ID
                               select a).FirstOrDefault();

                if (pareant != null)
                {
                    result.ParentName = pareant.POSITION_NAME;
                    GetFullPath(result, data, base.T_POSITION);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException("根据Name查询岗位异常", ex);
            }

            return result;
        }

        public PositionInfo QueryPositionByID(string posID)
        {
            PositionInfo result = new PositionInfo();
            var pID = Guid.Parse(posID);

            try
            {
                var data = (from a in base.T_POSITION
                            where a.ID == pID
                            select a).FirstOrDefault();

                result.ID = data.ID;
                result.PARENT_ID = data.PARENT_ID;
                result.SEQUENCE = data.SEQUENCE;
                result.POSITION_NAME = data.POSITION_NAME;
                result.POSITION_DESC = data.POSITION_DESC;
                result.FullPath = result.POSITION_NAME;

                var pareant = (from a in base.T_POSITION
                               where a.ID == data.PARENT_ID
                               select a).FirstOrDefault();

                if (pareant != null)
                {
                    result.ParentName = pareant.POSITION_NAME;
                    GetFullPath(result, data, base.T_POSITION);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException("根据ID查询岗位异常", ex);
            }

            return result;
        }

        private void GetFullPath(PositionInfo data, T_POSITION current, System.Data.Entity.DbSet<T_POSITION> all)
        {
            var parent = all.Where(a => a.ID == current.PARENT_ID).FirstOrDefault();

            if (parent != null)
            {
                data.FullPath += "->" + parent.POSITION_NAME;
                GetFullPath(data, parent, all);
            }
        }
    }

    public class PositionInfo : T_POSITION
    {
        public string ParentName { get; set; }
        public string FullPath { get; set; }
    }
}
