using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Arad.SMS.Gateway.Web.UI.GroupPrices
{
	public partial class SaveGroupPrice : UIUserControlBase
	{
		protected Guid firstParentMainAdmin;
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		protected bool IsMainAdmin
		{
			get { return Helper.GetBool(Session["IsMainAdmin"]); }
		}

		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private Guid GroupPriceGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType").ToLower(); }
		}

		public SaveGroupPrice()
		{
			AddDataBinderHandlers("gridAgentRatio", new DataBindHandler(gridAgentRatio_OnDataBind));
			AddDataRenderHandlers("gridAgentRatio", new CellValueRenderEventHandler(gridAgentRatio_OnDataRender));
		}

		public DataTable gridAgentRatio_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string agentGuid = string.Empty;
			string agentTitle = string.Empty;
			decimal currentUserRatio;
			decimal groupRatio;
			XElement groupPriceRatioElement;

			firstParentMainAdmin = Facade.SmsSenderAgent.GetFirstParentMainAdmin(UserGuid);
			List<DataRow> lstAgents = Facade.SmsSenderAgent.GetUserAgents(firstParentMainAdmin).AsEnumerable().ToList();

			DataTable dtAgentRatio = new DataTable();
			dtAgentRatio.Columns.Add("Guid", typeof(Guid));
			dtAgentRatio.Columns.Add("AgentID", typeof(string));
			dtAgentRatio.Columns.Add("Agent", typeof(string));
			dtAgentRatio.Columns.Add("CurrentRatio", typeof(decimal));
			dtAgentRatio.Columns.Add("NewRatio", typeof(decimal));

			Common.GroupPrice userGroupPrice = Facade.GroupPrice.LoadGroupPrice(Facade.User.GetGroupPriceGuid(UserGuid));
			var xelement = XElement.Parse(userGroupPrice.AgentRatio);
			List<XElement> lstUserRatio = xelement.Elements("Table").ToList();

			foreach (var item in lstAgents)
			{
				agentGuid = Helper.GetGuid(item["Guid"]).ToString();
				agentTitle = item["Name"].ToString();
				currentUserRatio = 1;
				groupRatio = 1;

				groupPriceRatioElement = lstUserRatio.Where(agent => Helper.GetGuid(agent.Element("AgentID").Value) == Helper.GetGuid(agentGuid)).FirstOrDefault();
				if (groupPriceRatioElement != null)
				{
					currentUserRatio = Helper.GetDecimal(groupPriceRatioElement.Element("Ratio").Value, 1);
					groupRatio = currentUserRatio;
				}

				if (ActionType == "edit")
				{
					Common.GroupPrice groupPrice = Facade.GroupPrice.LoadGroupPrice(GroupPriceGuid);
					xelement = XElement.Parse(groupPrice.AgentRatio);
					List<XElement> lstOperatorElement = xelement.Elements("Table").ToList();

					groupPriceRatioElement = lstOperatorElement.Where(agent => Helper.GetGuid(agent.Element("AgentID").Value) == Helper.GetGuid(agentGuid)).FirstOrDefault();
					if (groupPriceRatioElement != null)
							groupRatio = Helper.GetDecimal(groupPriceRatioElement.Element("Ratio").Value, 1);
				}
				dtAgentRatio.Rows.Add(Guid.NewGuid(), agentGuid, agentTitle, currentUserRatio, groupRatio);
			}

			return dtAgentRatio;
		}

		public string gridAgentRatio_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);
			btnSave.Attributes["onclick"] = "return validateGroupPrice();";

			if (ActionType == "edit")
			{
				Common.GroupPrice groupPrice = Facade.GroupPrice.LoadGroupPrice(GroupPriceGuid);
				chbIsPrivate.Checked = groupPrice.IsPrivate;
				chbIsPrivate.Enabled = groupPrice.IsPrivate ? false : true;
				chbDefaultGroupPrice.Checked = groupPrice.IsDefault;
				chbDecreaseTax.Checked = groupPrice.DecreaseTax;
				chbIsPrivate.Checked = groupPrice.IsPrivate;
				txtTitle.Text = groupPrice.Title;
				txtMinimumMessage.Text = Helper.FormatDecimalForDisplay(groupPrice.MinimumMessage);
				txtMaximumMessage.Text = Helper.FormatDecimalForDisplay(groupPrice.MaximumMessage);
				txtBasePrice.Text = Helper.FormatDecimalForDisplay(groupPrice.BasePrice);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.GroupPrice groupPrice = new Common.GroupPrice();
			string agentRatio = string.Empty;
			try
			{
				groupPrice.Title = txtTitle.Text;
				groupPrice.IsPrivate = chbIsPrivate.Checked;
				groupPrice.IsDefault = groupPrice.IsPrivate ? false : chbDefaultGroupPrice.Checked;
				groupPrice.MinimumMessage = groupPrice.IsPrivate ? 0 : Helper.GetLong(txtMinimumMessage.Text);
				groupPrice.MaximumMessage = groupPrice.IsPrivate ? 0 : Helper.GetLong(txtMaximumMessage.Text);
				groupPrice.GroupPriceGuid = GroupPriceGuid;
				groupPrice.BasePrice = Helper.GetDecimal(txtBasePrice.Text);
				groupPrice.DecreaseTax = chbDecreaseTax.Checked;
				groupPrice.AgentRatio = string.Empty;
				groupPrice.UserGuid = UserGuid;
				groupPrice.CreateDate = DateTime.Now;

				if (!Helper.CheckDataConditions(hdnAgentRatio.Value).IsEmpty)
					agentRatio = hdnAgentRatio.Value;

				if (!IsMainAdmin)
					Facade.GroupPrice.CompareAgentRatio(groupPrice.BasePrice, chbDecreaseTax.Checked, agentRatio, ParentGuid);

				switch (ActionType)
				{
					case "edit":
						if (groupPrice.HasError)
							throw new Exception(groupPrice.ErrorMessage);

						if (!groupPrice.IsPrivate)
						{
							if (groupPrice.MinimumMessage >= groupPrice.MaximumMessage)
								throw new Exception(Language.GetString("InvalidRange"));

							if (!Facade.GroupPrice.CheckExistRange(UserGuid, GroupPriceGuid, groupPrice.MinimumMessage, groupPrice.MaximumMessage))
								throw new Exception(Language.GetString("DuplicateRangeOfGroupPrice"));
						}
						if (!Facade.GroupPrice.UpdateGroupPrice(groupPrice, agentRatio,IsMainAdmin))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
					case "insert":
						if (groupPrice.HasError)
							throw new Exception(groupPrice.ErrorMessage);

						if (!groupPrice.IsPrivate)
						{
							if (groupPrice.MinimumMessage >= groupPrice.MaximumMessage)
								throw new Exception(Language.GetString("InvalidRange"));

							if (!Facade.GroupPrice.CheckExistRange(UserGuid, Guid.Empty, groupPrice.MinimumMessage, groupPrice.MaximumMessage))
								throw new Exception(Language.GetString("DuplicateRangeOfGroupPrice"));
						}

						if (!Facade.GroupPrice.Insert(groupPrice, agentRatio))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_GroupPrices_GroupPrice, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageGroupPrice);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_GroupPrices_SaveGroupPrice;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_GroupPrices_SaveGroupPrice.ToString());
		}
	}
}