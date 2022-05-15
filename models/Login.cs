
using System.Data;
using System.Data.SqlClient;

namespace evalproject.models
{
    public class Login:Imodel<Login>
    {
        public string email { get; set; }
        public string password { get; set; }

        public bool login(Login l)
        {

            SqlConnection con = new SqlConnection("Data Source=.\\SQLSERVER2019G3; Initial Catalog= evaluation; Integrated Security= true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from UsersAdmin where useremail=@email and password=@password and usertype='S' ");
            cmd.Connection = con;
            
            cmd.Parameters.AddWithValue("@email",l.email );
            cmd.Parameters.AddWithValue("@password",l.password);
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
        public bool update(Login l)
        {
            throw new System.NotImplementedException();
        }
    }
}
