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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

namespace VSOTeams.DataModel
{
    public class TeamRoomMessageDataSource
    {
        private static TeamRoomMessageDataSource _teamRoomMessagesDataSource = new TeamRoomMessageDataSource();
        private HttpClient _httpClient;
        private TeamRoom _currentRoom;

        private ObservableCollection<SimpleRoomMessage> _simpleRoomMessage = new ObservableCollection<SimpleRoomMessage>();
        public ObservableCollection<SimpleRoomMessage> SimpleRoomMessages
        {
            get { return this._simpleRoomMessage; }
        }

        public static async Task<ObservableCollection<SimpleRoomMessage>> GetAllRoomMessagesRoomsAsync(bool forceRefresh, TeamRoom tm)
        {

            await _teamRoomMessagesDataSource.GetTeamRoomMessagesDataAsync(forceRefresh, tm);
            return _teamRoomMessagesDataSource.SimpleRoomMessages;
        }

        private async Task GetTeamRoomMessagesDataAsync(bool forceRefresh, TeamRoom tm)
        {
            _currentRoom = tm;

            if (forceRefresh == true)
                this._simpleRoomMessage.Clear();

            if (this._simpleRoomMessage.Count != 0)
                return;

            try
            {
                var credentials = CredentialHelper.GetCredential();
                Helpers.CreateHttpClient(ref _httpClient);
                string uriString = String.Format("https://{0}.visualstudio.com/DefaultCollection/_apis/Chat/rooms/{1}/messages", credentials.Account, tm.id);
                

                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var teamroommessagebaselist = JsonConvert.DeserializeObject<TeamRoomMessages>(responseBody, new TeamRoomMessageCreator());

                IEnumerable<TeamRoomMessage> messages = teamroommessagebaselist.value;

                foreach (var item in messages)
                {
                    SimpleRoomMessage sm = new SimpleRoomMessage();
                    sm.message = item;
                    sm.Content = item.content.ToString();
                    sm.postedTime = item.postedTime;
                    sm.PostedByDisplayName = item.postedBy.displayName;
                    sm.PostedByImageUrl = item.postedBy.imageUrl;

                    if (item.content is TeamRoomMessage.Content.System)
                    {
                        //enter leave messages, don't show them
                    }

                    if (item.content is TeamRoomMessage.Content.Normal)
                    {
                        sm.PostedByImageLocation = await MessageIconSource(sm.PostedByDisplayName + ".png", sm.PostedByImageUrl); 
                        SimpleRoomMessages.Add(sm);
                    }
                    if (item.content is TeamRoomMessage.Content.Notification.BuildCompletedEvent)
                    {
                        var content = (TeamRoomMessage.Content.Notification.BuildCompletedEvent)item.content;
                        StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                        StorageFile file = await InstallationFolder.GetFileAsync(@"assets\buildcompletedevent1.png");

                        sm.PostedByImageLocation = file.Path;

                        sm.Url = content.url;
                        SimpleRoomMessages.Add(sm);
                    }

                    if (item.content is TeamRoomMessage.Content.Notification.WorkItemChangedEvent)
                    {
                        var content = (TeamRoomMessage.Content.Notification.WorkItemChangedEvent)item.content;
                        StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                        StorageFile file = await InstallationFolder.GetFileAsync(@"assets\workitemchangedevent1.png");

                        sm.PostedByImageLocation = file.Path;
                        sm.Content = "Work Item Changed by " + sm.PostedByDisplayName + " " + sm.Content;
                        sm.Url = content.url;
                        SimpleRoomMessages.Add(sm);
                    }

                    if (item.content is TeamRoomMessage.Content.Notification.CheckinEvent)
                    {
                        var content = (TeamRoomMessage.Content.Notification.CheckinEvent)item.content;
                        StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                        StorageFile file = await InstallationFolder.GetFileAsync(@"assets\checkinevent1.png");

                        sm.PostedByImageLocation = file.Path; 
                        sm.Url = content.url;
                        SimpleRoomMessages.Add(sm);
                    }
                    if (item.content is TeamRoomMessage.Content.Notification)
                    {
                        
                        var content = (TeamRoomMessage.Content.Notification)item.content;
                        if(content.type == "GitPullRequestEvent")
                        {
                            StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                            StorageFile file = await InstallationFolder.GetFileAsync(@"assets\Git.png");

                            sm.PostedByImageLocation = file.Path;
                            sm.Url = content.url;
                            sm.Content = "GIT Pull Request by " + sm.PostedByDisplayName + " " + sm.Content;
                            SimpleRoomMessages.Add(sm);
                        }
                    }
              }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async static Task PostMessage(string postMessageText, TeamRoom tm)
        {
            await _teamRoomMessagesDataSource.PostMessageAsync(postMessageText, tm);
        }

        private async Task PostMessageAsync(string postMessageText, TeamRoom tm)
        {
            var credentials = CredentialHelper.GetCredential();
            Helpers.CreateHttpClient(ref _httpClient);
            string uriString = String.Format("https://{0}.visualstudio.com/DefaultCollection/_apis/Chat/rooms/{1}/messages", credentials.Account, tm.id);

            var succes = await PostToVSOHelper.PostToVSO(uriString, postMessageText);
            if(succes == true)
            {
                await GetTeamRoomMessagesDataAsync(true, tm);
            }
        }


        private async Task<string> MessageIconSource(string icon, string downloadLocation)
        {
            StorageFile file = null;
            try
            {
                if( await FileHelper.CheckIfFileExsistsInLocalFolder(icon) != true)
                {
                    var _destinationFolder = ApplicationData.Current.LocalFolder;

                    var pictureFilename = icon;
                    file = await _destinationFolder.CreateFileAsync(pictureFilename, CreationCollisionOption.ReplaceExisting);

                    Helpers.CreateHttpClient(ref _httpClient);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(downloadLocation));
                    HttpResponseMessage response = await _httpClient.SendRequestAsync(request, HttpCompletionOption.ResponseHeadersRead);
                    var stream = await response.Content.ReadAsBufferAsync();
                    await Windows.Storage.FileIO.WriteBufferAsync(file, stream);
                }
                else
                {
                    file = await ApplicationData.Current.LocalFolder.GetFileAsync(icon);
                }
                return file.Path;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

    }
}

