using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySqlConnector;
namespace TODOProject.Modal
{
    public class connection
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter adpter;
        MySqlDataReader dataReader;
        public connection()
        {
            
            string hostname = "";
            string id = "";
            string password = "";
            string database = "";
            con = new MySqlConnection($"server={hostname};user id={id};password={password};database={database}");
        }
        public  DataTable GetList(string query)
        {
            cmd = new MySqlCommand(query, con);
            cmd.CommandTimeout = 30;
            adpter = new MySqlDataAdapter();
            adpter.SelectCommand = cmd;
            con.Open();
            DataTable dataTable = new DataTable();
            adpter.Fill(dataTable);
            con.Close();
            return dataTable;

        }
        public string  SaveQuery(string query)
        {
            string msg = string.Empty;
            cmd = new MySqlCommand(query, con);
            con.Open();
            dataReader =  cmd.ExecuteReader();
            msg = (dataReader.Read()?dataReader[0].ToString():null);
            con.Close();
            return msg;

        }

    }
}