//using DataAccess.Database;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccess.Validation
//{
//	public class ArticleValidator : AbstractValidator<Articles>
//	{
//		public ArticleValidator()
//		{
//			RuleFor(x => x.Id).NotNull();
//			RuleFor(x => x.Name).NotEmpty().Length(1, 50);
//			RuleFor(x => x.Description).NotEmpty().Length(1, 100);
//			RuleFor(x => x.Price).GreaterThan(0);
//			RuleFor(x => x.TotalInShelf).GreaterThanOrEqualTo(0);
//			RuleFor(x => x.TotalInVault).GreaterThanOrEqualTo(0);
//			RuleFor(x => x.StoreId).GreaterThan(0).WithMessage("store is required");
//		}
//	}
//}
