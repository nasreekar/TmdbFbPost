using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TmdbFbSync.Models
{
[DataContract]
    public class MovieResults
    {
    [DataMember]
        public List<Movie> results { get; set; }
    }
}