using LSSD.DistanceEdReg;
using LSSD.DistanceEdReg.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LSSD.DistanceEdReg.Data
{
    public class DistanceEdClassRepository
    {
        private readonly string _connStr = string.Empty;
        
        public DistanceEdClassRepository(string ConnectionString)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception("Connection String cannot be empty");
            }

            this._connStr = ConnectionString;
        }        

        private DistanceEdClass dataReaderToDistanceEdClass(SqlDataReader dataReader)
        {   
            return new DistanceEdClass()
            {
                ID = Parsers.ToInt(dataReader["id"].ToString()),
                Name = dataReader["Name"].ToString(),
                Description = dataReader["Description"].ToString(),
                RegistrationAvailableFrom = Parsers.ToDateTime(dataReader["RegistrationAvailableFrom"].ToString()),
                RegistrationAvailableTo = Parsers.ToDateTime(dataReader["RegistrationAvailableTo"].ToString()),
                Starts = Parsers.ToDateTime(dataReader["StartDate"].ToString()),
                Ends = Parsers.ToDateTime(dataReader["EndDate"].ToString()),
                MoreInfoURL = dataReader["InfoURL"].ToString(),
                DeliveryMethod = dataReader["DeliveryMethod"].ToString(),
                PreRequisites = dataReader["PreRequisites"].ToString(),
                RequiredMaterials = dataReader["RequiredMaterials"].ToString(),
                MaterialsAvailableToTeachers = Parsers.ToBool(dataReader["AreMaterialsAvailable"].ToString()),
                MentorTeacherRequired = Parsers.ToBool(dataReader["RequiresMentor"].ToString()),
                BlackboardID = dataReader["BlackboardID"].ToString(),
                IsRequestable = Parsers.ToBool(dataReader["IsRequestable"].ToString())
            };
        }

        public List<DistanceEdClass> GetAvailableClasses(DateTime ReferenceDate)
        {
            List<DistanceEdClass> returnMe = new List<DistanceEdClass>();

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM DistanceEdClass WHERE RegistrationAvailableFrom<=GETDATE() AND RegistrationAvailableTo>=GETDATE();"
                })
                {
                    sqlCommand.Connection.Open();
                    SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                    if (dbDataReader.HasRows)
                    {
                        while (dbDataReader.Read())
                        {
                            DistanceEdClass obj = dataReaderToDistanceEdClass(dbDataReader);
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

        public List<DistanceEdClass> GetAllClasses()
        {
            List<DistanceEdClass> returnMe = new List<DistanceEdClass>();

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM DistanceEdClass;"
                })
                {
                    sqlCommand.Connection.Open();
                    SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                    if (dbDataReader.HasRows)
                    {
                        while (dbDataReader.Read())
                        {
                            DistanceEdClass obj = dataReaderToDistanceEdClass(dbDataReader);
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

        public DistanceEdClass Get(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM DistanceEdClass WHERE ID=@CLASSID;"
                })
                {
                    sqlCommand.Parameters.AddWithValue("CLASSID", ID);

                    sqlCommand.Connection.Open();
                    
                    SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                    if (dbDataReader.HasRows)
                    {
                        while (dbDataReader.Read())
                        {
                            DistanceEdClass obj = dataReaderToDistanceEdClass(dbDataReader);
                            if (obj != null)
                            {
                                return obj;
                            }
                        }
                    }

                    sqlCommand.Connection.Close();
                }
            }

            return null;
        }

        public void Add(DistanceEdClass DEClass)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "INSERT INTO DistanceEdClass(BlackboardID,Name,RegistrationAvailableFrom,RegistrationAvailableTo,InfoURL,Description,IsRequestable,AreMaterialsAvailable,RequiresMentor,DeliveryMethod,PreRequisites,RequiredMaterials,StartDate,EndDate) VALUES(@BBID,@CNAME,@AVAILFROM,@AVAILTO,@INFOURL,@CDESC,@ISREQUESTABLE,@MATERIALSAVAIL,@REQUIRESMENTOR,@DELIVERYMETHOD,@PREREQS,@REQMATS,@STARTDATE,@ENDDATE);"
                })
                {
                    sqlCommand.Parameters.AddWithValue("@BBID",DEClass.BlackboardID);
                    sqlCommand.Parameters.AddWithValue("@CNAME",DEClass.Name);
                    sqlCommand.Parameters.AddWithValue("@AVAILFROM",DEClass.RegistrationAvailableFrom);
                    sqlCommand.Parameters.AddWithValue("@AVAILTO",DEClass.RegistrationAvailableTo);
                    sqlCommand.Parameters.AddWithValue("@INFOURL",DEClass.MoreInfoURL);
                    sqlCommand.Parameters.AddWithValue("@CDESC",DEClass.Description);
                    sqlCommand.Parameters.AddWithValue("@ISREQUESTABLE",DEClass.IsRequestable);
                    sqlCommand.Parameters.AddWithValue("@MATERIALSAVAIL",DEClass.MaterialsAvailableToTeachers);
                    sqlCommand.Parameters.AddWithValue("@REQUIRESMENTOR",DEClass.MentorTeacherRequired);
                    sqlCommand.Parameters.AddWithValue("@DELIVERYMETHOD",DEClass.DeliveryMethod);
                    sqlCommand.Parameters.AddWithValue("@PREREQS",DEClass.PreRequisites);
                    sqlCommand.Parameters.AddWithValue("@REQMATS",DEClass.RequiredMaterials);
                    sqlCommand.Parameters.AddWithValue("@STARTDATE",DEClass.Starts);
                    sqlCommand.Parameters.AddWithValue("@ENDDATE",DEClass.Ends);
                    sqlCommand.Connection.Open();
                    SqlDataReader dbDataReader = sqlCommand.ExecuteReader();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
        }

        public void Update(DistanceEdClass DEClass)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "UPDATE DistanceEdClass SET BlackboardID=@BBID, Name=@CNAME, RegistrationAvailableFrom=@AVAILFROM, RegistrationAvailableTo=@AVAILTO, InfoURL=@INFOURL, Description=@CDESC, IsRequestable=@ISREQUESTABLE, AreMaterialsAvailable=@MATERIALSAVAIL, RequiresMentor=@REQUIRESMENTOR, DeliveryMethod=@DELIVERYMETHOD, PreRequisites=@PREREQS, RequiredMaterials=@REQMATS, StartDate=@STARTDATE, EndDate=@ENDDATE WHERE id=@CID ;"
                })
                {
                    sqlCommand.Parameters.AddWithValue("@CID", DEClass.ID);
                    sqlCommand.Parameters.AddWithValue("@BBID", DEClass.BlackboardID);
                    sqlCommand.Parameters.AddWithValue("@CNAME", DEClass.Name);
                    sqlCommand.Parameters.AddWithValue("@AVAILFROM", DEClass.RegistrationAvailableFrom);
                    sqlCommand.Parameters.AddWithValue("@AVAILTO", DEClass.RegistrationAvailableTo);
                    sqlCommand.Parameters.AddWithValue("@INFOURL", DEClass.MoreInfoURL);
                    sqlCommand.Parameters.AddWithValue("@CDESC", DEClass.Description);
                    sqlCommand.Parameters.AddWithValue("@ISREQUESTABLE", DEClass.IsRequestable);
                    sqlCommand.Parameters.AddWithValue("@MATERIALSAVAIL", DEClass.MaterialsAvailableToTeachers);
                    sqlCommand.Parameters.AddWithValue("@REQUIRESMENTOR", DEClass.MentorTeacherRequired);
                    sqlCommand.Parameters.AddWithValue("@DELIVERYMETHOD", DEClass.DeliveryMethod);
                    sqlCommand.Parameters.AddWithValue("@PREREQS", DEClass.PreRequisites);
                    sqlCommand.Parameters.AddWithValue("@REQMATS", DEClass.RequiredMaterials);
                    sqlCommand.Parameters.AddWithValue("@STARTDATE", DEClass.Starts);
                    sqlCommand.Parameters.AddWithValue("@ENDDATE", DEClass.Ends);
                    sqlCommand.Connection.Open();
                    SqlDataReader dbDataReader = sqlCommand.ExecuteReader();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
        }
    }
}
