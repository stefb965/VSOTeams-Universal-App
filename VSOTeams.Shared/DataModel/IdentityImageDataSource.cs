using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Web.Http;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Common;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using VSOTeams.Model;

namespace VSOTeams.DataModel
{
    public class IdentityImageDataSource
    {

        private static IdentityImageDataSource _identityImageDataSource = new IdentityImageDataSource();
        private HttpClient _httpClient;


        private ObservableCollection<IdentityImage> _identitiesImages = new ObservableCollection<IdentityImage>();
        public ObservableCollection<IdentityImage> IdentitiesImages
        {
            get { return this._identitiesImages; }
        }

        private IdentityImage _identitiesImage = new IdentityImage();
        public IdentityImage IdentitiesImage
        {
            get { return this._identitiesImage; }
        }

        public static async Task<IEnumerable<IdentityImage>> DonwloadAllIdentityImagesAsync()
        {
            await _identityImageDataSource.DownloadIdentityImagesFromRestAsync();
            return _identityImageDataSource.IdentitiesImages;
        }

        public static async Task DownloadIdentityImagesForUser(VSOUser userId)
        {
            await _identityImageDataSource.DownloadIdentityImagesForUserFromRestAsync(userId);
            
        }

        private async Task DownloadIdentityImagesFromRestAsync()
        {
            var _destinationFolder = ApplicationData.Current.LocalFolder;
                       
            var vsoUsers = VSOUsersDataSource.GetAllVSOUsersAsync(false).Result;
            foreach (VSOUser i in vsoUsers)
            {
                var pictureFilename = i.DisplayName + ".png";
                StorageFile file = await _destinationFolder.CreateFileAsync(pictureFilename, CreationCollisionOption.ReplaceExisting);

                Helpers.CreateHttpClient(ref _httpClient);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, i.ProfileImage.DownloadURL);
                HttpResponseMessage response = await _httpClient.SendRequestAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var stream = await response.Content.ReadAsBufferAsync();
                await Windows.Storage.FileIO.WriteBufferAsync(file, stream);
                
            }
        }

        private async Task DownloadIdentityImagesForUserFromRestAsync(VSOUser user)
        {
            var _destinationFolder = ApplicationData.Current.LocalFolder;

            var pictureFilename = user.DisplayName + ".png";
            StorageFile file = await _destinationFolder.CreateFileAsync(pictureFilename, CreationCollisionOption.ReplaceExisting);

            Helpers.CreateHttpClient(ref _httpClient);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, user.ProfileImage.DownloadURL);
            HttpResponseMessage response = await _httpClient.SendRequestAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var stream = await response.Content.ReadAsBufferAsync();
            await Windows.Storage.FileIO.WriteBufferAsync(file, stream);
        }

    }
}
