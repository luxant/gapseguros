using AutoMapper;
using DataAccess.Database;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperZapatosGAP
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//CreateMap<Store, StoresDTO>();
			//CreateMap<Articles, ArticlesDTO>().ForMember(destination => destination.StoreName, source => source.MapFrom(y => y.Store.Name));
		}
	}
}
