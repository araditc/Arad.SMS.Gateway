using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.Business;

namespace Arad.SMS.Gateway.Web.UI.Widgets.SendSingleSms
{
	public partial class SendSingleSms : System.Web.UI.UserControl
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private void InitializePage()
		{
			//drpSendNumber.DataSource = Facade.UserPrivateNumber.GetUserPrivateNumbersForSend(UserGuid, ParentGuid);
			//drpSendNumber.DataTextField = "Number";
			//drpSendNumber.DataValueField = "NumberGuid";
			//drpSendNumber.DataBind();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			btnSend.Text = GeneralLibrary.Language.GetString(btnSend.Text);
		}

		protected void btnSend_Click(object sender, EventArgs e)
		{
			//Common.SmsSent smsSent = new Common.SmsSent();

			//smsSent.TypeSend = (int)Arad.SMS.Gateway.Business.SmsSendType.SendSingleSms;
			//smsSent.CreateDate = DateTime.Now;
			//smsSent.UserGuid = UserGuid;
			//smsSent.SmsBody = txtSmsBody.Text;
			//smsSent.SmsCount = txtSmsBody.SmsCount;
			//smsSent.PresentType = (int)Arad.SMS.Gateway.Business.Messageclass.Normal;
			//smsSent.UserPrivateNumberGuid = Helper.GetGuid(drpSendNumber.SelectedValue);
			//smsSent.Encoding = Helper.HasUniCodeCharacter(smsSent.SmsBody) ? (int)Encoding.Utf8 : (int)Encoding.Default;
			//smsSent.State = (int)Arad.SMS.Gateway.Business.SmsSentStates.Pending;

			//string lstNumbers = txtReceiverNumber.Text.Replace("\r\n", "\n");
			//string[] recieversNumber = lstNumbers.Split('\n');
			//string reportSend = string.Empty;
			//string cellPhone = string.Empty;
			//bool isValidCellPhone;
			//List<string> lstNumberFail = new List<string>();
			//int countSmsInserted = 0, countSmsFailInserted = 0;

			//for (int counterReciever = 0; counterReciever < recieversNumber.Length; counterReciever++)
			//{
			//  isValidCellPhone = false;
			//  cellPhone = recieversNumber[counterReciever].ToString();
			//  Helper.CheckingCellPhone(ref cellPhone, ref isValidCellPhone);
			//  if (isValidCellPhone)
			//  {
			//    smsSent.Reciever = cellPhone;
			//    if (Facade.SmsSent.InsertSendSingleSms(smsSent))
			//      countSmsInserted++;
			//    else
			//      countSmsFailInserted++;
			//  }
			//  else if (cellPhone != string.Empty)
			//    lstNumberFail.Add(cellPhone);
			//}

			//reportSend += Language.GetString("CountSmsInserted") + "  " + countSmsInserted + "  " + Language.GetString("Number") + "  " + "<br/>";
			//reportSend += Language.GetString("CountSmsFailInserted") + "  " + countSmsFailInserted + "  " + Language.GetString("Number") + "  " + "<br/>";
			//reportSend += Language.GetString("CountNumberFail") + "  " + lstNumberFail.Count + "  " + Language.GetString("Number") + "  " + "<br/>";
		}
	}
}