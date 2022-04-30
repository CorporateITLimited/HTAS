using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Users
{
    public class HolderUserGateway : DefaultGateway
    {

        public List<HolderUser> GetAllHolderUserList()
        {
            try
            {
                Sql_Query = "[user].[spHolderUserMaster]";
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
                List<HolderUser> holderUserVM = new List<HolderUser>();

                while (Data_Reader.Read())
                {
                    HolderUser holderUser = new HolderUser
                    {
                        LogInCredentialId = Convert.ToInt32(Data_Reader["LogInCredentialId"]),
                        UserName = Data_Reader["UserName"].ToString(),
                        HashPassword = Data_Reader["HashPassword"].ToString(),
                        UserTypeId = Convert.ToInt32(Data_Reader["UserTypeId"]),
                        LogIsActive = Data_Reader["LogIsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsActive"]) : (bool?)null,
                        LogIsDeleted = Data_Reader["LogIsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        CreatedByUserName = Data_Reader["CreatedByUserName"].ToString(),
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        Email = Data_Reader["Email"].ToString(),
                        IsEmailConfirmed = Data_Reader["IsEmailConfirmed"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsEmailConfirmed"]) : (bool?)null,
                        IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        MobileNumber = Data_Reader["MobileNumber"].ToString(),
                        IsMobileNumberConfirmed = Data_Reader["IsMobileNumberConfirmed"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsMobileNumberConfirmed"]) : (bool?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        HolderName = Data_Reader["HolderName"].ToString(),
                        HolderUserId = Convert.ToInt32(Data_Reader["HolderUserId"]),
                        RankId = Data_Reader["RankId"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["RankId"]) : (int?)null,
                        RankName = Data_Reader["RankName"].ToString(),
                        HolderNamecon = Data_Reader["HolderNamecon"].ToString(),
                    };



                    holderUserVM.Add(holderUser);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return holderUserVM;
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

        public HolderUser GetHolderUserById(int id)
        {
            try
            {
                Sql_Query = "[user].[spHolderUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@HolderUserId", SqlDbType.Int).Value = id;
                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                HolderUser vm = new HolderUser();

                while (Data_Reader.Read())
                {
                    vm.LogInCredentialId = Convert.ToInt32(Data_Reader["LogInCredentialId"]);
                    vm.UserName = Data_Reader["UserName"].ToString();
                    vm.HashPassword = Data_Reader["HashPassword"].ToString();
                    vm.UserTypeId = Convert.ToInt32(Data_Reader["UserTypeId"]);
                    vm.LogIsActive = Data_Reader["LogIsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsActive"]) : (bool?)null;
                    vm.LogIsDeleted = Data_Reader["LogIsDeleted"] !=
                                              DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsDeleted"]) : (bool?)null;
                    vm.CreatedBy = Data_Reader["CreatedBy"] !=
                                            DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    vm.CreatedByUserName = Data_Reader["CreatedByUserName"].ToString();
                    vm.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                            Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    vm.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                            Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    vm.Email = Data_Reader["Email"].ToString();
                        vm.IsEmailConfirmed = Data_Reader["IsEmailConfirmed"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsEmailConfirmed"]) : (bool?)null;
                    vm.IsActive = Data_Reader["IsActive"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    vm.IsDeleted = Data_Reader["IsDeleted"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    vm.MobileNumber = Data_Reader["MobileNumber"].ToString();
                    vm.IsMobileNumberConfirmed = Data_Reader["IsMobileNumberConfirmed"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["IsMobileNumberConfirmed"]) : (bool?)null;
                    vm.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                            DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    vm.UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString();
                    vm.HolderId = Convert.ToInt32(Data_Reader["HolderId"]);
                    vm.HolderName = Data_Reader["HolderName"].ToString();
                    vm.HolderUserId = Convert.ToInt32(Data_Reader["HolderUserId"]);

                    vm.RankId = Data_Reader["RankId"] !=
                                               DBNull.Value ? Convert.ToInt32(Data_Reader["RankId"]) : (int?)null;
                    vm.RankName = Data_Reader["RankName"].ToString();
                    vm.HolderNamecon = Data_Reader["HolderNamecon"].ToString();


                }
                vm.StringCreateDate = $"{vm.CreateDate:dd/MM/yyyy HH:mm:ss tt}" ?? "";
                vm.StringLastUpdated = $"{vm.LastUpdated:dd/MM/yyyy HH:mm:ss tt}" ?? "";

                Data_Reader.Close();
                Sql_Connection.Close();

                return vm;
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

        public int HolderUserInsert(HolderUser user)
        {
            try
            {
                Sql_Query = "[user].[spHolderUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;

                Sql_Command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                Sql_Command.Parameters.Add("@HashPassword", SqlDbType.NVarChar).Value = user.HashPassword;
                Sql_Command.Parameters.Add("@UserTypeId", SqlDbType.Int).Value = user.UserTypeId;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = user.IsDeleted;
                Sql_Command.Parameters.Add("@HolderUserId", SqlDbType.Int).Value = user.HolderUserId;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = user.HolderId;

                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = user.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = user.LastUpdated;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                Sql_Command.Parameters.Add("@IsEmailConfirmed", SqlDbType.Bit).Value = user.IsEmailConfirmed;
                Sql_Command.Parameters.Add("@MobileNumber", SqlDbType.NVarChar).Value = user.MobileNumber;
                Sql_Command.Parameters.Add("@IsMobileNumberConfirmed", SqlDbType.Bit).Value = user.IsMobileNumberConfirmed;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = user.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = user.CreateDate;

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

        public int HolderUserUpdate(HolderUser user)
        {
            try
            {
                Sql_Query = "[user].[spHolderUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                Sql_Command.Parameters.Add("@HashPassword", SqlDbType.NVarChar).Value = user.HashPassword;
                Sql_Command.Parameters.Add("@UserTypeId", SqlDbType.Int).Value = user.UserTypeId;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = user.IsDeleted;
                Sql_Command.Parameters.Add("@HolderUserId", SqlDbType.Int).Value = user.HolderUserId;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = user.HolderId;
                Sql_Command.Parameters.Add("@LogInCredentialId", SqlDbType.Int).Value = user.LogInCredentialId;

                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = user.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = user.LastUpdated;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                Sql_Command.Parameters.Add("@IsEmailConfirmed", SqlDbType.Bit).Value = user.IsEmailConfirmed;
                Sql_Command.Parameters.Add("@MobileNumber", SqlDbType.NVarChar).Value = user.MobileNumber;
                Sql_Command.Parameters.Add("@IsMobileNumberConfirmed", SqlDbType.Bit).Value = user.IsMobileNumberConfirmed;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = user.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = user.CreateDate;

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


        public List<HolderUser> GetAllHolderListForInsert()
        {
            try
            {
                Sql_Query = "[user].[spHolderUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "dropdown_for_create";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();
                List<HolderUser> holderUserVM = new List<HolderUser>();

                while (Data_Reader.Read())
                {
                    HolderUser holderUser = new HolderUser
                    {
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        HolderName = Data_Reader["HolderName"].ToString(),
                        HolderNamecon = Data_Reader["HolderNamecon"].ToString(),
                    };
                    holderUserVM.Add(holderUser);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return holderUserVM;
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

        public List<HolderUser> GetAllHolderListForUpdate()
        {
            try
            {
                Sql_Query = "[user].[spHolderUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "dropdown_for_update";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();
                List<HolderUser> holderUserVM = new List<HolderUser>();

                while (Data_Reader.Read())
                {
                    HolderUser holderUser = new HolderUser
                    {
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        HolderName = Data_Reader["HolderName"].ToString(),
                        HolderNamecon = Data_Reader["HolderNamecon"].ToString(),
                    };
                    holderUserVM.Add(holderUser);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return holderUserVM;
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