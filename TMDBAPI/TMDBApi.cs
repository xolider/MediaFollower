using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace TMDBAPI
{
    public class TMDBApi
    {
        private readonly RestClient _client = new RestClient("https://api.themoviedb.org/3");

        public TMDBApi()
        {
            _client.AddDefaultParameter("api_key", "d53bea9a896c69cd259ab241972b816f", ParameterType.QueryString);
        }

        public async Task<TMDBResult<T>> GetPopulars<T>(int page = 1, string lang = "en-US", JsonConverter converter = null)
        {
            var request = new RestRequest("movie/popular");
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("language", lang);
            var response = await _client.ExecuteAsync(request, Method.GET);
            return Deserialize<TMDBResult<T>>(response.Content, converter);
        }

        public async Task<byte[]> DownloadFile(string url)
        {
            var client = new RestClient();
            return (await client.ExecuteAsync(new RestRequest(url))).RawBytes;
        }

        public async Task<TMDBResult<T>> GetTopRated<T>(int page = 1, string lang = "en-US", JsonConverter converter = null)
        {
            var request = new RestRequest("movie/top_rated");
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("language", lang);
            var response = await _client.ExecuteAsync(request, Method.GET);
            return Deserialize<TMDBResult<T>>(response.Content, converter);
        }

        private T Deserialize<T>(string content, JsonConverter converter)
        {
            var converters = new List<JsonConverter>();
            if(converter != null) converters.Add(converter);
            return JsonConvert.DeserializeObject<T>(content, converters.ToArray());
        }
    }
}
