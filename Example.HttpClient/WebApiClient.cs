// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeQualityClient.cs" company="">
//   
// </copyright>
// <summary>
//   The code quality client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Covis.Data.DynamicLinq.CQuery.Contracts.Contract;

namespace Example.HttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.DynamicLinq;

    using Example.Data.Contract.Model;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    // Add for Identity/Token Deserialization:

    /// <summary>
    /// The code quality client.
    /// </summary>
    public class WebApiClient
    {
        #region Fields

        /// <summary>
        /// The the uri.
        /// </summary>
        private readonly Uri theUri = new Uri("http://localhost/Example.WebApi/api/Project");

        
        #endregion

        #region Public Methods and Operators

        
        

        public IEnumerable<TModel> GetTest<TModel>(QDescriptor descriptor)
        {
            using (var client = new HttpClient())
            {
                var dateTimeConverter = new IsoDateTimeConverter();
                // Default for IsoDateTimeConverter is yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK
                dateTimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm";

                var settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.Objects;
                settings.Converters = new List<JsonConverter>
                                                                                {
                                                                                    dateTimeConverter
                                                                                };

                var json = JsonConvert.SerializeObject(descriptor, settings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                using (Task<HttpResponseMessage> response = client.PostAsync(new Uri("http://localhost/Example.WebApi/api/Test/Default"), content))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        string jsonContent = response.GetAwaiter().GetResult().Content.ReadAsStringAsync().Result;

                        return JsonConvert.DeserializeObject<IEnumerable<TModel>>(jsonContent);
                    }

                    return null;
                }
            }

            return null;
        }
        public IEnumerable<TDto> GetProject<TDto>(QueryDescriptor descriptor)
        {
            using (var client = new HttpClient())
            {
                var dateTimeConverter = new IsoDateTimeConverter();
                // Default for IsoDateTimeConverter is yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK
                dateTimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm";

                var settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.Objects;
                settings.Converters = new List<JsonConverter>
                                                                                {
                                                                                    dateTimeConverter
                                                                                };

                var json = JsonConvert.SerializeObject(descriptor, settings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                
                using (Task<HttpResponseMessage> response = client.PostAsync(new Uri("http://pc-dle-2.covis.lan/Example.WebApi/api/Project/Projection"), content))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        string jsonContent = response.GetAwaiter().GetResult().Content.ReadAsStringAsync().Result;

                        return JsonConvert.DeserializeObject<IEnumerable<TDto>>(jsonContent);
                    }

                    return null;
                }
            }

            return null;
        }

        public IEnumerable<TDto> GetModel<TDto>(QueryDescriptor descriptor) where TDto : IModelEntity
        {
            using (var client = new HttpClient())
            {
                var dateTimeConverter = new IsoDateTimeConverter();
                // Default for IsoDateTimeConverter is yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK
                dateTimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm";

                var settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.Objects;
                settings.Converters = new List<JsonConverter>
                                                                                {
                                                                                    dateTimeConverter
                                                                                };

                var json = JsonConvert.SerializeObject(descriptor, settings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                using (Task<HttpResponseMessage> response = client.PostAsync(new Uri("http://pc-dle-2.covis.lan/Example.WebApi/api/Project/ProjectToModel"), content))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        string jsonContent = response.GetAwaiter().GetResult().Content.ReadAsStringAsync().Result;

                        return JsonConvert.DeserializeObject<IEnumerable<TDto>>(jsonContent);
                    }

                    return null;
                }
            }

            return null;
        }

        

        #endregion
    }
}