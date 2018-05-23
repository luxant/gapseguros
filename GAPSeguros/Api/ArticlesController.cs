//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using DataAccess.Database;
//using DataAccess.DTO;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SuperZapatosGAP.Auth;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace SuperZapatosGAP.Api
//{
//	//[BasicAuthentication("my_user", "my_password")]
//	[BasicAuthorize("my_user", "my_password")]
//	[Route("services/[controller]")]
//	public class ArticlesController : Controller
//	{
//		// Create a field to store the mapper object
//		private readonly IMapper _mapper;
//		private readonly GAPSuperZapatosContext _context;

//		public ArticlesController(GAPSuperZapatosContext context, IMapper mapper)
//		{
//			_context = context;
//			_mapper = mapper;
//		}


//		// GET: api/<controller>
//		[HttpGet]
//		public object Get()
//		{
//			var result = _context.Articles.Include(x => x.Store);

//			return new ResponseArticleDTO()
//			{
//				Articles = result.Select(x => _mapper.Map<Articles, ArticlesDTO>(x)),
//				Success = true,
//				TotalElements = result.Count()
//			};
//		}

//		// GET: api/<controller>
//		[HttpGet("stores/{id?}")]
//		public object GetStoreArticles(int? id)
//		{
//			if (id == null)
//			{
//				return new ResponseDTO()
//				{
//					Success = false,
//					ErrorCode = DataAccess.Enums.ErrorCode.BadRequest
//				};
//			}

//			// We check if the store exists in order to display the required error
//			var store = _context.Store.Find(id);

//			if (store == null)
//			{
//				return new ResponseDTO()
//				{
//					Success = false,
//					ErrorCode = DataAccess.Enums.ErrorCode.RecordNotFound
//				};
//			}

//			var result = _context.Articles
//				.Where(x => x.StoreId == id)
//				.Include(x => x.Store);

//			return new ResponseArticleDTO()
//			{
//				Articles = result.Select(x => _mapper.Map<Articles, ArticlesDTO>(x)),
//				Success = true,
//				TotalElements = result.Count()
//			};
//		}
//	}
//}
