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

namespace Arad.SMS.Gateway.WebApi.Models
{
    public class GetSendSmsViaURLModel
    {
        private Guid batchId;
        private string to;
        private string message;
        private int smsLen;
        private bool isUnicode;
        private List<string> lstReceivers;
        private List<InProgressSms> lstInProgressReceivers = new List<InProgressSms>();
        private InProgressSms inProgressSms;

        public GetSendSmsViaURLModel()
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
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                smsLen = Helper.GetSmsCount(Message);
                isUnicode = Helper.HasUniCodeCharacter(Message);
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
        public string To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
                lstReceivers = to.Split(',').ToList();
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

        public string from
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
