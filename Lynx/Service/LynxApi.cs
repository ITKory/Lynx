using Domain.Models;
using Lynx.Models;
using System.Text;

namespace Lynx.Service
{
    public class LynxApi 
    {
        private HttpClient _client;
        private JsonSerializerOptions _serializerOptions;
        private string phone = "http://192.168.1.95:5008/";
        public LynxApi()    
        {
            _client = new HttpClient
            {
                Timeout = new (0, 0, 20),
            };
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        private async Task<string> PostAsync<T>(string path, T entity)
        {
            var uri = new Uri(phone + path);
            string json = JsonSerializer.Serialize<T>(entity, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                return string.Empty;
        }

        private async Task<string> PutAsync<T>(string path, T entity)
        {
            var uri = new Uri(phone + path);
            string json = JsonSerializer.Serialize<T>(entity, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(uri, content);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                return string.Empty;
        }

        private async Task<string> GetAsync( string path )
        {
            var uri = new Uri(phone + path);
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
                return  await response.Content.ReadAsStringAsync();
            else
                return string.Empty;
            
        } 
        private async Task<string> DeleteAsync( string path )
        {
            var uri = new Uri(phone + path);
            var response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                return string.Empty;
        }
        public async Task<List<T>> GetDataListAsync<T>(string path )
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
     
        public async Task<LoggedInUserModel> LoginUser(string login, string password)
        {
           var responseContent = await PostAsync("api/user/login", new { login, password });
            if (responseContent == string.Empty)
                  throw new Exception("error");
            else
                return JsonSerializer.Deserialize<LoggedInUserModel>(responseContent, _serializerOptions);
        }


        public async Task<User> CreateUser(User user)
        {
            var responseContent = await PostAsync("api/user/add", user);
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<User>(responseContent, _serializerOptions);
        }

        public async Task<SearchRequest> CreateRequestAsync( SearchRequest request)
        {
            var responseContent = await PostAsync("api/request/add", request);
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<SearchRequest>(responseContent, _serializerOptions);
        }
        public async Task<SearchDeparture> CreateDepartureAsync(SearchDeparture request)
        {
            var responseContent = await PostAsync("api/departure/add", request);
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<SearchDeparture>(responseContent, _serializerOptions);
        }

        public async Task<SearchRequest> RegistrationSeekerAsync( int userId  , int departureId, TimeOnly startTime )
        {

            var responseContent = await PostAsync("api/departure/registration", new {userId,departureId, startTime});
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<SearchRequest>(responseContent, _serializerOptions);
        }

  
        public async Task<SearchDeparture> GetDepartureByIdAsync(int id )
        {
            var responseContent = await GetAsync($"api/departure/get?departureId={id}");
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<SearchDeparture>(responseContent, _serializerOptions);

        }
        public async Task<SearchRequest> GetRequestByIdAsync(int id)
        {
            var responseContent = await GetAsync($"api/request/get?requestId={id}");
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<SearchRequest>(responseContent, _serializerOptions);

        }    
        public async Task<ListItemModel> UpdateDepartureListItemAsync(ListItemModel item)
        {
            var responseContent = await PutAsync("api/departure/update", item);
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<ListItemModel>(responseContent, _serializerOptions);
        }    
        public async Task<ListItemModel> UpdateRequestListItemAsync(ListItemModel item)
        {
            var responseContent = await PutAsync("api/request/update", item);
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<ListItemModel>(responseContent, _serializerOptions);
        }
        public async Task<User> RemoveDepartureListItemByIdAsync(int id)
        {
            var responseContent = await DeleteAsync($"api/departure/remove?departureId={id}");
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<User>(responseContent, _serializerOptions);
        } 
        public async Task<User> RemoveRequestListItemByIdAsync(int id)
        {
            var responseContent = await DeleteAsync($"api/request/remove?requestId={id}");
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<User>(responseContent, _serializerOptions);
        }

        public async Task<Profile> GetProfileByIdAsync(int id)
        {
            var responseContent = await GetAsync($"api/profile/get?userId={id}");
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<Profile>(responseContent, _serializerOptions);
        }
     
        public async  Task<List<ParticipantModel>> GetParticipantsByDepartureIdAsync(int id)
        {
            var responseContent = await GetAsync($"api/departure/participants?departureId={id}");
            if (responseContent == string.Empty)
                return null;
            else
                return JsonSerializer.Deserialize<List<ParticipantModel>>(responseContent, _serializerOptions);
        }

    }
}
