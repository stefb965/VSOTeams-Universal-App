using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VSOTeams.Common
{
   internal static class PostToVSOHelper
    {
        internal static async Task<bool> PostToVSO(string requestUri, string msg)
        {
            var _credentials = CredentialHelper.GetCredential();
            var username = _credentials.UserName;
            var password = _credentials.Password;

            var messageRequestPOSTData =
                       new MessageRequest()
                       {
                           Content = msg
                       };

            HttpContent content = new StringContent(
                   JsonConvert.SerializeObject(messageRequestPOSTData),
                   Encoding.UTF8,
                   "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", username, password))));

                HttpResponseMessage response = await client.PostAsync
                    (requestUri, content);

                response.EnsureSuccessStatusCode();
                var test = await response.Content.ReadAsStringAsync();
            }

            return true;
        }

    }
    public class MessageRequest
    {
        public string Content { get; set; }
    }
}
