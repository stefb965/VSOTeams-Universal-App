using System.Threading.Tasks;
using VSOTeams.Common;
using VSOTeams.DataModel;
using VSOTeams.Model;
using VSOTeams.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace VSOTeams.Settings
{
    public sealed partial class CredentialSettingsFlyOut : SettingsFlyout
    {
        public CredentialSettingsFlyOut()
        {
            this.InitializeComponent();
        }

        private async Task<bool> SaveSettings()
        {
            try
            {
                if (Account.Text != null || Username.Text != null || Password.Password != null)
                {
                    CredentialHelper.SaveCredential(Account.Text, Username.Text, Password.Password);
                    if (await Helpers.IsValideCredential() == true)
                    {
                        Username.IsEnabled = false;
                        Password.IsEnabled = false;
                        Account.IsEnabled = false;

                        LoginButton.Visibility = Visibility.Collapsed;
                        RemoveButton.Visibility = Visibility.Visible;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
                
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        //private async void SettingsFlyout_GotFocus(object sender, RoutedEventArgs e)
        //{
          
        //}

        private void RemoveButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            mislukt.Visibility = Visibility.Collapsed;
            CredentialHelper.RemoveCredentials(Username.Text);

            Username.Text = "";
            Username.IsEnabled = true;

            Password.Password = "";
            Password.IsEnabled = true;
            
            Account.Text = "";
            Account.IsEnabled = true;

            LoginButton.Visibility = Visibility.Visible;
            RemoveButton.Visibility = Visibility.Collapsed;

            //remove projects and remove file
        }

        private async void LoginButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            mislukt.Visibility = Visibility.Collapsed;
            aanhetinloggen.IsIndeterminate = true;
            LoginButton.IsEnabled = false;

            if (await SaveSettings() == true)
            {
                Username.IsEnabled = false;
                Password.IsEnabled = false;
                Account.IsEnabled = false;

                LoginButton.Visibility = Visibility.Collapsed;
                RemoveButton.Visibility = Visibility.Visible;
            }
            else
            {
                mislukt.Visibility = Visibility.Visible;
            }

            aanhetinloggen.IsIndeterminate = false;
            LoginButton.IsEnabled = true;
        }

        private async void SettingsFlyout_Loaded(object sender, RoutedEventArgs e)
        {
            mislukt.Visibility = Visibility.Collapsed;

            var cred = CredentialHelper.GetCredential();
            if (cred != null)
            {
                Username.Text = cred.UserName;
                Password.Password = cred.Password;

                if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values["Account"] != null)
                {
                    Account.Text = Windows.Storage.ApplicationData.Current.RoamingSettings.Values["Account"].ToString();
                }
                else
                {
                    Account.Text = "";
                    Windows.Storage.ApplicationData.Current.RoamingSettings.Values["Account"] = "";
                }


                aanhetinloggen.IsIndeterminate = true;
                LoginButton.Visibility = Visibility.Visible;
                RemoveButton.Visibility = Visibility.Collapsed;
                LoginButton.IsEnabled = false;

                if (await Helpers.IsValideCredential() == true)
                {
                    LoginButton.Visibility = Visibility.Collapsed;
                    RemoveButton.Visibility = Visibility.Visible;
                }
                else
                {
                    Username.IsEnabled = true;
                    Password.IsEnabled = true;
                    Account.IsEnabled = true;
                    mislukt.Visibility = Visibility.Visible;
                    LoginButton.Visibility = Visibility.Visible;
                    RemoveButton.Visibility = Visibility.Collapsed;
                }

                aanhetinloggen.IsIndeterminate = false;
                LoginButton.IsEnabled = true;
            }
            else
            {
                Username.IsEnabled = true;
                Password.IsEnabled = true;
                Account.IsEnabled = true;

                LoginButton.Visibility = Visibility.Visible;
                RemoveButton.Visibility = Visibility.Collapsed;
            }
            
        }
    }
}
