// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using Arad.SMS.Gateway.GeneralLibrary.bankmellat.bpm;
using System;
using System.Text;

namespace Arad.SMS.Gateway.GeneralLibrary.OnlinePayment
{
	public class BehPardakhtMellat
	{
		private PaymentGatewayImplService behPardakhtWebService;
		//private string postUrl = "https://bpm.shaparak.ir/pgwchannel/startpay.mellat";
		//private long terminalID;
		//private string userName;
		//private string password;
		//private long orderID;
		//private long amount = 0;
		//private string additionalData;
		//private string callBackUrl;
		//private long payerID;
		private static Random random = new Random();

		public enum ResCode
		{
			TransactionWasSuccessful = 0,
			CardNumberIsInvalid = 11,
			AccountBalanceIsNotEnough = 12,
			PasswordIsIncorrect = 13,
			EnteringThePasswordIsExceeded = 14,
			CardIsInvalid = 15,
			TotalOfRecievingMoneyIsOutOfLimited = 16,
			UserCancelRequest = 17,
			CardIsExpired = 18,
			AmountOfRecieveMoneyIsOutOfLimit = 19,
			ExportingTheCardIsInvalid = 111,
			ExportingSwitchCardError = 112,
			DontRecieveResponseFromCardExporter = 113,
			CardHolderDontAllowedToPerformThisTransaction = 114,
			AcceptorIsInvalid = 21,
			SecurityErrorOccurred = 23,
			AcceptorInformationIsInvalid = 24,
			AmountIsInvalid = 25,
			ResponseIsInvalid = 31,
			EnteringInformationFormatIsInvalid = 32,
			AccountIsInvalid = 33,
			SystemError = 34,
			DateIsInvalid = 35,
			OrderIDIsDuplicate = 41,
			NotFoundSaleTransaction = 42,
			VerifyRequestIsDuplicate = 43,
			NotFoundVerifyRequest = 44,
			TransactionIsSettle = 45,
			TransactionIsNotSettle = 46,
			NotFoundSettleTransaction = 47,
			TransactionIsReverse = 48,
			NotFoundRefundTransaction = 49,
			IDBillIsWrong = 412,
			IDPaymentIsWrong = 413,
			ExporterOfTheBillIsInvalid = 414,
			SessionIsTimeOut = 415,
			RegisterInformationError = 416,
			PayerIDIsInvalid = 417,
			DefineCustomerDataError = 418,
			NumberOfDataEntryIsOutOfLimite = 419,
			IPIsInvalid = 421,
			TransactionIsDuplicate = 51,
			ReferenceTransactionDoesNotExist = 54,
			TransactionIsInvalid = 55,
			SettleError = 61,
		}

		public string PostUrl { get { return "https://bpm.shaparak.ir/pgwchannel/startpay.mellat"; } }
		//{
		//	get { return postUrl; }
		//	set { postUrl = value; }
		//}

		public long TerminalID { get; set; }
		//{
		//	get { return terminalID; }
		//	set { terminalID = value; }
		//}

		public string UserName { get; set; }
		//{
		//	get { return userName; }
		//	set { userName = value; }
		//}

		public string Password { get; set; }
		//{
		//	get { return password; }
		//	set { password = value; }
		//}

		public long OrderID { get; set; }
		//{
		//	get { return orderID; }
		//	set { orderID = value; }
		//}

		public long Amount { get; set; }
		//{
		//	get { return amount; }
		//	set { amount = value; }
		//}

		public string AdditionalData { get; set; }
		//{
		//	get { return additionalData; }
		//	set { additionalData = value; }
		//}

		public string CallBackUrl { get; set; }
		public string GatewayPaymentIP { get { return ConfigurationManager.GetSetting("MellatGatewayPaymentIP"); } }
		//{
		//	get { return callBackUrl; }
		//	set { callBackUrl = value; }
		//}

		public long PayerID { get; set; }
		//{
		//	get { return payerID; }
		//	set { payerID = value; }
		//}

		public string LocalDate
		{
			get
			{
				return DateManager.GetSolarDate(DateTime.Now).Replace("/", "");
			}
		}

		public string LocalTime
		{
			get
			{
				return DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
			}
		}

