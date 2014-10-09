using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Common;
using VSOTeams.DataModel;
using VSOTeams.Interfaces;
using VSOTeams.Model;

namespace VSOTeams.ViewModel
{
    public class UserViewModel : ViewModelBaseState
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        

        public UserViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        public async override void Activate(object parameter)
        {
            VisualStudioOnlineUser = (VSOUser)parameter;
           
            await LoadData(false);
        }
        private async Task LoadData(bool forceRefresh)
        {
            await LoadVSOUser(forceRefresh);
        }

        private async Task LoadVSOUser(bool forceRefresh)
        {
            IsLoading = true;
            LoadingText = "Loading Visual Studio Online user properties";
            try
            {
                VisualStudioOnlineUser.teams = await TeamDataSource.GetTeamsForVSOUserAsync(VisualStudioOnlineUser);

                if (VisualStudioOnlineUser == null)
                {
                   
                    LoadingText = "Visual Studio User not found.";
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

        private const string VisualStudioOnlineUserPropertyName = "VisualStudioOnlineUser";
        private VSOUser _visualStudioOnlineUser;
        public VSOUser VisualStudioOnlineUser
        {
            get { return _visualStudioOnlineUser ?? (_visualStudioOnlineUser = new VSOUser()); }
            set { Set(VisualStudioOnlineUserPropertyName, ref _visualStudioOnlineUser, value); }
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
