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
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class Data : CommonEntityBase
	{
		public enum TableFields
		{
			Title,
			Summary,
			Content,
			Keywords,
			FromDate,
			ToDate,
			CreateDate,
			Priority,
			ParentGuid,
			IsActive,
			ShowInHomePage,
			IsArchived,
			IsDeleted,
			DataCenterGuid
		}

		public Data()
			: base(TableNames.Datas.ToString())
		{
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Summary.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Content.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Keywords.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.FromDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ToDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Priority.ToString(), SqlDbType.Int);
			AddField(TableFields.ParentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.ShowInHomePage.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsArchived.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.DataCenterGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid DataGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Title
		{
			get { return Helper.GetString(this[TableFields.Title.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteTitleField");
				}
				else
					this[TableFields.Title.ToString()] = value;
			}
		}

		public string Summary
		{
			get { return Helper.GetString(this[TableFields.Summary.ToString()]); }
			set { this[TableFields.Summary.ToString()] = value; }
		}

		public string Content
		{
			get { return Helper.GetString(this[TableFields.Content.ToString()]); }
			set { this[TableFields.Content.ToString()] = value; }
		}

		public string Keywords
		{
			get { return Helper.GetString(this[TableFields.Keywords.ToString()]); }
			set { this[TableFields.Keywords.ToString()] = value; }
		}

		public bool IsActive
		{
			get { return Helper.GetBool(this[TableFields.IsActive.ToString()]); }
			set { this[TableFields.IsActive.ToString()] = value; }
		}

		public bool ShowInHomePage
		{
			get { return Helper.GetBool(this[TableFields.ShowInHomePage.ToString()]); }
			set { this[TableFields.ShowInHomePage.ToString()] = value; }
		}

		public bool IsArchived
		{
			get { return Helper.GetBool(this[TableFields.IsArchived.ToString()]); }
			set { this[TableFields.IsArchived.ToString()] = value; }
		}

		public DateTime FromDate
		{
			get { return Helper.GetDateTime(this[TableFields.FromDate.ToString()]); }
			set { this[TableFields.FromDate.ToString()] = value; }
		}

		public DateTime ToDate
		{
			get { return Helper.GetDateTime(this[TableFields.ToDate.ToString()]); }
			set { this[TableFields.ToDate.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}

		public int Priority
		{
			get { return Helper.GetInt(this[TableFields.Priority.ToString()]); }
			set { this[TableFields.Priority.ToString()] = value; }
		}

		public Guid ParentGuid
		{
			get { return Helper.GetGuid(this[TableFields.ParentGuid.ToString()]); }
			set { this[TableFields.ParentGuid.ToString()] = value; }
		}

		public Guid DataCenterGuid
		{
			get { return Helper.GetGuid(this[TableFields.DataCenterGuid.ToString()]); }
			set { this[TableFields.DataCenterGuid.ToString()] = value; }
		}
	}
}
