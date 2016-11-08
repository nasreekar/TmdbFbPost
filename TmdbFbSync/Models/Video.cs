using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TmdbFbSync.Models
{
    [DataContract]
    public class Video
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string iso_639_1 { get; set; }
        [DataMember]
        public string iso_3166_1 { get; set; }
        [DataMember]
        public string key { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string site { get; set; }
        [DataMember]
        public int size { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    [DataContract]
    public class VideoResults
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public List<Video> results { get; set; }
    }
}