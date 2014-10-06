using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Windows.Storage;

namespace VSOTeams.Model
{
     [DataContract]
    public class IdentityImage
    {
        //var source = "https://sogeti.visualstudio.com/DefaultCollection/_api/_common/identityImage?id=" + i.UniqueUserId;
        //Uri u = new Uri(source);
          [DataMember]
        public Uri DownloadURL { get; set; }
          [DataMember]
        public string SafeLocation { get; set; }
    }
}
