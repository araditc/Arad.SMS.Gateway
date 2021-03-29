using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.GeneralLibrary.Security;
using GeneralTools.DataGrid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class User : UIUserControlBase
	{
		protected Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid SubGridParentGuid
		{
			get { return Helper.RequestGuid(this, "SubGridParentGuid"); }
		}

		public User()
		{
			AddDataBinderHandlers("gridUsers", new DataBindHandler(gridUsers_OnDataBind));
			AddDataRenderHandlers("gridUsers", new CellValueRenderEventHandler(gridUsers_OnDataRender));
		}

		public DataTable gridUsers_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			Guid domainGuid = Guid.Empty;

			if (!string.IsNullOrEmpty(searchFiletrs))
			{
				JArray array = JArray.Parse(searchFiletrs);
				JObject userStatus = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "IsActive" && obj.Property("data").Value.ToString() == "-1").FirstOrDefault();
				array.Remove(userStatus);

				domainGuid = Helper.GetGuid(array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "DomainGuid").FirstOrDefault().Property("data").Value);
				array.Remove(array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "DomainGuid").FirstOrDefault());
				searchFiletrs = array.ToString();
			}

			string query = string.Empty;
			string filterQuery = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			string searchQuery = Helper.GenerateQueryFromToolbarFilters(searchFiletrs);

			query = string.Format("{0} {1} {2}",
													filterQuery,
													(!string.IsNullOrEmpty(filterQuery) && !string.IsNullOrEmpty(searchQuery)) ? "AND" : string.Empty,
													searchQuery);

			return Facade.User.GetPagedUsers(UserGuid, domainGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridUsers_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			string actions = string.Empty;
			string imgTagPattern = "<span class='ui-icon {0}' onclick='{1}' title='{2}' style='{3}'></span>";
			switch (sender.FieldName)
			{
				case "UserType":
					if (e.CurrentRowGenerateType == RowGenerateType.Normal)
						return string.Format(imgTagPattern,
																 e.CurrentRow["UserType"].ToString().ToLower() == "agent" ? "fa fa-users green" : "fa fa-user red",
																 string.Empty, Language.GetString(e.CurrentRow[sender.FieldName].ToString()), string.Empty);

					else
						return Language.GetString(e.CurrentRow[sender.FieldName].ToString());

				case "UserName":
					if (e.CurrentRowGenerateType == RowGenerateType.Normal)
						return string.Format("<a href='#' onclick=\"goToUsersPanel('{0}', '{1}')\">{2}</a>", Helper.Encrypt(e.CurrentRow["Guid"], Session), Helper.Encrypt(e.CurrentRow["DomainGuid"], Session), Helper.GetString(e.CurrentRow["UserName"]));
					else
						return Language.GetString(e.CurrentRow[sender.FieldName].ToString());

				case "Action":
					string icon = "<a href='{0}'><span class='ui-icon {1}' title='{2}' style='{3}'></span></a>";

					string activeLink = "/PageLoader.aspx?c=";

					string notActiveElementStyle = "opacity: 0.5;filter: Alpha(Opacity=20);";
					bool isAdmin = Helper.GetBool(e.CurrentRow["IsAdmin"]);
					bool isMainAdmin = Helper.GetBool(e.CurrentRow["IsMainAdmin"]);
					bool isSuperAdmin = Helper.GetBool(e.CurrentRow["IsSuperAdmin"]);
					Guid parentGuid = Helper.GetGuid(e.CurrentRow["ParentGuid"]);
					int maximumUser = Helper.GetInt(e.CurrentRow["MaximumUser"]);

					actions +=
					string.Format(icon,
											SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.ViewUserTransaction) ? activeLink + Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_ViewUserTransaction, Session) + "&UserGuid=" + e.CurrentRow["Guid"] : "#",
											"fa fa-credit-card green",
											Language.GetString("TransactionList"),
											SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.ViewUserTransaction) ? string.Empty : notActiveElementStyle) +

					string.Format(icon,
											SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.ViewUserEditProfile) ? activeLink + Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_EditProfile, Session) + "&UserGuid=" + e.CurrentRow["Guid"] + "&EditUser=1" : "#",
											"fa fa-pencil blue",
											Language.GetString("Edit"),
											SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.ViewUserEditProfile) ? string.Empty : notActiveElementStyle);

					if (Helper.GetGuid(e.CurrentRow["Guid"]) != UserGuid || isSuperAdmin)
					{
						actions += string.Format(icon,
															SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.AdvancedEditUser) ? activeLink + Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_AdvanceEdit, Session) + "&UserGuid=" + e.CurrentRow["Guid"] + "&ParentGuid=" + e.CurrentRow["ParentGuid"] : "#",
															"fa fa-edit orange",
															Language.GetString("AdvanceEdit"),
															SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.AdvancedEditUser) ? string.Empty : notActiveElementStyle);
					}
					if (isMainAdmin && !isSuperAdmin)
					{
						actions +=
						string.Format(icon,
													SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.DefineUserGroupPrice) ? activeLink + Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_DetermineGroupPrice, Session) + "&UserGuid=" + e.CurrentRow["Guid"] + "&ParentGuid=" + e.CurrentRow["ParentGuid"] : "#",
													"fa fa-usd green",
													Language.GetString("GroupPrice"),
													SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.DefineUserGroupPrice) ? string.Empty : notActiveElementStyle);
					}
					actions +=
					string.Format(icon,
											 SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.ConfirmUserDocument) ? activeLink + Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_ConfirmDocument, Session) + "&UserGuid=" + e.CurrentRow["Guid"] : "#",
											 "fa fa-floppy-o purple",
											 Language.GetString("UserDocument"),
											 SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.ConfirmUserDocument) ? string.Empty : notActiveElementStyle) +

					string.Format(icon,
											 (!(isMainAdmin && parentGuid == Guid.Empty) && SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.AssignPrivateNumberToUser) ?
											 activeLink + Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_AssignPrivateNumberToUsers, Session) + "&UserGuid=" + e.CurrentRow["Guid"] + "&ParentGuid=" + e.CurrentRow["ParentGuid"] : "#"),
											 "fa fa-sort-numeric-asc dark",
											 Language.GetString("AssignNumberToUsers"),
											 (!(isMainAdmin && parentGuid == Guid.Empty) && SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.AssignPrivateNumberToUser)) ? string.Empty : notActiveElementStyle) +

					string.Format(imgTagPattern,
												"fa fa-trash-o red",
												SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.ManageUser) ? "deleteUser(event);" : string.Empty,
												Language.GetString("Delete"),
												SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.ManageUser) ? string.Empty : notActiveElementStyle);

					return actions;
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			gridUsers.TopToolbarItems += string.Format(@"<div style=""margin:1px;"">");
			gridUsers.TopToolbarItems += string.Format(@"<label>{0}</label><select id=""drpDomain"" style=""width:200px;""></select>", Language.GetString("Domain"));
			gridUsers.TopToolbarItems += string.Format(@"<a href=""#"" id=""btnSearch"" class=""btn btn-sm btn-success"" style=""margin-right:3px;"" >{0}</a>", Language.GetString("Search"));
			gridUsers.TopToolbarItems += string.Format("<a href=\"/PageLoader.aspx?c={0}&ActionType=insert\" style=\"margin-right:10px;\" class=\"btn btn-sm btn-success\" >{1}</a>",
																									Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_DefineUser, Session), Language.GetString("RegisterUser"));
			gridUsers.TopToolbarItems += string.Format(@"</div>");


			drpStatus.Items.Add(new ListItem(string.Empty, "-1"));
			drpStatus.Items.Add(new ListItem(Language.GetString("InActive"), "0"));
			drpStatus.Items.Add(new ListItem(Language.GetString("Active"), "1"));

			if (SubGridParentGuid != Guid.Empty)
			{
				Guid parentGuid = Facade.User.LoadUser(SubGridParentGuid).ParentGuid;
				gridUsers.TopToolbarItems = string.Format("<a href=\"/PageLoader.aspx?c={0}&SubGridParentGuid={1}\" class=\"btn btn-default toolbarButton\" >{2}</a>",
																					 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_User, Session),
																					 parentGuid,
																					 Language.GetString("Return"));
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			if (!Helper.GetBool(Session["IsMainAdmin"]))
				permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageUser);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Users_User;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Users_User.ToString());
		}
	}
}
