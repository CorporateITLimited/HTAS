using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Users
{
    public class UserTypeGateway : DefaultGateway
    {
        public List<UserType> GetAllUserType()
        {
            try
            {
                Sql_Query = "[user].[spUserTypeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Select;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<UserType> UserTypeListVM = new List<UserType>();

                while (Data_Reader.Read())
                {
                    UserType UserTypeVM = new UserType
                    {
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                       
                        UserTypeId = Convert.ToInt32(Data_Reader["UserTypeId"]),
                        UserTypeName = Data_Reader["UserTypeName"].ToString(),
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUserName = Data_Reader["CreatedByUserName"].ToString()
                    };

                    UserTypeListVM.Add(UserTypeVM);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return UserTypeListVM;
            }
            catch (SqlException exception)
            {
                for (int i = 0; i < exception.Errors.Count; i++)
                {
                    ErrorMessages.Append("Index #" + i + "\n" +
                        "Message: " + exception.Errors[i].Message + "\n" +
                        "Error Number: " + exception.Errors[i].Number + "\n" +
                        "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
                        "Source: " + exception.Errors[i].Source + "\n" +
                        "Procedure: " + exception.Errors[i].Procedure + "\n");
                }
                throw new Exception(ErrorMessages.ToString());
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                    Sql_Connection.Close();
            }

        }


        public UserType GetUserTypeById(int id)
        {
            try
            {
                Sql_Query = "[user].[spUserTypeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@UserTypeId", SqlDbType.NVarChar).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                UserType UserTypeVM = new UserType();

                while (Data_Reader.Read())
                {
                    UserTypeVM.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    UserTypeVM.IsActive = Data_Reader["IsActive"] !=
                                           DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    UserTypeVM.IsDeleted = Data_Reader["IsDeleted"] !=
                                      DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                   
                    UserTypeVM.UserTypeId = Convert.ToInt32(Data_Reader["UserTypeId"]);
                    UserTypeVM.UserTypeName = Data_Reader["UserTypeName"].ToString();
                    UserTypeVM.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                         DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    UserTypeVM.UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString();
                    UserTypeVM.CreatedBy = Data_Reader["CreatedBy"] !=
                                               DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    UserTypeVM.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                            Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    UserTypeVM.CreatedByUserName = Data_Reader["CreatedByUserName"].ToString();


                };

                Data_Reader.Close();
                Sql_Connection.Close();

                return UserTypeVM;
            }
            catch (SqlException exception)
            {
                for (int i = 0; i < exception.Errors.Count; i++)
                {
                    ErrorMessages.Append("Index #" + i + "\n" +
                        "Message: " + exception.Errors[i].Message + "\n" +
                        "Error Number: " + exception.Errors[i].Number + "\n" +
                        "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
                        "Source: " + exception.Errors[i].Source + "\n" +
                        "Procedure: " + exception.Errors[i].Procedure + "\n");
                }
                throw new Exception(ErrorMessages.ToString());
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                    Sql_Connection.Close();
            }

        }

        //Create UserType 
        public int UserTypeInsert(UserType UserType)
        {
            try
            {
                Sql_Query = "[user].[spUserTypeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;
                Sql_Command.Parameters.Add("@UserTypeName", SqlDbType.NVarChar).Value = UserType.UserTypeName;
                
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = UserType.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = UserType.CreateDate;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = UserType.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = UserType.LastUpdated;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = UserType.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = UserType.IsDeleted;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();

                int rowAffected = Sql_Command.ExecuteNonQuery();
                Sql_Connection.Close();

                int resultOutPut = int.Parse(result.Value.ToString());

                return resultOutPut;
            }
            catch (SqlException exception)
            {
                for (int i = 0; i < exception.Errors.Count; i++)
                {
                    ErrorMessages.Append("Index #" + i + "\n" +
                        "Message: " + exception.Errors[i].Message + "\n" +
                        "Error Number: " + exception.Errors[i].Number + "\n" +
                        "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
                        "Source: " + exception.Errors[i].Source + "\n" +
                        "Procedure: " + exception.Errors[i].Procedure + "\n");
                }
                throw new Exception(ErrorMessages.ToString());
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                    Sql_Connection.Close();
            }

        }

        //Create UserType Update
        public int UserTypeUpdate(UserType UserType)
        {
            try
            {
                Sql_Query = "[user].[spUserTypeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@UserTypeName", SqlDbType.NVarChar).Value = UserType.UserTypeName;
                
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = UserType.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = UserType.LastUpdated;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = UserType.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = UserType.IsDeleted;

                Sql_Command.Parameters.Add("@UserTypeId", SqlDbType.Int).Value = UserType.UserTypeId;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                int rowAffected = Sql_Command.ExecuteNonQuery();
                Sql_Connection.Close();

                int resultOutPut = int.Parse(result.Value.ToString());

                return resultOutPut;
            }
            catch (SqlException exception)
            {
                for (int i = 0; i < exception.Errors.Count; i++)
                {
                    ErrorMessages.Append("Index #" + i + "\n" +
                        "Message: " + exception.Errors[i].Message + "\n" +
                        "Error Number: " + exception.Errors[i].Number + "\n" +
                        "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
                        "Source: " + exception.Errors[i].Source + "\n" +
                        "Procedure: " + exception.Errors[i].Procedure + "\n");
                }
                throw new Exception(ErrorMessages.ToString());
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                    Sql_Connection.Close();
            }
        }

        //Delete UserType
        public int UserTypeDelete(UserType UserType)
        {
            try
            {
                Sql_Query = "[user].[spUserTypeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Delete;

                Sql_Command.Parameters.Add("@UserTypeId", SqlDbType.Int).Value = UserType.UserTypeId;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                int rowAffected = Sql_Command.ExecuteNonQuery();
                Sql_Connection.Close();

                int resultOutPut = int.Parse(result.Value.ToString());

                return resultOutPut;
            }
            catch (SqlException exception)
            {
                for (int i = 0; i < exception.Errors.Count; i++)
                {
                    ErrorMessages.Append("Index #" + i + "\n" +
                        "Message: " + exception.Errors[i].Message + "\n" +
                        "Error Number: " + exception.Errors[i].Number + "\n" +
                        "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
                        "Source: " + exception.Errors[i].Source + "\n" +
                        "Procedure: " + exception.Errors[i].Procedure + "\n");
                }
                throw new Exception(ErrorMessages.ToString());
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                    Sql_Connection.Close();
            }
        }
    }
}