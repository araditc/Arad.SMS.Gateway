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

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Xml;
using System.Text;
using System.Net;
using System.IO;

namespace Arad.SMS.Gateway.RahyabRGSmsSender.ServiceManager
{
    public class Cls_SMS
    {
        public class ClsSend
        {
            public class STC_SMSSend
            {
                public string SourceAddress;
                public string DestAddress;
                public string Status;
                public string Response;
                public string SMSID;
                public STC_SMSSend(string sourceAddress, string destAddress, string status, string response, string smsid)
                {
                    this.SourceAddress = sourceAddress;
                    this.DestAddress = destAddress;
                    this.Status = status;
                    this.Response = response;
                    this.SMSID = smsid;
                }
            };
            private static readonly object syncLock = new object();
            public string Validate_Number(string Number)
            {
                string ret = Number.Trim();
                if (ret.Substring(0, 4) == "0098")
                {
                    ret = ret.Remove(0, 4);
                }
                if (ret.Substring(0, 3) == "098")
                {
                    ret = ret.Remove(0, 3);
                }
                if (ret.Substring(0, 3) == "+98")
                {
                    ret = ret.Remove(0, 3);
                }
                if (ret.Substring(0, 2) == "98")
                {
                    ret = ret.Remove(0, 2);
                }
                if (ret.Substring(0, 1) == "0")
                {
                    ret = ret.Remove(0, 1);
                }
                return "+98" + ret;
            }
            public string Validate_Message(ref string Message, bool IsPersian)
            {
                char cr = (char) 13;
                Message = Message.Replace(cr.ToString(), string.Empty);

                if (Message.EndsWith(Environment.NewLine))
                {
                    Message = Message.TrimEnd(Environment.NewLine.ToCharArray());
                }
                if (IsPersian)
                {
                    return C2Unicode(Message);
                }
                else
                {
                    return Message;
                }
            }
            public string C2Unicode(string Message)
            {
                int i;
                int preUnicode_Number;
                string preUnicode;
                string ret = "";
                string strHex = "";
                for (i = 0; i < Message.Length; i++)
                {
                    preUnicode_Number = 4 - string.Format("{0:X}", (int) (Message.Substring(i, 1)[0])).Length;
                    preUnicode = string.Format("{0:D" + preUnicode_Number.ToString() + "}", 0);
                    strHex = preUnicode + string.Format("{0:X}", (int) (Message.Substring(i, 1)[0]));
                    if (strHex.Length == 4)
                        ret += strHex;
                }
                return ret;
            }
            public static void FindTxtLanguageAndcount(string unicodeString, ref bool IsPersian, ref int SMSCount)
            {
                unicodeString = unicodeString.Replace("\r\n", "a");
                IsPersian = FindTxtLanguage(unicodeString);
                decimal msgCount = 0;
                int strLength = unicodeString.Length;
                if (IsPersian == true && strLength <= 70)
                    msgCount = 1;
                else if (IsPersian == true && strLength > 70)
                    msgCount = Convert.ToInt32(Math.Ceiling(strLength / 67.0));
                else if (IsPersian == false && strLength <= 160)
                    msgCount = 1;
                else if (IsPersian == false && strLength > 160)
                    msgCount = Convert.ToInt32(Math.Ceiling(strLength / 157.0));

                SMSCount = Convert.ToInt16(msgCount);
            }
            public static bool FindTxtLanguage(string unicodeString)
            {
                const int MaxAnsiCode = 255;
                bool IsPersian = true;
                if (unicodeString != string.Empty)
                    IsPersian = unicodeString.ToCharArray().Any(c => c > MaxAnsiCode);
                else
                    IsPersian = true;
                return IsPersian;
            }
            public string[] SendSMS_Single(string Message, string DestinationAddress, string Number, string userName, string password, string IP_Send, string Company, bool IsFlash)
            {
                string rawMessage = Message;
                string strIsPersian;
                string Identity = string.Empty;
                string[] RetValue = new string[2];
                RetValue[0] = "False";
                RetValue[1] = "0";
                bool IsPersian = FindTxtLanguage(Message);
                Validate_Message(ref Message, IsPersian);
                if (IsPersian)
                {
                    Message = C2Unicode(Message);
                    strIsPersian = "true";
                }
                else
                {
                    strIsPersian = "false";
                }

                lock (syncLock)
                {
                    try
                    {
                        Random _Random = new Random();
                        Identity = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now) + string.Format("{0:000}", _Random.Next(1000));
                        string dcs = IsPersian ? "8" : "0";
                        string msgClass = IsFlash ? "0" : "1";
                        StringBuilder _StringBuilder = new StringBuilder();
                        _StringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<!DOCTYPE smsBatch PUBLIC \"-//PERVASIVE//DTD CPAS 1.0//EN\" \"http://www.ubicomp.ir/dtd/Cpas.dtd\">");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<smsBatch company=\"" + Company + "\" batchID=\"" + Company + "+" + Identity + "\">");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<sms msgClass=\"" + msgClass + "\" binary=\"" + strIsPersian + "\" dcs=\"" + dcs + "\"" + ">");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<destAddr><![CDATA[" + Validate_Number(DestinationAddress) + "]]></destAddr>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<origAddr><![CDATA[" + Validate_Number(Number) + "]]></origAddr>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<message><![CDATA[" + Message + "]]></message>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("</sms>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("</smsBatch>");

                        string dataToPost = _StringBuilder.ToString();
                        byte[] buf = System.Text.UTF8Encoding.UTF8.GetBytes(_StringBuilder.ToString());
                        WebRequest objWebRequest = WebRequest.Create(IP_Send);
                        objWebRequest.Method = "POST";
                        objWebRequest.ContentType = "text/xml";
                        byte[] byt = System.Text.Encoding.UTF8.GetBytes(userName + ":" + password);
                        objWebRequest.Headers.Add("authorization", "Basic " + Convert.ToBase64String(byt));
                        Stream _Stream = objWebRequest.GetRequestStream();
                        StreamWriter _StreamWriter = new StreamWriter(_Stream);
                        _StreamWriter.Write(dataToPost);
                        _StreamWriter.Flush();
                        _StreamWriter.Close();
                        _Stream.Close();

                        WebResponse objWebResponse = objWebRequest.GetResponse();
                        Stream objResponseStream = objWebResponse.GetResponseStream();
                        StreamReader objStreamReader = new StreamReader(objResponseStream);
                        string dataToReceive = objStreamReader.ReadToEnd();
                        objStreamReader.Close();
                        objResponseStream.Close();
                        objWebResponse.Close();

                        if (dataToReceive.IndexOf("CHECK_OK") > 0)
                        {
                            RetValue[0] = "CHECK_OK";
                            RetValue[1] = Identity;
                            string[] Tonumber = new string[1];
                            Tonumber[0] = DestinationAddress;
                        }
                        else
                        {
                            try
                            {
                                string msg;
                                int firstIndex = dataToReceive.IndexOf("CDATA[");
                                int LastIndex = dataToReceive.IndexOf("]");
                                msg = dataToReceive.Substring(firstIndex, LastIndex - firstIndex);
                                RetValue[1] = msg;
                                return RetValue;
                            }
                            catch
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return RetValue;
                    }
                    return RetValue;
                }
            }
            public string[] SendSMS_Batch(string Message, string[] DestinationAddress, string Number, string userName, string password, string IP_Send, string Company, bool IsFlash)
            {
                int sentItems = 0;
                int smsPart = 0;
                int counter = 0;
                string[] RetValue = new string[2];
                RetValue[0] = "False";
                RetValue[1] = "0";
                bool IsPersian = false;
                FindTxtLanguageAndcount(Message, ref IsPersian, ref smsPart);

                if (DestinationAddress.Length * smsPart > 100)
                {
                    int batchLength = (int) (100 / smsPart);
                    int batchCount = (int) (DestinationAddress.Length / batchLength);
                    int batchIndex = 0;
                    string[] remainDestinationAddress = new string[DestinationAddress.Length - (batchCount * batchLength)];
                    string[] batchDestinationAddress = new string[batchLength];
                    for (int i = 0; i < batchCount; i++)
                    {
                        for (int j = 0; j < batchLength; j++)
                        {
                            batchDestinationAddress[j] = DestinationAddress[batchIndex + j];
                            counter += 1;
                        }

                        RetValue = SendSMS_Batch_Devided(Message, batchDestinationAddress, Number, userName, password, IP_Send, Company, IsPersian, IsFlash);
                        batchIndex += (batchLength);

                        if (RetValue[0] == "CHECK_OK")
                        {
                            sentItems += batchLength;
                        }

                    }
                    int diff = DestinationAddress.Length - counter;
                    for (int k = 0; k < diff; k++)
                    {
                        remainDestinationAddress[k] = DestinationAddress[counter + k];

                    }

                    RetValue = SendSMS_Batch_Devided(Message, remainDestinationAddress, Number, userName, password, IP_Send, Company, IsPersian, IsFlash);
                    if (RetValue[0] == "CHECK_OK")
                    {
                        sentItems += diff;
                    }
                    if ((sentItems * 1.0 / DestinationAddress.Length) > 0.9)
                    {
                        RetValue[0] = "CHECK_OK";
                    }
                }
                else
                {
                    RetValue = SendSMS_Batch_Devided(Message, DestinationAddress, Number, userName, password, IP_Send, Company, IsPersian, IsFlash);
                }

                return RetValue;
            }
            public string[] SendSMS_Batch_Devided(string Message, string[] DestinationAddress, string Number, string userName, string password, string IP_Send, string Company, bool IsPersian, bool IsFlash)
            {
                string rawMessage = Message;
                string strIsPersian;
                string Identity = string.Empty;
                string[] RetValue = new string[2];
                RetValue[0] = "False";
                RetValue[1] = "0";
                Validate_Message(ref Message, IsPersian);
                if (IsPersian)
                {
                    Message = C2Unicode(Message);
                    strIsPersian = "true";
                }
                else
                {
                    strIsPersian = "false";
                }

                lock (syncLock)
                {
                    try
                    {
                        Random _Random = new Random(Guid.NewGuid().GetHashCode());
                        Identity = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now) + string.Format("{0:000}", _Random.Next(1000));
                        string dcs = IsPersian ? "8" : "0";
                        string msgClass = IsFlash ? "0" : "1";
                        StringBuilder _StringBuilder = new StringBuilder();
                        _StringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<!DOCTYPE smsBatch PUBLIC \"-//PERVASIVE//DTD CPAS 1.0//EN\" \"http://www.ubicomp.ir/dtd/Cpas.dtd\">");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<smsBatch company=\"" + Company + "\" batchID=\"" + Company + "+" + Identity + "\">");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<sms msgClass=\"" + msgClass + "\" binary=\"" + strIsPersian + "\" dcs=\"" + dcs + "\"" + ">");
                        _StringBuilder.Append(Environment.NewLine);

                        for (int i = 0; i < DestinationAddress.Length; i++)
                        {
                            _StringBuilder.Append("<destAddr><![CDATA[" + Validate_Number(DestinationAddress[i]) + "]]></destAddr>");
                            _StringBuilder.Append(Environment.NewLine);
                        }

                        _StringBuilder.Append("<origAddr><![CDATA[" + Validate_Number(Number) + "]]></origAddr>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<message><![CDATA[" + Message + "]]></message>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("</sms>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("</smsBatch>");

                        string dataToPost = _StringBuilder.ToString();

                        byte[] buf = System.Text.UTF8Encoding.UTF8.GetBytes(_StringBuilder.ToString());
                        WebRequest objWebRequest = WebRequest.Create(IP_Send);
                        objWebRequest.Method = "POST";
                        objWebRequest.ContentType = "text/xml";
                        byte[] byt = System.Text.Encoding.UTF8.GetBytes(userName + ":" + password);
                        objWebRequest.Headers.Add("authorization", "Basic " + Convert.ToBase64String(byt));
                        Stream _Stream = objWebRequest.GetRequestStream();
                        StreamWriter _StreamWriter = new StreamWriter(_Stream);
                        _StreamWriter.Write(dataToPost);
                        _StreamWriter.Flush();
                        _StreamWriter.Close();
                        _Stream.Close();

                        WebResponse objWebResponse = objWebRequest.GetResponse();
                        Stream objResponseStream = objWebResponse.GetResponseStream();
                        StreamReader objStreamReader = new StreamReader(objResponseStream);
                        string dataToReceive = objStreamReader.ReadToEnd();
                        objStreamReader.Close();
                        objResponseStream.Close();
                        objWebResponse.Close();

                        if (dataToReceive.IndexOf("CHECK_OK") > 0)
                        {
                            RetValue[0] = "CHECK_OK";
                            RetValue[1] = Identity;
                        }
                        else
                        {
                            try
                            {
                                string msg;
                                int firstIndex = dataToReceive.IndexOf("CDATA[");
                                int LastIndex = dataToReceive.IndexOf("]");
                                msg = dataToReceive.Substring(firstIndex, LastIndex - firstIndex);
                                RetValue[1] = msg;
                                return RetValue;
                            }
                            catch
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return RetValue;
                    }
                }
                return RetValue;
            }
            public ArrayList SendSMS_Batch_Full(string Message, string[] DestinationAddress, string Number, string userName, string password, string IP_Send, string Company, bool IsFlash, ref string SendStatus, ref string BatchID, ref string Amount, ref string chargingAmount)
            {
                int sentItems = 0;
                int smsPart = 0;
                int counter = 0;
                ArrayList Arr_Res = new ArrayList();
                bool IsPersian = false;

                FindTxtLanguageAndcount(Message, ref IsPersian, ref smsPart);

                if (DestinationAddress.Length * smsPart > 100)
                {
                    int batchLength = (int) (100 / smsPart);
                    int batchCount = (int) (DestinationAddress.Length / batchLength);
                    int batchIndex = 0;
                    string[] remainDestinationAddress = new string[DestinationAddress.Length - (batchCount * batchLength)];
                    string[] batchDestinationAddress = new string[batchLength];
                    for (int i = 0; i < batchCount; i++)
                    {
                        for (int j = 0; j < batchLength; j++)
                        {
                            batchDestinationAddress[j] = DestinationAddress[batchIndex + j];
                            counter += 1;
                        }

                        Arr_Res = SendSMS_Batch_Devided_Full(Message, batchDestinationAddress, Number, userName, password, IP_Send, Company, IsPersian, IsFlash, ref SendStatus, ref BatchID, ref Amount, ref chargingAmount);
                        batchIndex += (batchLength);
                        if (SendStatus == "CHECK_OK")
                        {
                            sentItems += batchLength;
                        }
                    }
                    int diff = DestinationAddress.Length - counter;
                    for (int k = 0; k < diff; k++)
                    {
                        remainDestinationAddress[k] = DestinationAddress[counter + k];
                    }

                    Arr_Res = SendSMS_Batch_Devided_Full(Message, remainDestinationAddress, Number, userName, password, IP_Send, Company, IsPersian, IsFlash, ref SendStatus, ref BatchID, ref Amount, ref chargingAmount);
                    if (SendStatus == "CHECK_OK")
                        sentItems += diff;
                    if ((sentItems * 1.0 / DestinationAddress.Length) > 0.9)
                        SendStatus = "CHECK_OK";
                }
                else
                {
                    Arr_Res = SendSMS_Batch_Devided_Full(Message, DestinationAddress, Number, userName, password, IP_Send, Company, IsPersian, IsFlash, ref SendStatus, ref BatchID, ref Amount, ref chargingAmount);
                }

                return Arr_Res;
            }
            public ArrayList SendSMS_Batch_Devided_Full(string Message, string[] DestinationAddress, string Number, string userName, string password, string IP_Send, string Company, bool IsPersian, bool IsFlash, ref string SendStatus, ref string BatchID, ref string Amount, ref string chargingAmount)
            {
                string rawMessage = Message;
                string strIsPersian;
                string Identity = string.Empty;
                ArrayList Arr_Res = new ArrayList();
                Validate_Message(ref Message, IsPersian);
                if (IsPersian)
                {
                    Message = C2Unicode(Message);
                    strIsPersian = "true";
                }
                else
                {
                    strIsPersian = "false";
                }

                lock (syncLock)
                {
                    try
                    {
                        Random _Random = new Random(Guid.NewGuid().GetHashCode());
                        Identity = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now) + string.Format("{0:000}", _Random.Next(1000));
                        string dcs = IsPersian ? "8" : "0";
                        string msgClass = IsFlash ? "0" : "1";
                        string f = "Full";
                        StringBuilder _StringBuilder = new StringBuilder();
                        _StringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<!DOCTYPE smsBatch PUBLIC \"-//PERVASIVE//DTD CPAS 1.0//EN\" \"http://www.ubicomp.ir/dtd/Cpas.dtd\">");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<smsBatch ackType=\"" + f + "\" company=\"" + Company + "\" batchID=\"" + Company + "+" + Identity + "\">");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<sms msgClass=\"" + msgClass + "\" binary=\"" + strIsPersian + "\" dcs=\"" + dcs + "\"" + ">");
                        _StringBuilder.Append(Environment.NewLine);

                        for (int i = 0; i < DestinationAddress.Length; i++)
                        {
                            _StringBuilder.Append("<destAddr><![CDATA[" + Validate_Number(DestinationAddress[i]) + "]]></destAddr>");
                            _StringBuilder.Append(Environment.NewLine);
                        }

                        _StringBuilder.Append("<origAddr><![CDATA[" + Validate_Number(Number) + "]]></origAddr>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<message><![CDATA[" + Message + "]]></message>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("</sms>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("</smsBatch>");
                        string dataToPost = _StringBuilder.ToString();

                        byte[] buf = System.Text.UTF8Encoding.UTF8.GetBytes(_StringBuilder.ToString());
                        WebRequest objWebRequest = WebRequest.Create(IP_Send);
                        objWebRequest.Method = "POST";
                        objWebRequest.ContentType = "text/xml";
                        byte[] byt = System.Text.Encoding.UTF8.GetBytes(userName + ":" + password);
                        objWebRequest.Headers.Add("authorization", "Basic " + Convert.ToBase64String(byt));
                        Stream _Stream = objWebRequest.GetRequestStream();
                        StreamWriter _StreamWriter = new StreamWriter(_Stream);
                        _StreamWriter.Write(dataToPost);
                        _StreamWriter.Flush();
                        _StreamWriter.Close();
                        _Stream.Close();

                        WebResponse objWebResponse = objWebRequest.GetResponse();
                        Stream objResponseStream = objWebResponse.GetResponseStream();
                        StreamReader objStreamReader = new StreamReader(objResponseStream);
                        string dataToReceive = objStreamReader.ReadToEnd();
                        objStreamReader.Close();
                        objResponseStream.Close();
                        objWebResponse.Close();

                        XmlDocument XmlReader = new XmlDocument();
                        XmlReader.XmlResolver = null;
                        XmlReader.InnerXml = dataToReceive;

                        var ndRoot = XmlReader.DocumentElement;

                        if (ndRoot.Name == "errorResponse")
                        {
                            string strError = ndRoot.ChildNodes[1].InnerText;
                            SendStatus = strError;
                        }
                        else
                        {
                            BatchID = ndRoot.GetAttribute("batchId");
                            string SourceAddress = "";
                            string DestAddress = "";
                            string Status = "";
                            string Response = "";
                            string SMSID = "";
                            if (ndRoot.HasChildNodes)
                            {
                                SendStatus = "CHECK_OK";
                                foreach (XmlNode ndResponse in ndRoot.ChildNodes)
                                {
                                    if (ndResponse.Name == "submitResponse")
                                    {
                                        foreach (XmlAttribute attResponse in ndResponse.Attributes)
                                        {
                                            string attName = attResponse.Name;
                                            switch (attName)
                                            {
                                                case "sourceAddress":
                                                    SourceAddress = attResponse.Value;
                                                    break;
                                                case "destAddress":
                                                    DestAddress = attResponse.Value;
                                                    break;
                                                case "status":
                                                    Status = attResponse.Value;
                                                    break;
                                                case "response":
                                                    Response = attResponse.Value;
                                                    break;
                                                case "smsId":
                                                    SMSID = attResponse.Value;
                                                    break;
                                            }
                                        }
                                        Arr_Res.Add(new STC_SMSSend(SourceAddress, DestAddress, Status, Response, SMSID));
                                    }

                                    if (ndResponse.Name == "chargingInfo")
                                    {
                                        XmlNode ndbalance = ndResponse.ChildNodes[0];
                                        Amount = ndbalance.Attributes["amount"].Value;
                                        chargingAmount = ndbalance.Attributes["chargingAmount"].Value;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Arr_Res;
                    }
                }
                return Arr_Res;
            }
            public string[] SendSMS_LikeToLike(string[] Message, string[] DestinationAddress, string Number, string userName, string password, string IP_Send, string Company)
            {
                string[] RetValue = new string[2];
                RetValue[0] = "False";
                RetValue[1] = "0";
                string Identity = string.Empty;

                if (Message.Length != DestinationAddress.Length)
                {
                    RetValue[1] = "Incorrect array size for Messages and Destinations";
                    return RetValue;
                }

                int smsSize = DestinationAddress.Length;
                lock (syncLock)
                {
                    try
                    {
                        Random _Random = new Random(Guid.NewGuid().GetHashCode());
                        Identity = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now) + string.Format("{0:000}", _Random.Next(1000));
                        StringBuilder _StringBuilder = new StringBuilder();
                        _StringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<!DOCTYPE smsBatch PUBLIC \"-//PERVASIVE//DTD CPAS 1.0//EN\" \"http://www.ubicomp.ir/dtd/Cpas.dtd\">");
                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("<smsBatch company=\"" + Company + "\" batchID=\"" + Company + "+" + Identity + "\">");
                        _StringBuilder.Append(Environment.NewLine);

                        for (int i = 0; i < smsSize; i++)
                        {
                            string strMessage = Message[i];
                            string strDestinationAddress = DestinationAddress[i];

                            string strIsPersian;
                            bool IsPersian = FindTxtLanguage(strMessage);
                            if (IsPersian)
                            {
                                strMessage = C2Unicode(strMessage);
                                strIsPersian = "true";
                            }
                            else
                                strIsPersian = "false";
                            string dcs = IsPersian ? "8" : "0";

                            _StringBuilder.Append("<sms binary=\"" + strIsPersian + "\" dcs=\"" + dcs + "\"" + ">");
                            _StringBuilder.Append(Environment.NewLine);
                            _StringBuilder.Append("<origAddr><![CDATA[" + Validate_Number(Number) + "]]></origAddr>");
                            _StringBuilder.Append(Environment.NewLine);
                            _StringBuilder.Append("<destAddr><![CDATA[" + Validate_Number(strDestinationAddress) + "]]></destAddr>");
                            _StringBuilder.Append(Environment.NewLine);
                            _StringBuilder.Append("<message><![CDATA[" + strMessage + "]]></message>");
                            _StringBuilder.Append(Environment.NewLine);
                            _StringBuilder.Append("</sms>");
                        }

                        _StringBuilder.Append(Environment.NewLine);
                        _StringBuilder.Append("</smsBatch>");

                        string dataToPost = _StringBuilder.ToString();
                        byte[] buf = System.Text.UTF8Encoding.UTF8.GetBytes(_StringBuilder.ToString());
                        WebRequest objWebRequest = WebRequest.Create(IP_Send);
                        objWebRequest.Method = "POST";
                        objWebRequest.ContentType = "text/xml";
                        byte[] byt = System.Text.Encoding.UTF8.GetBytes(userName + ":" + password);
                        objWebRequest.Headers.Add("authorization", "Basic " + Convert.ToBase64String(byt));
                        Stream _Stream = objWebRequest.GetRequestStream();
                        StreamWriter _StreamWriter = new StreamWriter(_Stream);
                        _StreamWriter.Write(dataToPost);
                        _StreamWriter.Flush();
                        _StreamWriter.Close();
                        _Stream.Close();

                        WebResponse objWebResponse = objWebRequest.GetResponse();
                        Stream objResponseStream = objWebResponse.GetResponseStream();
                        StreamReader objStreamReader = new StreamReader(objResponseStream);
                        string dataToReceive = objStreamReader.ReadToEnd();
                        objStreamReader.Close();
                        objResponseStream.Close();
                        objWebResponse.Close();

                        if (dataToReceive.IndexOf("CHECK_OK") > 0)
                        {
                            RetValue[0] = "CHECK_OK";
                            RetValue[1] = Identity;
                        }
                        else
                        {
                            try
                            {
                                string msg;
                                int firstIndex = dataToReceive.IndexOf("CDATA[");
                                int LastIndex = dataToReceive.IndexOf("]");
                                msg = dataToReceive.Substring(firstIndex, LastIndex - firstIndex);
                                RetValue[1] = msg;
                                return RetValue;
                            }
                            catch (Exception ex)
                            {
                                RetValue[1] = ex.Message;
                                return RetValue;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        RetValue[1] = ex.Message;
                        return RetValue;
                    }
                }
                return RetValue;
            }
        }
        public class ClsStatus
        {
            public class STC_SMSStatus
            {
                public string ReceiveNumber;
                public string DeliveryStatus;
                public string Time;
                public STC_SMSStatus(string number, string status, string time)
                {
                    this.ReceiveNumber = number;
                    this.DeliveryStatus = status;
                    this.Time = time;
                }
            };
            public ClsStatus()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            public ArrayList StatusSMS(string UserName, string Password, string IP_Send, string Company, string BatchId)
            {
                try
                {
                    string[] S = new string[1];
                    string StrXML = "";

                    StringBuilder objStringBuilder = new StringBuilder();
                    objStringBuilder.Append("<?xml version=\"1.0\"?>");
                    objStringBuilder.Append(Environment.NewLine);
                    objStringBuilder.Append("<!DOCTYPE smsStatusPoll PUBLIC \"-//PERVASIVE//DTD CPAS 1.0//EN\" \"http://www.pervasive.ir/dtd/Cpas.dtd\" []>");
                    objStringBuilder.Append(Environment.NewLine);
                    objStringBuilder.Append("<smsStatusPoll company=\"" + Company + "\">");
                    objStringBuilder.Append(Environment.NewLine);
                    objStringBuilder.Append("<batch batchID=\"" + BatchId + "\" />");
                    objStringBuilder.Append(Environment.NewLine);
                    objStringBuilder.Append("</smsStatusPoll>");

                    string dataToPost = objStringBuilder.ToString();
                    WebRequest objWebRequest = WebRequest.Create(IP_Send);
                    objWebRequest.Method = "POST";
                    objWebRequest.ContentType = "text/xml";

                    byte[] byt = System.Text.Encoding.UTF8.GetBytes(UserName + ":" + Password);
                    objWebRequest.Headers.Add("authorization", "Basic " + Convert.ToBase64String(byt));
                    Stream objStream = objWebRequest.GetRequestStream();
                    StreamWriter objStreamWriter = new StreamWriter(objStream);
                    objStreamWriter.Write(dataToPost);
                    objStreamWriter.Flush();
                    objStreamWriter.Close();
                    objStream.Close();

                    WebResponse objWebResponse = objWebRequest.GetResponse();
                    Stream objResponseStream = objWebResponse.GetResponseStream();
                    StreamReader objStreamReader = new StreamReader(objResponseStream);
                    StrXML = objStreamReader.ReadToEnd();
                    objStreamReader.Close();
                    objResponseStream.Close();
                    objWebResponse.Close();

                    string ParseXML = "";

                    if (StrXML.Substring(42, 10) != null)
                        if (StrXML.Substring(42, 10) == "standalone")
                            ParseXML = StrXML.Substring(0, 41) + "?>" + StrXML.Substring(169);
                        else
                            ParseXML = StrXML.Substring(0, 43) + StrXML.Substring(153);
                    else
                        ParseXML = StrXML.Substring(0, 43) + StrXML.Substring(153);

                    XmlDocument XmlReader = new XmlDocument();
                    XmlReader.XmlResolver = null;
                    XmlReader.InnerXml = ParseXML;

                    ArrayList Arr_Res = new ArrayList();
                    var ndRoot = XmlReader.DocumentElement;
                    int i = 0;
                    string Status = "";
                    string ReceiveNum = "";
                    string Time = "";

                    foreach (XmlNode ndBatch in ndRoot.ChildNodes)
                    {
                        foreach (XmlNode ndSMS in ndBatch.ChildNodes)
                        {
                            ReceiveNum = ndSMS.ChildNodes[0].InnerText;
                            Status = ndSMS.ChildNodes[1].InnerText;
                            Time = ndSMS.ChildNodes[2].InnerText;
                            Arr_Res.Add(new STC_SMSStatus(ReceiveNum, Status, Time));
                        }
                        i = i + 1;
                    }
                    return Arr_Res;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public string[] ReadXML(string txt, string number)
            {
                string[] StrRecivedMessage = new string[2];
                int i = 0;
                System.Xml.XmlReader objXMLReader = System.Xml.XmlReader.Create(new System.IO.StringReader(txt));

                string element = string.Empty;
                string strSMS = string.Empty;
                string strMessage = string.Empty;
                string strTmp = string.Empty;
                bool blnFindNo = false;
                try
                {
                    while (objXMLReader.Read())
                    {
                        switch (objXMLReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                element = objXMLReader.Name;
                                if (objXMLReader.HasAttributes)
                                {
                                    while (objXMLReader.MoveToNextAttribute())
                                    {
                                        if (element == "batch")
                                        {

                                        }
                                    }
                                }
                                break;

                            case XmlNodeType.Text:
                                if (element == "status")
                                {
                                    //i = i;
                                    StrRecivedMessage[i] = objXMLReader.Value;
                                }
                                if (element == "time")
                                {
                                    i = i + 1;
                                    StrRecivedMessage[i] = objXMLReader.Value;
                                    break;
                                }
                                break;

                            case XmlNodeType.CDATA:
                                if (element == "destAddr")
                                {

                                    if (objXMLReader.Value != "+98" + number.Substring(1))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        blnFindNo = true;
                                    }

                                    i = i + 1;
                                    StrRecivedMessage[i] = objXMLReader.Value;
                                }
                                if (element == "origAddr")
                                {
                                    i = i + 1;
                                    StrRecivedMessage[i] = objXMLReader.Value;
                                }
                                else if (element == "message")
                                {
                                    strTmp = objXMLReader.Value;
                                }
                                break;
                        }
                    };
                    return StrRecivedMessage;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            public string DecodeUCS2(string Content)
            {
                string hextext = Content;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hextext.Length; i += 4)
                {
                    string hs = hextext.Substring(i, 4);
                    sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
                }
                string ascii = sb.ToString();
                return ascii;
            }
            public int MyASC(string OneChar)
            {
                if ((OneChar == ""))
                {
                    return 0;
                }
                else
                {
                    return Microsoft.VisualBasic.Strings.Asc(OneChar);
                }

            }
        }
        public class ClsGetRemain
        {
            public ClsGetRemain()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            public string GetRemainCredit(string UserName, string Password, string Company, string IP_Send)
            {
                try
                {
                    string SMSBalance = "";

                    StringBuilder objStringBuilder = new StringBuilder();
                    objStringBuilder.Append("<?xml version=\"1.0\"?>");
                    objStringBuilder.Append(Environment.NewLine);
                    objStringBuilder.Append("<getUserBalance company=\"" + Company + "\" />");
                    string dataToPost = objStringBuilder.ToString();

                    WebRequest objWebRequest = WebRequest.Create(IP_Send);
                    objWebRequest.Method = "POST";
                    objWebRequest.ContentType = "text/xml";

                    byte[] byt = System.Text.Encoding.UTF8.GetBytes(UserName + ":" + Password);
                    objWebRequest.Headers.Add("authorization", "Basic " + Convert.ToBase64String(byt));
                    Stream objStream = objWebRequest.GetRequestStream();
                    StreamWriter objStreamWriter = new StreamWriter(objStream);
                    objStreamWriter.Write(dataToPost);
                    objStreamWriter.Flush();
                    objStreamWriter.Close();
                    objStream.Close();

                    WebResponse objWebResponse = objWebRequest.GetResponse();
                    Stream objResponseStream = objWebResponse.GetResponseStream();
                    StreamReader objStreamReader = new StreamReader(objResponseStream);
                    SMSBalance = objStreamReader.ReadToEnd();
                    objStreamReader.Close();
                    objResponseStream.Close();
                    objWebResponse.Close();

                    XmlDocument XmlReader = new XmlDocument();
                    XmlReader.XmlResolver = null;
                    XmlReader.InnerXml = SMSBalance;

                    var ndRoot = XmlReader.DocumentElement;

                    foreach (XmlNode ndBatch in ndRoot.ChildNodes)
                    {
                        SMSBalance = ndBatch.InnerText;
                        break;
                    }

                    return SMSBalance;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public int MyASC(string OneChar)
            {
                if ((OneChar == ""))
                {
                    return 0;
                }
                else
                {
                    return Microsoft.VisualBasic.Strings.Asc(OneChar);
                }

            }
        }
    }
}
