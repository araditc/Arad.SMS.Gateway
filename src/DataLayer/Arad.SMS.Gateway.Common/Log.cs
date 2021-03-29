using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralLibrary.BaseCore;
using System.Data;
using GeneralLibrary;

namespace Common
{
	public class Log : CommonEntityBase
	{
		public enum TableFields
		{
			Type,
			Source,
			Name,
			Text,
			IPAddress,
			Browser,
			CreateDate,
			ReferenceGuid,
			UserGuid,
		}

		public Log()
			: base(TableNames.Logs.ToString())
		{
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.Source.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.Text.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.IPAddress.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.Browser.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ReferenceGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid LogGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public int Type
		{
			get { return Helper.GetInt(this[TableFields.Type.ToString()]); }
			set { this[TableFields.Type.ToString()] = value; }
		}

		public string Source
		{
			get { return Helper.GetString(this[TableFields.Source.ToString()]); }
			set { this[TableFields.Source.ToString()] = value; }
		}

		public string Name
		{
			get { return Helper.GetString(this[TableFields.Name.ToString()]); }
			set { this[TableFields.Name.ToString()] = value; }
		}

		public string Text
		{
			get { return Helper.GetString(this[TableFields.Text.ToString()]); }
			set { this[TableFields.Text.ToString()] = value; }
		}

		public string IPAddress
		{
			get { return Helper.GetString(this[TableFields.IPAddress.ToString()]); }
			set { this[TableFields.IPAddress.ToString()] = value; }
		}

		public string Browser
		{
			get { return Helper.GetString(this[TableFields.Browser.ToString()]); }
			set { this[TableFields.Browser.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}

		public Guid ReferenceGuid
		{
			get { return Helper.GetGuid(this[TableFields.ReferenceGuid.ToString()]); }
			set { this[TableFields.ReferenceGuid.ToString()] = value; }
		}

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}
	}
}
