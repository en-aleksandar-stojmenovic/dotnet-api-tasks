using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace ProjectSetup.Extensions
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
	}
}
