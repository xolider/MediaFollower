using MediaFollower.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaFollower.Views.Components
{
    public sealed partial class MediaListView : UserControl
    {

        public static readonly DependencyProperty LoadMoreCommandProperty = DependencyProperty.Register("LoadMoreCommand", typeof(ICommand),
            typeof(MediaListView), new PropertyMetadata(null));

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value);}
        }

        public static readonly DependencyProperty MediasProperty = DependencyProperty.Register("Medias", typeof(ObservableCollection<Movie>), typeof(MediaListView),
            new PropertyMetadata(new ObservableCollection<Movie>()));

        public ObservableCollection<Movie> Medias
        {
            get
            {
                return (ObservableCollection<Movie>)GetValue(MediasProperty);
            }
            set
            {
                SetValue(MediasProperty, value);
            }
        }

        public event TappedEventHandler MediaTapped;

        public MediaListView()
        {
            this.InitializeComponent();
            
        }

        private void ImagePanel_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            panel.CenterPoint = new System.Numerics.Vector3(panel.ActualSize.X / 2.0f, panel.ActualSize.Y / 2.0f, 0);

            panel.StartAnimation(AnimateImage(1.1f));

        }

        private SpringVector3NaturalMotionAnimation AnimateImage(float value)
        {
            var springAnim = Window.Current.Compositor.CreateSpringVector3Animation();
            springAnim.Target = "Scale";
            springAnim.FinalValue = new System.Numerics.Vector3(value);
            return springAnim;
        }

        private void StackPanel_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            panel.CenterPoint = new System.Numerics.Vector3(panel.ActualSize.X / 2.0f, panel.ActualSize.Y / 2.0f, 0);

            panel.StartAnimation(AnimateImage(1.0f));
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MediaTapped?.Invoke(sender, e);
        }
    }
}
