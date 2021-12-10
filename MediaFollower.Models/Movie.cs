using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MediaFollower.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public string OriginalTitle { get; set; }
        public string Title { get; set; }
        public string OriginalLanguage { get; set; }
        public string BackdropPath { get; set; }

        public override string ToString()
        {
            return "Movie[Title=" + Title + "]";
        }
    }
}
