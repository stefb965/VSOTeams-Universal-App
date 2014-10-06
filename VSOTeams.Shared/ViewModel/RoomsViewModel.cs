using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.DataModel;
using VSOTeams.Interfaces;
using VSOTeams.Model;

namespace VSOTeams.ViewModel
{
    public class RoomsViewModel : ViewModelBaseState
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public RoomsViewModel(DataService dataService, INavigationService navigationService)
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
            await LoadRooms(forceRefresh);
        }

        private async Task LoadRooms(bool forceRefresh)
        {
            IsLoading = true;
            LoadingText = "Loading Visual Studio Online users";
            try
            {
                TeamRooms = await TeamRoomDataSource.GetAllTeamRoomsAsync(forceRefresh);

                if (TeamRooms.Count == 0)
                {
                    LoadingText = "No Team Rooms found.";
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


        private const string TeamRoomsPropertyName = "TeamRooms";
        private ObservableCollection<TeamRoom> _teamRooms;
        public ObservableCollection<TeamRoom> TeamRooms
        {
            get { return _teamRooms ?? (_teamRooms = new ObservableCollection<TeamRoom>()); }
            set { Set(TeamRoomsPropertyName, ref _teamRooms, value); }
        }

        public const string SelectedTeamRoomPropertyName = "SelectedTeamRoom";
        private TeamRoom _selectedTeamRoom;
        public TeamRoom SelectedTeamRoom
        {
            get { return _selectedTeamRoom; }
            set
            {
                Set(SelectedTeamRoomPropertyName, ref _selectedTeamRoom, value);
                if (value != null)
                {
                    _navigationService.Navigate(typeof(TeamRoomHubPage), _selectedTeamRoom);
                }
            }
        }

    }
}
