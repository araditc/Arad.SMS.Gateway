using System;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class PhoneBook : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public PhoneBook()
		{
			AddDataBinderHandlers("treeGroups", new GeneralTools.TreeView.DataBindHandler(treeGroups_OnDataBind));
		}

		public List<GeneralTools.TreeView.TreeNode> treeGroups_OnDataBind(string parentID, string search)
		{
			DataTable dtGroups = Facade.PhoneBook.GetPhoneBookOfUser(UserGuid, Helper.GetGuid(parentID), search);
			var nodes = new List<GeneralTools.TreeView.TreeNode>();

			foreach (DataRow row in dtGroups.Rows)
			{
				var node = new GeneralTools.TreeView.TreeNode();
				node.id = string.Format("'{0}'", row["Guid"]);
				node.state = "closed";
				node.text = row["Name"].ToString();
				node.attributes = new { count = row["CountPhoneNumbers"].ToString(), type = Helper.GetInt(row["Type"], 1), id = row["ID"], serviceId = row["ServiceId"] };
				nodes.Add(node);
			}

			return nodes;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//ltrPhoneBookTree.Text = Facade.PhoneBook.GetPhoneBookOfUser(UserGuid, true, false);
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.PhoneBook);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_PhoneBook;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_PhoneBook.ToString());
		}
	}
}