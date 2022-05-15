using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace evalproject.models
{
    public class Questions
    {
        public static SqlConnection con;
        public static SqlCommand cmd;

        
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Answer { get; set; }
        public int Marks { get; set; }

        public static async Task<List<Questions>> ExelData(IFormFile file)
        {

            List<Questions> QArray = new List<Questions>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (var row = 2; row <= rowcount; row++)
                    {
                        QArray.Add(new Questions
                        {
                            Question = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            OptionA = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            OptionB = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            OptionC = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            OptionD = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            Answer = worksheet.Cells[row, 6].Value.ToString().Trim(),
                            Marks = Convert.ToInt32(worksheet.Cells[row, 7].Value)
                        });
                    }
                }
            }
            return QArray;
        }


        public async static void GetDetails(IFormFile path,string subject,string mail)
        {
            con = new SqlConnection("Data Source=.\\SQLSERVER2019G3;Initial Catalog=evaluation;Integrated Security=true");
            con.Open();

            List<Questions> QArray = await ExelData(path);
            foreach (var i in QArray)
            {
                cmd = new SqlCommand("insert into questionanswer @subject,@Mark,@Qstn,@Ans,@OptA,@OptB,@OptC,@OptD");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Qsubject", subject);
                cmd.Parameters.AddWithValue("@Qstn", i.Question);
                cmd.Parameters.AddWithValue("@OptA", i.OptionA);
                cmd.Parameters.AddWithValue("@OptB", i.OptionB);
                cmd.Parameters.AddWithValue("@OptC", i.OptionC);
                cmd.Parameters.AddWithValue("@OptD", i.OptionD);
                cmd.Parameters.AddWithValue("@Ans", i.Answer);
                cmd.Parameters.AddWithValue("@Mark", i.Marks);
                cmd.ExecuteNonQuery();
            }

        }
    }
}
