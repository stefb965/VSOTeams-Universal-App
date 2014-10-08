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
using Windows.Storage;
using Windows.UI.Xaml.Media;

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
                string uriString = "https://" + credentials.Account + ".VisualStudio.com/DefaultCollection/_apis/Chat/rooms";

                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                TeamRooms allTeamRooms = JsonConvert.DeserializeObject<TeamRooms>(responseBody);

                foreach (var room in allTeamRooms.value)
                {
                    room.ImageLocation = await DownloadTeamRoomImagesFromRestAsync(room);
                    room.lastActivity = String.Format("Last activity on: {0:ddd, MMM d}", Convert.ToDateTime(room.lastActivity));
                    TeamRooms.Add(room);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> DownloadTeamRoomImagesFromRestAsync(TeamRoom room)
        {
            var _destinationFolder = ApplicationData.Current.LocalFolder;

            var pictureFilename = room.name + ".png";
            StorageFile file = await _destinationFolder.CreateFileAsync(pictureFilename, CreationCollisionOption.ReplaceExisting);

            Helpers.CreateHttpClient(ref _httpClient);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(room.createdBy.imageUrl));
            HttpResponseMessage response = await _httpClient.SendRequestAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var stream = await response.Content.ReadAsBufferAsync();
            await Windows.Storage.FileIO.WriteBufferAsync(file, stream);
            return file.Path;
        }
    }
}

