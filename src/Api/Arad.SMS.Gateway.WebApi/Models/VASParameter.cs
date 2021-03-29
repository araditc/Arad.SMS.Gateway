using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
	public class VASParameter

	{
		[Required(ErrorMessage = "موبایل مشترک را وارد کنید")]
		public string Mobile { get; set; }
		public string ServiceId { get; set; }
	}
}
