using System;
namespace info_media_web.Models
{
    public class CategoryViewModel
    {
        
        public string _id { get; set; }
        public int id { get; set; }
        public string Name { get; set; }
        public string[] url { get; set; }
        public string[] tags { get; set; }
        public int visits { get; set; }
    }
}