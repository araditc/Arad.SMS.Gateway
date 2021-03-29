using GeneralLibrary.Parsian;
using System;

namespace GeneralLibrary.OnlinePayment
{
	public class ParsianPaymentGateway
	{

		private EShopService service;
		//private string postUrl = "https://pec.shaparak.ir/pecpaymentgateway";
		//private string pin;
		//private int orderID;
		//private int amount = 0;
		//private string callBackUrl;
		private static Random random;

		public enum Status
		{
			Successful = 0,
			PreRequest = 1,
			AccessViolation = 20,
			MerchantAuthenticationFailed = 22,
			SaleIsAlreadyDoneSuccessfully = 30,
			InvalidMerchantOrder = 34,
		}

		public string PostUrl { get { return "https://pec.shaparak.ir/pecpaymentgateway"; } }
		//{
		//	get { return postUrl; }
		//	set { postUrl = value; }
		//}

		public string Pin { get; set; }
		//	{
		//		get { return pin; }
		//set { pin = value; }
		//	}

		public int OrderID { get; set; }
		//{
		//	get { return orderID; }
		//	set { orderID = value; }
		//}

		public int Amount { get; set; }
		//{
		//	get { return amount; }
		//	set { amount = value; }
		//}

		public string CallBackUrl { get; set; }
		//{
		//	get { return callBackUrl; }
		//	set { callBackUrl = value; }
		//}
		public string GatewayPaymentIP { get { return ConfigurationManager.GetSetting("ParsianGatewayPaymentIP"); } }

		public ParsianPaymentGateway()
		{
			random = new Random();
			service = new EShopService();
			OrderID = random.Next();
		}

		public string Request()
		{
			try
			{
				long authority = 0;
				byte status = 0;
				service.PinPaymentRequest(Pin, Amount, OrderID, CallBackUrl, ref authority, ref status);

				foreach (Status result in Enum.GetValues(typeof(Status)))
					if (status == (int)result && result != Status.Successful)
						throw new Exception(Language.GetString(result.ToString()));

				if (authority == -1)
					throw new Exception(Language.GetString("InvalidAuthority"));

				return authority.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void VerifyRequest(long authority, ref byte status, ref long invoiceNumber)
		{
			try
			{
				service.PaymentEnquiry(Pin, authority, ref status, ref invoiceNumber);

				foreach (Status result in Enum.GetValues(typeof(Status)))
					if (Helper.GetInt(status) == (int)result && result != (int)Status.Successful)
						throw new Exception(Language.GetString(result.ToString()));

				if (invoiceNumber == -1)
					throw new Exception(Language.GetString("InvalidInvoiceNumber"));

				SettleRequest(Pin);
			}
			catch
			{
				ReversalRequest();
			}
		}

		public void ReversalRequest()
		{
			try
			{
				byte status = (int)Status.PreRequest;
				service.PinReversal(Pin, OrderID, OrderID, ref status);

				foreach (Status result in Enum.GetValues(typeof(Status)))
					if (Helper.GetInt(status) == (int)result && result != (int)Status.Successful)
						throw new Exception(Language.GetString(result.ToString()));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void SettleRequest(string pinCode)
		{
			try
			{
				byte status = (int)Status.PreRequest;
				service.PinSettlement(pinCode, ref status);

				foreach (Status result in Enum.GetValues(typeof(Status)))
					if (status == (int)result && result != Status.Successful)
						throw new Exception(Language.GetString(result.ToString()));
			}
			catch
			{
				ReversalRequest();
			}
		}
	}
}