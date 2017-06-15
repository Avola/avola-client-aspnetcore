using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace TravelClaimClient
{
    public class SwaggerAuthorizedMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly IpSecuritySettings IpSecuritySettings;

        public SwaggerAuthorizedMiddleware(RequestDelegate next, IOptions<IpSecuritySettings> ipSecuritySettings)
        {
            _next = next;
            IpSecuritySettings = ipSecuritySettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = (string)context.Connection.RemoteIpAddress?.ToString();

            if (context.Request.Path.StartsWithSegments("/swagger")
                && !IpSecuritySettings.AllowedIPsList.Contains(ipAddress))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next.Invoke(context);
        }
    }
}
