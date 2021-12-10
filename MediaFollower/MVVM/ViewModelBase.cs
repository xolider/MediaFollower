using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBAPI;

namespace MediaFollower.MVVM
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string UserLanguage { get; private set; }

        protected TMDBApi Api { get; private set; } = new TMDBApi();

        protected ViewModelBase()
        {
            var topUserLanguage = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
            UserLanguage = new Windows.Globalization.Language(topUserLanguage).LanguageTag;
        }

        protected void RaisePropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void DownloadAndCache(string url, string path)
        {
            Task.Run(async () =>
            {
                var bytes = await Api.DownloadFile(url + "/" + path);
                App.LocalStorage.Store(path, bytes, Common.StorageFoldersEnum.IMAGES);
            });
        }
    }
}
