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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;

namespace Arad.SMS.Gateway.WebApi.Models
{
	[XmlRoot(ElementName = "Sms")]
	public class PostSendGroupSmsModel
	{
		private Guid batchId;
		private string phoneBooks;
		private string smsText;
		private int smsLen;
		private bool isUnicode;
		private List<Guid> lstPhoneBooks;

		public PostSendGroupSmsModel()
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

		public Guid CheckId{get;set;}

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

		[Required(ErrorMessage = "Enter PhoneBook")]
		public string PhoneBook
		{
			get
			{
				return phoneBooks;
			}
			set
			{
				phoneBooks = value;
				lstPhoneBooks = phoneBooks.Split(',').Select(Guid.Parse).ToList();
				lstPhoneBooks.RemoveAll(id => id == Guid.Empty);
			}
		}


		public List<Guid> ListOfPhoneBooks
		{
			get
			{
				return lstPhoneBooks;
			}
		}

		public string SenderId { get; set; }

		public bool IsUnicode
		{
			get
			{
				return isUnicode;
			}
		}

		public bool IsFlash { get; set; }

		public string UDH { get; set; }
	}
}
