using System;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class DomainMenu : CommonEntityBase
	{
		public enum TableFields
		{
			Type,
			Title,
			Link,
			DataCenterGuid,
			StaticPageReference,
			TargetType,
			CreateDate,
			Priority,
			IsActive,
			IsDeleted,
			DomainGuid,
		}

		public DomainMenu()
			: base(TableNames.DomainMenus.ToString())
		{
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Link.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.DataCenterGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.StaticPageReference.ToString(), SqlDbType.Int);
			AddField(TableFields.TargetType.ToString(), SqlDbType.Int);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Priority.ToString(), SqlDbType.Int);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.DomainGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid DomainMenuGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public int Type
		{
			get { return Helper.GetInt(this[TableFields.Type.ToString()]); }
			set 
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteDomainMenuTypeField");
					HasError = true;
				}
				else
					this[TableFields.Type.ToString()] = value; 
			}
		}

		public string Title
		{
			get { return Helper.GetString(this[TableFields.Title.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteDomainMenuTitleField");
					HasError = true;
				}
				else
					this[TableFields.Title.ToString()] = value;
			}
		}

		public string Link
		{
			get { return Helper.GetString(this[TableFields.Link.ToString()]); }
			set { this[TableFields.Link.ToString()] = value; }
		}

		public Guid DataCenterGuid
		{
			get { return Helper.GetGuid(this[TableFields.DataCenterGuid.ToString()]); }
			set { this[TableFields.DataCenterGuid.ToString()] = value; }
		}

		public int StaticPageReference
		{
			get { return Helper.GetInt(this[TableFields.StaticPageReference.ToString()]); }
			set { this[TableFields.StaticPageReference.ToString()] = value; }
		}

		public int TargetType
		{
			get { return Helper.GetInt(this[TableFields.TargetType.ToString()]); }
			set { this[TableFields.TargetType.ToString()] = value; }
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

		public bool IsActive
		{
			get { return Helper.GetBool(this[TableFields.IsActive.ToString()]); }
			set { this[TableFields.IsActive.ToString()] = value; }
		}

		public Guid DomainGuid
		{
			get { return Helper.GetGuid(this[TableFields.DomainGuid.ToString()]); }
			set { this[TableFields.DomainGuid.ToString()] = value; }
		}
	}
}
