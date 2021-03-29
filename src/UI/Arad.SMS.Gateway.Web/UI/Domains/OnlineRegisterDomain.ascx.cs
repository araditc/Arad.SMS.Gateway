using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Business;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;

namespace MessagingSystem.UI.Domains
{
	public partial class OnlineRegisterDomain : UIUserControlBase
	{
		private GeneralLibrary.RegisterDomain registerDomain;
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}
		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private static DataTable dataTableDomainStatus = new DataTable();

		public OnlineRegisterDomain()
		{
			AddDataBinderHandlers("gridDomainList", new GeneralTools.DataGrid.DataBindHandler(gridDomainList_OnDataBind));
			AddDataRenderHandlers("gridDomainList", new GeneralTools.DataGrid.CellValueRenderEventHandler(gridDomainList_OnDataRender));
			try
			{
				registerDomain = new GeneralLibrary.RegisterDomain(ConfigurationManager.GetSetting("DomainRegisterUserName"),
																													 ConfigurationManager.GetSetting("DomainRegisterPassword"),
																													 ConfigurationManager.GetSetting("DomainRegisterName"));
			}
			catch { }
		}

		public DataTable gridDomainList_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount)
		{
			return dataTableDomainStatus;
		}

		public string gridDomainList_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					if (Helper.GetInt(e.CurrentRow["Status"]) == (int)Business.DomainStatus.Available)
						return string.Format("<a href='#' onclick='orderRegisterDomain(event);'>{0}</a>", Language.GetString("OrderRegister"));
					else
						return string.Format("---------");

				case "Status":
					if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)Business.DomainStatus.Unavailable)
						return string.Format("<img src='{0}' class='gridImageButton' title='{1}'/>", "/pic/CANCELED.png", Language.GetString("Unavailable"));
					else
						return string.Format("<img src='{0}' class='gridImageButton' title='{1}'/>", "/pic/FINISHED.png", Language.GetString("Available"));
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
			btnRegisterNicDomain.Text = GeneralLibrary.Language.GetString(btnRegisterNicDomain.Text);
			btnRegisterDirectDomain.Text = GeneralLibrary.Language.GetString(btnRegisterDirectDomain.Text);
			btnRegisterNicDomain.Attributes["onclick"] = "return checkValidateRegisterNicDomain();";
			btnRegisterDirectDomain.Attributes["onclick"] = "return checkValidateRegisterDirectDomain();";
			drpNicPeriod.Attributes["onchange"] = string.Format("getDomainPrice('{0}');", (int)Business.DomainType.Nic);
			drpDirectPeriod.Attributes["onchange"] = string.Format("getDomainPrice('{0}');", (int)Business.DomainType.Direct);
			btnSearch.Attributes["onclick"] = "return validationSearch();";
			btnPayment.Text = GeneralLibrary.Language.GetString(btnPayment.Text);

			foreach (DomainExtention typeExtention in System.Enum.GetValues(typeof(DomainExtention)))
				checkBoxListExtention.Items.Add(new ListItem(typeExtention.ToString().Replace('_', '.'), ((int)typeExtention).ToString()));
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				List<string> lstExtention = new List<string>();

				if (registerDomain.IsLoginSuccessful)
				{
					for (int Counter = 0; Counter < checkBoxListExtention.Items.Count; Counter++)
					{
						if (checkBoxListExtention.Items[Counter].Selected)
						{
							lstExtention.Add(checkBoxListExtention.Items[Counter].Text);
						}
					}

					if (lstExtention.Count == 0)
						throw new Exception(Language.GetString("SelectDomainExtention"));

					string[] domainExtention = registerDomain.CheckDomainExtentions(txtDomain.Text, lstExtention);
					dataTableDomainStatus = Facade.RegisteredDomain.GetDataTableDomainStatus(domainExtention);
					ClientSideScript = "searchDomain();";
				}
				else
					throw new Exception(Language.GetString("DomainAccountUnavailable"));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		protected void btnRegisterNicDomain_Click(object sender, EventArgs e)
		{
			try
			{
				Common.RegisteredDomain registeredDomain = new Common.RegisteredDomain();

				Guid domainGuid = Helper.DecryptGuid(hdnCheckedDomainGuid.Value, Session);
				DataRow domainSelected = (from row in dataTableDomainStatus.AsEnumerable()
																	where Helper.GetGuid(row["Guid"]) == domainGuid
																	select row).First();

				registeredDomain.RegisteredDomainGuid = Helper.GetGuid(domainSelected["Guid"]);
				registeredDomain.UserGuid = UserGuid;
				registeredDomain.DomainName = domainSelected["DomainName"].ToString().Split('.')[0];
				registeredDomain.DomainExtention = Helper.GetInt(domainSelected["Extention"]);
				registeredDomain.Type = (int)Business.DomainType.Nic;
				registeredDomain.DNS1 = txtNicDNS1.Text;
				registeredDomain.DNS2 = txtNicDNS2.Text;
				registeredDomain.DNS3 = txtNicDNS3.Text;
				registeredDomain.DNS4 = txtNicDNS4.Text;
				registeredDomain.IP1 = txtNicIP1.Text;
				registeredDomain.IP2 = txtNicIP2.Text;
				registeredDomain.IP3 = txtNicIP3.Text;
				registeredDomain.IP4 = txtNicIP4.Text;
				registeredDomain.Period = Helper.GetInt(drpNicPeriod.SelectedValue);
				registeredDomain.CreateDate = DateTime.Now;
				registeredDomain.ExpireDate = DateTime.Now.AddYears(registeredDomain.Period);
				registeredDomain.IsPayment = false;
				registeredDomain.Status = (int)Business.DomainRegisterStatus.Incomplete;
				registeredDomain.CustomerID = txtNicCustomerID.Text;
				registeredDomain.OfficeRelation = txtNicOfficeRelation.Text;
				registeredDomain.TechnicalRelation = txtNicTechnicalRelation.Text;
				registeredDomain.FinancialRelation = txtNicFinancialRelation.Text;

				if (registeredDomain.HasError)
					throw new Exception(registeredDomain.ErrorMessage);

				decimal domainPrice = Facade.DomainPrice.GetDomainPrice(UserGuid, (Business.DomainExtention)registeredDomain.DomainExtention, registeredDomain.Period);
				if (domainPrice == -1)
					throw new Exception("UndefinePriceForRegisterDomain");

				if (Facade.RegisteredDomain.InsertNicDomain(registeredDomain))
				{
					//registerDomain.RegisterNicDomian(registeredDomain.DomainName, ((Business.DomainExtention)registeredDomain.DomainExtention).ToString().Replace('_', '.'), (byte)registeredDomain.Period,
					//                                 registeredDomain.DNS1, registeredDomain.DNS2, registeredDomain.DNS3, registeredDomain.DNS4,
					//                                 registeredDomain.IP1, registeredDomain.IP2, registeredDomain.IP3, registeredDomain.IP4,
					//                                 registeredDomain.CustomerID, registeredDomain.OfficeRelation, registeredDomain.TechnicalRelation, registeredDomain.FinancialRelation);
					ClientSideScript = string.Format("paymentDomainPrice('{0}','{1}');", Helper.FormatDecimalForDisplay(domainPrice),registeredDomain.Period);
				}
				else
					throw new Exception(Language.GetString("ErrorRecord"));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		protected void btnRegisterDirectDomain_Click(object sender, EventArgs e)
		{
			try
			{
				Common.RegisteredDomain registeredDomain = new Common.RegisteredDomain();

				Guid domainGuid = Helper.DecryptGuid(hdnCheckedDomainGuid.Value, Session);
				DataRow domainSelected = (from row in dataTableDomainStatus.AsEnumerable()
																	where Helper.GetGuid(row["Guid"]) == domainGuid
																	select row).First();

				registeredDomain.RegisteredDomainGuid = Helper.GetGuid(domainSelected["Guid"]);
				registeredDomain.UserGuid = UserGuid;
				registeredDomain.DomainName = domainSelected["DomainName"].ToString().Split('.')[0];
				registeredDomain.DomainExtention = Helper.GetInt(domainSelected["Extention"]);
				registeredDomain.Type = (int)Business.DomainType.Direct;
				registeredDomain.DNS1 = txtDirectDNS1.Text;
				registeredDomain.DNS2 = txtDirectDNS2.Text;
				registeredDomain.Period = Helper.GetInt(drpDirectPeriod.SelectedValue);
				registeredDomain.CreateDate = DateTime.Now;
				registeredDomain.ExpireDate = DateTime.Now.AddYears(registeredDomain.Period);
				registeredDomain.IsPayment = false;
				registeredDomain.Status = (int)Business.DomainRegisterStatus.Incomplete;
				registeredDomain.CustomerID = txtDirectCustomerID.Text;
				registeredDomain.Email = txtEmail.Text;

				if (registeredDomain.HasError)
					throw new Exception(registeredDomain.ErrorMessage);

				decimal domainPrice = Facade.DomainPrice.GetDomainPrice(UserGuid, (Business.DomainExtention)registeredDomain.DomainExtention, registeredDomain.Period);
				if (domainPrice == -1)
					throw new Exception("UndefinePriceForRegisterDomain");

				if (Facade.RegisteredDomain.InsertDirectDomain(registeredDomain))
				{
					ClientSideScript = string.Format("paymentDomainPrice('{0}','{1}');", Helper.FormatDecimalForDisplay(domainPrice), registeredDomain.Period);
				}
				else
					throw new Exception(Language.GetString("ErrorRecord"));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		protected void btnPayment_Click(object sender, EventArgs e)
		{
			try
			{
				Guid domainGuid = Helper.DecryptGuid(hdnCheckedDomainGuid.Value, Session);
				DataRow domainSelected = (from row in dataTableDomainStatus.AsEnumerable()
																	where Helper.GetGuid(row["Guid"]) == domainGuid
																	select row).First();

				string domainName = domainSelected["DomainName"].ToString();
				Business.DomainExtention extention = (Business.DomainExtention)Helper.GetInt(domainSelected["Extention"]);
				int domainPeriod = Helper.GetInt(hdnDomainPeriod.Value);
				decimal domainPrice = Facade.DomainPrice.GetDomainPrice(UserGuid, extention, domainPeriod);
				if (domainPrice == -1)
					throw new Exception("UndefinePriceForRegisterDomain");

				Facade.Transaction.DecreaseCostOfRegisterDomain(UserGuid, ParentGuid, domainName, extention, domainPeriod, domainPeriod, domainGuid);
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Business.Services.ManageDomain);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Business.UserControls.UI_Domains_OnlineRegisterDomain;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_OnlineRegisterDomain.ToString());
		}

	}
}