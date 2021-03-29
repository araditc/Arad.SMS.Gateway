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
	public class SmsTemplate:CommonEntityBase
	{
		public enum TableFields
		{
			Body,
			CreateDate,
			IsDeleted,
			UserGuid,
		}

		public SmsTemplate()
			: base(Common.TableNames.SmsTemplates.ToString())
		{
			AddField(TableFields.Body.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.CreateDate.ToString(),SqlDbType.DateTime);
			AddReadOnlyField(TableFields.IsDeleted.ToString(),SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(),SqlDbType.UniqueIdentifier);
		}

		public Guid SmsTemplateGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public String Body
		{
			get
			{
				return Helper.GetString(this[TableFields.Body.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteBodyField");
					HasError = true;
				}
				else
					this[TableFields.Body.ToString()] = value;
			}
		}

		public DateTime CreateDate
		{
			get 
			{ 
				return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]);
			}
			set
			{
				this[TableFields.CreateDate.ToString()] = value;
			}
		}

		public Guid UserGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.UserGuid.ToString()]);
			}
			set
			{
				this[TableFields.UserGuid.ToString()] = value;
			}
		}
	}
}
