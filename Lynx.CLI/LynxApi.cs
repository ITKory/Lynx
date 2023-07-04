using Domain.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lynx.CLI
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

            Uri uri = new ("https://localhost:7065/api/departure/all");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
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
