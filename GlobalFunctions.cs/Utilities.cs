using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using info_media_web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace info_media_web.GlobalFunctions
{
    public class Utilities
    {
        
        public Utilities()
        {
        }

        /// <summary>
        /// Author: Martand Singh
        /// Date: 22-May-2020
        /// Scope: Call Get API with headers. It will return a generalized JObject
        /// </summary>
        /// <param name="EndPoint">Complete endpoint of the get REST API.</param>
        /// <param name="headers">dictionary of headers to be added. Default is null</param>
        /// <returns>Return JObject containing the response of REST API</returns>
        public async Task<List<T>> CallGetAPI<T>(string EndPoint, Dictionary<string, string> headers = null) where T: class
        {
            using (var httpClient = new HttpClient())
            {
                if(headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                }
                using (var response = await httpClient.GetAsync(EndPoint))
                {
                    List<T> result = null;
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(apiResponse))
                        {
                            result = JsonConvert.DeserializeObject<List<T>>(apiResponse);

                        }
                    }
                  
                    return result;
                    
                }
            }
        }

        /// <summary>
        /// Author: Martand Singh
        /// Date: 22-May-2020
        /// Scope: it will call get REST api and will return and object type
        /// </summary>
        /// <param name="EndPoint">GET REST api url</param>
        /// <param name="headers">Headers to be sent(if any)</param>
        /// <returns></returns>
        public async Task<object> CallGetAPIAsObject(string EndPoint, Dictionary<string, string> headers = null)
        {
            using (var httpClient = new HttpClient())
            {
                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                }
                using (var response = await httpClient.GetAsync(EndPoint))
                {
                    object result = null;
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(apiResponse))
                        {
                            result = apiResponse;

                        }
                    }

                    return result;

                }
            }
        }

        /// <summary>
        /// Author: Martand Singh
        /// Date: 22-May-2020
        /// Scope: Call Post API with headers and return an object type result.
        /// </summary>
        /// <typeparam name="T">Class Type</typeparam>
        /// <param name="EndPoint">Endpoint of the POST REST API</param>
        /// <param name="ObjectParam">Class object which we need to send</param>
        /// <param name="headers">Headers to be sent if any. Default is null</param>
        /// <returns></returns>
        public async Task<object> CallPostAPI<T>(string EndPoint, T ObjectParam
            ,Dictionary<string, string> headers = null) where T: class
        {
            using (var httpClient = new HttpClient())
            {
                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                }
                var json = JsonConvert.SerializeObject(ObjectParam);
                var final_param_string = new StringContent(json, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(EndPoint, final_param_string))
                {
                    object result = null;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(apiResponse))
                        {
                            result = (apiResponse);

                        }
                    }
                   
                    return result;

                }
            }
        }

        /// <summary>
        /// Author: Martand Singh
        /// Date: 22-May-2020
        /// Scope: Load all the resources from mongo
        /// </summary>
        /// <param name="_configuration">Configuration object</param>
        /// <returns></returns>
        public async Task<List<ResourceViewModel>> GetResources(IConfiguration _configuration)
        {

            List<ResourceViewModel> lstResource = new List<ResourceViewModel>();
            string host = _configuration.GetSection("API_SETTINGS").GetSection("INFO_MED_HOST_NAME").Value;
            string api_endpoint = _configuration.GetSection("API_SETTINGS").GetSection("GET_RESOURCES").Value;
            string FULL_END_POINT = $"{host}{api_endpoint}";
            lstResource = await CallGetAPI<ResourceViewModel>(FULL_END_POINT);
            return lstResource;

        }
    }
}
