﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace VSOTeams
{
    public partial class App : global::Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
        private global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlTypeInfoProvider _provider;

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            if(_provider == null)
            {
                _provider = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            if(_provider == null)
            {
                _provider = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(fullName);
        }

        public global::Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Windows.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }
}

namespace VSOTeams.VSOTeams_Windows_XamlTypeInfo
{
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByType(type);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByName(typeName);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;

        private void InitTypeTables()
        {
            _typeNameTable = new string[26];
            _typeNameTable[0] = "VSOTeams.ViewModel.ViewModelLocator";
            _typeNameTable[1] = "Object";
            _typeNameTable[2] = "VSOTeams.ViewModel.ProjectViewModel";
            _typeNameTable[3] = "VSOTeams.ViewModel.ViewModelBaseState";
            _typeNameTable[4] = "GalaSoft.MvvmLight.ViewModelBase";
            _typeNameTable[5] = "GalaSoft.MvvmLight.ObservableObject";
            _typeNameTable[6] = "VSOTeams.ViewModel.ProjectTeamsViewModel";
            _typeNameTable[7] = "VSOTeams.ViewModel.TeamsViewModel";
            _typeNameTable[8] = "VSOTeams.ViewModel.UserViewModel";
            _typeNameTable[9] = "VSOTeams.ViewModel.AllUsersViewModel";
            _typeNameTable[10] = "VSOTeams.ViewModel.TeamRoomsViewModel";
            _typeNameTable[11] = "VSOTeams.ViewModel.MessagesViewModel";
            _typeNameTable[12] = "VSOTeams.Converter.BooleanToVisibilityConverter";
            _typeNameTable[13] = "VSOTeams.ViewBase";
            _typeNameTable[14] = "Windows.UI.Xaml.Controls.Page";
            _typeNameTable[15] = "Windows.UI.Xaml.Controls.UserControl";
            _typeNameTable[16] = "VSOTeams.AllUsersHubPage";
            _typeNameTable[17] = "VSOTeams.ProjectTeamsHubPage";
            _typeNameTable[18] = "VSOTeams.ProjectHubPage";
            _typeNameTable[19] = "VSOTeams.Settings.CredentialSettingsFlyOut";
            _typeNameTable[20] = "Windows.UI.Xaml.Controls.SettingsFlyout";
            _typeNameTable[21] = "Windows.UI.Xaml.Controls.ContentControl";
            _typeNameTable[22] = "VSOTeams.TeamHubPage";
            _typeNameTable[23] = "VSOTeams.TeamRoomHubPage";
            _typeNameTable[24] = "VSOTeams.TeamRoomsHubPage";
            _typeNameTable[25] = "VSOTeams.UserDetailsHubPage";

            _typeTable = new global::System.Type[26];
            _typeTable[0] = typeof(global::VSOTeams.ViewModel.ViewModelLocator);
            _typeTable[1] = typeof(global::System.Object);
            _typeTable[2] = typeof(global::VSOTeams.ViewModel.ProjectViewModel);
            _typeTable[3] = typeof(global::VSOTeams.ViewModel.ViewModelBaseState);
            _typeTable[4] = typeof(global::GalaSoft.MvvmLight.ViewModelBase);
            _typeTable[5] = typeof(global::GalaSoft.MvvmLight.ObservableObject);
            _typeTable[6] = typeof(global::VSOTeams.ViewModel.ProjectTeamsViewModel);
            _typeTable[7] = typeof(global::VSOTeams.ViewModel.TeamsViewModel);
            _typeTable[8] = typeof(global::VSOTeams.ViewModel.UserViewModel);
            _typeTable[9] = typeof(global::VSOTeams.ViewModel.AllUsersViewModel);
            _typeTable[10] = typeof(global::VSOTeams.ViewModel.TeamRoomsViewModel);
            _typeTable[11] = typeof(global::VSOTeams.ViewModel.MessagesViewModel);
            _typeTable[12] = typeof(global::VSOTeams.Converter.BooleanToVisibilityConverter);
            _typeTable[13] = typeof(global::VSOTeams.ViewBase);
            _typeTable[14] = typeof(global::Windows.UI.Xaml.Controls.Page);
            _typeTable[15] = typeof(global::Windows.UI.Xaml.Controls.UserControl);
            _typeTable[16] = typeof(global::VSOTeams.AllUsersHubPage);
            _typeTable[17] = typeof(global::VSOTeams.ProjectTeamsHubPage);
            _typeTable[18] = typeof(global::VSOTeams.ProjectHubPage);
            _typeTable[19] = typeof(global::VSOTeams.Settings.CredentialSettingsFlyOut);
            _typeTable[20] = typeof(global::Windows.UI.Xaml.Controls.SettingsFlyout);
            _typeTable[21] = typeof(global::Windows.UI.Xaml.Controls.ContentControl);
            _typeTable[22] = typeof(global::VSOTeams.TeamHubPage);
            _typeTable[23] = typeof(global::VSOTeams.TeamRoomHubPage);
            _typeTable[24] = typeof(global::VSOTeams.TeamRoomsHubPage);
            _typeTable[25] = typeof(global::VSOTeams.UserDetailsHubPage);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_ViewModelLocator() { return new global::VSOTeams.ViewModel.ViewModelLocator(); }
        private object Activate_3_ViewModelBaseState() { return new global::VSOTeams.ViewModel.ViewModelBaseState(); }
        private object Activate_5_ObservableObject() { return new global::GalaSoft.MvvmLight.ObservableObject(); }
        private object Activate_12_BooleanToVisibilityConverter() { return new global::VSOTeams.Converter.BooleanToVisibilityConverter(); }
        private object Activate_13_ViewBase() { return new global::VSOTeams.ViewBase(); }
        private object Activate_16_AllUsersHubPage() { return new global::VSOTeams.AllUsersHubPage(); }
        private object Activate_17_ProjectTeamsHubPage() { return new global::VSOTeams.ProjectTeamsHubPage(); }
        private object Activate_18_ProjectHubPage() { return new global::VSOTeams.ProjectHubPage(); }
        private object Activate_19_CredentialSettingsFlyOut() { return new global::VSOTeams.Settings.CredentialSettingsFlyOut(); }
        private object Activate_22_TeamHubPage() { return new global::VSOTeams.TeamHubPage(); }
        private object Activate_23_TeamRoomHubPage() { return new global::VSOTeams.TeamRoomHubPage(); }
        private object Activate_24_TeamRoomsHubPage() { return new global::VSOTeams.TeamRoomsHubPage(); }
        private object Activate_25_UserDetailsHubPage() { return new global::VSOTeams.UserDetailsHubPage(); }

        private global::Windows.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  VSOTeams.ViewModel.ViewModelLocator
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.Activator = Activate_0_ViewModelLocator;
                userType.AddMemberName("ProjectsVM");
                userType.AddMemberName("ProjectTeamsVM");
                userType.AddMemberName("TeamsVM");
                userType.AddMemberName("UserVM");
                userType.AddMemberName("AllUsersVM");
                userType.AddMemberName("TeamRoomsVM");
                userType.AddMemberName("MessagesVM");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 1:   //  Object
                xamlType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  VSOTeams.ViewModel.ProjectViewModel
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewModel.ViewModelBaseState"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 3:   //  VSOTeams.ViewModel.ViewModelBaseState
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("GalaSoft.MvvmLight.ViewModelBase"));
                userType.Activator = Activate_3_ViewModelBaseState;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 4:   //  GalaSoft.MvvmLight.ViewModelBase
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("GalaSoft.MvvmLight.ObservableObject"));
                xamlType = userType;
                break;

            case 5:   //  GalaSoft.MvvmLight.ObservableObject
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.Activator = Activate_5_ObservableObject;
                xamlType = userType;
                break;

            case 6:   //  VSOTeams.ViewModel.ProjectTeamsViewModel
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewModel.ViewModelBaseState"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 7:   //  VSOTeams.ViewModel.TeamsViewModel
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewModel.ViewModelBaseState"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 8:   //  VSOTeams.ViewModel.UserViewModel
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewModel.ViewModelBaseState"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 9:   //  VSOTeams.ViewModel.AllUsersViewModel
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewModel.ViewModelBaseState"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 10:   //  VSOTeams.ViewModel.TeamRoomsViewModel
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewModel.ViewModelBaseState"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 11:   //  VSOTeams.ViewModel.MessagesViewModel
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewModel.ViewModelBaseState"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 12:   //  VSOTeams.Converter.BooleanToVisibilityConverter
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.Activator = Activate_12_BooleanToVisibilityConverter;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 13:   //  VSOTeams.ViewBase
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_13_ViewBase;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 14:   //  Windows.UI.Xaml.Controls.Page
                xamlType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 15:   //  Windows.UI.Xaml.Controls.UserControl
                xamlType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 16:   //  VSOTeams.AllUsersHubPage
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewBase"));
                userType.Activator = Activate_16_AllUsersHubPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 17:   //  VSOTeams.ProjectTeamsHubPage
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewBase"));
                userType.Activator = Activate_17_ProjectTeamsHubPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 18:   //  VSOTeams.ProjectHubPage
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewBase"));
                userType.Activator = Activate_18_ProjectHubPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 19:   //  VSOTeams.Settings.CredentialSettingsFlyOut
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
                userType.Activator = Activate_19_CredentialSettingsFlyOut;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 20:   //  Windows.UI.Xaml.Controls.SettingsFlyout
                xamlType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 21:   //  Windows.UI.Xaml.Controls.ContentControl
                xamlType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 22:   //  VSOTeams.TeamHubPage
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewBase"));
                userType.Activator = Activate_22_TeamHubPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 23:   //  VSOTeams.TeamRoomHubPage
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewBase"));
                userType.Activator = Activate_23_TeamRoomHubPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 24:   //  VSOTeams.TeamRoomsHubPage
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewBase"));
                userType.Activator = Activate_24_TeamRoomsHubPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 25:   //  VSOTeams.UserDetailsHubPage
                userType = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("VSOTeams.ViewBase"));
                userType.Activator = Activate_25_UserDetailsHubPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;
            }
            return xamlType;
        }


        private object get_0_ViewModelLocator_ProjectsVM(object instance)
        {
            var that = (global::VSOTeams.ViewModel.ViewModelLocator)instance;
            return that.ProjectsVM;
        }
        private object get_1_ViewModelLocator_ProjectTeamsVM(object instance)
        {
            var that = (global::VSOTeams.ViewModel.ViewModelLocator)instance;
            return that.ProjectTeamsVM;
        }
        private object get_2_ViewModelLocator_TeamsVM(object instance)
        {
            var that = (global::VSOTeams.ViewModel.ViewModelLocator)instance;
            return that.TeamsVM;
        }
        private object get_3_ViewModelLocator_UserVM(object instance)
        {
            var that = (global::VSOTeams.ViewModel.ViewModelLocator)instance;
            return that.UserVM;
        }
        private object get_4_ViewModelLocator_AllUsersVM(object instance)
        {
            var that = (global::VSOTeams.ViewModel.ViewModelLocator)instance;
            return that.AllUsersVM;
        }
        private object get_5_ViewModelLocator_TeamRoomsVM(object instance)
        {
            var that = (global::VSOTeams.ViewModel.ViewModelLocator)instance;
            return that.TeamRoomsVM;
        }
        private object get_6_ViewModelLocator_MessagesVM(object instance)
        {
            var that = (global::VSOTeams.ViewModel.ViewModelLocator)instance;
            return that.MessagesVM;
        }

        private global::Windows.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlMember xamlMember = null;
            global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "VSOTeams.ViewModel.ViewModelLocator.ProjectsVM":
                userType = (global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType)GetXamlTypeByName("VSOTeams.ViewModel.ViewModelLocator");
                xamlMember = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlMember(this, "ProjectsVM", "VSOTeams.ViewModel.ProjectViewModel");
                xamlMember.Getter = get_0_ViewModelLocator_ProjectsVM;
                xamlMember.SetIsReadOnly();
                break;
            case "VSOTeams.ViewModel.ViewModelLocator.ProjectTeamsVM":
                userType = (global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType)GetXamlTypeByName("VSOTeams.ViewModel.ViewModelLocator");
                xamlMember = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlMember(this, "ProjectTeamsVM", "VSOTeams.ViewModel.ProjectTeamsViewModel");
                xamlMember.Getter = get_1_ViewModelLocator_ProjectTeamsVM;
                xamlMember.SetIsReadOnly();
                break;
            case "VSOTeams.ViewModel.ViewModelLocator.TeamsVM":
                userType = (global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType)GetXamlTypeByName("VSOTeams.ViewModel.ViewModelLocator");
                xamlMember = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlMember(this, "TeamsVM", "VSOTeams.ViewModel.TeamsViewModel");
                xamlMember.Getter = get_2_ViewModelLocator_TeamsVM;
                xamlMember.SetIsReadOnly();
                break;
            case "VSOTeams.ViewModel.ViewModelLocator.UserVM":
                userType = (global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType)GetXamlTypeByName("VSOTeams.ViewModel.ViewModelLocator");
                xamlMember = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlMember(this, "UserVM", "VSOTeams.ViewModel.UserViewModel");
                xamlMember.Getter = get_3_ViewModelLocator_UserVM;
                xamlMember.SetIsReadOnly();
                break;
            case "VSOTeams.ViewModel.ViewModelLocator.AllUsersVM":
                userType = (global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType)GetXamlTypeByName("VSOTeams.ViewModel.ViewModelLocator");
                xamlMember = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlMember(this, "AllUsersVM", "VSOTeams.ViewModel.AllUsersViewModel");
                xamlMember.Getter = get_4_ViewModelLocator_AllUsersVM;
                xamlMember.SetIsReadOnly();
                break;
            case "VSOTeams.ViewModel.ViewModelLocator.TeamRoomsVM":
                userType = (global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType)GetXamlTypeByName("VSOTeams.ViewModel.ViewModelLocator");
                xamlMember = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlMember(this, "TeamRoomsVM", "VSOTeams.ViewModel.TeamRoomsViewModel");
                xamlMember.Getter = get_5_ViewModelLocator_TeamRoomsVM;
                xamlMember.SetIsReadOnly();
                break;
            case "VSOTeams.ViewModel.ViewModelLocator.MessagesVM":
                userType = (global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlUserType)GetXamlTypeByName("VSOTeams.ViewModel.ViewModelLocator");
                xamlMember = new global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlMember(this, "MessagesVM", "VSOTeams.ViewModel.MessagesViewModel");
                xamlMember.Getter = get_6_ViewModelLocator_MessagesVM;
                xamlMember.SetIsReadOnly();
                break;
            }
            return xamlMember;
        }
    }

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlSystemBaseType : global::Windows.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlUserType : global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlSystemBaseType
    {
        global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Windows.UI.Xaml.Markup.IXamlType _baseType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, global::System.Type fullType, global::Windows.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }

        override public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        override public object CreateFromString(string input)
        {
            if (_enumValues != null)
            {
                int value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    int enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt32(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return value; 
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlMember : global::Windows.UI.Xaml.Markup.IXamlMember
    {
        global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::VSOTeams.VSOTeams_Windows_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Windows.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Windows.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}


