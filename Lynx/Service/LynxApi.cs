using Domain.Models;
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

        public List<SearchDeparture> Departures { get; private set; }

        public LynxApi()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<SearchDeparture>> RefreshDataAsync()
        {
            Departures = new List<SearchDeparture>();

            Uri uri = new("http://10.0.2.2:5008/api/departure/all");
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Departures = JsonSerializer.Deserialize<List<SearchDeparture>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }


            return Departures;
        }


    }
}
