using SocketTool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
namespace SocketTool
{
	public class MainForm : Form
	{
		private TreeNode rootNode1;
		private TreeNode rootNode2;
		private int index1 = 1;
		private int index2 = 1;
		private int pageIndex;
		private List<Form> pageList = new List<Form>();
		private List<SocketInfo> socketInfoList = new List<SocketInfo>();
		private string XMLFileName = "socketinfo.xml";
		private IContainer components;
		private TreeView deviceTree;
		private TabControl tabControl1;
		private ToolStrip toolStrip1;
		private ToolStripButton tsbAddClient;
		private ToolStripButton tsbAddServer;
		private ToolStripButton tsbDelete;
		private TabPage tabPage1;
		private ImageList imageList1;
		public ToolStripButton tsbAbout;
		public MainForm()
		{
			this.InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			this.rootNode1 = new TreeNode("客户终端", 5, 5);
			this.deviceTree.Nodes.Add(this.rootNode1);
			this.rootNode2 = new TreeNode("服务器终端", 6, 6);
			this.deviceTree.Nodes.Add(this.rootNode2);
			try
			{
				this.socketInfoList = MySerializer.Deserialize<List<SocketInfo>>(this.XMLFileName);
				foreach (SocketInfo current in this.socketInfoList)
				{
					if (current.Types == "Server")
					{
						this.AddServerFormNode(current.Names, current);
					}
					else
					{
						this.AddClientFormNode(current.Names, current);
					}
				}
			}
			catch (Exception)
			{
			}
		}
		private void AddClientFormNode(string name, SocketInfo si)
		{
			TreeNode treeNode = this.rootNode1.Nodes.Add(name, name, 7, 7);
			this.index1++;
			ClientForm_New clientForm = new ClientForm_New();
			if (si != null)
			{
				clientForm.SocketInfo = si;
			}
			TabPage tabPage = this.addPage(name, clientForm);
			tabPage.ImageIndex = 2;
			treeNode.Tag = tabPage;
			this.rootNode1.ExpandAll();
			this.deviceTree.SelectedNode = treeNode;
		}
		private void AddServerFormNode(string name, SocketInfo si)
		{
			TreeNode treeNode = this.rootNode2.Nodes.Add(name, name, 8, 8);
			this.index2++;
			ServerForm serverForm = new ServerForm();
			if (si != null)
			{
				serverForm.SocketInfo = si;
			}
			TabPage tabPage = this.addPage(name, serverForm);
			treeNode.Tag = tabPage;
			tabPage.ImageIndex = 3;
			this.rootNode2.ExpandAll();
			this.deviceTree.SelectedNode = treeNode;
		}
		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Name == "tsbAddClient")
			{
				string name = "客户端" + this.index1;
				this.AddClientFormNode(name, null);
				return;
			}
			if (e.ClickedItem.Name == "tsbAddServer")
			{
				string name2 = "服务器端" + this.index2;
				this.AddServerFormNode(name2, null);
				return;
			}
			if (e.ClickedItem.Name == "tsbAbout")
			{
				Process.Start("iexplore.exe", "www.ltmonitor.com");
				return;
			}
			if (e.ClickedItem.Name == "tsbDelete")
			{
				TreeNode selectedNode = this.deviceTree.SelectedNode;
				if (selectedNode.Level < 1)
				{
					return;
				}
				TabPage value = (TabPage)selectedNode.Tag;
				this.tabControl1.TabPages.Remove(value);
				selectedNode.Parent.Nodes.Remove(selectedNode);
			}
		}
		private TabPage addPage(string pageText, Form form)
		{
			form.TopLevel = false;
			form.Dock = DockStyle.Fill;
			TabPage tabPage = this.tabPage1;
			if (this.pageIndex == 0)
			{
				tabPage.Controls.Add(form);
			}
			else
			{
				tabPage = new TabPage();
				tabPage.ImageIndex = 3;
				tabPage.Name = "Page" + this.pageIndex.ToString();
				tabPage.TabIndex = this.pageIndex;
				this.tabControl1.Controls.Add(tabPage);
				tabPage.Controls.Add(form);
				this.tabControl1.SelectedTab = tabPage;
			}
			tabPage.Text = pageText;
			this.pageIndex++;
			form.Show();
			this.pageList.Add(form);
			return tabPage;
		}
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.socketInfoList = new List<SocketInfo>();
			foreach (TreeNode treeNode in this.deviceTree.Nodes)
			{
				foreach (TreeNode treeNode2 in treeNode.Nodes)
				{
					TabPage tabPage = (TabPage)treeNode2.Tag;
					if (tabPage != null)
					{
						tabPage.Text = treeNode2.Text;
					}
					foreach (Form form in tabPage.Controls)
					{
						ISocketInfo socketInfo = (ISocketInfo)form;
						socketInfo.SocketInfo.Names = tabPage.Text;
						form.Close();
						this.socketInfoList.Add(socketInfo.SocketInfo);
					}
				}
			}
            var doc = new XmlDocument();
            doc.Load(this.XMLFileName);
            doc.DocumentElement.InnerXml = string.Empty;
            var xmlStringBuild = new System.Text.StringBuilder("");
            this.socketInfoList.ForEach(x =>
            {
                var json = JsonConvert.SerializeObject(x);
                try
                {
                    XNode node = JsonConvert.DeserializeXNode(json, "SocketInfo");
                    var xml = node.ToString();
                    xmlStringBuild.AppendLine(xml);
                }
                catch (Exception ex)
                {

                    throw;
                }
            });
            doc.DocumentElement.InnerXml = xmlStringBuild.ToString();
            doc.Save(this.XMLFileName);
			//MySerializer.Serialize<List<SocketInfo>>(this.socketInfoList, this.XMLFileName);
		}
		private void deviceTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Level == 1)
			{
				TabPage selectedTab = (TabPage)e.Node.Tag;
				this.tabControl1.SelectedTab = selectedTab;
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			this.deviceTree = new TreeView();
			this.imageList1 = new ImageList(this.components);
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.toolStrip1 = new ToolStrip();
			this.tsbAddClient = new ToolStripButton();
			this.tsbAddServer = new ToolStripButton();
			this.tsbDelete = new ToolStripButton();
			this.tsbAbout = new ToolStripButton();
			this.tabControl1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.deviceTree.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.deviceTree.BackColor = SystemColors.ControlLightLight;
			this.deviceTree.FullRowSelect = true;
			this.deviceTree.HideSelection = false;
			this.deviceTree.HotTracking = true;
			this.deviceTree.ImageIndex = 0;
			this.deviceTree.ImageList = this.imageList1;
			this.deviceTree.LabelEdit = true;
			this.deviceTree.Location = new Point(-1, 27);
			this.deviceTree.Name = "deviceTree";
			this.deviceTree.SelectedImageIndex = 0;
			this.deviceTree.ShowNodeToolTips = true;
			this.deviceTree.Size = new Size(160, 516);
			this.deviceTree.TabIndex = 0;
			this.deviceTree.AfterSelect += new TreeViewEventHandler(this.deviceTree_AfterSelect);
			this.imageList1.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "delete.png");
			this.imageList1.Images.SetKeyName(1, "add-new-paper-file.png");
			this.imageList1.Images.SetKeyName(2, "add-new-window.png");
			this.imageList1.Images.SetKeyName(3, "add-new-tab.png");
			this.imageList1.Images.SetKeyName(4, "error-red-circle.png");
			this.imageList1.Images.SetKeyName(5, "gear-advanced-options.png");
			this.imageList1.Images.SetKeyName(6, "application_cascade.png");
			this.imageList1.Images.SetKeyName(7, "arrow-back-previous.png");
			this.imageList1.Images.SetKeyName(8, "arrow-forward-next.png");
			this.imageList1.Images.SetKeyName(9, "paper-page-square-text-arrows-reload-refresh.png");
			this.imageList1.Images.SetKeyName(10, "jigsaw-puzzle-piece-extension.png");
			this.tabControl1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.ImageList = this.imageList1;
			this.tabControl1.Location = new Point(157, 28);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(666, 516);
			this.tabControl1.TabIndex = 1;
			this.tabPage1.BorderStyle = BorderStyle.Fixed3D;
			this.tabPage1.ImageIndex = 1;
			this.tabPage1.Location = new Point(4, 23);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(658, 489);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "请添加通信终端";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.toolStrip1.BackColor = SystemColors.ButtonFace;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.tsbAddClient,
				this.tsbAddServer,
				this.tsbDelete,
				this.tsbAbout
			});
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(830, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
			this.tsbAddClient.Image = (Image)componentResourceManager.GetObject("tsbAddClient.Image");
			this.tsbAddClient.ImageTransparentColor = Color.Magenta;
			this.tsbAddClient.Name = "tsbAddClient";
			this.tsbAddClient.Size = new Size(85, 22);
			this.tsbAddClient.Text = "添加客户端";
			this.tsbAddServer.Image = (Image)componentResourceManager.GetObject("tsbAddServer.Image");
			this.tsbAddServer.ImageTransparentColor = Color.Magenta;
			this.tsbAddServer.Name = "tsbAddServer";
			this.tsbAddServer.Size = new Size(97, 22);
			this.tsbAddServer.Text = "添加服务器端";
			this.tsbDelete.Image = (Image)componentResourceManager.GetObject("tsbDelete.Image");
			this.tsbDelete.ImageTransparentColor = Color.Magenta;
			this.tsbDelete.Name = "tsbDelete";
			this.tsbDelete.Size = new Size(49, 22);
			this.tsbDelete.Text = "删除";
			this.tsbAbout.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
			this.tsbAbout.ForeColor = SystemColors.ActiveCaption;
			this.tsbAbout.Image = (Image)componentResourceManager.GetObject("tsbAbout.Image");
			this.tsbAbout.ImageTransparentColor = Color.Magenta;
			this.tsbAbout.Name = "tsbAbout";
			this.tsbAbout.Size = new Size(51, 22);
			this.tsbAbout.Text = "关于";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(830, 552);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.deviceTree);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "MainForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "网络通信测试工具(www.ltmonitor.com)";
			base.Load += new EventHandler(this.Form1_Load);
			base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
			this.tabControl1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
