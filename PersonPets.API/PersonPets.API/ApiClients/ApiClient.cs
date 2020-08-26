using PersonPets.API.ApiClients.Interfaces;
using PersonPets.API.Common.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PersonPets.API.ApiClients
{
    public class ApiClient : IApiClient
    {
        private readonly string _baseApiUrl;
        private readonly IJsonSerializer _jsonSerializer;
        public ApiClient(string baseApiUrl, IJsonSerializer jsonSerializer)
        {
            _baseApiUrl = baseApiUrl;
            _jsonSerializer = jsonSerializer;
        }


        public async Task<T> GetAsync<T>(string uri)
        {
            using (HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_baseApiUrl)
            })
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    string message = $"Error during GET request for {_baseApiUrl}{uri}. Response Content: {responseContent}";
                    throw new Exception(message);
                }

                T responseData = _jsonSerializer.Deserialize<T>(responseContent);

                return responseData;
            }

        }
}