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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class Route : CommonEntityBase
	{
		private enum TableFields
		{
			Name,
			Username,
			Password,
			Domain,
			Link,
			QueueLength,
			SmsSenderAgentGuid,
			OperatorID,
		}

		public Route()
			: base(TableNames.Routes.ToString())
		{
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.Username.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.Password.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.Domain.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.Link.ToString(), SqlDbType.NVarChar, 512);
			AddField(TableFields.QueueLength.ToString(), SqlDbType.Int);
			AddField(TableFields.SmsSenderAgentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.OperatorID.ToString(), SqlDbType.TinyInt);
		}

		public Guid RouteGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Name
		{
			get
			{
				return Helper.GetString(this[TableFields.Name.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteNameField");
					HasError = true;
				}
				else
					this[TableFields.Name.ToString()] = value;
			}
		}

		public string Username
		{
			get
			{
				return Helper.GetString(this[TableFields.Username.ToString()].ToString());
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteUserNameField");
					HasError = true;
				}
				else
					this[TableFields.Username.ToString()] = value;
			}
		}

		public string Password
		{
			get
			{
				return Helper.GetString(this[TableFields.Password.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompletePasswordField");
					HasError = true;
				}
				else
					this[TableFields.Password.ToString()] = value;
			}
		}

		public string Domain
		{
			get
			{
				return Helper.GetString(this[TableFields.Domain.ToString()]);
			}
			set
			{

				this[TableFields.Domain.ToString()] = value;
			}
		}

		public string Link
		{
			get
			{
				return Helper.GetString(this[TableFields.Link.ToString()]);
			}
			set
			{
				this[TableFields.Link.ToString()] = value;
			}
		}

		public int QueueLength
		{
			get
			{
				return Helper.GetInt(this[TableFields.QueueLength.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteQueueLengthField");
					HasError = true;
				}
				else if (!Helper.CheckDataConditions(value).IsIntNumber)
				{
					ErrorMessage += Language.GetString("QueueLengthIsInvalid");
					HasError = true;
				}
				else if (Helper.GetInt(value) == 0 || Helper.GetInt(value) < 0)
				{
					ErrorMessage += Language.GetString("QueueLengthIsInvalid");
					HasError = true;
				}
				else
					this[TableFields.QueueLength.ToString()] = value;
			}
		}

		public Guid SmsSenderAgentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.SmsSenderAgentGuid.ToString()]);
			}
			set
			{
				this[TableFields.SmsSenderAgentGuid.ToString()] = value;
			}
		}

		public byte OperatorID
		{
			get
			{
				return byte.Parse(this[TableFields.OperatorID.ToString()].ToString());
			}
			set
			{
				this[TableFields.OperatorID.ToString()] = value;
			}
		}
	}
}
