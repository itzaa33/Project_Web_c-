using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Web_db.MiddleWare
{
    public class CheckBan_md
    {
        private RequestDelegate _next;

        public CheckBan_md(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string culture = context.Request.Query["culture"];

            if(!string.IsNullOrWhiteSpace(culture))
            {
                CultureInfo cultureInfo = new CultureInfo(culture);

                CultureInfo.CurrentCulture = cultureInfo;
            }

            if (int.Parse(context.Session.GetString("Userstate")) == 0)
            {
                await _next(context);
            }
            else
            {
                context.Response.Redirect("/Account/Lockout");
            }

           
        }

        public static class CheckBan_mdExtensions
        {
            public static IApplicationBuilder UseCheckBan_md(IApplicationBuilder appbuilder)
            {
                return appbuilder.UseMiddleware<CheckBan_md>();
            }
        }
    }
}
