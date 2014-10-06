using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VSOTeams.Common;
using VSOTeams.Model;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace VSOTeams.DataModel
{
    public class VSOUsersDataSource
    {
        private static VSOUsersDataSource _VSOUsersDataSource = new VSOUsersDataSource();

        private ObservableCollection<VSOUser> _vsoUsers = new ObservableCollection<VSOUser>();
        public ObservableCollection<VSOUser> VSOUsers
        {
            get { return this._vsoUsers; }
        }

        public static async Task<ObservableCollection<VSOUser>> GetAllVSOUsersAsync(bool forceRefresh)
        {
            await _VSOUsersDataSource.GetVSOUsersDataAsync(forceRefresh);
            return _VSOUsersDataSource.VSOUsers;
        }

        public static async Task<VSOUser> GetVSOUsersAsync(TeamMember tm)
        {
            await _VSOUsersDataSource.GetVSOUsersDataAsync(false);
            var matches = _VSOUsersDataSource.VSOUsers.Where((vsoUser) => vsoUser.userId.Equals(tm.id));

            if (matches.Count() == 0)
            {
                matches = _VSOUsersDataSource.VSOUsers.Where((vsoUser) => vsoUser.DisplayName.Equals(tm.displayName));
            }
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetVSOUsersDataAsync(bool forceRefresh)
        {
            if (forceRefresh == true)
                this._vsoUsers.Clear();

            if (this._vsoUsers.Count != 0)
                return;

            var dataSavedLocal = await FileHelper.CheckIfFileExsistsInLocalFolder("VSOUser.json");
            if (dataSavedLocal == true)
            {
                var teamMembers = await TeamMemberDataSource.GetAllTeamMembersAsync();

                _vsoUsers = await ConvertFileToVSOUsersList();
                return;
            }

            var allLicenses = await LicensesDataSource.GetLicensesAsync();

            
            var credentials = CredentialHelper.GetCredential();

            foreach (License licenes in allLicenses)
            {
                VSOUser vsoUser = new VSOUser();
                vsoUser.accountId = licenes.accountId;
                vsoUser.assignmentDate = licenes.assignmentDate;
                vsoUser.license = licenes.license;
                vsoUser.userId = licenes.userId;
                vsoUser.userStatus = licenes.userStatus;

                var identity = await IdentityDataSource.GetIdentityByAccountIDAsync(vsoUser.userId);
                vsoUser.DisplayName = identity.DisplayName;
                vsoUser.Mail = identity.Properties.Mail;

                var source = "https://" + credentials.Account + ".visualstudio.com/DefaultCollection/_api/_common/identityImage?id=" + vsoUser.userId;
                Uri u = new Uri(source);

                IdentityImage ii = new IdentityImage();
                ii.SafeLocation = ApplicationData.Current.LocalFolder.Path + "\\" + vsoUser.DisplayName + ".png";
                ii.DownloadURL = u;
                vsoUser.ProfileImage = ii;

                await IdentityImageDataSource.DownloadIdentityImagesForUser(vsoUser); 

                _vsoUsers.Add(vsoUser);
            }
            await TryWriteJsonToFileAsync(SerializeToJson(_vsoUsers));
        }

        private async Task<ObservableCollection<VSOUser>> ConvertFileToVSOUsersList()
        {
            try
            {
                StorageFile file = await FileHelper.GetFileFromLocalFolder("VSOUser.json");
                string jsonData = await FileIO.ReadTextAsync(file);

                var vsoUserList = JsonHelper.Deserialize<ObservableCollection<VSOUser>>(jsonData);
                return vsoUserList;
            }
            catch
            {
                return null;
            }
        }

        public string SerializeToJson(ObservableCollection<VSOUser> vsoUsers)
        {
            string jsonData;
            try
            {
                jsonData = JsonHelper.Serialize(vsoUsers);
            }
            catch (SerializationException se)
            {
                Debug.WriteLine(se.Message);
                throw;
            }
            return jsonData;
        }

        private async Task TryWriteJsonToFileAsync(string json)
        {
            if (json != null)
            {
                try
                {
                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("VSOUser.json", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file, json);
                }
                catch (Exception e)
                { 
                    Debug.WriteLine(e.Message); 
                }
            }
        }

        public static async Task<StorageFile> SaveVSOUsersToCSV()
        {
            var folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add(".csv");
            folderPicker.ViewMode = PickerViewMode.Thumbnail;
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.SettingsIdentifier = "FolderPicker";
 
#if WINDOWS_PHONE_APP
            StorageFile csvFile = await KnownFolders.DocumentsLibrary.CreateFileAsync("AllVSOUsers.csv", CreationCollisionOption.ReplaceExisting);
#else
            var folder = await folderPicker.PickSingleFolderAsync();
            StorageFile csvFile = await folder.CreateFileAsync("AllVSOUsers.csv", CreationCollisionOption.ReplaceExisting);
#endif
            using (IRandomAccessStream fileStream = await csvFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (IOutputStream outputStream = fileStream.GetOutputStreamAt(0))
                {
                    using (DataWriter dataWriter = new DataWriter(outputStream))
                    {
                        for (int i = 0; i < _VSOUsersDataSource._vsoUsers.Count(); i++)
                        {
                            string displayName = _VSOUsersDataSource._vsoUsers[i].DisplayName;
                            string mail = _VSOUsersDataSource._vsoUsers[i].Mail;
                            string licenses = _VSOUsersDataSource._vsoUsers[i].license;
                            string hasteams = "Nee";
                            //TEMP TEAM

                            var vsoUserTeams = TeamDataSource.GetTeamsForVSOUserAsync(_VSOUsersDataSource._vsoUsers[i]).Result;
                            if (vsoUserTeams.Count() != 0)
                            {
                                hasteams = "Ja";
                            }
                            //

                            string csvRow = string.Format("{0}; {1}; {2}; {3}; {4}", displayName, mail, licenses, hasteams, Environment.NewLine);
                            dataWriter.WriteString(csvRow);
                        }
                        await dataWriter.StoreAsync();
                        dataWriter.DetachStream();
                    }

                    await outputStream.FlushAsync();
                }
            }

 


            return csvFile;
        }
    }
}
