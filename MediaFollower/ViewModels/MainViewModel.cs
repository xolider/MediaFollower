using MediaFollower.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaFollower.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        private BitmapSource _userPicture;
        public BitmapSource UserPicture
        {
            get { return _userPicture; }
            set
            {
                _userPicture = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            SetUserName();
        }

        private async void SetUserName()
        {
            var users = await User.FindAllAsync();
            var current = users.Where(p => p.AuthenticationStatus == UserAuthenticationStatus.LocallyAuthenticated &&
                p.Type == UserType.LocalUser).FirstOrDefault();
            var name = await current.GetPropertyAsync(KnownUserProperties.FirstName) as string;
            UserName = name;

            var picture = await current.GetPictureAsync(UserPictureSize.Size64x64);
            if(picture != null)
            {
                var stream = await picture.OpenReadAsync();
                var bitImage = new BitmapImage();
                bitImage.SetSource(stream);
                UserPicture = bitImage;
            }
        }
    }
}
