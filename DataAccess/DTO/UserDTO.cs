using System;
using System.Collections.Generic;

namespace DataAccess.DTO
{
	public class UserDTO
	{
		public int UserId { get; set; }
		public string Name { get; set; }

		public IEnumerable<PolicyByUserDTO> PolicyByUser { get; set; }
	}
}
