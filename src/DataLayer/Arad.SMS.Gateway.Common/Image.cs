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
	public class Image : CommonEntityBase
	{
		public enum TableFields
		{
			GalleryImageGuid,
			DataGuid,
			Title,
			Description,
			ImagePath,
			CreateDate,
			IsActive,
			IsDeleted
		}

		public Image()
			: base(TableNames.Images.ToString())
		{
			AddField(TableFields.GalleryImageGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.DataGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Description.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.ImagePath.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid ImageGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public Guid GalleryImageGuid
		{
			get { return Helper.GetGuid(this[TableFields.GalleryImageGuid.ToString()]); }
			set { this[TableFields.GalleryImageGuid.ToString()] = value; }
		}

		public Guid DataGuid
		{
			get { return Helper.GetGuid(this[TableFields.DataGuid.ToString()]); }
			set { this[TableFields.DataGuid.ToString()] = value; }
		}

		public string Title
		{
			get { return Helper.GetString(this[TableFields.Title.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteImageTitleField");
				}
				else
					this[TableFields.Title.ToString()] = value;
			}
		}

		public string Description
		{
			get { return Helper.GetString(this[TableFields.Description.ToString()]); }
			set { this[TableFields.Description.ToString()] = value; }
		}

		public string ImagePath
		{
			get { return Helper.GetString(this[TableFields.ImagePath.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteImagePathField");
				}
				else
				{
					HasError = false;
					this[TableFields.ImagePath.ToString()] = value;
				}
			}
		}

		public bool IsActive
		{
			get { return Helper.GetBool(this[TableFields.IsActive.ToString()]); }
			set { this[TableFields.IsActive.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}
	}
}
