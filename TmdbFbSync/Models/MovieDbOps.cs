using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TmdbFbSync.Models
{
    
    public class MovieDbOps
    {
        public static int UploadDbContent(int Id, string Name)
        {
            string sqlQuery = String.Format("Insert into Movie_UploadTb (Id, Movie_Name) Values('{0}', '{1}');", Id, Name);

            //Create and open a connection to SQL Server 
            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            connection.Open();

            //Create a Command object
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            //Execute the command to SQL Server and return the newly created ID
            int uploadId = (int)command.ExecuteNonQuery();

            //Close and dispose
            command.Dispose();
            connection.Close();
            connection.Dispose();

            //Set return value
            return uploadId;
        }

        public static bool checkValueExists(int Id)
        {

            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from [Movie_UploadTb]";
            cmd.Connection = connection;

            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                if (rd[0].Equals(Id))
                {
                    return true;
                }
            }

            return false;
        }
    }
}