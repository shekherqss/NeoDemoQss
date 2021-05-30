using NeoDemoQss.Data.Remote.Constants;
using NeoDemoQss.Data.Remote.Exceptions;
using NeoDemoQss.Data.Remote.Repository.Contract;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NeoDemoQss.Data.Remote.Repository.Implementation
{
    public class ApisRepository : IApisRepository
    {
        private static bool isProcessing = false;
        public ApisRepository()
        {
        }

        public async Task<T> GetAsync<T>(string uri, string authToken = "", bool hasJsonResponse = false)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);
                string serializedResult = string.Empty;

                var responseMessage = await httpClient.GetAsync(uri);

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (!isProcessing)
                    {
                       //refresh
                    }
                }

                if (responseMessage.IsSuccessStatusCode)
                {
                    serializedResult = await responseMessage.Content.ReadAsStringAsync();

                    if (serializedResult == string.Empty)
                    {
                        serializedResult = "Status: 200";
                    }

                    var responseData = JsonConvert.DeserializeObject<T>(serializedResult);
                    return responseData;
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, serializedResult);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                return default;
            }
        }


        public async Task<T> PostAsync<TData, T>(string uri, TData data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(uri);
                StringContent content;
                content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;


                var responseMessage = await httpClient.PostAsync(uri, content);

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                   // refresh
                }

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<T>(jsonResult);
                    return responseData;
                }



                // throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
                return default;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                return default;
            }
        }


        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(uri);
                string result = string.Empty;
                var content = new StringContent(JsonConvert.SerializeObject(data));
                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    result = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var responseData = JsonConvert.DeserializeObject<T>(result);
                    return responseData;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(result);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, result);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }


        public async Task<T> DeleteAsync<T>(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            var response = await httpClient.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var resp = JsonConvert.DeserializeObject<T>(jsonResult);

                return resp;
            }

            return default;
        }


        private HttpClient CreateHttpClient(string authToken)
        {         
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiConstants.API_BASE_URL);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           

            if (authToken != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }

            return httpClient;
        }

        public async Task<T> SendAsync<T>(string uri, string data, HttpMethod method, string authToken = "")
        {
            try
            {
                HttpClient client = CreateHttpClient(uri);
                string stringResponse = string.Empty;
                HttpRequestMessage request = new HttpRequestMessage(method, uri);
                request.Content = new StringContent(data,
                                        Encoding.UTF8,
                                        "application/json");//CONTENT-TYPE header

                var responseMessage = await client.SendAsync(request);

                if (responseMessage.IsSuccessStatusCode)
                {
                    stringResponse = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var responseData = JsonConvert.DeserializeObject<T>(stringResponse);
                    return responseData;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                   throw new ServiceAuthenticationException(stringResponse);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, stringResponse);
            }
            catch (HttpRequestException ex)
            {
                return default;
            }
        }
    }
}
