using MediaFollower.Common;
using MediaFollower.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaFollower.Converters
{
    internal class StringToUriConverter : IValueConverter
    {
        private const string _baseUri = "https://image.tmdb.org/t/p/original";

        private ILocalStorage _localStorage = App.LocalStorage;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string path = value as string;
            path = path.Substring(1, path.Length - 1);
            if(_localStorage.IsCached(path, StorageFoldersEnum.IMAGES))
            {
                path = "ms-appdata:///local/Images/" + path;
            }
            else
            {
                (((Window.Current.Content as Frame).Content as Page).DataContext as ViewModelBase).DownloadAndCache(_baseUri, path);
                path = _baseUri + "/" + path;
            }
            return new BitmapImage(new Uri(path));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
