using MediaFollower.Models;
using MediaFollower.ViewModels;
using MediaFollower.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
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
    public sealed partial class MainPage : Page
    {
        private UIElement _selectedElementNav;

        private MainViewModel _vm;

        private readonly object _imageLoadingLock = new object();

        public MainPage()
        {
            this.InitializeComponent();
            _vm = this.DataContext as MainViewModel;
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            TitleBar.Height = coreTitleBar.Height;
            Window.Current.SetTitleBar(MainTitleBar);
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            TitleBar.Height = sender.Height + 20;
            RightMask.Width = sender.SystemOverlayRightInset;
        }

        private void PopularPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var panel = sender as StackPanel;
            _selectedElementNav = panel;
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("MediaForwardAnimation", panel);
            Frame window = Window.Current.Content as Frame;
            window.Navigate(typeof(MediaSummaryPage), panel.DataContext, new SuppressNavigationTransitionInfo());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if(e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimation anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("MediaBackAnimation");
                if(anim != null)
                {
                    anim.TryStart(_selectedElementNav);
                }
            }
        }

        private async void Help_Clicked(object sender, RoutedEventArgs args)
        {
            AboutDialog about = new AboutDialog();
            await about.ShowAsync();
        }
    }
}
