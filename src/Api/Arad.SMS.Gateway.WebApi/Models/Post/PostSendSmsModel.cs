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

using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;

namespace Arad.SMS.Gateway.WebApi.Models
{
	[XmlRoot(ElementName = "Sms")]
	public class PostSendSmsModel
	{
		private Guid batchId;
		private string receivers;
		private string smsText;
		private int smsLen;
		private bool isUnicode;
		private List<string> lstReceivers;
		private List<InProgressSms> lstInProgressReceivers = new List<InProgressSms>();
		private InProgressSms inProgressSms;

		public PostSendSmsModel()
		{
			batchId = Guid.NewGuid();
		}

		public Guid BatchId
		{
			get
			{
				return batchId;
			}
		}

		public Guid CheckId
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Enter SMS Text")]
		public string SmsText
		{
			get
			{
				return smsText;
			}
			set
			{
				smsText = value;
				smsLen = Helper.GetSmsCount(SmsText);
				isUnicode = Helper.HasUniCodeCharacter(SmsText);
			}
		}

		public int SmsLen
		{
			get
			{
				return smsLen;
			}
		}

		[Required(ErrorMessage = "Enter Receivers List")]
		[MinLength(11, ErrorMessage = "Minimum Receiver Length should be 11 character")]
		public string Receivers
		{
			get
			{
				return receivers;
			}
			set
			{
				receivers = value;
				lstReceivers = receivers.Split(',').ToList();
				Facade.Outbox.GetCountNumberOfOperators(ref lstReceivers);
				foreach (string number in lstReceivers)
				{
					inProgressSms = new InProgressSms();
					inProgressSms.RecipientNumber = number;
					lstInProgressReceivers.Add(inProgressSms);
				}
			}
		}

		public List<string> ReceiverList
		{
			get
			{
				return lstReceivers;
			}
		}

		public List<InProgressSms> InProgressSmsList
		{
			get
			{
				return lstInProgressReceivers;
			}
		}

		public string SenderId
		{
			get;
			set;
		}

		public bool IsUnicode
		{
			get
			{
				return isUnicode;
			}
		}

		public bool IsFlash
		{
			get;
			set;
		}

		public string UDH
		{
			get;
			set;
		}
	}
}
