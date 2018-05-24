using System;
using System.Collections.Generic;

namespace DataAccess.DTO
{
	public class PolicyDTO
	{
		public int PolicyId { get; set; }
		public int Coverage { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime ValidityStart { get; set; }
		public int CoverageSpan { get; set; }
		public decimal Price { get; set; }
		public int RiskTypeId { get; set; }
	}
}
