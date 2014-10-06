using System;
using System.Collections.Generic;
using Windows.Web.Http;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VSOTeams.Common;
using Windows.Data.Json;
using System.Linq;
using Windows.Storage;
using System.Runtime.Serialization;
using System.Diagnostics;
using VSOTeams.Model;

namespace VSOTeams.DataModel
{
    public sealed class TeamMemberDataSource
    {
        private static TeamMemberDataSource _datasource = new TeamMemberDataSource();
        private HttpClient _httpClient;

        private ObservableCollection<TeamMember> _teamMembers = new ObservableCollection<TeamMember>();

        public ObservableCollection<TeamMember> TeamMembers
        {
            get { return this._teamMembers; }
        }
        public static async Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync()
        {
            if (!_datasource.TeamMembers.Any())
                await _datasource.GetAllTeamMembersAllProjectFromRestAsync();

            return _datasource.TeamMembers;
        }
        private async Task GetAllTeamMembersAllProjectFromRestAsync()
        {
            
            var allTeams = await TeamDataSource.GetAllTeamsAsync();
            foreach (Team tm in allTeams)
            {
                string jsonFileName;
                jsonFileName = "Team " + tm.id + ".json";
                if (await FileHelper.CheckIfFileExsistsInLocalFolder(jsonFileName) == true)
                {
                    tm.teammembers = await ConvertFileToTeamMemberList(jsonFileName);
                }
                else
                {
                    await _datasource.GetMembersByTeamFromRestAsync(tm);
                    await TryWriteJsonToFileAsync(SerializeToJson(tm.teammembers), jsonFileName);
                }
            }

            if (await FileHelper.CheckIfFileExsistsInLocalFolder("TeamMembers.json") == true)
            {
                _teamMembers = await ConvertFileToTeamMemberList("TeamMembers.json");
            }
            else
            {
                await TryWriteJsonToFileAsync(SerializeToJson(_teamMembers), "TeamMembers.json");
            }
        }

        private async Task<ObservableCollection<TeamMember>> ConvertFileToTeamMemberList(string jsonfile)
        {
            try
            {
                StorageFile file = await FileHelper.GetFileFromLocalFolder(jsonfile);
                string jsonData = await FileIO.ReadTextAsync(file);

                var teamMemberList = JsonHelper.Deserialize<ObservableCollection<TeamMember>>(jsonData);
                return teamMemberList;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<ObservableCollection<TeamMember>> GetAllTeamMembersPerTeamAsync(Team tm)
        {
            await GetAllTeamMembersAsync();
            return tm.teammembers;
        }


        private async Task GetMembersByTeamFromRestAsync(Team team)
        {
            var credentials = CredentialHelper.GetCredential(); 

            Helpers.CreateHttpClient(ref _httpClient);
            string uriString = "https://" + credentials.Account + ".visualstudio.com/DefaultCollection/_apis/projects/" + team.projectId + "/teams/" + team.id + "/members";
            Uri resourceAddress = new Uri(uriString);

            HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JsonObject jsonObject = JsonObject.Parse(responseBody);
            JsonArray jsonArray = jsonObject["value"].GetArray();

            ObservableCollection<TeamMember> projectTeammembers = new ObservableCollection<TeamMember>();

            try
            {
                foreach (var item in jsonArray)
                {
                    JsonObject fields = item.GetObject();
                    var teamMember = new TeamMember(GetStringField(fields, "id"),
                                                    GetStringField(fields, "displayName"),
                                                    GetStringField(fields, "uniqueName"),
                                                    GetStringField(fields, "url"),
                                                    GetStringField(fields, "imageUrl"));

                    projectTeammembers.Add(teamMember);
                    this.TeamMembers.Add(teamMember); // eerst nog kijken of ie er niet al is.
                }
                team.teammembers = projectTeammembers;
            }
            catch 
            {
                //TODO
            }
        }
        private string GetStringField(JsonObject fields, string fieldname)
        {
            if (fields.ContainsKey(fieldname) && fields[fieldname].ValueType == JsonValueType.String) return fields[fieldname].GetString();
            return string.Empty;
        }


        internal async static Task<TeamMember> GetTeamMemberForVSOUser(VSOUser user)
        {
            var teamMembers = await GetAllTeamMembersAsync();
            
            TeamMember tm =
                (from teammember in teamMembers
                where teammember.id == user.userId
                select teammember).FirstOrDefault();

            return tm;
        }


        public string SerializeToJson(ObservableCollection<TeamMember> teamMember)
        {
            string jsonData;
            try
            {
                jsonData = JsonHelper.Serialize(teamMember);
            }
            catch (SerializationException se)
            {
                Debug.WriteLine(se.Message);
                throw;
            }
            return jsonData;
        }

        private async Task TryWriteJsonToFileAsync(string json, string jsonfileName)
        {
            if (json != null)
            {
                try
                {
                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(jsonfileName, CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file, json);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }
    }
}
