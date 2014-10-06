using System;
using System.Collections.Generic;
using System.Text;
using Windows.Security.Credentials;
using Windows.Web.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using VSOTeams.DataModel;

namespace VSOTeams.Common
{
    internal sealed class CredentialHelper
    {
        

        private static CredentialHelper _credentials = new CredentialHelper();
        public string  UserName { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        public string MemberId { get; set; }
        
        internal async static void SaveCredential(string account, string userName, string password)
        {
            Windows.Storage.ApplicationData.Current.RoamingSettings.Values["Account"] = account;
            
         
            var vault = new PasswordVault();
            try
            {
                var bestaat = vault.RetrieveAll();
                foreach (var item in bestaat)
                {
                    if (item.Resource == "VSOLogin")
                    {
                        vault.Remove(item);
                        ProjectDataSource pd = new ProjectDataSource();
                        await pd.ClearProjects();
                    }
                }
               
                
                var credential = new PasswordCredential("VSOLogin", userName, password);
                vault.Add(credential);
            }
            catch (Exception)
            {
                throw;
            }
            

        }


        internal static CredentialHelper GetCredential()
        {
            var vault = new PasswordVault();
            try
            {
                var credentials = vault.FindAllByResource("VSOLogin");

                if (credentials.Count != 0 )
                {
                    if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values["Account"] != null)
                    {
                        _credentials.Account = Windows.Storage.ApplicationData.Current.RoamingSettings.Values["Account"].ToString();
                    }
                    else
                    {
                        _credentials.Account = "";
                        Windows.Storage.ApplicationData.Current.RoamingSettings.Values["Account"] = "";
                    }
                    
                    _credentials.UserName = credentials[0].UserName;
                    _credentials.Password = vault.Retrieve("VSOLogin", _credentials.UserName).Password;
                }
                return _credentials;
            }
            catch (Exception)
            {
                _credentials.Account = "";
                _credentials.UserName = "";
                _credentials.Password = "";
                return _credentials;
            }
        }


        internal async static void RemoveCredentials(string username)
        {
            var vault = new PasswordVault();
            var cred = vault.Retrieve("VSOLogin", username);
            vault.Remove(cred);
            ProjectDataSource pd = new ProjectDataSource();
            await pd.ClearProjects();
        }
    }
}
