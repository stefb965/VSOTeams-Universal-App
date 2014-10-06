using System;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;

namespace VSOTeams.Common
{
    internal static class Helpers
    {
        internal static void CreateHttpClient(ref HttpClient httpClient)
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
            }

            var credentials = CredentialHelper.GetCredential();

            HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
            filter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.Default;
            filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.Default;

            httpClient = new HttpClient(filter);

            httpClient.DefaultRequestHeaders.Accept.Add(
                new HttpMediaTypeWithQualityHeaderValue("application/json"));

            httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Basic",
                     Convert.ToBase64String(
                         System.Text.Encoding.UTF8.GetBytes(
                             string.Format("{0}:{1}", credentials.UserName, credentials.Password))));

            httpClient.DefaultRequestHeaders.UserAgent.Add(new HttpProductInfoHeaderValue("VSOSample", "v1"));

        }


        public async static Task<bool> IsValideCredential()
        {
            try
            {
                var account = Windows.Storage.ApplicationData.Current.RoamingSettings.Values["Account"].ToString();
                HttpClient _httpClient = null;
                Helpers.CreateHttpClient(ref _httpClient);

                string uriString = "https://" + account + ".visualstudio.com/DefaultCollection/_apis/projects/";
                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                if (response.StatusCode == HttpStatusCode.Ok)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                    
                
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool IsConnectedToInternet()
        {
            ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);

        }
    }
}
