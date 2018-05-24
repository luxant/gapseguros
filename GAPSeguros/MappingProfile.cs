using AutoMapper;
using DataAccess.Database;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPSeguros
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDTO>();
			CreateMap<Policy, PolicyDTO>();
		}
	}
}
