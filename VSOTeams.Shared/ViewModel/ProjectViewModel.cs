using Microsoft.WindowsAzure.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VSOTeams.Common;
using VSOTeams.DataModel;
using VSOTeams.Interfaces;
using VSOTeams.Model;
using Windows.Networking.PushNotifications;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace VSOTeams.ViewModel
{
    public class ProjectViewModel : ViewModelBaseState
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public ProjectViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        public async override void Activate(object parameter)
        {
            await LoadData(false);
        }


        private async Task LoadData(bool forceRefresh)
        {
            await LoadProjects(forceRefresh);
        }
        
        private async Task LoadProjects(bool forceRefresh)
        {
            IsLoading = true;
            IsLoadingUsers = false;

            LoadingText = "Loading teams from Visual Studio Online";
            try
            {
                Projects = await ProjectDataSource.GetProjectsAsync(forceRefresh);
                
                if (Projects.Count == 0)
                {
                    LoadingText = "No projects found.";
                }
                else
                {
                    LoadingText = "";
                    await LoadAllOtherData(forceRefresh);
                }

            }
            catch (Exception ex)
            {
                LoadingText = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
            
        }

        private async Task LoadAllOtherData(bool forceRefresh)
        {
            LoadingText = "Loading VSO users...";
            var AllVSOUsers = await VSOUsersDataSource.GetAllVSOUsersAsync(forceRefresh);
            IsLoadingUsers = true;
            IsLoadingRooms = true;
            LoadingText = "";
        }

        public const string IsLoadingUsersPropertyName = "IsLoadingUsers";
        private bool _isLoadingUsers;
        public bool IsLoadingUsers
        {
            get { return _isLoadingUsers; }
            set { Set(IsLoadingUsersPropertyName, ref _isLoadingUsers, value); }
        }

        public const string IsLoadingRoomsPropertyName = "IsLoadingRooms";
        private bool _isLoadingRooms;
        public bool IsLoadingRooms
        {
            get { return _isLoadingRooms; }
            set { Set(IsLoadingRoomsPropertyName, ref _isLoadingRooms, value); }
        }

        private const string ProjectsPropertyName = "Projects";
        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get { return _projects ?? (_projects = new ObservableCollection<Project>()); }
            set { Set(ProjectsPropertyName, ref _projects, value); }
        }


        public const string SelectedProjectPropertyName = "SelectedProject";
        private Project _selectedProject;
        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                Set(SelectedProjectPropertyName, ref _selectedProject, value);
                if (value != null)
                {
                    _navigationService.Navigate(typeof(ProjectTeamsHubPage), _selectedProject);
                }
            }
        }



        private RelayCommand _refreshProjects;
        public RelayCommand RefeshProjectsCommand
        {
            get
            {
                return _refreshProjects ?? (_refreshProjects = new RelayCommand(RefeshProjectsAsync));
            }
        }

        private async void RefeshProjectsAsync()
        {
            await LoadProjects(true);
        }

        //

        private RelayCommand _gotoRoomsPage;
        public RelayCommand GotoRoomsPageCommand
        {
            get
            {
                return _gotoRoomsPage ?? (_gotoRoomsPage = new RelayCommand(GotoRoomsPage));
            }
        }

        private void GotoRoomsPage()
        {
            _navigationService.Navigate(typeof(TeamRoomsHubPage));
        }

        private RelayCommand _gotoAllUsersPage;
        public RelayCommand GotoAllUsersPageCommand
        {
            get
            {
                return _gotoAllUsersPage ?? (_gotoAllUsersPage = new RelayCommand(GotoAllUsersPage));
            }
        }

        private void GotoAllUsersPage()
        {
            _navigationService.Navigate(typeof(AllUsersHubPage));
        }


    }
}
