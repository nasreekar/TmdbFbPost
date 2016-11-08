using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using System.Runtime.Serialization.Json;
using TmdbFbSync.Models;
using System.Text;
using System.Dynamic;

namespace TmdbFbSync
{
    public partial class facebooksync : System.Web.UI.Page
    {
        Movie m;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["code"] != null)
                    GetLatestMovie();
                else
                    GetRequestCode();
            }
        }

        //1. REQUEST CODE METHOD
        public void GetRequestCode()
        {
            if (Request["code"] == null)
            {
                Response.Redirect(string.Format(
                    "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                    Config.app_id, Request.Url.AbsoluteUri, Config.scope));
            }
        }

        public void GetLatestMovie()
        {
            var url = "https://api.themoviedb.org/3/movie/upcoming?api_key=" + Config.ApiKey +"&language=en-US";

            var syncClient = new WebClient();
            string content = syncClient.DownloadString(url);
            MovieResults mr = new MovieResults();

            // Create the Json serializer and parse the movie results response
            mr = JSONHelper.Deserialise<MovieResults>(content);

                var movieData = mr.results.FirstOrDefault(); //to get the first item of the results and parse 
                m = new Movie();
                m.id = movieData.id;
                m.overview = movieData.overview;
                m.title = movieData.title;
                m.poster_path = movieData.poster_path;
            
               if (MovieDbOps.checkValueExists(movieData.id))
                {
                    System.Diagnostics.Debug.WriteLine("Already Posted on Fb");
                }
                else
                {
                    MovieDbOps.UploadDbContent(m.id, m.title);
                    System.Diagnostics.Debug.WriteLine("Data saved in db");
          
                    var videoUrl = "https://api.themoviedb.org/3/movie/"+m.id+"/videos?api_key=" + Config.ApiKey + "&language=en-US";
                    string videoContent = syncClient.DownloadString(videoUrl);
                    VideoResults vr = new VideoResults();

                    // Create the Json serializer and parse the videourl response
                    vr = JSONHelper.Deserialise<VideoResults>(videoContent);
                    var videoData = vr.results.FirstOrDefault();
                    string videoKey = "https://www.youtube.com/watch?v=" + videoData.key.ToString();
                    
                    PostOnFb(m,videoKey);
                }
        }

        public void PostOnFb(Movie m, string videoUrl )
        {
            Dictionary<string, string> tokens = new Dictionary<string, string>();

            string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                Config.app_id, Request.Url.AbsoluteUri, Config.scope, Request["code"].ToString(), Config.app_secret);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string vals = reader.ReadToEnd();

                foreach (string token in vals.Split('&'))
                {
                    tokens.Add(token.Substring(0, token.IndexOf("=")),
                        token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                }
            }
            string access_token = tokens["access_token"];


            var client = new FacebookClient(access_token);

            client.Post("/958521567572809/feed", new
            {
                message ="Movie Synopsis: "+ m.overview, 
                name = m.title, 
                link = videoUrl, 
                description = "", 
                type = "photo", 
                access_token = access_token });
            
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Video Details posted on Fb')", true);
        }
    }
}