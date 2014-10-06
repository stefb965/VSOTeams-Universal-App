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
        public DateTime lastActivityDT { get; set; }
        public string createdDate { get; set; }
        public ImageSource ImageUri { get; set; }
    }

    public class TeamRooms
    {
        public ObservableCollection<TeamRoom> value { get; set; }
    }
}
