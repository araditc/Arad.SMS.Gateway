using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class GroupTemplate:CommonEntityBase
	{
		public enum TableFields
		{
			Title,
			CreateDate,
			IsDeleted,
			UserGuid
		}

		public GroupTemplate()
			: base(TableNames.GroupTemplates.ToString())
		{
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid GroupTemplateGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public String Title
		{
			get
			{
				return Helper.GetString(this[TableFields.Title.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteTitleField");
					HasError = true;
				}
				else
					this[TableFields.Title.ToString()] = value;
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
