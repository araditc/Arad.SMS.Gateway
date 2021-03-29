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

namespace Arad.SMS.Gateway.SqlLibrary
{
	[Serializable]
	public class InProgressSms
	{
		private int deliveryStatus;
		private int deliveryTryCount;
		private DateTime deliveryGetTime;
		private bool saveDelivery;
		private static int minimumGetDeliveryTime = 15;

		public InProgressSms()
		{
			deliveryTryCount = 0;
			deliveryGetTime = DateTime.Now;
		}

		public int SendTryCount { get; set; }

		public string RecipientNumber { get; set; }

		public int OperatorType { get; set; }

		public int SendStatus { get; set; }

		public int DeliveryStatus
		{
			get { return deliveryStatus; }
			set
			{
				deliveryTryCount++;
				saveDelivery = (deliveryStatus == value && deliveryTryCount < 6) ? false : true;
				deliveryGetTime = deliveryGetTime.AddMinutes(minimumGetDeliveryTime * deliveryTryCount);
				deliveryStatus = value;
			}
		}

		public int DeliveryTryCount
		{
			get { return deliveryTryCount; }
		}

		public DateTime DeliveryGetTime
		{
			get { return deliveryGetTime; }
			set { deliveryGetTime = value; }
		}

		public string ReturnID { get; set; }

		public string CheckID { get; set; }

		public bool SaveToDatabase { get; set; }

		public bool SaveDelivery
		{
			get { return saveDelivery; }
			set { saveDelivery = value; }
		}

		public int IsBlackList{ get; set; }
	}
}