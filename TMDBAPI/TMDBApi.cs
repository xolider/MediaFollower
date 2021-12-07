using RestSharp;
using System;
using System.Collections.Generic;
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
    }
}
