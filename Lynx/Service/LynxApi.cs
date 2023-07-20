using Domain.Models;
using Lynx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.Service
{
    public class LynxApi 
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        public LynxApi()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<T>> GetRefreshDataListAsync<T>(Uri uri )
        {

            List<T> RefreshData = new();
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    RefreshData = JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return RefreshData;
        }
     
        public async Task<LoggedInUserModel> LoginUser(Uri uri,string login, string password)
        {
         
            string json = JsonSerializer.Serialize(new { login, password }, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LoggedInUserModel>(responseContent, _serializerOptions);
            }
            return  null;
        }

        public async Task  CreateEntityAsync<T>(Uri uri , T entity)
        {
            string json = JsonSerializer.Serialize<T>(entity, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            var   response = await _client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                throw new NullReferenceException($"track list is null; response status code:{response.StatusCode}");
         
        } 
        public async Task<SearchDeparture> GetDepartureByIdAsync(Uri uri )
        {

           // _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var   response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SearchDeparture>(responseContent, _serializerOptions);
            }
            return null;

        }

    }
}
