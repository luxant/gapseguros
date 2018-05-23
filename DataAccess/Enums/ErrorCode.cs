using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Enums
{
	public enum ErrorCode
	{
		BadRequest = 400,
		NotAuthorized = 401,
		RecordNotFound = 404,
		ServerError = 500,
	}
}
