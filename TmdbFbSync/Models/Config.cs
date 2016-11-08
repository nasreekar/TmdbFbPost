using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TmdbFbSync.Models
{
    public class Config
    {
        public static string BaseUrl = "http://image.tmdb.org/t/p/";
        public static string ThumbnailSize = "w92";
        public static string PosterSize = "w342";

        public static string ApiKey = "cb44a0858d07d72908cb02677d4f901a";

        public static string ConnectionString = "Data Source=ABHIJITH;Initial Catalog=Tmdb_db;Integrated Security=True";
        public static string app_id = "1171418069606882";
        public static string app_secret = "1b331060fe30ee9126765c2025aef305";
        public static string scope = "publish_pages,manage_pages";

    }
}