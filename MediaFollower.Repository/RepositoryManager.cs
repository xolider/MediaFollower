using MediaFollower.Common;
using System;
using System.Collections.Generic;
using System.Text;
using TMDBAPI;

namespace MediaFollower.Repository
{
    public class RepositoryManager
    {
        private static RepositoryManager _instance;
        public static RepositoryManager Instance
        {
            get { return _instance ?? (_instance = new RepositoryManager()); }
        }

        internal static ILocalStorage Cache { get; private set; }

        internal static TMDBApi Api { get; private set; } = new TMDBApi();

        public MoviesRepository MoviesRepository { get; private set; } = new MoviesRepository();

        private RepositoryManager()
        {

        }

        public static void InitializeCache(ILocalStorage cache)
        {
            Cache = cache;
        }
    }
}
