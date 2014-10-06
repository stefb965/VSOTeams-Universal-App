using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Common;
using VSOTeams.Model;
using Windows.Data.Json;
using Windows.Web.Http;

namespace VSOTeams.DataModel
{
    public class IdentityDataSource
    {
        private static IdentityDataSource _identityDataSource = new IdentityDataSource();
        private HttpClient _httpClient;

     
        private ObservableCollection<Identity> _identities = new ObservableCollection<Identity>();
        public ObservableCollection<Identity> Identities
        {
            get { return this._identities; }
        }

        public static async Task<IEnumerable<Identity>> GetIdentitiesAsync()
        {
            await _identityDataSource.GetIdentitiesDataAsync();
            return _identityDataSource.Identities;
        }
        public static async Task<Identity> GetIdentityByAccountIDAsync(string accountId)
        {
            return await _identityDataSource.GetIdentityByAccountIDFromRestAsync(accountId);
        }

        private Task GetIdentitiesDataAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<Identity> GetIdentityByAccountIDFromRestAsync(string p)
        {
            var credentials = CredentialHelper.GetCredential();

            Helpers.CreateHttpClient(ref _httpClient);
            string uriString = "https://" + credentials.Account + ".vssps.visualstudio.com/_apis/Identities/" + p;
            Uri resourceAddress = new Uri(uriString);

            HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JsonObject jsonObject = JsonObject.Parse(responseBody);
            
            try
            {
                Identity identity = new Identity();

                identity.DisplayName = GetStringField(jsonObject, "DisplayName");
                JsonObject itemValue = jsonObject["Properties"].GetObject();
                Properties prop = new Properties();
                prop.Mail = GetStringField(itemValue, "Mail");
                identity.Properties = prop;
            
                return identity;
            }
            catch
            {
                return null;
                //
            }
        }
        private string GetStringField(JsonObject fields, string fieldname)
        {
            if (fields.ContainsKey(fieldname) && fields[fieldname].ValueType == JsonValueType.String) return fields[fieldname].GetString();
            return string.Empty;
        }
    }
}
