using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Common;
using VSOTeams.DataModel;
using VSOTeams.Interfaces;

namespace VSOTeams.ViewModel
{
    public class ViewModelLocator
    {
     
        public ProjectViewModel ProjectsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectViewModel>();
            }
        }

        public ProjectTeamsViewModel ProjectTeamsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectTeamsViewModel>();
            }
        }

        public TeamsViewModel TeamsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeamsViewModel>();
            }
        }

        public UserViewModel UserVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserViewModel>();
            }
        }

        public AllUsersViewModel AllUsersVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AllUsersViewModel>();
            }
        }

        public TeamRoomsViewModel TeamRoomsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeamRoomsViewModel>();
            }
        }


        public MessagesViewModel MessagesVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MessagesViewModel>();
            }
        }
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<INavigationService>(() => new NavigationService());

            SimpleIoc.Default.Register<ProjectViewModel>();
            SimpleIoc.Default.Register<ProjectTeamsViewModel>();
            SimpleIoc.Default.Register<TeamsViewModel>();
            SimpleIoc.Default.Register<AllUsersViewModel>();
            SimpleIoc.Default.Register<UserViewModel>();

            SimpleIoc.Default.Register<TeamRoomsViewModel>();
            SimpleIoc.Default.Register<MessagesViewModel>();
        }
    }

}
