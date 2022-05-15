using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace evalproject.models
{
    public class RegisterTable
    {
        public static SqlConnection con;

        public static SqlCommand cmd;

        public string RId { get; set; }
        public string RMail { get; set; }
        public string Name { get; set; }
        public string RPassword { get; set; }
        public static bool Registering(RegisterTable R)
        {
            try
            {
                con = new SqlConnection("Data Source=.\\SQLSERVER2019G3; Initial Catalog= evaluation; Integrated Security= true");
                con.Open();
                cmd = new SqlCommand("insert into usersadmin values(@Email,@Name,@password,@usertype)");
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@Email", R.RMail);
                cmd.Parameters.AddWithValue("@Name", R.Name);
                cmd.Parameters.AddWithValue("@password", R.RPassword);
                cmd.Parameters.AddWithValue("@usertype", R.RId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }


        }

    }
}
