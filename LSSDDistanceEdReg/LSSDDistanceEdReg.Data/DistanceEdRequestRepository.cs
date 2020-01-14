using LSSD.DistanceEdReg.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LSSD.DistanceEdReg.Data
{
    public class DistanceEdRequestRepository
    {
        private readonly string _connStr = string.Empty;

        public DistanceEdRequestRepository(string ConnectionString)
        {
            this._connStr = ConnectionString;
        }

        public void AddNewRequests(List<DistanceEdRequest> NewRequests)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {

                foreach (DistanceEdRequest NewRegistration in NewRequests)
                {
                    using (SqlCommand sqlCommand = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandText = "INSERT INTO DistanceEdRequest(CourseId,StudentName,StudentNumber,StudentSchool,MentorTeacherName,Requestor,Comments,DateRequested) VALUES(@COURSEID,@STUDNAME,@STUDNUMBER,@STUDSCHOOL,@MENTORNAME,@REQUESTOR,@COMMENTs,GETDATE());"
                    })
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("@COURSEID", NewRegistration.CourseID);
                        sqlCommand.Parameters.AddWithValue("@STUDNAME", NewRegistration.StudentName);
                        sqlCommand.Parameters.AddWithValue("@STUDNUMBER", NewRegistration.StudentNumber);
                        sqlCommand.Parameters.AddWithValue("@STUDSCHOOL", NewRegistration.StudentBaseSchool);
                        sqlCommand.Parameters.AddWithValue("@MENTORNAME", NewRegistration.MentorTeacherName);
                        sqlCommand.Parameters.AddWithValue("@REQUESTOR", NewRegistration.Requestor);
                        sqlCommand.Parameters.AddWithValue("@COMMENTs", NewRegistration.Comments);
                        sqlCommand.Connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Connection.Close();
                    }
                }
            }
        }

        public void AddNewRequest(DistanceEdRequest NewRequest)
        {
            AddNewRequests(new List<DistanceEdRequest>() { NewRequest });
        }

    }
}
