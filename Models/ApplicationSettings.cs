using System;
using System.Collections.Generic;

namespace info_media_web.Models
{
    public class ApplicationSettings
    {
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string ApplicationGuid { get; set; }
        public string DevelopedBy { get; set; }
        public List<ResourceViewModel> Resources { get; set; }
        public EnumLangauges CurrentLanguage { get; set; } = EnumLangauges.np;
    }
}
