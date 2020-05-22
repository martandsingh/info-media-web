using System;
using System.Collections.Generic;

namespace info_media_web.Models
{
    public class HomePageViewModel
    {
        public List<CategoryViewModel> PopularCategories { get; set; }
        public List<CategoryViewModel> AllCategories { get; set; }
    }
}
