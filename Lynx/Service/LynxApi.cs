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
        private string emulator = "http://10.0.2.2:5008/";
        private string phone = "http://192.168.31.236:5008/";
        public LynxApi()
        {
            _client = new HttpClient
            {
                Timeout = new (0, 0, 10)
            };
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<T>> GetRefreshDataListAsync<T>(string path )
        {
            var uri = new Uri(phone + path);

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
     
        public async Task<LoggedInUserModel> LoginUser(string path,string login, string password)
        {
            var uri = new Uri(phone + path);

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

        public async void CreateEntityAsync<T>(string path , T entity)
        {
            var uri = new Uri(phone + path);


            var accessToken = await SecureStorage.Default.GetAsync("access_token");
            if (accessToken != null && _client.DefaultRequestHeaders.Contains("Authorization")  == false)
            {
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }

            string json = JsonSerializer.Serialize<T>(entity, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            var   response = await _client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                throw new NullReferenceException($" response status code:{response.StatusCode}");
        }

        public async void CreateRequestAsync(string path, SearchRequest request)
        {
            var uri = new Uri(phone + path);

            string json = JsonSerializer.Serialize(request, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                throw new NullReferenceException($" response status code:{response.StatusCode}");
        }
        public async void CloseDepartureAsync(string path, int departureId)
        {
            var uri = new Uri(phone + path);
            string json = JsonSerializer.Serialize(departureId, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                throw new NullReferenceException($" response status code:{response.StatusCode}");
        }
        public async void RegistrationSeekerAsync(string path, int userId  , int departureId, TimeOnly startTime)
        {
            var uri = new Uri(phone + path);
 
            string json = JsonSerializer.Serialize(new {  departureId,userId,startTime }, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                throw new NullReferenceException($" response status code:{response.StatusCode}");
        }

  
        public async Task<SearchDeparture> GetDepartureByIdAsync(string path )
        {
            var uri = new Uri(phone + path);


            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SearchDeparture>(responseContent, _serializerOptions);
            }
            return null;

        }
        public async Task<SearchRequest> GetRequestByIdAsync(string path)
        {
            var uri = new Uri(phone + path);


            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SearchRequest>(responseContent, _serializerOptions);
            }
            return null;

        }    
        public async void UpdateRequestListItemAsync(string path , ListItemModel item)
        {
            var uri = new Uri(phone + path);
            string json = JsonSerializer.Serialize(item, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(uri,content);
            if (!response.IsSuccessStatusCode)
                throw new NullReferenceException($" response status code:{response.StatusCode}");
        }
        public async void RemoveRequestListItemByIdAsync(string path)
        {
            var uri = new Uri(phone + path);
            var response = await _client.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
                throw new NullReferenceException($" response status code:{response.StatusCode}");
        }

        public async Task<Profile> GetProfileAsync(string path)
        {
            var uri = new Uri(phone + path);


            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Profile>(responseContent, _serializerOptions);
            }
            else
            {
                throw new NullReferenceException($" response status code:{response.StatusCode}");
            }
        }
     
        public async  Task<List<ParticipantModel>> GetParticipantsAsync(string path)
        {
            var uri = new Uri(phone + path);
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ParticipantModel>>(responseContent, _serializerOptions);
            }
            else
            {
                throw new NullReferenceException($" response status code:{response.StatusCode}");
            }
 

        }

    }
}
