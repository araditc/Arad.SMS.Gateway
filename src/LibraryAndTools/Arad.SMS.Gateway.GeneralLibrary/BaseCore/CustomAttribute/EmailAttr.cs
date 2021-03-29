using System;
using System.ComponentModel.DataAnnotations;

namespace GeneralLibrary.BaseCore
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class EmailAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			return Helper.CheckDataConditions(value).IsEmail;
		}

		public override string FormatErrorMessage(string name)
		{
			string errorMessage = base.FormatErrorMessage(name);
			return Language.GetString(errorMessage);
		}
	}
}
