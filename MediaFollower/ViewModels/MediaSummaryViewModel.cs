using MediaFollower.Models;
using MediaFollower.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFollower.ViewModels
{
    internal class MediaSummaryViewModel : ViewModelBase
    {
        private string _summary;
        public string Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                RaisePropertyChanged();
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                LoadMovie();
            }
        }

        public MediaSummaryViewModel()
        {

        }

        private async void LoadMovie()
        {
            var movie = await Api.GetDetails<Movie>(_id, UserLanguage);
            Summary = movie.Overview;
        }
    }
}
