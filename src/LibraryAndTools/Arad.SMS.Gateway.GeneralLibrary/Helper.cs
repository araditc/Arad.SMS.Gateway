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

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace Arad.SMS.Gateway.GeneralLibrary
{
	public class Helper
	{
		private static Random rnd = new Random();

		#region Convertors
		public static Guid GetGuid(object obj)
		{
			try
			{
				return new Guid(obj.ToString());
			}
			catch
			{
				return Guid.Empty;
			}
		}

		public static decimal GetDecimal(object obj)
		{
			try
			{
				return decimal.Parse(obj.ToString().Replace(",", string.Empty));
			}
			catch
			{
				return 0;
			}
		}

		public static decimal GetDecimal(object obj, decimal defaultValue)
		{
			try
			{
				return decimal.Parse(obj.ToString().Replace(",", string.Empty));
			}
			catch
			{
				return defaultValue;
			}
		}

		public static int GetInt(object obj)
		{
			try
			{
				return int.Parse(ConvertNumbers(obj.ToString().Replace(",", string.Empty)));
			}
			catch
			{
				return 0;
			}
		}

		public static byte GetByte(object obj)
		{
			try
			{
				return byte.Parse(obj.ToString().Replace(",", string.Empty));
			}
			catch
			{
				return 0;
			}
		}

		public static int GetInt(object obj, int defaultValue)
		{
			try
			{
				return int.Parse(obj.ToString().Replace(",", string.Empty));
			}
			catch
			{
				return defaultValue;
			}
		}

		public static long GetLong(object obj)
		{
			try
			{
				return long.Parse(obj.ToString().Replace(",", string.Empty));
			}
			catch
			{
				return 0;
			}
		}

		public static float GetFloat(object obj)
		{
			try
			{
				return float.Parse(obj.ToString().Replace(",", string.Empty));
			}
			catch
			{
				return 0;
			}
		}

		public static bool GetBool(object obj)
		{
			try
			{
				string b = obj.ToString().ToLower();
				return b == "true" || b == "1" || b == "on" || b == "yes" || b == "ok";
			}
			catch
			{
				return false;
			}
		}

		public static object GetDateTimeForDB(DateTime date)
		{
			if (date == DateTime.MinValue)
				return DBNull.Value;

			return string.Format("{0}-{1}-{2} {3}:{4}:{5}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
		}

		public static string GetString(object obj)
		{
			string ret;
			if (obj == null)
				ret = string.Empty;
			else
				ret = obj.ToString();

			return ret;
		}

		public static DateTime GetDateTime(object obj)
		{
			try
			{
				return DateTime.Parse(obj.ToString());
			}
			catch
			{
				return DateTime.MinValue;
			}
		}
		#endregion

		#region Request
		public static string Request(System.Web.HttpRequest requestObject, string paramKey)
		{
			return Request(requestObject, paramKey, string.Empty);
		}
		public static string Request(System.Web.HttpRequest requestObject, string paramKey, string defaultValue)
		{
			string ret;

			ret = requestObject[paramKey] == null ? string.Empty : requestObject[paramKey].ToString();

			if (ret == string.Empty)
			{
				try
				{
					int paramIndex = -1;
					string queryString = HttpUtility.UrlDecode(requestObject.UrlReferrer.Query);

					if (queryString != string.Empty)
					{

						if (queryString.IndexOf("&" + paramKey + "=") != -1)
							paramIndex = queryString.IndexOf("&" + paramKey + "=") + paramKey.Length + 2;

						if (paramIndex == -1 && queryString.IndexOf("?" + paramKey + "=") != -1)
							paramIndex = queryString.IndexOf("?" + paramKey + "=") + paramKey.Length + 2;

						if (paramIndex != -1)
						{
							queryString = queryString.Substring(paramIndex);
							int nextParamIndex = queryString.IndexOf('&');
							if (nextParamIndex != -1)
								ret = queryString.Substring(0, nextParamIndex);
							else
								ret = queryString;
						}
					}
				}
				catch
				{
					ret = string.Empty;
				}
			}

			ret = ret == string.Empty ? defaultValue : ret;
			return ret;
		}
		public static string Request(BaseCore.UIUserControlBase pageObject, string paramKey, string defaultValue)
		{
			return Request(pageObject.Request, paramKey, defaultValue);
		}
		public static string Request(System.Web.UI.Page pageObject, string paramKey, string defaultValue)
		{
			return Request(pageObject.Request, paramKey, defaultValue);
		}
		public static string Request(BaseCore.UIUserControlBase pageObject, string paramKey)
		{
			return Request(pageObject.Request, paramKey, string.Empty);
		}
		public static string Request(System.Web.UI.Page pageObject, string paramKey)
		{
			return Request(pageObject.Request, paramKey, string.Empty);
		}
		public static bool RequestBool(BaseCore.UIUserControlBase pageObject, string paramKey)
		{
			return GetBool(Request(pageObject, paramKey));
		}
		public static bool RequestBool(System.Web.UI.Page pageObject, string paramKey)
		{
			return GetBool(Request(pageObject, paramKey));
		}
		public static int RequestInt(BaseCore.UIUserControlBase pageObject, string paramKey)
		{
			return GetInt(Request(pageObject, paramKey));
		}
		public static int RequestInt(System.Web.UI.Page pageObject, string paramKey)
		{
			return GetInt(Request(pageObject, paramKey));
		}
		public static long RequestLong(System.Web.UI.Page pageObject, string paramKey)
		{
			return GetLong(Request(pageObject, paramKey));
		}
		public static Guid RequestGuid(System.Web.UI.Page pageObject, string paramKey)
		{
			try
			{
				return GetGuid(Request(pageObject, paramKey));
			}
			catch
			{
				return Guid.Empty;
			}
		}
		public static Guid RequestGuid(GeneralLibrary.BaseCore.UIUserControlBase pageObject, string paramKey)
		{
			return GetGuid(Request(pageObject, paramKey));
		}
		public static string RequestEncrypted(GeneralLibrary.BaseCore.UIUserControlBase pageObject, string paramKey)
		{
			return RequestEncrypted(pageObject, paramKey, string.Empty);
		}
		public static string RequestEncrypted(System.Web.UI.Page pageObject, string paramKey)
		{
			return RequestEncrypted(pageObject, paramKey, string.Empty);
		}
		public static string RequestEncrypted(GeneralLibrary.BaseCore.UIUserControlBase pageObject, string paramKey, string defaultValue)
		{
			string encValue = Request(pageObject, paramKey);
			try
			{
				return Decrypt(encValue, pageObject.Session);
			}

			catch
			{
				if (GetInt(encValue) < 0)
					return encValue;
				else
					return defaultValue;
			}
		}
		public static string RequestEncrypted(System.Web.UI.Page pageObject, string paramKey, string defaultValue)
		{
			string encValue = Request(pageObject, paramKey);
			try
			{
				return Decrypt(encValue, pageObject.Session);
			}

			catch
			{
				if (GetInt(encValue) < 0)
					return encValue;
				else
					return defaultValue;
			}
		}
		public static int RequestEncryptedInt(GeneralLibrary.BaseCore.UIUserControlBase pageObject, string paramKey)
		{
			return GetInt(RequestEncrypted(pageObject, paramKey, "0"));
		}
		public static int RequestEncryptedInt(System.Web.UI.Page pageObject, string paramKey)
		{
			return GetInt(RequestEncrypted(pageObject, paramKey, "0"));
		}
		public static Guid RequestEncryptedGuid(GeneralLibrary.BaseCore.UIUserControlBase pageObject, string paramKey)
		{
			return GetGuid(RequestEncrypted(pageObject, paramKey, Guid.Empty.ToString()));
		}
		public static Guid RequestEncryptedGuid(System.Web.UI.Page pageObject, string paramKey)
		{
			return GetGuid(RequestEncrypted(pageObject, paramKey, Guid.Empty.ToString()));
		}
		#endregion

		#region Encryption
		public static string ByteArrayToHex(byte[] byteArray)
		{
			string result = string.Empty;
			if (byteArray == null)
				return string.Empty;
			for (int i = 0; i < byteArray.Length; i++)
				result += ((byteArray[i] < 16) ? "0" : string.Empty) + byteArray[i].ToString("x");

			return result;
		}

		public static byte[] HexToByteArray(string hexString)
		{
			if (hexString.IndexOf("0x") == 0 || hexString.IndexOf("0X") == 0)
				return HexToByteArray(hexString, (hexString.Length - 2) / 2);
			else
				return HexToByteArray(hexString, hexString.Length / 2);
		}

		public static byte[] HexToByteArray(string hexString, int length)
		{
			byte[] byteArray = new byte[length];
			if ((hexString.Length % 2) != 0)
				return null;
			if (hexString.IndexOf("0x") == 0 || hexString.IndexOf("0X") == 0)
				hexString = hexString.Substring(2);
			string iValue = string.Empty;
			string hex = string.Empty;
			for (int i = 0; i < hexString.Length; i += 2)
			{
				hex = hexString.Substring(i, 2);
				iValue = int.Parse(hex, System.Globalization.NumberStyles.HexNumber).ToString();
				byteArray[(int)Math.Floor((double)i / 2)] = byte.Parse(iValue);
			}
			return byteArray;
		}

		public static string Encrypt(object source, System.Web.SessionState.HttpSessionState sessionObject)
		{
			CheckEncryptorSession(sessionObject);
			return ((CryptorEngine)sessionObject["encryptor"]).Encrypt(source.ToString());
		}

		private static void CheckEncryptorSession(System.Web.SessionState.HttpSessionState sessionObject)
		{
			if (sessionObject["encryptor"] == null)
				sessionObject["encryptor"] = new CryptorEngine();
		}

		public static string Decrypt(string encryptedText, System.Web.SessionState.HttpSessionState sessionObject)
		{
			CheckEncryptorSession(sessionObject);
			return ((CryptorEngine)sessionObject["encryptor"]).Decrypt(encryptedText);
		}

		public static int DecryptInt(string encryptedText, System.Web.SessionState.HttpSessionState sessionObject)
		{
			return GetInt(((CryptorEngine)sessionObject["encryptor"]).Decrypt(encryptedText));
		}

		public static Guid DecryptGuid(string encryptedText, System.Web.SessionState.HttpSessionState sessionObject)
		{
			return GetGuid(((CryptorEngine)sessionObject["encryptor"]).Decrypt(encryptedText));
		}
		#endregion

		public static bool IsUserAccess(Guid AccessGuid, System.Web.SessionState.HttpSessionState sessionObject)
		{
			if (sessionObject["UserGuid"] != null && GetGuid(sessionObject["UserGuid"]) == Guid.Empty)
				return false;

			return true;
		}

		public static CheckDataConditionsResult CheckDataConditions(object value)
		{
			CheckDataConditionsResult result = new CheckDataConditionsResult();

			if (value.ToString() == string.Empty)
			{
				result.IsEmpty = true;
			}
			else
			{
				//Check Email
				string patern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
																	@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
																	@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
				if (Regex.IsMatch(value.ToString(), patern))
					result.IsEmail = true;

				//Check Decimal Number
				patern = @"^[-+]?[0-9]+(\.[0-9][0-9]?)?";
				if (Regex.IsMatch(value.ToString(), patern))
					result.IsIntNumber = true;

				//Check Integer Number
				patern = @"^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?$";
				if (Regex.IsMatch(value.ToString(), patern))
					result.IsDecimalNumber = true;
			}

			return result;
		}

		public static Guid ImportGuidData(object sourceString, string tag)
		{
			return GetGuid(ImportData(sourceString, tag));
		}

		public static decimal ImportDecimalData(object sourceString, string tag, decimal defaultValue = 0)
		{
			return GetDecimal(ImportData(sourceString, tag), defaultValue);
		}

		public static int ImportIntData(object sourceString, string tag)
		{
			return GetInt(ImportData(sourceString, tag));
		}

		public static bool ImportBoolData(object sourceString, string tag)
		{
			return GetBool(ImportData(sourceString, tag));
		}

		public static string ImportData(object sourceString, string tag)
		{
			bool found;
			return ImportData(sourceString, tag, out found);
		}

		public static string ImportData(object sourceString, string tag, out bool found)
		{
			if (sourceString == null || tag == null)
			{
				found = false;
				return string.Empty;
			}

			Regex rx = new Regex(string.Format(@"(?<=([{2}]|^){0}{1}\()((\w+{1}\((.|\s)*?\){2})+|((.|\s)*?))(?=\){2})",
			tag.Replace("?", @"\?"), "{", "}"));

			string tagValue = rx.Match(sourceString.ToString()).Value;
			if (tagValue == string.Empty)
				found = false;
			else
				found = true;
			return tagValue;
		}

		public static void ShowPanel(GeneralLibrary.BaseCore.UIUserControlBase userControlObject, string panelID)
		{
			for (int counterControl = 0; counterControl < userControlObject.Controls.Count; counterControl++)
			{
				if (userControlObject.Controls[counterControl].GetType().Name == "Panel")
					userControlObject.Controls[counterControl].Visible = false;
			}
			userControlObject.FindControl(panelID).Visible = true;
		}

		public static void ClearTextBox(System.Web.UI.Control panelControlObject)
		{
			for (int counterTextBoxControl = 0; counterTextBoxControl < panelControlObject.Controls.Count; counterTextBoxControl++)
				if (panelControlObject.Controls[counterTextBoxControl].GetType().Name == "TextBox")
					((TextBox)panelControlObject.Controls[counterTextBoxControl]).Text = string.Empty;
		}

		public static string RandomString()
		{
			return RandomString(16);
		}

		public static string RandomString(int length)
		{
			return RandomString(length, true);
		}

		public static string RandomString(int length, bool onlyLowerCase)
		{
			StringBuilder builder = new StringBuilder();

			int start = onlyLowerCase ? 97 : 48;

			lock (rnd)
			{
				for (int i = 0; i < length; i++)
					builder.Append((char)rnd.Next(start, 122));
			}

			return builder.ToString();
		}

		public static string GetStandardizeCharacters(string inputString)
		{
			return GetStandardizeCharacters(inputString, "persian");
		}

		public static string GetStandardizeCharacters(string inputString, string persianKeyboardBehavior)
		{
			string outputString = inputString.Replace("'", "`").Replace("ی", "ي");
			// This is the third shape of charachter 'ی'.

			if (persianKeyboardBehavior == "arabic")
				outputString = outputString.Replace('ى', 'ي').Replace('ک', 'ك');
			else
				outputString = outputString.Replace('ي', 'ی').Replace('ك', 'ک');
			return outputString;
		}

		public static string FormatDecimalForDisplay(object decimalValue)
		{
			try
			{
				return decimal.Parse(decimalValue.ToString()).ToString("N").Replace(".00", string.Empty);
			}
			catch
			{
				return string.Empty;
			}
		}

		public static string FormatDecimalForDisplay(object decimalValue, int decimalDigitCount)
		{
			try
			{
				return decimal.Parse(decimalValue.ToString()).ToString("N" + decimalDigitCount.ToString()).Replace("." + Helper.ZeroPad("0", decimalDigitCount), string.Empty);
			}
			catch
			{
				return string.Empty;
			}
		}

		public static string ZeroPad(object number, int length)
		{
			string r = number.ToString();

			for (byte i = 0; i < length; i++)
				r = "0" + r;

			return r.Substring(r.Length - length);
		}

		public static string GetSerializeDateForSearchBox(System.Data.DataTable info, string textFieldName, string valueFieldName)
		{
			string result = string.Empty;

			foreach (System.Data.DataRow row in info.Rows)
				result += string.Format("{0}|{1}\n", GetString(row[textFieldName]), GetString(row[valueFieldName]));

			return result.TrimEnd('\n');
		}

		public static bool HasUniCodeCharacter(string text)
		{
			return Regex.IsMatch(text, "[^\u0000-\u00ff]");
		}

		public static int GetSmsCount(string text)
		{
			int standardSmsLen = 160;
			int standardUdhLen = 7;

			if (Helper.HasUniCodeCharacter(text))
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

		public static string ByteArrayToString(byte[] byteArray)
		{
			if (byteArray == null)
				return string.Empty;

			UTF8Encoding encoding = new UTF8Encoding();

			return encoding.GetString(byteArray);
		}

		public static string GetStreamContent(System.IO.Stream stream)
		{
			byte[] byteArray;
			if (stream.ReadByte() == 239 && stream.ReadByte() == 187 && stream.ReadByte() == 191) //Removing the utf-8 first 3 characters if the stream is started with them since they are going to be added in ByteArrayToString method.
			{
				byteArray = new byte[stream.Length - 3];
			}
			else
			{
				byteArray = new byte[stream.Length];
				stream.Position = 0;
			}

			stream.Read(byteArray, 0, (int)byteArray.Length);

			return ByteArrayToString(byteArray);
		}

		public static string GetTempFileName()
		{
			return GetTempFileName("tmp");
		}

		public static string GetTempFileName(string extension)
		{
			return string.Format("{0}\\tempFile_{1}_{2}.{3}", System.IO.Path.GetTempPath().TrimEnd(System.IO.Path.DirectorySeparatorChar), DateTime.Now.Ticks, Guid.NewGuid(), extension);
		}

		public static string GetTempDirectory()
		{
			return System.IO.Path.GetTempPath().TrimEnd(System.IO.Path.DirectorySeparatorChar);
		}

		public static T[] GetPartOfArray<T>(T[] array, int startIndex)
		{
			return GetPartOfArray<T>(array, startIndex, array.Length - startIndex);
		}

		public static T[] GetPartOfArray<T>(T[] array, int startIndex, int lenght)
		{
			if (lenght >= array.Length)
				return array;
			else
			{
				T[] newArray = new T[lenght];
				int counter = 0;

				for (int i = startIndex; i < startIndex + lenght && i < array.Length; i++)
					newArray[counter++] = array[i];

				return newArray;
			}
		}

		public static T[] GetPartOfArrayWithCast<T>(object[] array, int startIndex, int lenght)
		{
			T[] newArray = new T[lenght < array.Length ? lenght : array.Length];
			int counter = 0;

			for (int i = startIndex; i < startIndex + lenght && i < array.Length; i++)
				newArray[counter++] = (T)array[i];

			return newArray;
		}

		public static bool CheckLicense()
		{
			//if (License.Status.GetHardwareID(true, true, false, false) == License.Status.License_HardwareID)
			return true;
			//else
			//{
			//  System.Web.HttpContext.Current.Response.Redirect("http://yahoo.com");
			//  return false;
			//}
		}

		public static string GetLocalPrivateNumber(string number)
		{
			if (number.StartsWith("098") || number.StartsWith("+98"))
				return number.Substring(3);
			else if (number.StartsWith("0098"))
				return number.Substring(4);
			else if (number.StartsWith("98"))
				return number.Substring(2);
			else
				return number;
		}

		public static string GetLocalMobileNumber(string cellPhone)
		{
            //if (cellPhone.StartsWith("09"))
            //    cellPhone = "98" + cellPhone.Substring(1);
            //else if (cellPhone.StartsWith("+98"))
            //    cellPhone = cellPhone.Substring(1);
            //else if (cellPhone.StartsWith("0098"))
            //    cellPhone = cellPhone.Substring(2);
            //else if (cellPhone.StartsWith("98"))
            //    cellPhone = cellPhone.Substring(0);
            //else if (cellPhone.StartsWith("9"))
            //    cellPhone = "98" + cellPhone;
            //return cellPhone;
            return GetInternationalMobileNumber(cellPhone);
		}

        public static string GetInternationalMobileNumber(string cellPhone)
        {
            CheckingCellPhone(ref cellPhone);
            return cellPhone;
        }

		public static bool CheckingCellPhone(ref string cellPhone)
		{
			bool isValid = IsCellPhone(cellPhone) > 0;
            // if local 
			if (isValid && IsCellPhone(cellPhone) <= 6)
			{
                if (cellPhone.StartsWith("09"))
                    cellPhone = "98" + cellPhone.Substring(1);
                else if (cellPhone.StartsWith("+98"))
                    cellPhone = cellPhone.Substring(1);
                else if (cellPhone.StartsWith("0098"))
                    cellPhone = cellPhone.Substring(2);
                else if (cellPhone.StartsWith("98"))
                    cellPhone = cellPhone.Substring(0);
                else if (cellPhone.StartsWith("9"))
                    cellPhone = "98" + cellPhone;
                //cellPhone =  GetLocalMobileNumber(cellPhone);
			}
            // if international
            else if (isValid && IsCellPhone(cellPhone) > 6)
            {
                cellPhone = cellPhone.Replace("+" , "");
                cellPhone = cellPhone.Replace("00", "");
                cellPhone = "00" + cellPhone;
            }
			else
				cellPhone = string.Empty;

			return isValid;
		}

		public static int IsCellPhone(string cellPhone)
		{
            if (Regex.IsMatch(cellPhone, "(^(09|9|989|00989|\\+989)[1|9][0-9]\\d{7}$)"))
                return 1;//MCI
            else if (Regex.IsMatch(cellPhone, "(^(09|9|989|00989|\\+989)(01|02|03|04|05|30|33|35|36|37|38|39)[0-9]\\d{6}$)"))
                return 2;//MTN
            else if (Regex.IsMatch(cellPhone, "(^(09|9|989|00989|\\+989)[2](0|1|2)[0-9]\\d{6}$)")) //(^(09|9|989|00989|\\+989)[2][0-2][0-9]\\d{6}$)
                return 3;//Rightel
            else if (Regex.IsMatch(cellPhone, "(^(09|9|989|00989|\\+989)[3][2][0-9]\\d{6}$)"))
                return 4;//Taliya
            else if (Regex.IsMatch(cellPhone, "(^(09|9|989|00989|\\+989)[3][1][0-9]\\d{6}$)"))
                return 5;//MTCE
            else if (Regex.IsMatch(cellPhone, "(^(09|9|989|00989|\\+989)[3][4][0-9]\\d{6}$)"))
                return 6;//Kish-TCI
            else if (Regex.IsMatch(cellPhone, "^(((\\+44|44\\s?\\d{4}|\\(?0\\d{4}\\)?)\\s?\\d{3}\\s?\\d{3})|((\\+44|44\\s?\\d{3}|\\(?0\\d{3}\\)?)\\s?\\d{3}\\s?\\d{4})|((\\+44|44\\s?\\d{2}|\\(?0\\d{2}\\)?)\\s?\\d{4}\\s?\\d{4}))(\\s?\\#(\\d{4}|\\d{3}))?$"))
                return 7;//UK
            else if (Regex.IsMatch(cellPhone, "(^(\\+357|00357|357)\\d{2}\\d{6}$)"))
                return 8; // Cyprus
            else if (Regex.IsMatch(cellPhone, "^(00234|234|\\+234)(90|70|80|81)[0-9]\\d{7}$"))
                return 9; // Nigeria  
            else if (Regex.IsMatch(cellPhone, "^(0091|\\+91|91)?[789]\\d{9}$"))
                return 10; // India
            else if (Regex.IsMatch(cellPhone, "^(009665|9665|\\+9665|05|5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$"))
                return 11; // Saudi Arabia
            else if (Regex.IsMatch(cellPhone, "^(00971|971|\\+971)?(?:50|51|52|55|56|2|3|4|6|7|9)\\d{7}$"))
                return 12; // UAE
            else if (Regex.IsMatch(cellPhone, "^(00973|973|\\+973)?\\d{8}$"))
                return 13; // Bahrain
            else if (Regex.IsMatch(cellPhone, "^(00965|965|\\+965)?[569]\\d{7}$"))
                return 14; // Kuwait
            else if (Regex.IsMatch(cellPhone, "^(00968|968|\\+968)?(?:9)\\d{8}$"))
                return 15; // Oman
            else if (Regex.IsMatch(cellPhone, "^(00964|964|\\+964)\\d{3}\\d{7}$"))
                return 16; // Iraq
            else if (Regex.IsMatch(cellPhone, "^(0092|\\+92|92)?[0][\\d]{3}-[\\d]{7}$"))
                return 17; // Pakistan
            else if (Regex.IsMatch(cellPhone, "^(?:\\+?86)?1(?:3\\d{3}|5[^4\\D]\\d{2}|8\\d{3}|7(?:[01356789]\\d{2}|4(?:0\\d|1[0-2]|9\\d))|9[189]\\d{2}|6[567]\\d{2}|4[579]\\d{2})\\d{6}$"))
                return 18; // China
            else if (Regex.IsMatch(cellPhone, "^(961|00961|\\+961(3|70|71)|(03|70|71))\\d{6}$"))
                return 19; // Lebanon
            else if (Regex.IsMatch(cellPhone, "^(00374|374|\\+374)(47|97|77|91|92|93|94|99|10)[0-9]\\d{6}$"))
                return 20; // Armenia
            else if (Regex.IsMatch(cellPhone, "^((\\+7|7|8)+([0-9]){10})$"))
                return 21; // Russia
            else if (Regex.IsMatch(cellPhone, "1?\\W*([2-9][0-8][0-9])\\W*([2-9][0-9]{2})\\W*([0-9]{4})(\\se?x?t?(\\d*))?"))
                return 22; // US & Canada
            else if (Regex.IsMatch(cellPhone, "^(00955|955|\\+955)(55|57|58|77|93)[0-9]\\d{6}$"))
                return 23; // Georgia
            else if (Regex.IsMatch(cellPhone, "^(00992|992|\\+992)(505|915|917|927|935|951|962|973|981|998|918|919)[0-9]\\d{5}$"))
                return 24; // Tajikistan
            else if (Regex.IsMatch(cellPhone, "^(0090|90|\\+90)(500|501|502|503|504|505|506|507|508|509|530|531|532|533|534|535|536|537|538|539|540|541|542|543|544|545|546|547|548|549|550|551|552|553|554|555|556|557|558|559)[0-9]\\d{7}$"))
                return 25; // Turkey
            else if (Regex.IsMatch(cellPhone, "^(00998|998|\\+998)(90|91|92|93|94|97|98)[0-9]\\d{7}$"))
                return 26; // Uzbekistan
            else if (Regex.IsMatch(cellPhone, "^(00996|996|\\+996)(66)[0-9]\\d{6}$"))
                return 27; // Turkmenistan
            //else if (Regex.IsMatch(cellPhone, "^(00937|\\+937|937)[0-9]{0,11}$|7[0-9]{0,9}$"))
                //return 28; // Afghanistan


            else
                return 0;//InValid
		}

		public static string GetFileExtension(string fileName)
		{
			try
			{
				return Path.GetExtension(fileName);
			}
			catch
			{
				return string.Empty;
			}
		}

		public static bool CheckFileExtension(string fileName, string[] extensions)
		{
			bool result = false;
			try
			{
				foreach (string extension in extensions)
					if (Path.GetExtension(fileName).ToLower() == extension.ToLower())
					{
						result = true;
						break;
					}
				return result;
			}
			catch
			{
				return false;
			}
		}

		public static bool CheckDnsOfEmail(string email)
		{
			try
			{
				string[] host = (email.Split('@'));
				string hostname = host[1];

				IPHostEntry hostInfo = Dns.GetHostEntry(hostname);
				IPAddress[] address = hostInfo.AddressList;

				return address.Length > 0 ? true : false;
			}
			catch
			{
				return false;
			}

		}

		public static string GetHostOfDomain(string domain)
		{
			if (domain.StartsWith("www."))
				domain = domain.Remove(0, 4);
			if (domain.StartsWith("http://www."))
				domain = domain.Remove(0, 11);
			if (domain.LastIndexOf('.') > 0)
				domain.Substring(0, domain.LastIndexOf('.'));
            if(domain.StartsWith("localhost"))
                domain = "localhost";
            return domain;
		}

		public static string GetDomain(string domain)
		{
			if (domain.StartsWith("www."))
				domain = domain.Remove(0, 4);
			if (domain.StartsWith("http://www."))
				domain = domain.Remove(0, 11);
			//if (domain.IndexOf(':') != -1)
			//	domain = domain.Split(':')[0];
			return domain;
		}

		public static string GetLocalDomain(string domain)
		{
			domain = domain.ToLower();
			if (domain.StartsWith("www."))
				domain = domain.Remove(0, 4);
			if (domain.StartsWith("http://www."))
				domain = domain.Remove(0, 11);
			return domain;
		}

		public static string GetCompleteDomain(string domain)
		{
			if (domain.StartsWith("http://www."))
				return domain;
			else if (domain.StartsWith("www."))
				domain = "http://" + domain;
			else
				domain = "http://www." + domain;
			return domain;
		}

		public static string GetUrlWithoutPortNumber(string url)
		{
			string urlWithoutPort = string.Empty;
			if (url.StartsWith("http://www."))
			{
				int indexOfEndDomain = url.IndexOf("/", 11);
				bool hasPort = url.Substring(11, indexOfEndDomain).IndexOf(':') > 0 ? true : false;
				urlWithoutPort = hasPort ? url.Substring(0, 11) + url.Substring(11, url.Substring(11, indexOfEndDomain).IndexOf(':')) + url.Substring(indexOfEndDomain) : url;
			}
			else if (url.StartsWith("http://"))
			{
				int indexOfEndDomain = url.IndexOf("/", 7);
				bool hasPort = url.Substring(7, indexOfEndDomain).IndexOf(':') > 0 ? true : false;
				urlWithoutPort = hasPort ? url.Substring(0, 7) + url.Substring(7, url.Substring(7, indexOfEndDomain).IndexOf(':')) + url.Substring(indexOfEndDomain) : url;
			}
			else
			{
				int indexOfEndDomain = url.IndexOf('/');
				bool hasPort = url.Substring(0, indexOfEndDomain).IndexOf(':') > 0 ? true : false;
				urlWithoutPort = hasPort ? url.Substring(0, url.Substring(0, indexOfEndDomain).IndexOf(':')) + url.Substring(indexOfEndDomain) : url;
			}

			return urlWithoutPort;
		}

		public static long GetSeconds(string timeConvertingStatus, object value)
		{
			long result = 0;
			try
			{
				switch (timeConvertingStatus.ToLower())
				{
					case "yearly":
						result = GetLong(value) * 365 * 24 * 60 * 60;     //	Period * 365Days * 24Hours * 60Minutes * 60Seconds
						break;
					case "monthly":
						result = GetLong(value) * 30 * 24 * 60 * 60;      //	Period * 30Days * 24Hours * 60Minutes * 60Seconds
						break;
					case "weekly":
						result = GetLong(value) * 7 * 24 * 60 * 60;      //		Period * 7Days * 24Hours * 60Minutes * 60Seconds
						break;
					case "daily":
						result = GetLong(value) * 24 * 60 * 60;         //		Period * 24Hours * 60Minutes * 60Seconds
						break;
					case "hour":
						result = GetLong(value) * 60 * 60;              //		Period * 60Minutes * 60Seconds
						break;
					case "minute":
						result = GetLong(value) * 60;                   //		Period * 60Seconds
						break;
				}
			}
			catch { }
			return result;
		}

		public static DateTime AddDate(string datePart, int countNumber, DateTime sourceDateTime)
		{
			try
			{
				switch (datePart.ToLower())
				{
					case "yearly":
						sourceDateTime = sourceDateTime.AddYears(countNumber);
						break;
					case "monthly":
						sourceDateTime = sourceDateTime.AddMonths(countNumber);
						break;
					case "weekly":
						sourceDateTime = sourceDateTime.AddDays(countNumber * 7);
						break;
					case "daily":
						sourceDateTime = sourceDateTime.AddDays(countNumber);
						break;
					case "hour":
						sourceDateTime = sourceDateTime.AddHours(countNumber);
						break;
					case "minute":
						sourceDateTime = sourceDateTime.AddMinutes(countNumber);
						break;
				}
			}
			catch
			{
				return DateTime.MinValue;
			}
			return sourceDateTime;
		}

		public static bool CheckServiceAvailable(string url)
		{
			try
			{
				WebClient client = new WebClient();
				var request = (HttpWebRequest)WebRequest.Create(url);
				var response = (HttpWebResponse)request.GetResponse();
				if (response.StatusCode == HttpStatusCode.OK)
					return true;
				else
					return false;
			}
			catch (Exception ex)
			{
				//LogController<>.LogInFile(string.Format("{0} ===>> Error : {1}", "CheckServiceAvailable", ex.Message));
				return false;
			}
		}

		public static string Encrypt(string value)
		{
			CryptorEngine crytor = new CryptorEngine(ConfigurationManager.GetSetting("SecurityIV"), ConfigurationManager.GetSetting("SecurityKey"));
			return crytor.Encrypt(value);
		}

		public static string Decrypt(string value)
		{
			CryptorEngine crytor = new CryptorEngine(ConfigurationManager.GetSetting("SecurityIV"), ConfigurationManager.GetSetting("SecurityKey"));
			return crytor.Decrypt(value);
		}

		public static DataTable DeSerializeXml(string XmlDoc)
		{
			DataSet dataSetSaveFile = new DataSet();
			XmlDocument xmlDoc = new XmlDocument();
			MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(XmlDoc));
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlDocument));
			StreamReader streamReader = new StreamReader(memoryStream);
			xmlDoc = (XmlDocument)xmlSerializer.Deserialize(streamReader);
			dataSetSaveFile.ReadXml(XmlReader.Create(new StringReader(xmlDoc.InnerXml)));
			memoryStream.Close();
			streamReader.Close();
			return dataSetSaveFile.Tables[0];
		}

		private static Byte[] StringToUTF8ByteArray(string xmlDoc)
		{
			System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
			byte[] byteArray = encoding.GetBytes(xmlDoc);
			return byteArray;
		}

		public static TimeSpan GetTimeSpan(object obj)
		{
			try
			{
				return TimeSpan.Parse(obj.ToString());
			}
			catch
			{
				return TimeSpan.Parse("0");
			}
		}

		public static string GetMd5Hash(string inputString)
		{
			var md5Hash = MD5.Create();
			var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));
			var sBuilder = new StringBuilder();

			for (int i = 0; i < data.Length; i++)
				sBuilder.Append(data[i].ToString("x2"));

			return sBuilder.ToString();
		}

		public static string GenerateQueryFromToolbarFilters(string toolbarFilters)
		{
			string query = string.Empty;
			if (string.IsNullOrEmpty(toolbarFilters))
				return query;

			JArray array = JArray.Parse(toolbarFilters);
			foreach (JObject content in array.Children<JObject>())
				query += string.Format("{0} ", CaseSearchOperator(content.Property("field").Value.ToString(), content.Property("op").Value.ToString(), content.Property("data").Value.ToString()));

			query = query.Trim();

			if (query.EndsWith("AND"))
				query = query.Substring(0, query.Length - 3);

			return query;
		}

		private static string CaseSearchOperator(string field, string opt, string data)
		{
			string result = string.Empty;
			DateTime christianDateTime;
			if (string.IsNullOrEmpty(data))
				return string.Empty;

			if (DateManager.PersianDateIsValid(data, out christianDateTime))
			{
				int timePosition = data.IndexOf(" ");
				if (timePosition != -1)
				{
					data = DateManager.GetChristianDateTime(christianDateTime);
				}
				else
				{
					field = string.Format("CONVERT(DATE,{0})", field);
					data = christianDateTime.ToShortDateString();
				}
			}

			switch (opt)
			{
				case "eq":
					result = string.Format("{0} = N'{1}'", field, data);
					break;
				case "ne":
					result = string.Format("{0} <> N'{1}'", field, data);
					break;
				case "lt":
					result = string.Format("{0} < N'{1}'", field, data);
					break;
				case "le":
					result = string.Format("{0} <= N'{1}'", field, data);
					break;
				case "gt":
					result = string.Format("{0} > N'{1}'", field, data);
					break;
				case "ge":
					result = string.Format("{0} >= N'{1}'", field, data);
					break;
				case "bw":
					result = string.Format("{0} LIKE N'{1}%'", field, data);
					break;
				case "bn":
					result = string.Format("{0} NOT LIKE N'{1}%'", field, data);
					break;
				case "ew":
					result = string.Format("{0} LIKE N'%{1}'", field, data);
					break;
				case "en":
					result = string.Format("{0} NOT LIKE N'%{1}'", field, data);
					break;
				case "cn":
					result = string.Format("{0} LIKE N'%{1}%'", field, data);
					break;
				case "nc":
					result = string.Format("{0} NOT LIKE N'%{1}%'", field, data);
					break;
				default:
					return string.Empty;
			}
			return string.Format("{0} AND", result);
		}

		public static void CheckEmailsList(ref List<string> lstEmails, ref List<string> lstFailedEmails)
		{
			lstFailedEmails = lstEmails.Where(email => !CheckDataConditions(email).IsEmail).ToList();
			lstEmails = lstEmails.Where(email => CheckDataConditions(email).IsEmail).ToList();
		}

		public static string ConvertNumbers(string str)
		{
			decimal[] persianDigitsUnicode = { 1632, 1633, 1634, 1635, 1636, 1637, 1638, 1639, 1640, 1641 };
			decimal[] arabicDigitsUnicode = { 1776, 1777, 1778, 1779, 1780, 1781, 1782, 1783, 1784, 1785 };
			decimal[] englishDigitsUnicode = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 };

			for (int x = 0; x <= 9; x++)
			{
				str = str.ToCharArray().Contains((char)persianDigitsUnicode[x]) ? str.Replace((char)persianDigitsUnicode[x], (char)englishDigitsUnicode[x]) : str;
				str = str.ToCharArray().Contains((char)arabicDigitsUnicode[x]) ? str.Replace((char)arabicDigitsUnicode[x], (char)englishDigitsUnicode[x]) : str;
			}
			return str;
		}

		public static bool IsValidNationalCode(String nationalCode, ref string error)
		{
			try
			{
				if (String.IsNullOrEmpty(nationalCode))
					throw new Exception(Language.GetString("CheckEmptyNationalCodeInput"));

				if (nationalCode.Length != 10)
					throw new Exception(Language.GetString("FailNationalCodeInput"));

				var regex = new Regex(@"\d{10}");
				if (!regex.IsMatch(nationalCode))
					throw new Exception(Language.GetString("FailNationalCodeInput"));

				var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
				if (allDigitEqual.Contains(nationalCode)) return false;

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

				return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
			}
			catch (Exception ex)
			{
				error = ex.Message;
				return false;
			}
		}

		public static string Base64Encode(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}

		public static string Base64Decode(string base64EncodedData)
		{
			var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(base64EncodedBytes);
		}

        public static string PersianNumberToEnglish(string text)
        {
            try
            {
                return text.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
            }
            catch
            {
                return text;
            }
        }

        public static string ConvertNumEn2Fa(string text)
        {
            string result = string.Empty; foreach (char c in text.ToCharArray())
            {
                switch (c)
                {
                    case '0': result += "٠"; break;
                    case '1': result += "١"; break;
                    case '2': result += "٢"; break;
                    case '3': result += "٣"; break;
                    case '4': result += "۴"; break;
                    case '5': result += "۵"; break;
                    case '6': result += "۶"; break;
                    case '7': result += "٧"; break;
                    case '8': result += "٨"; break;
                    case '9': result += "٩"; break;
                    default: result += c; break;

                }
            }
            return result;
        }

        public static string ConvertNumFa2En(string text)
        {
            string result = string.Empty; foreach (char c in text.ToCharArray())
            {
                switch (c)
                {
                    case '٠': result += "0"; break;
                    case '١': result += "1"; break;
                    case '٢': result += "2"; break;
                    case '٣': result += "3"; break;
                    case '۴': result += "4"; break;
                    case '۵': result += "5"; break;
                    case '۶': result += "6"; break;
                    case '٧': result += "7"; break;
                    case '٨': result += "8"; break;
                    case '٩': result += "9"; break;
                    default: result += c; break;

                }
            }
            return result;
        }
        public static string EncodeBase64(string data)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }
    }
}
