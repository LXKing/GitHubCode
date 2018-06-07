using BLL.ExamDesign;
using Ext.Extension.Message;
using Ext.Extension.TreePanelEx;
using Ext.Net;
using MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnLine.Pages.ExamDesign
{
    public partial class ExamTypeManagement : TabBasePage
    {
        BLL_ExamTypeManagement bllExamType = new BLL_ExamTypeManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(X.IsAjaxRequest)
            {

            }
            else
            {
                GetExamTypeData();
                panelTree.UpdateContent();
            }
        }
        private void GetExamTypeData()
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_EXAM_TYPE>("EXAM_TYPE_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                treePanelExamTypeList.SetdataSource(data);
                treePanelExamTypeList.ExpandAllNode();
                //departMent1.ExpandAllNode();
                this.panelTree.UpdateContent();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void txtExamTypeParent_IndicatorIconClick(object sender, DirectEventArgs e)
        {
            var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_EXAM_TYPE>("EXAM_TYPE_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
            treePanelExamTypeWin.SetdataSource(data);
            treePanelExamTypeWin.Show();
        }

        protected void treePanelExamTypeWin_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            txtExamTypeParent.Text = e.NodeDbClick.Text;
            hidden_ParentID.Text = e.NodeDbClick.NodeID;
        }

        protected void btnSave_Click(object sender, DirectEventArgs e)
        {
            try
            {
                T_EXAM_TYPE entity = new T_EXAM_TYPE();

                #region 判断ID
                if (hidden_AddOrUpdate.Text == "u")
                {
                    Guid id;
                    var success = Guid.TryParse(hidden_CurrentID.Text, out id);
                    if (!success)
                    {
                        throw new Exception("无效的当前节点ID!");
                    }
                    entity.ID = id;
                }
                else
                {
                    entity.ID = Guid.NewGuid();
                } 
                #endregion

                if (hidden_ParentID.Text.NotNullAndEpmty())
                {
                    Guid id;
                    var success = Guid.TryParse(hidden_ParentID.Text, out id);
                    if (!success)
                    {
                        throw new Exception("无效的父节点!");
                    }
                    entity.PARENT_ID = id;
                }
                
                entity.EXAM_TYPE_NAME = txtExamTypeName.Text;
                entity.SEQUENCE = txtSequence.Text;
                entity.EXAM_TYPE_DESC = txtExamTypeDesc.Text;
                if(hidden_AddOrUpdate.Text=="u")
                {
                    var res = bllExamType.UpdateExamType(entity);
                    if (res.Success)
                    {
                        MessageBoxExt.ShowPrompt("考试类型更新成功!", "hideLoad();");
                        GetExamTypeData();
                    }
                        
                    else
                        MessageBoxExt.ShowWarning("考试类型更新失败!", "hideLoad();");
                }
               else
                {
                    
                    var result = bllExamType.AddExamType(entity);
                    if (result.Success)
                    {
                        GetExamTypeData();
                        MessageBoxExt.ShowPrompt("考试类型添加成功!", @"
                        hideLoad();
                        btnClear_Click();
                    ");
                    }
                    else
                    {
                        MessageBoxExt.ShowError(result.Message, "hideLoad();");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void treePanelExamTypeList_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            hidden_AddOrUpdate.Text = "u";
            hidden_CurrentID.Text = e.NodeDbClick.NodeID;

            T_EXAM_TYPE entity = new T_EXAM_TYPE();
            Guid id;
            var success = Guid.TryParse(hidden_CurrentID.Text, out id);
            if (!success)
            {
                throw new Exception("无效的父节点!");
            }
            ResultInfo<object> r = bllExamType.QueryExamType(id);
            if (r.Success)
            {
                if (r.Data.NotNull())
                {
                    entity = r.Data as T_EXAM_TYPE;
                    if(entity.PARENT_ID.NotNull())
                    {
                        var parentResult =bllExamType.QueryExamType(entity.PARENT_ID);
                        if(parentResult.Data.NotNull())
                        {
                            var parent =parentResult.Data as T_EXAM_TYPE;
                            txtExamTypeParent.Text = parent.EXAM_TYPE_NAME;
                            hidden_ParentID.Text = parent.ID.ToString();
                        }
                    }
                    else
                    {
                        txtExamTypeParent.Reset();
                        hidden_ParentID.Reset();
                    }
                    txtExamTypeName.Text = entity.EXAM_TYPE_NAME;
                    txtExamTypeDesc.Text = entity.EXAM_TYPE_DESC.IsNull() ? "" : entity.EXAM_TYPE_DESC;
                    txtSequence.Text = entity.SEQUENCE.IsNull() ? "" : entity.SEQUENCE;
                }
                else
                {
                    throw new Exception("不存在要修改的考试类型!");
                }
            }
        }

        protected void btnCancel_Click(object sender,DirectEventArgs e)
        {
            try
            {
                if (hidden_AddOrUpdate.Text != "u" || hidden_CurrentID.Text.IsNullOrEmpty())
                {
                    throw new Exception("请选择要删除的节点!");
                }
                else
                {
                    Guid id = Guid.Parse(hidden_CurrentID.Text);
                    var result = bllExamType.DeleteExamType(id);
                    if (result.Success)
                    {
                        MessageBoxExt.ShowPrompt("删除节点成功!", @"
                        btnClear_Click();
                        hideLoad();");
                        GetExamTypeData();
                    }
                    else
                    {
                        MessageBoxExt.ShowError(result.Message, "hideLoad();");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}