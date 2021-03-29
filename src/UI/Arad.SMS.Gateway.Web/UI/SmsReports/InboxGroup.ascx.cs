using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.SmsReports
{
	public partial class InboxGroup : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public InboxGroup()
		{
			AddDataBinderHandlers("treeGroups", new GeneralTools.TreeView.DataBindHandler(treeGroups_OnDataBind));
		}

		public List<GeneralTools.TreeView.TreeNode> treeGroups_OnDataBind(string parentID, string search)
		{
			DataTable dtGroups = Facade.InboxGroup.GetUserInboxGroups(UserGuid, Helper.GetGuid(parentID), search);
			var nodes = new List<GeneralTools.TreeView.TreeNode>();

			foreach (DataRow row in dtGroups.Rows)
			{
				var node = new GeneralTools.TreeView.TreeNode();
				node.id = string.Format("'{0}'", row["Guid"]);
				node.state = "closed";
				node.text = row["Title"].ToString();
				nodes.Add(node);
			}

			return nodes;
		}

		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			frm.Attributes["src"] = string.Format("/PageLoader.aspx?c={0}&GroupGuid='{1}'", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_Inbox, Session), Guid.Empty);
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.InboxGroup);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_InboxGroup;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsReports_InboxGroup.ToString());
		}
	}
}