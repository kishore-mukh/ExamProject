using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace evalproject.models
{
    public class Result
    {
        public string studentid { get; set; }
        public string studentName { get; set; }
        public string subjectName { get; set; }
        public DateTime dateofExam { get; set; }
        public string questionpapercode { get; set; }
        public int scoreObtained { get; set; }
        public Result()
        {

        }
        public Result(string sid, string studentname, string subjectname, DateTime dateofexam, string qnscode, int score)
        {
            studentid = sid;
            studentName = studentname;
            subjectName = subjectname;
            dateofExam = dateofexam;
            questionpapercode = qnscode;
            scoreObtained = score;
        }

        public static List<Result> fetchResults()
        {
            
            List<Result> stu = new List<Result>();
            SqlConnection con = new SqlConnection("Data Source=.\\SQLSERVER2019G3; Initial Catalog= evaluation; Integrated Security= true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select Studentid,StudentName,SubjectName,DateofExam,Questionpapercode,sum(ScoreObtained) as totalmark from StudentHistory group by Studentid,StudentName,SubjectName,DateofExam,Questionpapercode");
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Result e = new Result();
                e.studentid = dr[0].ToString();
                e.studentName = dr[1].ToString();
                e.subjectName = dr[2].ToString();
                e.dateofExam = Convert.ToDateTime(dr[3]);
                e.questionpapercode = dr[4].ToString();
                e.scoreObtained = Convert.ToInt32(dr[5]);
                stu.Add(e);

            }
            return stu;
        }

        public static List<Result> fetchstudentdata(string mail)
        {

            List<Result> stu = new List<Result>();
            SqlConnection con = new SqlConnection("Data Source=.\\SQLSERVER2019G3; Initial Catalog= evaluation; Integrated Security= true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select Studentid,StudentName,SubjectName,DateofExam,Questionpapercode,sum(ScoreObtained) as totalmark from StudentHistory where studentid=@mail group by Studentid,StudentName,SubjectName,DateofExam,Questionpapercode");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@mail", mail);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Result e = new Result();
                e.studentid = dr[0].ToString();
                e.studentName = dr[1].ToString();
                e.subjectName = dr[2].ToString();
                e.dateofExam = Convert.ToDateTime(dr[3]);
                e.questionpapercode = dr[4].ToString();
                e.scoreObtained = Convert.ToInt32(dr[5]);
                stu.Add(e);

            }
            return stu;
        }

    }
}
