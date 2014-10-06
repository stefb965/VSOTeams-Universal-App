using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.DataModel;

namespace VSOTeams.Model
{
    [DataContract]
    public class TeamMember
    {
         [DataMember]
        public string id { get; set; }
        [DataMember]
        public string displayName { get; set; }
        [DataMember]
        public string uniqueName { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string imageUrl { get; set; }

        public TeamMember(string id, string displayName, string uniqueName, string url, string imageUrl)
        {
            this.id = id;
            this.displayName = displayName;
            this.uniqueName = uniqueName;
            this.url = url;
            this.imageUrl = imageUrl;
        }

        public async Task<VSOUser> ToVSOUser(TeamMember tm)
        {
            return await VSOUsersDataSource.GetVSOUsersAsync(tm);
        }
    }
}
