using System;
using System.Collections.Generic;
using System.Text;

namespace MediaFollower.Common
{
    public interface ILocalStorage
    {

        bool IsCached(string filename, StorageFoldersEnum folder);

        void Store(string filename, byte[] content, StorageFoldersEnum folder);

        string Retrieve(string filename, StorageFoldersEnum folder);

        DateTimeOffset FileTime(string filename, StorageFoldersEnum folder);
    }
}
