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

using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.ManageThread;
using System;
using System.Collections.Generic;

namespace Arad.SMS.Gateway.RahyabRGSmsSender
{
    public class RahyabThread : SendThread
    {
        public RahyabThread(int timeOut)
            : base(timeOut)
        {
            this.ServiceManager = new RahyabRGSmsServiceManager();

            //sendQueueRecipientAddress = ConfigurationManager.GetSetting("RecipientAddress");
            sentMessageQueue = ConfigurationManager.GetSetting("SentMessageQueue");

            isActiveDeliveryRelay = Helper.GetBool(ConfigurationManager.GetSetting("IsActiveDeliveryRelay"));

            getDeliveryQueue = ConfigurationManager.GetSetting("GetDeliveryQueue");

            Dictionary<string, string> queueInfo = ConfigurationManager.GetSettingWithSpecificPrefix("SendQueue");
            foreach (KeyValuePair<string, string> info in queueInfo)
            {              
               sendQueueInfo.Add(info.Value,
                                                    new Tuple<int, TimeSpan, TimeSpan>
                                                    (
                                                        Helper.GetInt(ConfigurationManager.GetAttributeValue(info.Key, "Capacity"), 5),
                                                        TimeSpan.Parse(ConfigurationManager.GetAttributeValue(info.Key, "StartTime")),
                                                        TimeSpan.Parse(ConfigurationManager.GetAttributeValue(info.Key, "EndTime")))
                                                    );
            }
        }
    }
}
