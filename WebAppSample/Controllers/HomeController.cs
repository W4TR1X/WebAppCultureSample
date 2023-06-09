﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WebAppSample.Models;
using WebAppSampleCore.Interfaces.Services;

namespace WebAppSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILocalizationService _localizer;
        private readonly IOptions<RequestLocalizationOptions> _locOptions;

        public HomeController(ILogger<HomeController> logger
            , IOptions<RequestLocalizationOptions> locOptions
            , ILocalizationService localizer)
        {
            _logger = logger;
            _localizer = localizer;
            _locOptions = locOptions;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var isExists = _locOptions.Value.SupportedUICultures!
                .Any(x => x.Name == culture);

            if (isExists)
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}