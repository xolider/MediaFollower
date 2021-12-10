using System;
using System.Collections.Generic;
using System.Text;

namespace TMDBAPI
{
    public class TMDBResult<T>
    {
        public int Page { get; set; }

        public T[] Results { get; set; }

        public int TotalResults { get; set; }

        public int TotalPages { get; set; }
    }
}
