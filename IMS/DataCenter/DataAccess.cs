using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.DataCenter
{
    public class DataAccess
    {
        MySqlConnection cn = new MySqlConnection("server=localhost;userid=root;password=;database=inventory;");

        public MySqlConnection ConOpen()
        {
            if(cn.State != System.Data.ConnectionState.Open)
            {
                cn.Open();
            }

            if(cn == null)
            {
                cn.Open();
            }
            return cn;
        }


        public DataTable InsertData(string query)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(query, ConOpen());
            da.Fill(dt);
            return dt;
        }


    }
}
