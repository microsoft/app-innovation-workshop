using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoMaintenance.Web.Models.Interfaces;
using ContosoMaintenance.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContosoMaintenance.Web.Controllers
{
    public class SettingsController : Controller
    {

        private IUserConfig _config;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public SettingsController(IUserConfig config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            SettingsViewModel model = new SettingsViewModel()
            {
                UserOverrideConfig = _config.UserOverrideConfig,
                OverrideBaseUrl = _config.OverrideConfig.BaseUrl,
                DefaultBaseUrl = _config.DefaultConfig.BaseUrl,
                DefaultAPIManagementKey = _config.DefaultConfig.APIManagementKey
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Update config stored in cookie
                _config.UserOverrideConfig = model.UserOverrideConfig;
                _config.OverrideConfig.BaseUrl = model.OverrideBaseUrl;
                _config.OverrideConfig.APIManagementKey = model.OverrideAPIManagementKey;
                var cookieData = JsonConvert.SerializeObject(_config);

                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(2);
                Response.Cookies.Append(Constants.UserSettingsCookie, cookieData, option);

                TempData["message"] = "Settings Updated";
                TempData["message-details"] = " - Return to the Jobs page to see the effect of your changes";

                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}