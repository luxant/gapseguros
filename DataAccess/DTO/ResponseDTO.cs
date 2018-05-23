using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
	public class ResponseDTO
	{
		public bool Success { get; set; }

		public ErrorCode? ErrorCode { get; set; }

		public string ErrorMsg
		{
			get
			{
				if (!ErrorCode.HasValue)
				{
					return null;
				}

				switch (ErrorCode.Value)
				{
					case DataAccess.Enums.ErrorCode.BadRequest:
						return "Bad request";
					case DataAccess.Enums.ErrorCode.NotAuthorized:
						return "Not authorized";
					case DataAccess.Enums.ErrorCode.RecordNotFound:
						return "Record not found";
					case DataAccess.Enums.ErrorCode.ServerError:
						return "Server Error";
					default:
						return null;
				}
			}
		}

	}
}
