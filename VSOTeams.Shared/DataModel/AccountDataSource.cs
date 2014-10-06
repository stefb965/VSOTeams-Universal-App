using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VSOTeams.Common;
using VSOTeams.Model;
using Windows.Web.Http;
using Newtonsoft.Json;

namespace VSOTeams.DataModel
{
    public sealed class AccountDataSource
    {
        private static AccountDataSource _dataSource = new AccountDataSource();
        private HttpClient _httpClient;
       
        private ObservableCollection<Account> _accounts = new ObservableCollection<Account>();
      
        public ObservableCollection<Account> Accounts
        {
            get { return this._accounts; }
        }

        public static async Task<ObservableCollection<Account>> GetAccountsAsync()
        {
            return await _dataSource.GetAccountsFromRestAsync();
        }

        private async Task<string> GetMemberID()
        {
            try
            {
                Helpers.CreateHttpClient(ref _httpClient);
                string uriString = "https://app.vssps.visualstudio.com/_apis/profile/profiles/me";
                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                string responseBody = await response.Content.ReadAsStringAsync();
                var memberId = JsonConvert.DeserializeObject<Profile>(responseBody).id;
                return memberId;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private async Task<ObservableCollection<Account>> GetAccountsFromRestAsync()
        {
            try
            {

                Helpers.CreateHttpClient(ref _httpClient);

                var credentials = CredentialHelper.GetCredential();
                if (credentials.MemberId == null)
                {
                    return null;
                    //credentials.MemberId = GetMemberID().Result;
                }
                string uriString = "https://app.vssps.visualstudio.com/_apis/accounts?memberid=" + credentials.MemberId;
                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var allAccounts = JsonConvert.DeserializeObject<Accounts>(responseBody);
                return allAccounts.value;
            }
            catch (Exception)
            {
                
                return null;
            }
            
        }
    }
}
