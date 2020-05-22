using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using info_media_web.Models;
using info_media_web.Repositories;
using Microsoft.Extensions.Configuration;
using info_media_web.GlobalFunctions;

namespace info_media_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly CategoryRepo _categories = null;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            
            _logger = logger;
            _configuration = configuration;
            _categories = new CategoryRepo(_configuration);
        }

        public async Task<IActionResult> Index()
        {
            HomePageViewModel model = new HomePageViewModel();
            
            try
            {
                string lang = Convert.ToString(Request.Query["lang"]);
                ApplicationConfiguration.SetLanguage(lang);
                // Configuration
                ViewData["APP_NAME"] = ApplicationConfiguration._settings.ApplicationName;

                // ALL Categories
                List<CategoryViewModel> lstCategories = new List<CategoryViewModel>();
                lstCategories = await _categories.GetCategories();

                // Popular Categories
                int popular_cat = Convert.ToInt16( _configuration.GetSection("APP_SETTINGS").GetSection("NO_OF_POPULAR_CATS").Value);
                List<CategoryViewModel> lstPopularCategories = new List<CategoryViewModel>();
                lstPopularCategories = await _categories.GetPopularCategories(popular_cat);

                // Preparing final model
                model.AllCategories = lstCategories;
                model.PopularCategories = lstPopularCategories;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
