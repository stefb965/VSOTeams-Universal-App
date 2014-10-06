using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using VSOTeams.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VSOTeams.Model
{
    [DataContract]
    public class Team : Modelbase
    {
          [DataMember]
        public string id { get; set; }

          [DataMember]
        public string projectId { get; set; }

        private string _name;
          [DataMember]
        public string name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }

        private string _url;
          [DataMember]
        public string url { get { return _url; } set { _url = value; NotifyPropertyChanged(); } }

        private string _description;
          [DataMember]
        public string description { get { return _description; } set { _description = value; NotifyPropertyChanged(); } }
        

        private string _identityUrl;
          [DataMember]
        public string identityUrl { get { return _identityUrl; } set { _identityUrl = value; NotifyPropertyChanged(); } }

          [DataMember]
        public ObservableCollection<TeamMember> teammembers { get; set; }

        public Team(string name, string url, string description, string id, string identityUrl, string projectId)
        {
            this.name = name;
            this.id = id;
            this.projectId = projectId;
            this.description = description;
            this.url = url;
            this.identityUrl = identityUrl;
        }

        private RelayCommand _openTeamCommand;
        public RelayCommand OpenTeamCommand
        {
            get
            {
                return _openTeamCommand ?? (_openTeamCommand = new RelayCommand(OpenThisTeam));
            }
        }

        private void OpenThisTeam()
        {
            // He bah dit is niet handig om hier te doen
            Frame rootFrame = Window.Current.Content as Frame;

            if (!rootFrame.Navigate(typeof(TeamHubPage), this))
            {
                throw new Exception("Failed to create initial page");
            }

        }

    }
}
