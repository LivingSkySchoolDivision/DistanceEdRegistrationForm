using LSSD.DistanceEdReg.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LSSD.DistanceEdReg.Data
{
    public class DistanceEdRequestRepository : IDisposable
    {
        private readonly string _connStr = string.Empty;
        private Dictionary<int, DistanceEdClass> _classCache = new Dictionary<int, DistanceEdClass>();

        public DistanceEdRequestRepository(string ConnectionString)
        {
            this._connStr = ConnectionString;


            DistanceEdClassRepository _classRepo = new DistanceEdClassRepository(ConnectionString);
            foreach(DistanceEdClass dec in _classRepo.GetAllClasses())
            {
                _classCache.Add(dec.ID, dec);
            }
            
        }

        private DistanceEdRequest dataReaderToDistanceEdRequest(SqlDataReader dataReader)
        {
            int ID = dataReader["id"].ToString().Trim().ToInt();
            int CourseID = dataReader["CourseId"].ToString().Trim().ToInt();

            if (_classCache.ContainsKey(CourseID))
            {
                return new DistanceEdRequest()
                {
                    ID = ID,
                    StudentName = dataReader["StudentName"].ToString().Trim(),
                    StudentNumber = dataReader["StudentNumber"].ToString().Trim(),
                    StudentBaseSchool = dataReader["StudentSchool"].ToString().Trim(),
                    Comments = dataReader["Comments"].ToString().Trim(),
                    CourseID = CourseID,
                    MentorTeacherName = dataReader["MentorTeacherName"].ToString().Trim(),
                    Requestor = dataReader["Requestor"].ToString().Trim(),
                    DateRequested = dataReader["DateRequested"].ToString().Trim().ToDateTime(),
                    HelpDeskNotificationSent = dataReader["NotificationSentToHelpDesk"].ToString().Trim().ToBool(),
                    DistanceEdClass = _classCache[CourseID]
                };
            }

            throw new Exception("Couldn't find corresponding class for ID " + dataReader["CourseId"].ToString());
            return null;            
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

        public void MarkAsHelpDeskNotified(int id)
        {
            MarkAsHelpDeskNotified(new List<int>() { id });
        }

        public void MarkAsHelpDeskNotified(List<int> ids)
        {
            if (ids.Count > 0)
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    foreach (int id in ids)
                    {
                        using (SqlCommand sqlCommand = new SqlCommand
                        {
                            Connection = connection,
                            CommandType = CommandType.Text,
                            CommandText = "UPDATE DistanceEdRequest SET NOtificationSentToHelpDesk=1 WHERE id=@RID"
                        })
                        {
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.AddWithValue("@RID", id);
                            sqlCommand.Connection.Open();
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand.Connection.Close();
                        }
                    }
                }
            }
        }

        public void MarkAsHelpDeskNotified(DistanceEdRequest request)
        {
            MarkAsHelpDeskNotified(request.ID);
        }

        public List<DistanceEdRequest> GetRequestsRequiringNotification()
        {
            List<DistanceEdRequest> returnMe = new List<DistanceEdRequest>();

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT TOP 25 * FROM DistanceEdRequest WHERE NotificationSentToHelpDesk=0"
                })
                {
                    sqlCommand.Connection.Open();
                    SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                    if (dbDataReader.HasRows)
                    {
                        while (dbDataReader.Read())
                        {
                            DistanceEdRequest obj = dataReaderToDistanceEdRequest(dbDataReader);
                            if (obj != null)
                            {
                                returnMe.Add(obj);
                            }
                        }
                    }

                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        public void AddNewRequest(DistanceEdRequest NewRequest)
        {
            AddNewRequests(new List<DistanceEdRequest>() { NewRequest });
        }

        public void Dispose()
        {
            
        }
    }
}
