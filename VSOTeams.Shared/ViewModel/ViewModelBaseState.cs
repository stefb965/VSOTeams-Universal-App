using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Common;
using VSOTeams.Interfaces;
using Windows.UI.Xaml.Media.Imaging;

namespace VSOTeams.ViewModel
{
    public class ViewModelBaseState : ViewModelBase, INavigable
    {

        public virtual void Activate(object parameter)
        {

        }

        public virtual void Deactivate(object parameter)
        {

        }


        public const string LoadingTextPropertyName = "LoadingText";
        private string _loadingTextProject;
        public string LoadingText
        {
            get { return _loadingTextProject; }
            set { Set(LoadingTextPropertyName, ref _loadingTextProject, value); }
        }

        public const string IsLoadingPropertyName = "IsLoading";
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(IsLoadingPropertyName, ref _isLoading, value); }
        }



        public const string ProjectImagePropertyName = "ProjectImage";
        private BitmapImage _projectImage;
        public BitmapImage ProjectImage
        {
            get { return new BitmapImage(new Uri("ms-appx:/Assets/bg.png")); }
            set { Set(ProjectImagePropertyName, ref _projectImage, value); }
        }

    }
}
