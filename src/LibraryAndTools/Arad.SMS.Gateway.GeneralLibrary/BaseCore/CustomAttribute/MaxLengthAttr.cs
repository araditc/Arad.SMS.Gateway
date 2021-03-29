using System;
using System.ComponentModel.DataAnnotations;

namespace GeneralLibrary.BaseCore
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class MaxLengthAttr : MaxLengthAttribute
	{
		public MaxLengthAttr(int length)
			: base(length)
		{

		}
		public override bool IsValid(object value)
		{
			return base.IsValid(value);
		}

		public override string FormatErrorMessage(string name)
		{
			string errorMessage = base.FormatErrorMessage(name);
			return Language.GetString(errorMessage);
		}
	}
}
