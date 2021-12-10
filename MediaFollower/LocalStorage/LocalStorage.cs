using MediaFollower.Common;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;

namespace MediaFollower.LocalStorage
{
    internal class LocalStorage : ILocalStorage
    {
        private StorageFolder _cacheFolder;

        public LocalStorage()
        {
            _cacheFolder = ApplicationData.Current.LocalFolder;
            CreateStructureAsync();
        }

        private async void CreateStructureAsync()
        {
            await _cacheFolder.CreateFolderAsync("Images", CreationCollisionOption.OpenIfExists);
        }

        public bool IsCached(string filename, StorageFoldersEnum folder)
        {
            var storageFolder = GetFolder(folder);
            return storageFolder.GetFilesAsync().AsTask().Result.Any(file => file.Name == filename);
        }

        public void Store(string filename, byte[] content, StorageFoldersEnum folder)
        {
            var storageFolder = GetFolder(folder);
            ulong size = storageFolder.GetBasicPropertiesAsync().AsTask().Result.Size;
            Debug.WriteLine(size);
            var file = storageFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting).AsTask().Result;
            FileIO.WriteBytesAsync(file, content).AsTask().Wait();
        }

        public string Retrieve(string filename, StorageFoldersEnum folder)
        {
            var storageFolder = GetFolder(folder);
            var file = storageFolder.GetFileAsync(filename).AsTask().Result;
            return file.Path;
        }

        public DateTimeOffset FileTime(string filename, StorageFoldersEnum folder)
        {
            var storageFolder = GetFolder(folder);
            var file = storageFolder.GetFileAsync(filename).AsTask().Result;
            return file.DateCreated;
        }

        private StorageFolder GetFolder(StorageFoldersEnum folder)
        {
            switch(folder)
            {
                case StorageFoldersEnum.IMAGES:
                    return _cacheFolder.GetFolderAsync("Images").AsTask().Result;
                default:
                    return _cacheFolder;
            }
        }
    }
}
