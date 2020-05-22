using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using info_media_web.GlobalFunctions;
using info_media_web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace info_media_web.Repositories
{
    public class CategoryRepo
    {
        private readonly IConfiguration _configuration;
        string host = string.Empty;
        Utilities _util = new Utilities();
        public CategoryRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            host = _configuration.GetSection("API_SETTINGS").GetSection("INFO_MED_HOST_NAME").Value;
        }

        public async Task<List<CategoryViewModel>> GetCategories(int skip=0, int limit = 10)
        {
            List<CategoryViewModel> lstCategories = new List<CategoryViewModel>();

            string api_endpoint = _configuration.GetSection("API_SETTINGS").GetSection("GET_CATEGORIES").Value;
            string FULL_END_POINT = $"{host}{api_endpoint}?skip={skip}&limit={limit}";
            lstCategories = await _util.CallGetAPI<CategoryViewModel>(FULL_END_POINT);
            
            return lstCategories;
        }

        public async Task<List<CategoryViewModel>> GetPopularCategories(int top)
        {
            List<CategoryViewModel> lstCategories = new List<CategoryViewModel>();

            string api_endpoint = _configuration.GetSection("API_SETTINGS")
                .GetSection("GET_TOP_N_CATEGORIES").Value;
            string FULL_END_POINT = $"{host}{api_endpoint}/{top}";
            lstCategories = await _util.CallGetAPI<CategoryViewModel>(FULL_END_POINT);

            return lstCategories;
        }
    }
}
