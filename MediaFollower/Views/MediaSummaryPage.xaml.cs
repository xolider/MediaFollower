using MediaFollower.Models;
using MediaFollower.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaFollower.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MediaSummaryPage : Page
    {
        private MediaSummaryViewModel _vm;
        public MediaSummaryPage()
        {
            this.InitializeComponent();

            _vm = this.DataContext as MediaSummaryViewModel;

            /*
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            titleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;

            TitleBar_LayoutMetricsChanged(titleBar, null);
            */

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            TitleBar.Height = coreTitleBar.Height;
            Window.Current.SetTitleBar(MainTitleBar);
        }

        private void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            //BackButton.Margin = new Thickness(10, sender.Height, 0, 0);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = Window.Current.Content as Frame;
            frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Movie movie = e.Parameter as Movie;
            PosterImage.Source = StringToUriConverter.Convert(movie.PosterPath, typeof(BitmapImage), null, null) as BitmapImage;
            MediaTitle.Text = movie.Title;
            _vm.Id = movie.Id;

            var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("MediaForwardAnimation");
            if(anim != null)
            {
                anim.TryStart(MediaPoster);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if(e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("MediaBackAnimation", MediaPoster);
            }
        }
    }
}
