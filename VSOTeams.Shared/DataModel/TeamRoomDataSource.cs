using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Web.Http;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Common;
using VSOTeams.Model;
using Windows.Data.Json;
using Windows.UI.Xaml.Controls;

namespace VSOTeams.DataModel
{
    public class TeamRoomDataSource
    {
        private static TeamRoomDataSource _teamRoomDataSource = new TeamRoomDataSource();
        private HttpClient _httpClient;

        private ObservableCollection<TeamRoom> _teamRooms = new ObservableCollection<TeamRoom>();
        public ObservableCollection<TeamRoom> TeamRooms
        {
            get { return this._teamRooms; }
        }

        public static async Task<ObservableCollection<TeamRoom>> GetAllTeamRoomsAsync(bool forceRefresh)
        {
            await _teamRoomDataSource.GetTeamRoomsDataAsync(forceRefresh);
            return _teamRoomDataSource.TeamRooms;
        }

        private async Task GetTeamRoomsDataAsync(bool forceRefresh)
        {
            if (forceRefresh == true)
                this._teamRooms.Clear();

            if (this._teamRooms.Count != 0)
                return;

            try
            {
                var credentials = CredentialHelper.GetCredential();
                Helpers.CreateHttpClient(ref _httpClient);
                string uriString = "https://" + credentials.Account + "/DefaultCollection/_apis/Chat/rooms";

                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                TeamRooms allTeamRooms = JsonConvert.DeserializeObject<TeamRooms>(responseBody);

                //var roomsImage = new Image { Source = new FileImageSource { File = "room.png" } };
                foreach (var room in allTeamRooms.value)
                {
                    //room.ImageUri = roomsImage.Source;
                    room.lastActivity = String.Format("Last activity on: {0:ddd, MMM d}", Convert.ToDateTime(room.lastActivity));
                    TeamRooms.Add(room);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

