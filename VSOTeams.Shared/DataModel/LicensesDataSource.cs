using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Common;
using Windows.Data.Json;
using Windows.Web.Http;
using System.Linq;
using VSOTeams.Model;

namespace VSOTeams.DataModel
{
    public sealed class LicensesDataSource
    {
        private static LicensesDataSource _licenseDataSource = new LicensesDataSource();
        private HttpClient _httpClient;

        private ObservableCollection<License> _licenses = new ObservableCollection<License>();

        public ObservableCollection<License> Licenses
        {
            get { return this._licenses; }
        }

        public static async Task<IEnumerable<License>> GetLicensesAsync()
        {
            if (!_licenseDataSource.Licenses.Any())          // TODO - Caching or iets dergelijk zodat na een tijdje de lijst ververst wordt
                await _licenseDataSource.GetLicensesFromRestAsync();
            
            return _licenseDataSource.Licenses;
        }

        private async Task GetLicensesFromRestAsync()
        {
            var credentials = CredentialHelper.GetCredential();

            Helpers.CreateHttpClient(ref _httpClient);
            string uriString = "https://" + credentials.Account + ".vssps.visualstudio.com/_apis/Licensing/Entitlement";
            Uri resourceAddress = new Uri(uriString);

            HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JsonObject jsonObject = JsonObject.Parse(responseBody);
            JsonArray jsonArray = jsonObject["value"].GetArray();

            try
            {
                foreach (var item in jsonArray)
                {
                    JsonObject fields = item.GetObject();
                    this.Licenses.Add(new License(GetStringField(fields, "accountId"),
                                                    GetStringField(fields, "userId"),
                                                    GetStringField(fields, "license"),
                                                    GetStringField(fields, "userStatus"),
                                                    GetStringField(fields, "assignmentDate")
                                                    ));
                }

            }
            catch 
            {
                
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
