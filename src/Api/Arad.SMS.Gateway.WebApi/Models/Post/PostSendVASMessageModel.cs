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
using System.Data;
using System.Xml.Serialization;

namespace Arad.SMS.Gateway.WebApi.Models
{
	[XmlRoot(ElementName = "VAS")]
	public class PostSendVASMessageModel
	{
		private Guid batchId;
		private string smsText;
		private int smsLen;
		private bool isUnicode;
		private string receiver;
		private string senderId;
		private string serviceId;
		private int groupId;
		private InProgressSms inProgressSms;
		private List<InProgressSms> lstInProgressReceiver = new List<InProgressSms>();
		private Guid privateNumberGuid;
		private Guid referenceGuid;

		public PostSendVASMessageModel()
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

		public string Receiver
		{
			get
			{
				return receiver;
			}
			set
			{
				if (Helper.IsCellPhone(value) != 0)
				{
					inProgressSms = new InProgressSms();
					inProgressSms.RecipientNumber = value;
					lstInProgressReceiver.Add(inProgressSms);
					receiver = value;
				}
				else
					receiver = value;
			}
		}

		public List<InProgressSms> InProgressSmsList
		{
			get
			{
				return lstInProgressReceiver;
			}
		}

		public string SenderId
		{
			get
			{
				return senderId;
			}
		}

		[Required(ErrorMessage = "Enter Service ID")]
		public string ServiceId
		{
			get
			{
				return serviceId;
			}
			set
			{
				DataTable dtInfo = Facade.PrivateNumber.GetVASNumber(value);
				if (dtInfo.Rows.Count > 0)
				{
					senderId = dtInfo.Rows[0]["Number"].ToString();
					privateNumberGuid = Helper.GetGuid(dtInfo.Rows[0]["Guid"]);
				}
				serviceId = value;
			}
		}

		[Required(ErrorMessage = "Enter VAS group ID")]
		[Range(1000, Double.MaxValue, ErrorMessage = "Invalid VAS group ID")]
		public int GroupId
		{
			get
			{
				return groupId;
			}
			set
			{
				var principal = System.Threading.Thread.CurrentPrincipal;
				if (principal.Identity.IsAuthenticated)
				{
					Common.User userInfo = ((MyPrincipal)principal).UserDetails;
					referenceGuid = Facade.PhoneBook.GetUserVasGroup(Helper.GetInt(value), serviceId, userInfo.UserGuid);
				}
				groupId = value;
			}
		}

		public bool IsUnicode
		{
			get
			{
				return isUnicode;
			}
		}

		public Guid PrivateNumberGuid
		{
			get
			{
				return privateNumberGuid;
			}
		}

		public Guid ReferenceGuid
		{
			get
			{
				return referenceGuid;
			}
		}
	}
}
