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

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Arad.SMS.Gateway.SqlLibrary
{
	public class SQLHelper
	{
		private static Random random = new Random();
		public static int HasUniCodeCharacter(string text)
		{
			return Convert.ToInt32(Regex.IsMatch(text, "[^\u0000-\u00ff]"));
		}

		public static int GetSmsCount(string text)
		{
			int standardSmsLen = 160;
			int standardUdhLen = 7;

			if (HasUniCodeCharacter(text) == 1)
			{
				standardSmsLen = 70;
				standardUdhLen = 4;
			}

			double smsLen = text.Replace("\r\n", "\n").Length;
			double smsCount = 0;

			if (smsLen > standardSmsLen)
				smsCount = Math.Ceiling(smsLen / (standardSmsLen - standardUdhLen));
			else
				smsCount = Math.Ceiling(smsLen / standardSmsLen);

			return (int)smsCount;
		}

		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static int GetMobileOperator(string mobile)
		{
			//if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[1][0-9]\\d{7}$)"))
			//	return 1;//MCI
			//else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)(01|02|30|33|35|36|37|38|39)[0-9]\\d{6}$)"))
			//	return 2;//MTN
			//else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[2][0-1][0-9]\\d{6}$)"))
			//	return 3;//Rightel
			//else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[3][2][0-9]\\d{6}$)"))
			//	return 4;//Taliya
			//else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[3][1][0-9]\\d{6}$)"))
			//	return 5;//MTCE
			//else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[3][4][0-9]\\d{6}$)"))
			//	return 6;//Kish-TCI
			//else
			//	return 0;

            if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[1|9][0-9]\\d{7}$)"))
                return 1;//MCI
            else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)(01|02|03|04|05|30|33|35|36|37|38|39)[0-9]\\d{6}$)"))
                return 2;//MTN
            else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[2](0|1|2)[0-9]\\d{6}$)")) //(^(09|9|989|00989|\\+989)[2][0-2][0-9]\\d{6}$)
                return 3;//Rightel
            else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[3][2][0-9]\\d{6}$)"))
                return 4;//Taliya
            else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[3][1][0-9]\\d{6}$)"))
                return 5;//MTCE
            else if (Regex.IsMatch(mobile, "(^(09|9|989|00989|\\+989)[3][4][0-9]\\d{6}$)"))
                return 6;//Kish-TCI
            else if (Regex.IsMatch(mobile, "^(((\\+44|44\\s?\\d{4}|\\(?0\\d{4}\\)?)\\s?\\d{3}\\s?\\d{3})|((\\+44|44\\s?\\d{3}|\\(?0\\d{3}\\)?)\\s?\\d{3}\\s?\\d{4})|((\\+44|44\\s?\\d{2}|\\(?0\\d{2}\\)?)\\s?\\d{4}\\s?\\d{4}))(\\s?\\#(\\d{4}|\\d{3}))?$"))
                return 7;//UK
            else if (Regex.IsMatch(mobile, "(^(\\+357|00357|357)\\d{2}\\d{6}$)"))
                return 8; // Cyprus
            else if (Regex.IsMatch(mobile, "^(00234|234|\\+234)(90|70|80|81)[0-9]\\d{7}$"))
                return 9; // Nigeria  
            else if (Regex.IsMatch(mobile, "^(0091|\\+91|91)?[789]\\d{9}$"))
                return 10; // India
            else if (Regex.IsMatch(mobile, "^(009665|9665|\\+9665|05|5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$"))
                return 11; // Saudi Arabia
            else if (Regex.IsMatch(mobile, "^(00971|971|\\+971)?(?:50|51|52|55|56|2|3|4|6|7|9)\\d{7}$"))
                return 12; // UAE
            else if (Regex.IsMatch(mobile, "^(00973|973|\\+973)?\\d{8}$"))
                return 13; // Bahrain
            else if (Regex.IsMatch(mobile, "^(00965|965|\\+965)?[569]\\d{7}$"))
                return 14; // Kuwait
            else if (Regex.IsMatch(mobile, "^(00968|968|\\+968)?(?:9)\\d{8}$"))
                return 15; // Oman
            else if (Regex.IsMatch(mobile, "^(00964|964|\\+964)\\d{3}\\d{7}$"))
                return 16; // Iraq
            else if (Regex.IsMatch(mobile, "^(0092|\\+92|92)?[0][\\d]{3}-[\\d]{7}$"))
                return 17; // Pakistan
            else if (Regex.IsMatch(mobile, "^(?:\\+?86)?1(?:3\\d{3}|5[^4\\D]\\d{2}|8\\d{3}|7(?:[01356789]\\d{2}|4(?:0\\d|1[0-2]|9\\d))|9[189]\\d{2}|6[567]\\d{2}|4[579]\\d{2})\\d{6}$"))
                return 18; // China
            else if (Regex.IsMatch(mobile, "^(961|00961|\\+961(3|70|71)|(03|70|71))\\d{6}$"))
                return 19; // Lebanon
            else if (Regex.IsMatch(mobile, "^(00374|374|\\+374)(47|97|77|91|92|93|94|99|10)[0-9]\\d{6}$"))
                return 20; // Armenia
            else if (Regex.IsMatch(mobile, "^((\\+7|7|8)+([0-9]){10})$"))
                return 21; // Russia
            else if (Regex.IsMatch(mobile, "1?\\W*([2-9][0-8][0-9])\\W*([2-9][0-9]{2})\\W*([0-9]{4})(\\se?x?t?(\\d*))?"))
                return 22; // US & Canada
            else if (Regex.IsMatch(mobile, "^(00955|955|\\+955)(55|57|58|77|93)[0-9]\\d{6}$"))
                return 23; // Georgia
            else if (Regex.IsMatch(mobile, "^(00992|992|\\+992)(505|915|917|927|935|951|962|973|981|998|918|919)[0-9]\\d{5}$"))
                return 24; // Tajikistan
            else if (Regex.IsMatch(mobile, "^(0090|90|\\+90)(500|501|502|503|504|505|506|507|508|509|530|531|532|533|534|535|536|537|538|539|540|541|542|543|544|545|546|547|548|549|550|551|552|553|554|555|556|557|558|559)[0-9]\\d{7}$"))
                return 25; // Turkey
            else if (Regex.IsMatch(mobile, "^(00998|998|\\+998)(90|91|92|93|94|97|98)[0-9]\\d{7}$"))
                return 26; // Uzbekistan
            else if (Regex.IsMatch(mobile, "^(00996|996|\\+996)(66)[0-9]\\d{6}$"))
                return 27; // Turkmenistan
                           //else if (Regex.IsMatch(cellPhone, "^(00937|\\+937|937)[0-9]{0,11}$|7[0-9]{0,9}$"))
                           //return 28; // Afghanistan


            else
                return 0;//InValid



        }

		public static string SendRequestToUrl(string url)
		{
			try
			{
				using (var wb = new WebClient())
				{
					var response = wb.DownloadString(url);
					return response;
				}
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
		}

		public static int IsMatch(string input, string pattern)
		{
			try
			{
				return Regex.IsMatch(input, pattern) ? 1 : 0;
			}
			catch
			{
				return 0;
			}
		}

		[SqlFunction(DataAccess = DataAccessKind.Read)]
		public static string SendSms(string queue, int isRemoteQueue, string remoteQueueIP, int smsSendType,
																 int pageNo, string sender, Guid privateNumberGuid, int totalCount, string receivers,
																 string serviceID, string message, int smsLen, int tryCount,
																 long smsIdentifier, int smsPartIndex, int isFlash,
																 int isUnicode, string id, string guid, string username,
																 string password, string domain, string sendLink,string receiveLink,
																 string deliveryLink, int agentReference)
		{
			try
			{
				string messageId = Guid.Empty.ToString();
				InProgressSms inProgressSms;

				BatchMessage batch = new BatchMessage();
				batch.CheckId = random.Next().ToString();
				batch.QueueName = queue;
				batch.SmsSendType = smsSendType;
				batch.SenderNumber = sender;
				batch.PrivateNumberGuid = privateNumberGuid;
				batch.ServiceId = serviceID;
				batch.SmsText = message;
				batch.SmsLen = smsLen;
				batch.MaximumTryCount = tryCount;
				batch.SmsIdentifier = smsIdentifier;
				batch.SmsPartIndex = smsPartIndex;
				batch.IsFlash = isFlash == 1 ? true : false;
				batch.IsUnicode = isUnicode == 1 ? true : false;
				batch.Id = long.Parse(id);
				batch.Guid = Guid.Parse(guid);
				batch.Username = username;
				batch.Password = password;
				batch.Domain = domain;
				batch.SendLink = sendLink;
				batch.ReceiveLink = receiveLink;
				batch.DeliveryLink = deliveryLink;
				batch.SmsSenderAgentReference = agentReference;
				batch.PageNo = pageNo;
				batch.TotalCount = totalCount;

				DataSet dtsReceivers = new DataSet();
				dtsReceivers.ReadXml(new StringReader(receivers));

				List<InProgressSms> lstInProgressSms = new List<InProgressSms>();
				foreach (DataRow row in dtsReceivers.Tables[0].Rows)
				{
					inProgressSms = new InProgressSms();

					inProgressSms.SendTryCount = 0;
					inProgressSms.RecipientNumber = row["Mobile"].ToString();
					inProgressSms.OperatorType = int.Parse(row["Operator"].ToString());
					inProgressSms.IsBlackList = int.Parse(row["IsBlackList"].ToString());
					inProgressSms.SendStatus = 2;//WatingForSend
					inProgressSms.DeliveryStatus = 12;//IsSending
					inProgressSms.ReturnID = string.Empty;
					inProgressSms.CheckID = random.Next().ToString();
					inProgressSms.SaveToDatabase = false;

					lstInProgressSms.Add(inProgressSms);
				}

				batch.Receivers = lstInProgressSms.ToList();

				if (isRemoteQueue == 0)
					messageId = ManageQueue.SendMessage(queue, batch, string.Format("{0}-{1}", id, pageNo));
				else if (isRemoteQueue == 1)
					messageId = ManageQueue.SendMessage(queue, remoteQueueIP, batch, string.Format("{0}-{1}", id, pageNo));

				return messageId.Split('\\')[1];
			}
			catch (Exception ex)
			{
				return string.Format("Message:{0},StackTrace:{1}", ex.Message, ex.StackTrace);
			}
		}


		public static string JoinString(string str, string separator, string columnName)
		{
			try
			{
				DataSet dtsReceivers = new DataSet();
				dtsReceivers.ReadXml(new StringReader(str));
				List<string> lstData = new List<string>();
				foreach (DataRow row in dtsReceivers.Tables[0].Rows)
					lstData.Add(row[columnName].ToString());

				return string.Join(separator, lstData);
			}
			catch (Exception ex)
			{
				return string.Format("Message:{0},StackTrace:{1}", ex.Message, ex.StackTrace);
			}
		}

		[SqlFunction(DataAccess = DataAccessKind.Read)]
		public static string InsertLog(int type, string source, string name, string text, string ip, string browser, Guid referenceGuid, Guid userGuid)
		{
			try
			{
				Log log = new Log();

				log.Type = type;
				log.Source = source;
				log.Name = name;
				log.Text = text;
				log.IPAddress = ip;
				log.Browser = browser;
				log.ReferenceGuid = referenceGuid;
				log.UserGuid = userGuid;
				log.CreateDate = DateTime.Now;

				return ManageQueue.SendMessage("log", log, string.Format("{0}-{1}", source, name));
			}
			catch (Exception ex)
			{
				return string.Format("Message:{0},StackTrace:{1}", ex.Message, ex.StackTrace);
			}
		}

		public static void InsertLogInfo(Log log)
		{
			try
			{
				ManageQueue.SendMessage("log", log, string.Format("{0}-{1}", log.Source, log.Name));
			}
			catch { }
		}

		public static int IsValidNationalCode(String nationalCode)
		{
			try
			{
				if (String.IsNullOrEmpty(nationalCode))
					throw new Exception();

				if (nationalCode.Length != 10)
					throw new Exception();

				var regex = new Regex(@"\d{10}");
				if (!regex.IsMatch(nationalCode))
					throw new Exception();

				var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
				if (allDigitEqual.Contains(nationalCode)) return 0;

				var chArray = nationalCode.ToCharArray();
				var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
				var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
				var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
				var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
				var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
				var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
				var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
				var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
				var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
				var a = Convert.ToInt32(chArray[9].ToString());

				var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
				var c = b % 11;

				return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a))) ? 1 : 0;
			}
			catch
			{
				return 0;
			}
		}
	}
}
