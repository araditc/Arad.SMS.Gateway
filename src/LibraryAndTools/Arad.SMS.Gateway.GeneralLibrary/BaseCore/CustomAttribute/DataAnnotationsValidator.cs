using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeneralLibrary.BaseCore
{
	public static class DataAnnotationsValidator
	{
		public static bool Validate(this object @object, out List<ValidationResult> results)
		{
			var context = new ValidationContext(@object);
			results = new List<ValidationResult>();
			return Validator.TryValidateObject(
					@object, context, results,
					validateAllProperties: true
			);
		}
	}
}
