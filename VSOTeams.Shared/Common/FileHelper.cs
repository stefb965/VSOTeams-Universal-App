using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Linq;

namespace VSOTeams.Common
{
    internal static class FileHelper
    {

        internal static async Task<StorageFolder> GetStorageFolder(string foldername)
        {
            bool bestaatie = false;
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFolder returnFolder = null; 
            try
            {
                returnFolder = await storageFolder.GetFolderAsync(foldername);
                bestaatie = true;
            }
            catch (Exception)
            { 
                // hij bestaat niet
            }
            
            if (bestaatie  != true)
            {
                returnFolder =  storageFolder.CreateFolderAsync(foldername, CreationCollisionOption.FailIfExists).GetResults();
            }

            return returnFolder;
            
        }

        internal static async Task<bool> DeleteEmptyFile(StorageFile image)
        {
            var size = await image.GetBasicPropertiesAsync();
            if (size == null || size.Size == 0)
            {
                await image.DeleteAsync();
                return true;
            }
            return false;
        }

        internal static async Task<bool> DeleteStorageFile(string fileName)
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal static async Task<bool> CheckIfFileExsistsInLocalFolder(string fileName)
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal static async Task<StorageFile> GetFileFromLocalFolder(string fileName)
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return file;
            }
            catch (Exception)
            {
                return null;
            }
        }


        internal static async Task DeleteFolderContentsAsync(StorageFolder folder,
   StorageDeleteOption option)
        {
            // Try to delete all files
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                await file.DeleteAsync(option);
            }

            // Iterate through all subfolders
            var subFolders = await folder.GetFoldersAsync();
            foreach (var subFolder in subFolders)
            {
                // Delete the contents
                await DeleteFolderContentsAsync(subFolder, option);

                // Delete the subfolder
                await subFolder.DeleteAsync(option);
            }
        }
    }
}
