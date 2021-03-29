using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Data
{
	public partial class SaveContent : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid DataGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType").ToLower(); }
		}

		private Guid DataCenterGuid
		{
			get { return Helper.RequestGuid(this, "DataCenterGuid"); }
		}

		private int Type
		{
			get { return Helper.RequestInt(this, "Type"); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Type == (int)Arad.SMS.Gateway.Business.DataCenterType.Menu)
				pnlParentData.Visible = true;

			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Attributes["onclick"] = "return validateRequiredFields();";
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);

			int resultCount = 0;
			drpParent.DataSource = Facade.Data.GetPagedData(UserGuid, DataCenterGuid, "CreateDate", 0, 0, ref resultCount);
			drpParent.DataTextField = "Title";
			drpParent.DataValueField = "Guid";
			drpParent.DataBind();
			drpParent.Items.Insert(0, new ListItem(string.Empty, Guid.Empty.ToString()));

			if (ActionType == "edit")
			{
				Common.Data data = Facade.Data.LoadData(DataGuid);
				txtTitle.Text = data.Title;
				txtPriority.Text = data.Priority.ToString();
				txtBreif.Text = data.Summary;
				txtBody.Text = data.Content;
				dtpFromDate.Value = data.FromDate != DateTime.MinValue ? DateManager.GetSolarDate(data.FromDate) : string.Empty;
				dtpToDate.Value = data.ToDate != DateTime.MinValue ? DateManager.GetSolarDate(data.ToDate) : string.Empty;
				drpParent.SelectedValue = data.ParentGuid.ToString();
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.Data data = new Common.Data();

			try
			{
				data.DataCenterGuid = DataCenterGuid;
				data.Title = txtTitle.Text;
				data.Priority = Helper.GetInt(txtPriority.Text, 1);
				data.Summary = txtBreif.Text;
				data.Content = txtBody.Text;
				data.CreateDate = DateTime.Now;

                var dateTime = dtpFromDate.FullDateTime;
                var toDate = dtpToDate.FullDateTime;

                if (Session["Language"].ToString() == "fa")
                {
                    data.FromDate = DateManager.GetChristianDateTimeForDB(dateTime);
                    data.ToDate = DateManager.GetChristianDateTimeForDB(toDate);
                }
                else
                {
                    data.FromDate = DateTime.Parse(dateTime);
                    data.ToDate = DateTime.Parse(toDate);
                }
				data.ParentGuid = Helper.GetGuid(drpParent.SelectedValue);

				if (data.HasError)
					throw new Exception(data.ErrorMessage);

				switch (ActionType)
				{
					case "insert":
						if (!Facade.Data.InsertData(data))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
					case "edit":
						data.DataGuid = DataGuid;
						if (!Facade.Data.UpdateData(data))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}&DataCenterGuid={1}&Type={2}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Data_Contents, Session), DataCenterGuid, Type));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}&DataCenterGuid={1}&Type={2}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Data_Contents, Session), DataCenterGuid,Type));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveContent);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Data_SaveContent;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Data_SaveContent.ToString());
		}

	}
}
