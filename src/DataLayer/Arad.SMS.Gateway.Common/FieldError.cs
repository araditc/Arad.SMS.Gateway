using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class FieldError
	{
		private string fieldName;
		//private ErrorType errorType;

		public string FieldName
		{
			get { return fieldName; }
			set { fieldName = value; }
		}

		//public ErrorType ErrorType
		//{
		//	get { return errorType; }
		//	set { errorType = value; }
		//}
	}
}
