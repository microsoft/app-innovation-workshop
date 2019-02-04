using ContosoMaintenance.Web.Models;
using ContosoMaintenance.Web.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoMaintenance.Web
{
    public class CookieMiddleware
    {

        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IUserConfig config, IConfiguration appConfig)
        {

            config.DefaultConfig = new Config()
            {
                BaseUrl = appConfig["BaseUrl"],
                APIManagementKey = appConfig["APIManagementKey"]
            };

            var userSettingsCookie = httpContext.Request.Cookies[Constants.UserSettingsCookie];
            
            if (userSettingsCookie != null)
            {
                var cookieConfig = JsonConvert.DeserializeObject<UserConfig>(userSettingsCookie);
                if (cookieConfig != null)
                {
                    config.OverrideConfig = cookieConfig.OverrideConfig;
                    config.UserOverrideConfig = cookieConfig.UserOverrideConfig;
                }
            }
            return _next(httpContext);
        }
    }
}
