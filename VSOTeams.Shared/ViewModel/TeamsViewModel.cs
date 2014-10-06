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
    public class TeamsViewModel : ViewModelBaseState
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private Team _currentTeam;

        public TeamsViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        public async override void Activate(object parameter)
        {
            _currentTeam = (Team)parameter;
            TeamName = _currentTeam.name;

            await LoadData(false);
        }

        public override void Deactivate(object parameter)
        {
            TeamMembers = null;
        }

        private async Task LoadData(bool forceRefresh)
        {
            await LoadTeamMemebers(forceRefresh);
        }

        private async Task LoadTeamMemebers(bool forceRefresh)
        {
            IsLoading = true;
            LoadingText = "Loading team members from Visual Studio Online";
            try
            {
                 ObservableCollection<TeamMember> tms = await TeamMemberDataSource.GetAllTeamMembersPerTeamAsync(_currentTeam);
                 foreach (TeamMember tm in tms)
                 {
                    TeamMembers.Add(await tm.ToVSOUser(tm));
                 }
                
                if (TeamMembers.Count == 0)
                {
                    LoadingText = "No Team Members found.";
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

        private const string TeamMembersPropertyName = "TeamMembers";
        private ObservableCollection<VSOUser> _teamMembers;
        public ObservableCollection<VSOUser> TeamMembers
        {
            get { return _teamMembers ?? (_teamMembers = new ObservableCollection<VSOUser>()); }
            set { Set(TeamMembersPropertyName, ref _teamMembers, value); }
        }

       
        public const string SelectedTeamMemberPropertyName = "SelectedTeamMember";
        private VSOUser _selectedTeamMember;
        public VSOUser SelectedTeamMember
        {
            get { return _selectedTeamMember; }
            set
            {
                Set(SelectedTeamMemberPropertyName, ref _selectedTeamMember, value);
                if (value != null)
                {
                    _navigationService.Navigate(typeof(UserDetailsHubPage), _selectedTeamMember);
                }
            }
        }


        public const string TeamNamePropertyName = "TeamName";
        private string _teamName;
        public string TeamName
        {
            get { return _teamName; }
            set
            {
                Set(TeamNamePropertyName, ref _teamName, value);
            }
        }

        private RelayCommand _refreshTeamMemebers;
        public RelayCommand RefeshTeamMemebersCommand
        {
            get
            {
                return _refreshTeamMemebers ?? (_refreshTeamMemebers = new RelayCommand(RefeshTeamMemebersAsync));
            }
        }

        private async void RefeshTeamMemebersAsync()
        {
            await LoadTeamMemebers(true);
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
