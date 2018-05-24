using System;
using System.Collections.Generic;

namespace DataAccess.DTO
{
	public class PolicyByUserDTO
	{
		public int PolicyByUserId { get; set; }
		public int PolicyId { get; set; }
		public int UserId { get; set; }

		public PolicyDTO Policy { get; set; }
	}
}
