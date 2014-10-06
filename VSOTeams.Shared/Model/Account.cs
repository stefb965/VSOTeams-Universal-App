using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace VSOTeams.Model
{
    [DataContract]
    public class Accounts : ObservableObject
    {
        [DataMember]
        public ObservableCollection<Account> value { get; set; }
        public int count { get; set; }
    }


    public class Account : ObservableObject
    {
        public const string AccountIdPropertyName = "AccountId";
        private string _accountId = string.Empty;
        public string AccountId
        {
            get { return _accountId; }
            set { Set(AccountIdPropertyName, ref _accountId, value); }
        }

        public const string NamespaceIdPropertyName = "NamespaceId";
        private string _namespaceIdPropertyName = string.Empty;
        public string NamespaceId
        {
            get { return _namespaceIdPropertyName; }
            set { Set(NamespaceIdPropertyName, ref _namespaceIdPropertyName, value); }
        }

        public const string AccountNamePropertyName = "AccountName";
        private string _accountName = string.Empty;
        public string AccountName
        {
            get { return _accountName; }
            set { Set(AccountNamePropertyName, ref _accountName, value); }
        }

        public const string OrganizationNamePropertyName = "OrganizationName";
        private string _organizationName = string.Empty;
        public string OrganizationName
        {
            get { return _organizationName; }
            set { Set(OrganizationNamePropertyName, ref _organizationName, value); }
        }

        public const string AccountTypePropertyName = "AccountType";
        private string _accountType = string.Empty;
        public string AccountType
        {
            get { return _accountType; }
            set { Set(AccountTypePropertyName, ref _accountType, value); }
        }

        public const string AccountOwnerPropertyName = "AccountOwner";
        private string _accountOwner = string.Empty;
        public string AccountOwner
        {
            get { return _accountOwner; }
            set { Set(AccountOwnerPropertyName, ref _accountOwner, value); }
        }

        public const string CreatedByPropertyName = "CreatedBy";
        private string _createdBy = string.Empty;
        public string CreatedBy
        {
            get { return _createdBy; }
            set { Set(CreatedByPropertyName, ref _createdBy, value); }
        }

        public const string CreatedDatePropertyName = "CreatedDate";
        private string _createdDate = string.Empty;
        public string CreatedDate
        {
            get { return _createdDate; }
            set { Set(CreatedDatePropertyName, ref _createdDate, value); }
        }


        public const string AccountStatusPropertyName = "AccountStatus";
        private string _accountStatus = string.Empty;
        public string AccountStatus
        {
            get { return _accountStatus; }
            set { Set(AccountStatusPropertyName, ref _accountStatus, value); }
        }


        public const string StatusReasonPropertyName = "StatusReason";
        private string _statusReason = string.Empty;
        public string StatusReason
        {
            get { return _statusReason; }
            set { Set(StatusReasonPropertyName, ref _statusReason, value); }
        }

        public const string LastUpdatedByPropertyName = "LastUpdatedBy";
        private string _lastUpdatedBy = string.Empty;
        public string LastUpdatedBy
        {
            get { return _lastUpdatedBy; }
            set { Set(LastUpdatedByPropertyName, ref _lastUpdatedBy, value); }
        }
    }

}
