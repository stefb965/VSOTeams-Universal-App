using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI.Xaml.Media;

namespace VSOTeams.Model
{
    public class TeamRoom
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string lastActivity { get; set; }
        public CreatedBy createdBy { get; set; }
        public string createdDate { get; set; }
        public bool hasAdminPermissions { get; set; }
        public bool hasReadWritePermissions { get; set; }
        public string ImageLocation { get; internal set; }
    }

        public class CreatedBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class TeamRooms
    {
        public ObservableCollection<TeamRoom> value { get; set; }
    }
}
