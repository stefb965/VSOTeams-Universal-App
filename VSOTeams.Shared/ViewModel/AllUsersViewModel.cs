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
    public class AllUsersViewModel : ViewModelBaseState
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public AllUsersViewModel(IDataService dataService, INavigationService navigationService)
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
            await LoadAllVSOUsers(forceRefresh);
        }

        private async Task LoadAllVSOUsers(bool forceRefresh)
        {
            IsLoading = true;
            LoadingText = "Loading Visual Studio Online users";
            try
            {
                AllVSOUsers= await VSOUsersDataSource.GetAllVSOUsersAsync(false);

                if (AllVSOUsers.Count == 0 )
                {
                    LoadingText = "No Users found.";
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

        private const string AllVSOUsersPropertyName = "AllVSOUsers";
        private ObservableCollection<VSOUser> _allVSOUsers;
        public ObservableCollection<VSOUser> AllVSOUsers
        {
            get { return _allVSOUsers ?? (_allVSOUsers = new ObservableCollection<VSOUser>()); }
            set { Set(AllVSOUsersPropertyName, ref _allVSOUsers, value); }
        }

       
        private RelayCommand _saveAllUsersToCSV;
        public RelayCommand SaveAllUsersToCSVCommand
        {
            get
            {
                return _saveAllUsersToCSV ?? (_saveAllUsersToCSV = new RelayCommand(SaveAllUsersToCSVAsync));
            }
        }

        public const string SelectedUserPropertyName = "SelectedUser";
        private VSOUser _selectedUser;
        public VSOUser SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                Set(SelectedUserPropertyName, ref _selectedUser, value);
                if (value != null)
                {
                    _navigationService.Navigate(typeof(UserDetailsHubPage), _selectedUser);
                }
            }
        }

        private async void SaveAllUsersToCSVAsync()
        {
            try
            {
                var file = await VSOUsersDataSource.SaveVSOUsersToCSV();
            }
            catch (Exception ex )
            {

                LoadingText = ex.Message;
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
