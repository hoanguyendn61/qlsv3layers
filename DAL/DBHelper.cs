using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace qlSV_3Layers.DAL
{
    public class DBHelper
    {
        private static DBHelper _Instance;
        private string cnnstr = ConfigurationManager.ConnectionStrings["qlSV_3Layers.Properties.Settings.Setting"].ConnectionString;
        public static DBHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DBHelper();
                }
                return _Instance;
            }
            private set { }
        }
        private DBHelper()
        {
            
        }
        public DataTable GetRecords(string sql)
        {
            DataTable dt = new DataTable();
            //SqlCommand cmd = new SqlCommand(sql, cnn);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //if (cnn.State == ConnectionState.Closed)
            //{
            //    cnn.Open();
            //}
            //    da.Fill(dt);
            //    cnn.Close();

            using (SqlConnection cnn = new SqlConnection(cnnstr))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(sql, cnn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                cnn.Close();
            }
            return dt;
                
        }
        public bool ExecuteDB(string sql)
        {
            //    SqlCommand cmd = new SqlCommand(sql, cnn);
            //if (cnn.State == ConnectionState.Closed)
            //{
            //    cnn.Open();
            //}
            //cmd.ExecuteNonQuery();
            //cnn.Close();
           // int data = 0;

            using (SqlConnection cnn = new SqlConnection(cnnstr))
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    return false;
                }
                cnn.Close();
            }
            return true;
        }
    }
}
