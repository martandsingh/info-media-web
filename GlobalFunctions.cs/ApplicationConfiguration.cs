using System;
using System.Collections.Generic;
using info_media_web.Models;

namespace info_media_web.GlobalFunctions
{
    public static class ApplicationConfiguration
    {
        public static ApplicationSettings _settings;

        /// <summary>
        ///  Author: Martand Singh
        ///  Date: 22-May-2020
        ///  Scope: It will load the resource value based on given language
        /// </summary>
        /// <param name="ResourceName">Name of the resource(label_name property)</param>
        /// <param name="language">language</param>
        /// <returns></returns>
        public static string GetResourceValue(string ResourceName, EnumLangauges language = EnumLangauges.en_us)
        {

            string Value = string.Empty;
            if (!string.IsNullOrEmpty(ResourceName))
            {
                // English
                if (language == EnumLangauges.en_us)
                {
                    Value = _settings.Resources.Find(p => p.label_name == ResourceName).en_us;
                }
                // Chinese
                else if (language == EnumLangauges.ch)
                {
                    Value = _settings.Resources.Find(p => p.label_name == ResourceName).ch;
                }
                // Nepali
                else if (language == EnumLangauges.np)
                {
                    Value = _settings.Resources.Find(p => p.label_name == ResourceName).np;
                }
            }
            return Value;
        }

        /// <summary>
        /// Author: Martand Singh
        /// Scope: Set the application language
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string SetLanguage(string lang)
        {

            string Value = string.Empty;
            
                if (lang == null || lang == "en" || lang == string.Empty)
                {
                    _settings.CurrentLanguage = EnumLangauges.en_us;
                }
                else if (lang == "ch")
                {
                    _settings.CurrentLanguage = EnumLangauges.ch;
                }
                else if (lang == "np")
                {
                    _settings.CurrentLanguage = EnumLangauges.np;
                }

            
            return Value;
        }
    }
}
