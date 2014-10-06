using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using VSOTeams.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VSOTeams.Model
{

    public class Projects : ObservableObject
    {
    
        public ObservableCollection<Project> value { get; set; }
    }


    public class Project : ObservableObject
    {
        public const string IdPropertyName = "id";
        
     
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { Set(IdPropertyName, ref _id, value); }
        }

        public const string namePropertyName = "name";

      
        private string _name = string.Empty;
        public string name
        {
            get { return _name; }
            set { Set(namePropertyName, ref _name, value); }
        }

        public const string urlPropertyName = "url";

       
        private string _url = string.Empty;
        public string url
        {
            get { return _url; }
            set { Set(urlPropertyName, ref _url, value); }
        }


        public const string descriptionPropertyName = "description";

    
        private string _description = string.Empty;
        public string description
        {
            get { return _description; }
            set { Set(descriptionPropertyName, ref _description, value); }
        }

        public const string teamsPropertyName = "description";

      
        private ObservableCollection<Team> _teams = null;
        public ObservableCollection<Team> teams
        {
            get { return _teams; }
            set { Set(teamsPropertyName, ref _teams, value); }
        }

        public Collection collection { get; set; }

        public DefaultTeam defaultTeam { get; set; }

    }
}
