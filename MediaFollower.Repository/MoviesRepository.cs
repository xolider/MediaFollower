using MediaFollower.Models;
using MediaFollower.Repository.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMDBAPI;

namespace MediaFollower.Repository
{
    public class MoviesRepository
    {
        internal MoviesRepository()
        {

        }

        public async Task<TMDBResult<Movie>> GetPopulars(int page = 1, string language = "en-US")
        {
            var movies = await RepositoryManager.Api.GetPopulars<Movie>(converter: new UriConverter(), page: page, lang: language);
            return movies;
        }

        public async Task<TMDBResult<Movie>> GetTopRated(int page = 1, string language = "en-US")
        {
            return await RepositoryManager.Api.GetTopRated<Movie>(converter: new UriConverter(), page: page, lang: language);
        }
    }
}
