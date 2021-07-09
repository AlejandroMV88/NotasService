using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Middleware
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			Int32 userId = (Int32)context.HttpContext.Items["User"];
			if (userId == 0)
			{
				// not logged in
				context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
			}
		}
	}
}
