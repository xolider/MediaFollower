using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MediaFollower.Repository.Converters
{
    internal class UriConverter : JsonConverter<Uri>
    {
        private const string _baseUri = "https://image.tmdb.org/t/p/original";

        public override Uri ReadJson(JsonReader reader, Type objectType, Uri existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var path = reader.Value as string;
            path = path.Substring(1, path.Length-1);
            if(!RepositoryManager.Cache.IsCached(path, Common.StorageFoldersEnum.IMAGES))
            {
                Task.Run(async () =>
                {
                    var content = await RepositoryManager.Api.DownloadFile(_baseUri + "/" + path);
                    RepositoryManager.Cache.Store(path, content, Common.StorageFoldersEnum.IMAGES);
                });
                Debug.WriteLine("not cached");
                return new Uri(_baseUri + "/" + path);
            }
            return RepositoryManager.Cache.Retrieve(path, Common.StorageFoldersEnum.IMAGES);
        }

        public override void WriteJson(JsonWriter writer, Uri value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
