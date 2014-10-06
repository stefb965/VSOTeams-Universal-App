using System;
using System.Collections.Generic;
using System.Text;

namespace VSOTeams.Model
{
    public class License : Modelbase
    {
        public License(string accountId, string userId, string license, string userStatus, string assignmentDate)
        {
            
            this.accountId = accountId;
            this.userId = userId;
            this.license = license;
            this.userStatus = userStatus;
            this.assignmentDate = assignmentDate;
        }

        private string _accountId;
        public string accountId { get { return _accountId; } set { _accountId = value; NotifyPropertyChanged(); } }

        private string _userId;
        public string userId { get { return _userId; } set { _userId = value; NotifyPropertyChanged(); } }

        private string _license;
        public string license { get { return _license; } set { _license = value; NotifyPropertyChanged(); } }

        private string _userStatus;
        public string userStatus { get { return _userStatus; } set { _userStatus = value; NotifyPropertyChanged(); } }

        private string _assignmentDate;
        public string assignmentDate { get { return _assignmentDate; } set { _assignmentDate = value; NotifyPropertyChanged(); } }

    }


}
