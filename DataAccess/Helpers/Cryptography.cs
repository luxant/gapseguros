using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Helpers
{
	public class Cryptography
	{
		public static string Sha1(string origianlValue)
		{
			var sha1Provider = new SHA1CryptoServiceProvider();

			var result = sha1Provider.ComputeHash(Encoding.UTF8.GetBytes(origianlValue));

			return BitConverter.ToString(result).Replace("-", string.Empty).ToLowerInvariant();
		}
	}
}
