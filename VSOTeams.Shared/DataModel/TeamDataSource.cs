using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Common;
using Windows.Data.Json;
using Windows.Web.Http;
using System.Linq;
using Windows.Storage;
using System.Runtime.Serialization;
using System.Diagnostics;
using VSOTeams.Model;

namespace VSOTeams.DataModel
{
    public sealed class TeamDataSource
    {
        private static TeamDataSource _datasource = new TeamDataSource();
        private HttpClient _httpClient;

        private ObservableCollection<Team> _teams = new ObservableCollection<Team>();

        public ObservableCollection<Team> Teams
        {
            get { return this._teams; }
        }

        public static async Task<ObservableCollection<Team>> GetTeamsByProjectIdAsync(string projectId)
        {
            await GetAllTeamsAsync();

            IEnumerable<Team> teamsInProject =
                from team in _datasource.Teams
                where team.projectId == projectId
                select team;

            var returnValue = new ObservableCollection<Team>(teamsInProject);
            return returnValue;
        }

        public static async Task<ObservableCollection<Team>> GetTeamsForVSOUserAsync(VSOUser user)
        {
           // await TeamMemberDataSource.GetAllTeamMembersAsync();   
            TeamMember tm = await  TeamMemberDataSource.GetTeamMemberForVSOUser(user);
            ObservableCollection<Team> userTeams = new ObservableCollection<Team>();
            if (tm == null)
                return userTeams;

                
            foreach (Team t in _datasource.Teams)
            {
                var members =
                    from n in t.teammembers
                    where n.id == tm.id
                    select n;

                if (members.Count() != 0)
                    userTeams.Add(t);                
            }
            return userTeams;
        }

        public static async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            if (!_datasource.Teams.Any())
                await _datasource.GetAllTeamsFromRestAsync();

            return _datasource.Teams;
        }
        private async Task GetAllTeamsFromRestAsync()
        {
            string jsonfile = "";
            var allProjects = await ProjectDataSource.GetProjectsAsync(false);
            foreach (Project prj in allProjects)
            {
                jsonfile = "Project - " + prj.id + " - Teams.json";
                if (await FileHelper.CheckIfFileExsistsInLocalFolder(jsonfile) == true)
                {
                    prj.teams = await ConvertFileToTeamsList(jsonfile);
                }
                else
                {
                    await _datasource.GetFromRestAsync(prj);
                    await TryWriteJsonToFileAsync(SerializeToJson(prj.teams), jsonfile);
                }
            }

            jsonfile ="Teams.json";
            if (await FileHelper.CheckIfFileExsistsInLocalFolder(jsonfile) == true)
            {
                _teams = await ConvertFileToTeamsList(jsonfile);
            }
            else
            {
                await TryWriteJsonToFileAsync(SerializeToJson(_teams), jsonfile);
            }
            
        }


        private async Task<ObservableCollection<Team>> ConvertFileToTeamsList(string jsonfile)
        {
            try
            {
                StorageFile file = await FileHelper.GetFileFromLocalFolder(jsonfile);
                string jsonData = await FileIO.ReadTextAsync(file);

                var teamsList = JsonHelper.Deserialize<ObservableCollection<Team>>(jsonData);
                return teamsList;
            }
            catch
            {
                return null;
            }
        }

        public string SerializeToJson(ObservableCollection<Team> teams)
        {
            string jsonData;
            try
            {
                jsonData = JsonHelper.Serialize(teams);
            }
            catch (SerializationException se)
            {
                Debug.WriteLine(se.Message);
                throw;
            }
            return jsonData;
        }

        private async Task TryWriteJsonToFileAsync(string json, string jsonfile)
        {
            if (json != null)
            {
                try
                {
                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(jsonfile, CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file, json);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }


        private async Task GetFromRestAsync(Project project)
        {
            var credentials = CredentialHelper.GetCredential(); 

            Helpers.CreateHttpClient(ref _httpClient);
            string uriString = "https://" + credentials.Account + ".visualstudio.com/DefaultCollection/_apis/projects/" + project.id + "/teams";
            Uri resourceAddress = new Uri(uriString);

            HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JsonObject jsonObject = JsonObject.Parse(responseBody);
            JsonArray jsonArray = jsonObject["value"].GetArray();

            ObservableCollection<Team> projectTeams = new ObservableCollection<Team>();

            try
            {
                foreach (var item in jsonArray)
                {
                    JsonObject fields = item.GetObject();
                    var team = new Team(GetStringField(fields, "name"), 
                                                    GetStringField(fields, "url"), 
                                                    GetStringField(fields, "description"),
                                                    GetStringField(fields, "id"),
                                                    GetStringField(fields, "identityUrl"),
                                                    project.id);
                    projectTeams.Add(team);
                    this.Teams.Add(team);

                }
                project.teams = projectTeams;
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
    }
}

