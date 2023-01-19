using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Twitter.Extensions
{
	public static class GeneralExtensions
	{
		public static Guid GetUserId(this HttpContext httpContext)
		{
			if (httpContext.User == null)
			{
				return Guid.Empty;
			}

			return Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);
		}

		public static bool AddIfNotExists<T>(this List<T> list, T value)
		{
			if (!list.Contains(value))
			{
				list.Add(value);
				return true;
			}
			return false;
		}
	}
}
