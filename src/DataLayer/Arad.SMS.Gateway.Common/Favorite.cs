using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Common
{
	public class Favorite : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			Title,
			ParentGuid,
		}
		public Favorite()
			: base(TableNames.Favorites.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.Int);
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.ParentGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid JobGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public int ID
		{
			get
			{
				return Helper.GetInt(this[TableFields.ID.ToString()]);
			}
			set
			{
				this[TableFields.ID.ToString()] = value;
			}
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

		public Guid ParentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ParentGuid.ToString()]);
			}
			set
			{
				this[TableFields.ParentGuid.ToString()] = value;
			}
		}

	}
}
