using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.Business;


namespace Arad.SMS.Gateway.Web.UI.SmsTemplates
{
	public partial class SaveSmsTemplate : UIUserControlBase
	{
		private string ActionType
		{
			get { return Helper.Request(this, "ActionType").ToLower(); }
		}

		private Guid SmsTemplateGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		//private Guid GroupTemplateGuid
		//{
		//	get { return Helper.RequestEncryptedGuid(this, "Guid"); }
		//}

		//private bool RequestFromSmsBodyBox
		//{
		//	get { return Helper.RequestBool(this, "RequestFromSmsBodyBox"); }
		//}

		//private string SmsBody
		//{
		//	get { return Helper.Request(this, "SmsBody"); }
		//}

		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);
			btnSave.Attributes["onclick"] = "return validateRequiredFields();";

			if (ActionType == "edit")
			{
				Common.SmsTemplate smsTemplate = Facade.SmsTemplate.LoadSmsTemplate(SmsTemplateGuid);
				txtSmsBody.Text = smsTemplate.Body;
				//drpGroupTemplate.SelectedValue = smsTemplate.GroupTemplateGuid.ToString();
			}

			//if (ActionType == "insert" && RequestFromSmsBodyBox)
			//{
			//	txtSmsBody.Text = SmsBody.Replace("\\r\\n", "\n");
			//	txtSmsBody.Text = txtSmsBody.Text.Replace("\\n", "\n");
			//}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.SmsTemplate smsTemplate = new Common.SmsTemplate();
			try
			{
				smsTemplate.SmsTemplateGuid = SmsTemplateGuid;
				smsTemplate.Body = txtSmsBody.Text;
				smsTemplate.CreateDate = DateTime.Now;
				smsTemplate.UserGuid = UserGuid;
				switch (ActionType)
				{
					case "edit":
						if (smsTemplate.HasError)
							throw new Exception(smsTemplate.ErrorMessage);

						if (!Facade.SmsTemplate.UpdateSmsTemplate(smsTemplate))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
					case "insert":
						if (smsTemplate.HasError)
							throw new Exception(smsTemplate.ErrorMessage);

						if (!Facade.SmsTemplate.Insert(smsTemplate))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}
				ClientSideScript = string.Format("result('ok','{0}')", Language.GetString("InsertRecord"));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('error','{0}')", ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_SmsTemplates_SmsTemplate, Session)));
		}
		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permission = new List<int>();
			permission.Add((int)Arad.SMS.Gateway.Business.Services.AddSmsTemplate);
			return permission;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsTemplates_SaveSmsTemplate;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsTemplates_SaveSmsTemplate.ToString());
		}
	}
}