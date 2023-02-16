using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace StudentRegWebApi.Services
{
    public class StudentInfoService : IStudentInfoService
    {
        public int Add(StudentInfo studentInfo)
        {
            string sQry = "INSERT INTO [StudentRegistration] ([AdminNo],[LastName],[FirstName],[MiddleName],[StudentCourse]) " +
                "VALUES('" + studentInfo.AdminNo + "','" + studentInfo.LastName + "','" + studentInfo.FirstName + "','" +
                studentInfo.MiddleName + "','" + studentInfo.StudentCourse + "')";
            int retVal=ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        public int AddRange(IEnumerable<StudentInfo> students)
        {
            //string sQry = "INSERT INTO [BillGatesPlaceInfo] ([Place],[About],[City],[State],[Country]) VALUES";
            string sQry = "INSERT INTO [StudentRegistration] ([AdminNo],[LastName],[FirstName],[MiddleName],[StudentCourse]) VALUES";
            string sVal = "";
            foreach(var student in students)            
              sVal+= "('" + student.AdminNo + "','" + student.LastName + "','" + student.FirstName + "','" + student.MiddleName + "','" + student.StudentCourse + "'),";
            sVal = sVal.TrimEnd(',');
            sQry = sQry + sVal;
            int retVal=ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        public StudentInfo Find(int id)
        {
            StudentInfo studentInfo = null;
            string sQry = "SELECT * FROM [StudentRegistration] WHERE [idx]=" + id;
            DataTable dtStudentInfo = ExecuteQuery(sQry);
            if (dtStudentInfo != null)
            {
                DataRow dr = dtStudentInfo.Rows[0];
                studentInfo = GetPlaceInfoByRow(dr);
            }
            return studentInfo;
        }

        public IEnumerable<StudentInfo> GetAll()
        {
            List<StudentInfo> studentInfos = null;
            string sQry = "SELECT * FROM [StudentRegistration]";
            DataTable dtStudnetInfo = ExecuteQuery(sQry);           
            if (dtStudnetInfo != null)
            {
                studentInfos = new List<StudentInfo>();
                foreach (DataRow dr in dtStudnetInfo.Rows)
                    studentInfos.Add(GetPlaceInfoByRow(dr));
            }
            return studentInfos;
        }

        public int Remove(int id)
        {
            string sQry = "DELETE FROM [StudentRegistration] WHERE [idx]=" + id;
            int retVal=ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        public int Update(StudentInfo placeInfo)
        {
            string sQry = "UPDATE [StudentRegistration] SET [AdminNo]='" + placeInfo.AdminNo + "',[LastName]='" + placeInfo.LastName + "',[FirstName]='" + placeInfo.FirstName + "',[MiddleName]='" + placeInfo.MiddleName + "',[StudentCourse]='" + placeInfo.StudentCourse + "' WHERE [idx]=" + placeInfo.Id;
            int retVal=ExecuteCRUDByQuery(sQry);
            return retVal;            
        }


        private int ExecuteCRUDByQuery(string strSql)
        {
            string sConStr = "Data Source=DESKTOP-7LARJR9\\MSSQLERVER2919;Initial Catalog=Students;Integrated Security=True";
            SqlConnection conn = null;
            int iR = 0;
            try
            {
                conn = new SqlConnection(sConStr);
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                //Execute the command
                iR = cmd.ExecuteNonQuery();
            }
            catch { iR = 0; }
            finally { if (conn.State != 0) conn.Close(); }
            return iR;
        }

        private DataTable ExecuteQuery(string strSql)
        {
            string sConStr = "Data Source=DESKTOP-7LARJR9\\MSSQLERVER2919;Initial Catalog=Students;Integrated Security=True";
            SqlConnection conn = null;
            DataTable dt = null;
            try
            {
                conn = new SqlConnection(sConStr);                
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                dt = new DataTable();
                //Fill the dataset
                da.Fill(dt);
                if (!(dt.Rows.Count > 0)) dt = null;
            }
            catch { dt = null;  }
            finally { if (conn.State != 0) conn.Close(); }
            return dt;
        }

        private StudentInfo GetPlaceInfoByRow(DataRow dr)
        {
            StudentInfo placeInfo = new StudentInfo();
            placeInfo.Id = Convert.ToInt32(dr["idx"]);
            placeInfo.AdminNo = dr["AdminNo"].ToString();
            placeInfo.LastName = dr["LastName"].ToString();
            placeInfo.FirstName = dr["FirstName"].ToString();
            placeInfo.MiddleName = dr["MiddleName"].ToString();
            placeInfo.StudentCourse = dr["StudentCourse"].ToString();
            return placeInfo;
        }

    }
}
