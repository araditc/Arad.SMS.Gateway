using System;
using System.Data;

namespace GeneralLibrary.BaseCore
{
	public class SqlProperty : Attribute
	{
		public SqlDbType SqlDataType { get; set; }
		public short Size { get; set; }
		public bool SqlIgnore { get; set; }
	}
}
