using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using Ext.Net.Utilities.Inflatr;
using Ext.Net.Utilities;
using System.Collections;

namespace Ext.Net
{
    public static class GridPanelEx
    {
        /// <summary>
        /// 获取GridPanel当前选中的数据(扩展方法)
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static GridPanelSelectData<RecodEntity>  GetSelectEntitys(this GridPanel  g)
        {
            if (g.GetSelectionModel() != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<GridPanelSelectData<RecodEntity>>(g.GetSelectionModel().InitialConfig);
                return result;
            }
            else
            {
                return new GridPanelSelectData<RecodEntity>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="columnCollection"></param>
        public static  void BindColumns(this GridPanel  g,IList<ColumnBase> columnCollection)
        {
            g.ColumnModel.Columns.AddRange(columnCollection);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="modelFieldCollection"></param>
        public static void BindStoreFields(this GridPanel  g,IList<ModelField> modelFieldCollection)
        {
            g.GetStore().Fields.AddRange(modelFieldCollection);
        }
    }

    public class GridPanelSelectData<T>
    {
        public string proxyId
        {
            get; set;
        }
        public string selType
        {
            get; set;
        }
        public IEnumerable<T> selectedData
        {
            get; set;
        }
    }

    public class RecodEntity
    {
        public string recordID
        {
            get; set;
        }

        public int rowIndex
        {
            get; set;
        }
    }
}
