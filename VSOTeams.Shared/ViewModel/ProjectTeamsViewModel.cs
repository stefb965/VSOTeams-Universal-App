using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Common;
using VSOTeams.DataModel;
using VSOTeams.Interfaces;
using VSOTeams.Model;
using Windows.UI.Xaml.Media.Imaging;

namespace VSOTeams.ViewModel
{
    public class ProjectTeamsViewModel : ViewModelBaseState
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private Project _currentProject;

        public ProjectTeamsViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        public async override void Activate(object parameter)
        {
            _currentProject = (Project)parameter;
            ProjectName = _currentProject.name;
            await LoadData(false);
        }


        private async Task LoadData(bool forceRefresh)
        {
            await LoadProjectTeams(forceRefresh);
        }

        private async Task LoadProjectTeams(bool forceRefresh)
        {
            IsLoading = true;
            LoadingText = "Loading teams from Visual Studio Online";
            try
            {
                ProjectTeams = await TeamDataSource.GetTeamsByProjectIdAsync(_currentProject.id);
                if (ProjectTeams.Count == 0)
                {
                    LoadingText = "No teams found.";
                }
                else
                {
                    LoadingText = "";
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

        public const string ProjectNamePropertyName = "ProjectName";
        private string _projectName;
        public string ProjectName
        {
            get { return _projectName + " project"; }
            set
            {
                Set(ProjectNamePropertyName, ref _projectName, value);
            }
        }



        private const string ProjectTeamsPropertyName = "ProjectTeams";
        private ObservableCollection<Team> _projectTeams;
        public ObservableCollection<Team> ProjectTeams
        {
            get { return _projectTeams ?? (_projectTeams = new ObservableCollection<Team>()); }
            set { Set(ProjectTeamsPropertyName, ref _projectTeams, value); }
        }



        private RelayCommand _refreshProjectTeams;
        public RelayCommand RefeshProjectTeamsCommand
        {
            get
            {
                return _refreshProjectTeams ?? (_refreshProjectTeams = new RelayCommand(RefeshProjectTeamsAsync));
            }
        }

        private async void RefeshProjectTeamsAsync()
        {
            await LoadProjectTeams(true);
        }


        public const string SelectedTeamPropertyName = "SelectedTeam";
        private Team _selectedTeam;
        public Team SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                Set(SelectedTeamPropertyName, ref _selectedTeam, value);
                if (value != null)
                {
                    _navigationService.Navigate(typeof(TeamHubPage), _selectedTeam);
                }
            }
        }

        private RelayCommand _goToMainPage;
        public RelayCommand GoToMainPageCommand
        {
            get
            {
                return _goToMainPage ?? (_goToMainPage = new RelayCommand(GoToMainPage));
            }
        }

        private void GoToMainPage()
        {
            _navigationService.Navigate(typeof(ProjectHubPage));
        }
    }
}