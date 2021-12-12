using MediaFollower.Models;
using MediaFollower.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Media.Imaging;
using MediaFollower.Extensions;
using System.Diagnostics;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.System.Threading;

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

        private string _test;
        public string Test
        {
            get => _test;
            set
            {
                _test = value;
                RaisePropertyChanged();
            }
        }

        private bool _popularsLoading = true;
        public bool PopularsLoading
        {
            get => _popularsLoading;
            set
            {
                _popularsLoading = value;
                RaisePropertyChanged();
                _popularsLoadMoreCommand.RaiseCanExecute();
            }
        }

        private bool _topRatedLoading = true;
        public bool TopRatedLoading
        {
            get => _topRatedLoading;
            set
            {
                _topRatedLoading = value;
                RaisePropertyChanged();
                _topRatedLoadMoreCommand.RaiseCanExecute();
            }
        }

        private ObservableCollection<Movie> _popularMovies = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> PopularMovies => _popularMovies;

        private ObservableCollection<Movie> _topRatedMovies = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> TopRatedMovies => _topRatedMovies;

        private CommandBase _popularsLoadMoreCommand;
        public CommandBase PopularsLoadMoreCommand => _popularsLoadMoreCommand;

        private CommandBase _topRatedLoadMoreCommand;
        public CommandBase TopRatedLoadMoreCommand => _topRatedLoadMoreCommand;

        private int _popularsPage = 1;
        private int _topRatedPage = 1;

        public MainViewModel() : base()
        {
            _popularsLoadMoreCommand = new CommandBase(p => !_popularsLoading, async p =>
            {
                PopularsLoading = true;
                _popularsPage++;
                await SetPopularMovies();
            });

            _topRatedLoadMoreCommand = new CommandBase(p => !_topRatedLoading, async p =>
            {
                TopRatedLoading = true;
                _topRatedPage++;
                await SetTopRatedMovies();
            });

            SetupUI();
        }

        private async void SetupUI()
        {
            await SetUserName();
            await SetPopularMovies();
            await SetTopRatedMovies();
        }

        private async Task SetUserName()
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

        private async Task SetPopularMovies()
        {
            var movies = await Api.GetPopulars<Movie>(_popularsPage, UserLanguage);
            _popularMovies.AddRange(movies.Results);
            PopularsLoading = false;
        }

        private async Task SetTopRatedMovies()
        {
            var movies = await Api.GetTopRated<Movie>(_topRatedPage, UserLanguage);
            _topRatedMovies.AddRange(movies.Results);
            TopRatedLoading = false;
        }
    }
}
