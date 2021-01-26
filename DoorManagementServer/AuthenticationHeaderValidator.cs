using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DoorManagementServer
{
    public class AuthenticationHeaderValidator
    {
        private RequestDelegate _next;
        public AuthenticationHeaderValidator(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }


        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["X-Auth-Security"];
            if (authHeader != null)
            {
                if (authHeader.Equals("830866A3-0964-411E-B0E8-3A223438AF8E",StringComparison.OrdinalIgnoreCase))
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
    }
}
