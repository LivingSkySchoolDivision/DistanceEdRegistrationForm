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
                BlackboardID = dataReader["BlackboardID"].ToString()
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

        public List<DistanceEdClass> GetAllClasses(DateTime ReferenceDate)
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
    }
}
