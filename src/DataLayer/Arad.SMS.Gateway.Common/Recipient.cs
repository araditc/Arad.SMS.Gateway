using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class Recipient:CommonEntityBase
	{
		public enum TableFields
		{
			ID,
			Mobile,
			ScheduledSmsGuid,
		}

		public Recipient()
			: base(TableNames.Recipients.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.Int);
			AddField(TableFields.Mobile.ToString(), SqlDbType.VarChar, 11);
			AddField(TableFields.ScheduledSmsGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid RecipientGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Mobile
		{
			get { return Helper.GetString(this[TableFields.Mobile.ToString()]); }
			set { this[TableFields.Mobile.ToString()] = value; }
		}

		public Guid ScheduledSmsGuid
		{
			get { return Helper.GetGuid(this[TableFields.ScheduledSmsGuid.ToString()]); }
			set { this[TableFields.ScheduledSmsGuid.ToString()] = value; }
		}
	}
}
