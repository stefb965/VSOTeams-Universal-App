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
    public class MessagesViewModel : ViewModelBaseState
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private TeamRoom _currentTeamRoom;

        public MessagesViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        public async override void Activate(object parameter)
        {
            _currentTeamRoom = (TeamRoom)parameter;
            await LoadData(false);
        }


        private async Task LoadData(bool forceRefresh)
        {
            await LoadProjects(forceRefresh);
        }

        private async Task LoadProjects(bool forceRefresh)
        {
            IsLoading = true;

            LoadingText = "Loading messages from Visual Studio Online";
            try
            {
                TeamRoomMessages = await TeamRoomMessageDataSource.GetAllRoomMessagesRoomsAsync(forceRefresh, _currentTeamRoom);

                if (TeamRoomMessages.Count == 0)
                {
                    LoadingText = "No messages found.";
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

        private const string TeamRoomMessagesPropertyName = "TeamRoomMessages";
        private ObservableCollection<SimpleRoomMessage> _teamRoomMessages;
        public ObservableCollection<SimpleRoomMessage> TeamRoomMessages
        {
            get { return _teamRoomMessages ?? (_teamRoomMessages = new ObservableCollection<SimpleRoomMessage>()); }
            set { Set(TeamRoomMessagesPropertyName, ref _teamRoomMessages, value); }
        }

        private const string PostMessageTextPropertyName = "PostMessageText";
        private string _postMessageText = "";
        public string PostMessageText
        {
            get { return _postMessageText; }
            set { Set(PostMessageTextPropertyName, ref _postMessageText, value); }
        }


        private RelayCommand _refreshMessages;
        public RelayCommand RefeshMessagesCommand
        {
            get
            {
                return _refreshMessages ?? (_refreshMessages = new RelayCommand(RefeshMessagesAsync));
            }
        }

        private async void RefeshMessagesAsync()
        {
            await LoadData(true);
        }

        private RelayCommand _postMessage;
        public RelayCommand PostMessageCommand
        {
            get
            {
                return _postMessage ?? (_postMessage = new RelayCommand(PostMessageAsync));
            }
        }

        private async void PostMessageAsync()
        {
            await TeamRoomMessageDataSource.PostMessage(PostMessageText);
        }
    }
}
