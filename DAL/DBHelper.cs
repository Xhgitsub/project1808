using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class DBHelper
    {
        public static string StrConn = $"Data Source=DESKTOP-P71O688;Initial Catalog=PH6Lianxi;User ID=sa;Pwd=1234";

        public static int ExcureNonQuery(string sql) 
        {
            using (SqlConnection conn=new SqlConnection(StrConn)) 
            {
                int n=0;
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                     n = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                   
                    throw;
                }
                
            
                return n;
            }
        }

        public static DataSet GetSet(string sql)
        {
            using (SqlConnection conn=new SqlConnection(StrConn)) 
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql,conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
        }
    }
}
