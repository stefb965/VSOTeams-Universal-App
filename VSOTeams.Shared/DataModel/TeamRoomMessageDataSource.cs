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
                string uriString = String.Format("/DefaultCollection/_apis/Chat/rooms/{0}/messages", tm.id);

                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var teamroommessagebaselist = JsonConvert.DeserializeObject<TeamRoomMessages>(responseBody, new TeamRoomMessageCreator());

                IEnumerable<TeamRoomMessage> messages = teamroommessagebaselist.value;

                var BuildCompletedEventImage = new Image();
                BuildCompletedEventImage.Source = await MessageIconSource("buildcompletedevent.png");

                var BuildCompletedEventImageBig = new Image();
                BuildCompletedEventImageBig.Source = await MessageIconSource("buildcompletedevent1.png");

                var workitemchangedeventImage = new Image();
                workitemchangedeventImage.Source = await MessageIconSource("workitemchangedevent.png");

                var workitemchangedeventImageBig = new Image();
                workitemchangedeventImageBig.Source = await MessageIconSource("workitemchangedevent1.png");

                var checkineventImage = new Image();
                checkineventImage.Source = await MessageIconSource("checkinevent.png");

                var checkineventImageBig = new Image();
                checkineventImageBig.Source = await MessageIconSource("checkinevent1.png");

                foreach (var item in messages)
                {
                    SimpleRoomMessage sm = new SimpleRoomMessage();
                    sm.message = item;
                    sm.Content = item.content.ToString();

                    sm.PostedByDisplayName = item.postedBy.displayName + ".png";
                    sm.PostedByImageUrl = item.postedBy.imageUrl;

                    if (item.content is TeamRoomMessage.Content.System)
                    {
                        //enter leave messages, don't show them
                    }

                    if (item.content is TeamRoomMessage.Content.Normal)
                    {
                        ImageSource imgResult = await MessageIconSource(sm.PostedByImageUrl);

                        sm.PostedByImageSource = imgResult;
                        sm.MessageTypeURI = imgResult;
                        sm.MessageTypeURIBig = imgResult;
                        SimpleRoomMessages.Add(sm);
                    }
                    if (item.content is TeamRoomMessage.Content.Notification.BuildCompletedEvent)
                    {
                        var content = (TeamRoomMessage.Content.Notification.BuildCompletedEvent)item.content;
                        sm.MessageTypeURI = BuildCompletedEventImage.Source;
                        sm.MessageTypeURIBig = BuildCompletedEventImageBig.Source;
                        sm.Url = content.url;
                        SimpleRoomMessages.Add(sm);
                    }

                    if (item.content is TeamRoomMessage.Content.Notification.WorkItemChangedEvent)
                    {
                        var content = (TeamRoomMessage.Content.Notification.WorkItemChangedEvent)item.content;
                        sm.MessageTypeURI = workitemchangedeventImage.Source;
                        sm.MessageTypeURIBig = workitemchangedeventImageBig.Source;
                        sm.Url = content.url;
                        SimpleRoomMessages.Add(sm);
                    }

                    if (item.content is TeamRoomMessage.Content.Notification.CheckinEvent)
                    {
                        var content = (TeamRoomMessage.Content.Notification.CheckinEvent)item.content;
                        sm.MessageTypeURI = checkineventImage.Source;
                        sm.MessageTypeURIBig = checkineventImageBig.Source;
                        sm.Url = content.url;
                        SimpleRoomMessages.Add(sm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async static Task PostMessage(string postMessageText)
        {
            await _teamRoomMessagesDataSource.PostMessageAsync(postMessageText);
        }

        private async Task PostMessageAsync(string postMessageText)
        {
            string uriString = String.Format("/DefaultCollection/_apis/Chat/rooms/{0}/messages", _currentRoom.id);

            var succes = await PostToVSOHelper.PostToVSO(uriString, postMessageText);
        }


        private async Task<BitmapImage> MessageIconSource(string icon)
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(icon);
                var fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var img = new BitmapImage();
                img.SetSource(fileStream);

                return img;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

    }
}

