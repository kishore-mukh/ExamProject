using System;
using System.Data.SqlClient;

namespace evalproject.models
{
    public class Adminlogin 
    {
        public string email { get; set; }
        public string password { get; set; }

        public static bool login(Adminlogin l)
        {

            SqlConnection con = new SqlConnection("Data Source=.\\SQLSERVER2019G3; Initial Catalog= evaluation; Integrated Security= true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from UsersAdmin where useremail=@email and password=@password and usertype='A' ");
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@email", l.email);
            cmd.Parameters.AddWithValue("@password", l.password);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }


        }

        public static bool resetpass(string mail,string pass)
        {
            try {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLSERVER2019G3; Initial Catalog= evaluation; Integrated Security= true");
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from UsersAdmin where useremail=@email");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@email", mail);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cmd = new SqlCommand("update UsersAdmin set password=@password where useremail=@email");
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@email", mail);
                    cmd.Parameters.AddWithValue("@password", pass);
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

        public static bool resetpassword(string mail, string oldpass,string newpass)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLSERVER2019G3; Initial Catalog= evaluation; Integrated Security= true");
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from UsersAdmin where useremail=@email and password=@password");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@email", mail);
                cmd.Parameters.AddWithValue("@password", oldpass);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cmd = new SqlCommand("update UsersAdmin set password=@password where useremail=@email");
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@email", mail);
                    cmd.Parameters.AddWithValue("@password", newpass);
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

        public bool update(Login l)
        {
            throw new System.NotImplementedException();
        }
    }
}
