using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using VSOTeams.Common;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Runtime.Serialization;
using VSOTeams.DataModel;

namespace VSOTeams.Model
{
        [DataContract]
    public class VSOUser : Modelbase
    {
            
        private string _displayName;

        [DataMember]
        public string DisplayName { get { return _displayName; } set { _displayName = value; NotifyPropertyChanged(); } }

        private string _mail;
         [DataMember]
            public string Mail { get { return _mail; } set { _mail = value; NotifyPropertyChanged(); } }

    
        private IdentityImage _profileImage;
              [DataMember]
        public IdentityImage ProfileImage { get { return _profileImage; } set { _profileImage = value; NotifyPropertyChanged(); } }

           
        private string _accountId;
              [DataMember]
        public string accountId { get { return _accountId; } set { _accountId = value; NotifyPropertyChanged(); } }

        private string _userId;
              [DataMember]
        public string userId { get { return _userId; } set { _userId = value; NotifyPropertyChanged(); } }

        private string _license;
              [DataMember]
        public string license { get { return _license; } set { _license = value; NotifyPropertyChanged(); } }

        private string _userStatus;
              [DataMember]
        public string userStatus { get { return _userStatus; } set { _userStatus = value; NotifyPropertyChanged(); } }

        private string _assignmentDate;
        [DataMember]
        public string assignmentDate
        {
            get
            {
                DateTime tmp = Convert.ToDateTime(_assignmentDate);

                return tmp.ToString("MMMM dd, yyyy");
            }
            set
            {
                _assignmentDate = value; NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Team> _teams;
              [DataMember]
        public ObservableCollection<Team> teams
        {
            get
            {
                return _teams;
            }
            set
            {
                var test =  TeamDataSource.GetTeamsForVSOUserAsync(this).Result;
                _teams = test;
                NotifyPropertyChanged();
            }
        }
    }
}
