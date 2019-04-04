using System;
using MySql.Data.MySqlClient;
using PBScraper;

namespace PBScraper.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(Startup.DBConfiguration.ConnectionString);
            return conn;
        }
    }
}