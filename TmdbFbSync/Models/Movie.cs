using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TmdbFbSync.Models
{
    [DataContract]
    public class Movie
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string overview { get; set; }
        [DataMember]
        public object poster_path { get; set; }
        [DataMember]
        public string release_date { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public bool video { get; set; }

        public string Thumbnail
        {
            get
            {
                return string.Format("{0}{1}{2}", Config.BaseUrl, Config.ThumbnailSize, poster_path);
            }
        }

        public string Poster
        {
            get
            {
                return string.Format("{0}{1}{2}", Config.BaseUrl, Config.PosterSize, poster_path);
            }
        }

    }
}