		public BehPardakhtMellat()
		{
			behPardakhtWebService = new PaymentGatewayImplService();
			OrderID = GenerateRandomOrderID();
		}

		public long GenerateRandomOrderID()
		{
			return random.Next();
		}

		public string Request(ref string result)
		{
			try
			{
				int resultResCode = -1;
				string resultRefID = string.Empty;
				string response = behPardakhtWebService.bpPayRequest(TerminalID, UserName, Password, OrderID, Amount, LocalDate, LocalTime, AdditionalData, CallBackUrl, PayerID);

				if (response.IndexOf(',') != -1)
				{
					resultResCode = Helper.GetInt(response.Split(',')[0]);
					resultRefID = Helper.GetString(response.Split(',')[1]);
				}
				else
					resultResCode = Helper.GetInt(response);

				foreach (ResCode resCode in Enum.GetValues(typeof(ResCode)))
					if (resultResCode == (int)resCode && resCode != ResCode.TransactionWasSuccessful)
						throw new Exception(Language.GetString(resCode.ToString()));

				return resultRefID;
			}
			catch (Exception ex)
			{
				//throw new Exception(Language.GetString("DontRecieveResponseFromBank"));
				throw ex;
			}
		}

		public string GotoGateway(string refId)
		{
			StringBuilder script = new StringBuilder();
			script.Append("function postRefId(refIdValue) {");
			script.Append("var form = document.createElement('form');");
			script.Append("form.setAttribute('method', 'POST');");
			script.Append("form.setAttribute('action', '" + PostUrl + "');");
			script.Append("form.setAttribute('target', '_parent');");
			script.Append("var hiddenField = document.createElement('input');");
			script.Append("hiddenField.setAttribute('name', 'RefId');");
			script.Append("hiddenField.setAttribute('value', refIdValue);");
			script.Append("form.appendChild(hiddenField);");
			script.Append("document.body.appendChild(form);");
			script.Append("form.submit();");
			script.Append("document.body.removeChild(form);");
			script.Append("}");
			script.Append("postRefId('" + refId + "');");
			return script.ToString();
		}

		public void VerifyRequest(long saleReferenceId)
		{
			try
			{
				string Result = behPardakhtWebService.bpVerifyRequest(TerminalID, UserName, Password, OrderID, OrderID, saleReferenceId);

				foreach (ResCode resCode in System.Enum.GetValues(typeof(ResCode)))
					if (Helper.GetInt(Result) == (int)resCode && resCode != (int)ResCode.TransactionWasSuccessful)
						throw new Exception(Language.GetString(resCode.ToString()));

				SettleRequest(saleReferenceId);
			}
			catch
			{
				InquiryRequest(saleReferenceId);
			}
		}

		public void InquiryRequest(long saleReferenceId)
		{
			try
			{
				string Result = behPardakhtWebService.bpInquiryRequest(TerminalID, UserName, Password, OrderID, OrderID, saleReferenceId);

				foreach (ResCode resCode in System.Enum.GetValues(typeof(ResCode)))
					if (Helper.GetInt(Result) == (int)resCode && resCode != (int)ResCode.TransactionWasSuccessful)
						throw new Exception(Language.GetString(resCode.ToString()));

				SettleRequest(saleReferenceId);
			}
			catch
			{
				ReversalRequest(saleReferenceId);
			}
		}

		public string ReversalRequest(long saleReferenceId)
		{
			try
			{
				string Result = behPardakhtWebService.bpReversalRequest(TerminalID, UserName, Password, OrderID, OrderID, saleReferenceId);

				foreach (ResCode resCode in System.Enum.GetValues(typeof(ResCode)))
					if (Helper.GetInt(Result) == (int)resCode && resCode != (int)ResCode.TransactionWasSuccessful)
						throw new Exception(Language.GetString(resCode.ToString()));

				return Result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public string SettleRequest(long saleRefrenceId)
		{
			try
			{
				string Result = behPardakhtWebService.bpSettleRequest(TerminalID, UserName, Password, OrderID, OrderID, saleRefrenceId);

				foreach (ResCode resCode in System.Enum.GetValues(typeof(ResCode)))
					if (Helper.GetInt(Result) == (int)resCode && resCode != (int)ResCode.TransactionWasSuccessful)
						throw new Exception(Language.GetString(resCode.ToString()));

				return Result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
