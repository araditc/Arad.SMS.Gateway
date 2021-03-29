using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.DataModels
{
	[ComplexType]
	public class CompanyInfo
	{
		public string CompanyName { get; set; }
		public string CompanyNationalId { get; set; }
		public string EconomicCode { get; set; }
		public string CompanyAddress { get; set; }
		public string CompanyPhone { get; set; }
		public string CompanyZipCode { get; set; }
		public string CompanyCEOName { get; set; }
		public string CompanyCEONationalCode { get; set; }
		public string CompanyCEOMobile { get; set; }
	}
}
