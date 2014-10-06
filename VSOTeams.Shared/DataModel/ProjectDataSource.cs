using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Common;
using Windows.Data.Json;
using Windows.Web.Http;
using System.Linq;
using Windows.Storage;
using System.Runtime.Serialization;
using System.Diagnostics;
using VSOTeams.Model;
using Newtonsoft.Json;
using Windows.UI.Core;
using Windows.Networking.PushNotifications;
using Microsoft.WindowsAzure.Messaging;

namespace VSOTeams.DataModel
{
    public sealed class ProjectDataSource
    {
        private static ProjectDataSource _projectDataSource = new ProjectDataSource();
        private HttpClient _httpClient;
        private string _localFile = "Projects.json";
       
        private ObservableCollection<Project> _projects = new ObservableCollection<Project>();
      
        public ObservableCollection<Project> Projects
        {
            get { return this._projects; }
        }

        public static async Task<ObservableCollection<Project>> GetProjectsAsync(bool refresh)
        {
            if (refresh == true)
            {
                await FileHelper.DeleteFolderContentsAsync(ApplicationData.Current.LocalFolder, StorageDeleteOption.PermanentDelete);

                _projectDataSource._projects = await _projectDataSource.GetProjectsFromRestAsync(); 
            }
            else
            {
                if (await Helpers.IsValideCredential() != true)
                {
                    await _projectDataSource.ClearProjects();
                    throw new Exception("Login failed, please validate your credentials");
                }
                if (_projectDataSource.Projects == null)
                {
                    _projectDataSource._projects = new ObservableCollection<Project>();
                }
                if (_projectDataSource.Projects.Count() == 0)
                {
                    await _projectDataSource.GetProjectsFromFileAsync();
                }
            }

           // await _projectDataSource.RegisterNotificationHubs();
            
            return _projectDataSource._projects;                
        }


        private async Task<ObservableCollection<Project>> GetProjectsFromRestAsync()
        {
            try
            {
                var credentials = CredentialHelper.GetCredential();

                Helpers.CreateHttpClient(ref _httpClient);
                string uriString = string.Format("https://{0}.visualstudio.com/DefaultCollection/_apis/projects/", credentials.Account);
                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Projects allProjects = JsonConvert.DeserializeObject<Projects>(responseBody);

                await JsonHelper.TryWriteJsonToFileAsync(_localFile, responseBody);

                return allProjects.value;
            }
            catch (JsonReaderException ex)
            {
                var t = ex.ToString();
                return null;
            }
            catch (Exception ex)
            {
                var t = ex.ToString();
                return null;
            }
        }

        private async Task GetProjectsFromFileAsync()
        {
            try
            {
                if (await FileHelper.CheckIfFileExsistsInLocalFolder(_localFile) == true)
                {

                    StorageFile file = await FileHelper.GetFileFromLocalFolder(_localFile);
                    string jsonData = await FileIO.ReadTextAsync(file);

                    Projects allProjects = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Projects>(jsonData));
                    _projectDataSource._projects = allProjects.value;
                }
                else
                {
                    _projectDataSource._projects = await _projectDataSource.GetProjectsFromRestAsync();
                }
            }
            catch (Exception ex)
            {
                var t = ex.ToString();
            }
        }


        internal async Task ClearProjects()
        {
            _projectDataSource._projects = null;

            if (await FileHelper.CheckIfFileExsistsInLocalFolder(_localFile) == true)
            {
                StorageFile file = await FileHelper.GetFileFromLocalFolder(_localFile);
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
        }



        private CoreDispatcher dispatcher = null;
        const string notificationHubPath = "vsoteams";
        const string notificationHubConnectionString = "Endpoint=sb://vsoteams-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=pSxp6dk9lAnxnEWhxxn3iouaCdgbxVvRxEKLvoPF5s4=";

        private async Task RegisterNotificationHubs()
        {
                await UnRegisterNotificationsForMyProjectsAsync();
                await InitNotificationsForMyProjectsAsync();
        }

        private async Task UnRegisterNotificationsForMyProjectsAsync()
        {
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            var hub = new NotificationHub(notificationHubPath, notificationHubConnectionString);
            await hub.UnregisterAllAsync(channel.Uri);
        }

        private async Task InitNotificationsForMyProjectsAsync()
        {
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            var hub = new NotificationHub(notificationHubPath, notificationHubConnectionString);

            var tags = getTags();

            var result = await hub.RegisterNativeAsync(channel.Uri, tags);
            if (result.RegistrationId == null)
            {
                throw new Exception(String.Format("Registration with hub '{0}' did not succeed)", result.NotificationHubPath));
            }

            if (channel != null)
            {
                dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
                channel.PushNotificationReceived += OnPushNotificationReceived;
            }
        }

        private string[] getTags()
        {
            if (Projects != null)
            {
                int tel = 0;
                string[] projectTags = new string[_projectDataSource._projects.Count];
                foreach (var prj in Projects)
                {
                    projectTags[tel] = prj.id;
                    tel = tel + 1;
                }

                return projectTags;
            }
            return null;

        }

        void OnPushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs e)
        {
            string typeString = string.Empty;
            string notificationContent = String.Empty;
            switch (e.NotificationType)
            {
                case PushNotificationType.Badge:
                    typeString = "Badge";
                    notificationContent = e.BadgeNotification.Content.GetXml();
                    break;
                case PushNotificationType.Tile:
                    notificationContent = e.TileNotification.Content.GetXml();
                    typeString = "Tile";
                    break;
                case PushNotificationType.Toast:
                    notificationContent = e.ToastNotification.Content.GetXml();
                    typeString = "Toast";
                    // Setting the cancel property prevents the notification from being delivered. It's especially important to do this for toasts:
                    // if your application is already on the screen, there's no need to display a toast from push notifications.
                    e.Cancel = true;
                    break;
                case PushNotificationType.Raw:
                    notificationContent = e.RawNotification.Content;
                    typeString = "Raw";
                    break;
            }

            string text = "Received a " + typeString + " notification, containing: " + notificationContent;
            var ignored = dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // rootPage.NotifyUser(text, NotifyType.StatusMessage);
            });
        }


    }
}

