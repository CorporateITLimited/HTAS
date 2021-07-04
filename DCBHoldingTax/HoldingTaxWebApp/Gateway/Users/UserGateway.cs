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
    public class UserGateway : DefaultGateway
    {

        public List<clsUser> GetAllUserList()
        {
            try
            {
                Sql_Query = "[user].[spUserMaster]";
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
                List<clsUser> userListVM = new List<clsUser>();

                while (Data_Reader.Read())
                {
                    clsUser userVM = new clsUser
                    {
                        // Portion LogInCredential
                        LogInCredentialId = Convert.ToInt32(Data_Reader["LogInCredentialId"]),
                        UserName = Data_Reader["UserName"].ToString(),
                        HashPassword = Data_Reader["HashPassword"].ToString(),
                        UserTypeId = Convert.ToInt32(Data_Reader["UserTypeId"]),
                        LogIsActive = Data_Reader["LogIsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsActive"]) : (bool?)null,
                        LogIsDeleted = Data_Reader["LogIsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsDeleted"]) : (bool?)null,

                        // Portion User
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
                        EmpolyeeId = Data_Reader["EmpolyeeId"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["EmpolyeeId"]) : (int?)null,
                        IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        MobileNumber = Data_Reader["MobileNumber"].ToString(),
                        IsMobileNumberConfirmed = Data_Reader["IsMobileNumberConfirmed"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsMobileNumberConfirmed"]) : (bool?)null,
                        RoleId = Convert.ToInt32(Data_Reader["RoleId"]),
                        RoleName = Data_Reader["RoleName"].ToString(),
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        UserDetails = Data_Reader["UserDetails"].ToString(),
                        UserFullName = Data_Reader["UserFullName"].ToString(),
                        UserId = Convert.ToInt32(Data_Reader["UserId"]),
                        EmployeeName = Data_Reader["EmployeeName"].ToString()
                    };



                    userListVM.Add(userVM);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return userListVM;
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

        //public List<clsUser> GetAllUserListNonAdmin()
        //{
        //    try
        //    {
        //        Sql_Query = "[user].[spUserMaster]";
        //        Sql_Command = new SqlCommand
        //        {
        //            CommandText = Sql_Query,
        //            Connection = Sql_Connection,
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        Sql_Command.Parameters.Clear();

        //        Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "non_admin";

        //        SqlParameter result = new SqlParameter
        //        {
        //            ParameterName = "@result",
        //            SqlDbType = SqlDbType.Int,
        //            Direction = ParameterDirection.Output
        //        };
        //        Sql_Command.Parameters.Add(result);

        //        Sql_Connection.Open();
        //        Data_Reader = Sql_Command.ExecuteReader();
        //        List<UserVM> userListVM = new List<UserVM>();

        //        while (Data_Reader.Read())
        //        {
        //            UserVM userVM = new UserVM
        //            {
        //                // Portion LogInCredential
        //                LogInCredentialId = Convert.ToInt32(Data_Reader["LogInCredentialId"]),
        //                UserName = Data_Reader["UserName"].ToString(),
        //                HashPassword = Data_Reader["HashPassword"].ToString(),
        //                UserTypeId = Convert.ToInt32(Data_Reader["UserTypeId"]),
        //                LogIsActive = Data_Reader["LogIsActive"] !=
        //                                        DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsActive"]) : (bool?)null,
        //                LogIsDeleted = Data_Reader["LogIsDeleted"] !=
        //                                        DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsDeleted"]) : (bool?)null,

        //                // Portion User
        //                CreatedBy = Data_Reader["CreatedBy"] !=
        //                                        DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
        //                CreatedByUserName = Data_Reader["CreatedByUserName"].ToString(),
        //                CreatedDate = Data_Reader["CreatedDate"] != DBNull.Value ?
        //                                        Convert.ToDateTime(Data_Reader["CreatedDate"]) : (DateTime?)null,
        //                LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
        //                                        Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
        //                Email = Data_Reader["Email"].ToString(),
        //                IsEmailConfirmed = Data_Reader["IsEmailConfirmed"] !=
        //                                        DBNull.Value ? Convert.ToBoolean(Data_Reader["IsEmailConfirmed"]) : (bool?)null,
        //                EmployeeId = Data_Reader["EmployeeId"].ToString(),
        //                IsActive = Data_Reader["IsActive"] !=
        //                                        DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
        //                IsDeleted = Data_Reader["IsDeleted"] !=
        //                                        DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
        //                MobileNumber = Data_Reader["MobileNumber"].ToString(),
        //                IsMobileNumberConfirmed = Data_Reader["IsMobileNumberConfirmed"] !=
        //                                        DBNull.Value ? Convert.ToBoolean(Data_Reader["IsMobileNumberConfirmed"]) : (bool?)null,
        //                RoleId = Convert.ToInt32(Data_Reader["RoleId"]),
        //                RoleName = Data_Reader["RoleName"].ToString(),
        //                LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
        //                                        DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
        //                UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
        //                UserDetails = Data_Reader["UserDetails"].ToString(),
        //                UserFullName = Data_Reader["UserFullName"].ToString(),
        //                UserId = Convert.ToInt32(Data_Reader["UserId"]),
        //                EmployeeName = Data_Reader["EmployeeName"].ToString()
        //            };



        //            userListVM.Add(userVM);
        //        }

        //        Data_Reader.Close();
        //        Sql_Connection.Close();

        //        return userListVM;
        //    }
        //    catch (SqlException exception)
        //    {
        //        for (int i = 0; i < exception.Errors.Count; i++)
        //        {
        //            ErrorMessages.Append("Index #" + i + "\n" +
        //                "Message: " + exception.Errors[i].Message + "\n" +
        //                "Error Number: " + exception.Errors[i].Number + "\n" +
        //                "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
        //                "Source: " + exception.Errors[i].Source + "\n" +
        //                "Procedure: " + exception.Errors[i].Procedure + "\n");
        //        }
        //        throw new Exception(ErrorMessages.ToString());
        //    }
        //    finally
        //    {
        //        if (Sql_Connection.State == ConnectionState.Open)
        //            Sql_Connection.Close();

        //    }

        //}

        public clsUser GetUserById(int id)
        {
            try
            {
                Sql_Query = "[user].[spUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@UserId", SqlDbType.Int).Value = id;
                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                clsUser userVM = new clsUser();

                while (Data_Reader.Read())
                {
                    // Portion LogInCredential
                    userVM.LogInCredentialId = Convert.ToInt32(Data_Reader["LogInCredentialId"]);
                    userVM.UserName = Data_Reader["UserName"].ToString();
                    userVM.HashPassword = Data_Reader["HashPassword"].ToString();
                    userVM.UserTypeId = Convert.ToInt32(Data_Reader["UserTypeId"]);
                    userVM.LogIsActive = Data_Reader["LogIsActive"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsActive"]) : (bool?)null;
                    userVM.LogIsDeleted = Data_Reader["LogIsDeleted"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["LogIsDeleted"]) : (bool?)null;

                    // Portion User
                    userVM.CreatedBy = Data_Reader["CreatedBy"] !=
                                            DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    userVM.CreatedByUserName = Data_Reader["CreatedByUserName"].ToString();
                    userVM.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                            Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    userVM.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                            Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    userVM.Email = Data_Reader["Email"].ToString();
                    userVM.IsEmailConfirmed = Data_Reader["IsEmailConfirmed"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["IsEmailConfirmed"]) : (bool?)null;
                    userVM.EmpolyeeId = Data_Reader["EmpolyeeId"] !=
                                            DBNull.Value ? Convert.ToInt32(Data_Reader["EmpolyeeId"]) : (int?)null;
                    userVM.IsActive = Data_Reader["IsActive"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    userVM.IsDeleted = Data_Reader["IsDeleted"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    userVM.MobileNumber = Data_Reader["MobileNumber"].ToString();
                    userVM.IsMobileNumberConfirmed = Data_Reader["IsMobileNumberConfirmed"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["IsMobileNumberConfirmed"]) : (bool?)null;
                    userVM.RoleId = Convert.ToInt32(Data_Reader["RoleId"]);
                    userVM.RoleName = Data_Reader["RoleName"].ToString();
                    userVM.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                            DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    userVM.UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString();
                    userVM.UserDetails = Data_Reader["UserDetails"].ToString();
                    userVM.UserFullName = Data_Reader["UserFullName"].ToString();
                    userVM.UserId = Convert.ToInt32(Data_Reader["UserId"]);
                    userVM.EmployeeName = Data_Reader["EmployeeName"].ToString();
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return userVM;
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

        public int UserInsert(clsUser user)
        {
            try
            {
                Sql_Query = "[user].[spUserMaster]";
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

                Sql_Command.Parameters.Add("@UserDetails", SqlDbType.NVarChar).Value = user.UserDetails;
                Sql_Command.Parameters.Add("@EmpolyeeId", SqlDbType.Int).Value = user.EmpolyeeId;
                Sql_Command.Parameters.Add("@RoleId", SqlDbType.Int).Value = user.RoleId;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = user.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = user.LastUpdated;
                Sql_Command.Parameters.Add("@UserFullName", SqlDbType.NVarChar).Value = user.UserFullName;
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

        public int UserUpdate(clsUser user)
        {
            try
            {
                Sql_Query = "[user].[spUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@LogInCredentialId", SqlDbType.Int).Value = user.LogInCredentialId;
                Sql_Command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                Sql_Command.Parameters.Add("@HashPassword", SqlDbType.NVarChar).Value = user.HashPassword;
                Sql_Command.Parameters.Add("@UserTypeId", SqlDbType.Int).Value = user.UserTypeId;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = user.IsDeleted;

                Sql_Command.Parameters.Add("@UserId", SqlDbType.Int).Value = user.UserId;
                Sql_Command.Parameters.Add("@UserDetails", SqlDbType.NVarChar).Value = user.UserDetails;
                Sql_Command.Parameters.Add("@EmpolyeeId", SqlDbType.Int).Value = user.EmpolyeeId;
                Sql_Command.Parameters.Add("@RoleId", SqlDbType.Int).Value = user.RoleId;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = user.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = user.LastUpdated;
                Sql_Command.Parameters.Add("@UserFullName", SqlDbType.NVarChar).Value = user.UserFullName;
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

        public int UserDelete(clsUser user)
        {
            try
            {
                Sql_Query = "[user].[spUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Delete;

                Sql_Command.Parameters.Add("@LogInCredentialId", SqlDbType.Int).Value = user.LogInCredentialId;

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

        #region user-permission

        public List<clsUser> GetAllUserListForPermissionInsert()
        {
            try
            {
                Sql_Query = "[user].[spUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "permission_insert";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();
                List<clsUser> userListVM = new List<clsUser>();

                while (Data_Reader.Read())
                {
                    clsUser userVM = new clsUser
                    {
                        UserFullName = Data_Reader["UserFullName"].ToString(),
                        UserId = Convert.ToInt32(Data_Reader["UserId"])
                    };

                    userListVM.Add(userVM);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return userListVM;
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

        public List<clsUser> GetAllUserListForPermissionUpdate()
        {
            try
            {
                Sql_Query = "[user].[spUserMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "permission_update";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();
                List<clsUser> userListVM = new List<clsUser>();

                while (Data_Reader.Read())
                {
                    clsUser userVM = new clsUser
                    {
                        UserName = Data_Reader["UserName"].ToString(),
                        UserFullName = Data_Reader["UserFullName"].ToString(),
                        UserId = Convert.ToInt32(Data_Reader["UserId"])
                    };
                    userListVM.Add(userVM);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return userListVM;
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

        public List<UserPermission> GetControllerList()
        {
            try
            {
                Sql_Query = "[user].[spControllerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@ControllerId", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@ControllerName", SqlDbType.VarChar).Value = null;
                Sql_Command.Parameters.Add("@Description", SqlDbType.VarChar).Value = null;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = null;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = null;
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.VarChar).Value = CommonConstantHelper.Select;


                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<UserPermission> userPermissionList = new List<UserPermission>();

                while (Data_Reader.Read())
                {
                    var uP = new UserPermission()
                    {
                        ControllerId = Convert.ToInt32(Data_Reader["ControllerId"]),
                        ControllerName = Convert.ToString(Data_Reader["ControllerName"]),
                        Description = Convert.ToString(Data_Reader["Description"]),
                        IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null
                    };
                    userPermissionList.Add(uP);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return userPermissionList;
            }
            catch
            {
                throw new Exception("No Access");
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                {
                    Sql_Connection.Close();
                }
            }
        }

        public int UserPermissionInsert(UserPermission uP)
        {
            try
            {
                Sql_Query = "[user].[spUserPermissionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;
                Sql_Command.Parameters.Add("@PermissionId", SqlDbType.Int).Value = uP.PermissionId;
                Sql_Command.Parameters.Add("@UserId", SqlDbType.Int).Value = uP.UserId;
                Sql_Command.Parameters.Add("@ControllerId", SqlDbType.Int).Value = uP.ControllerId;
                Sql_Command.Parameters.Add("@ReadWriteStatus", SqlDbType.Bit).Value = uP.ReadWriteStatus;
                Sql_Command.Parameters.Add("@CanAccess", SqlDbType.Bit).Value = uP.CanAccess;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = uP.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = uP.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = uP.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = uP.LastUpdatedBy;

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

        public int UserPermissionUpdate(UserPermission uP)
        {
            try
            {
                Sql_Query = "[user].[spUserPermissionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@PermissionId", SqlDbType.Int).Value = uP.PermissionId;
                Sql_Command.Parameters.Add("@UserId", SqlDbType.Int).Value = uP.UserId;
                Sql_Command.Parameters.Add("@ControllerId", SqlDbType.Int).Value = uP.ControllerId;
                Sql_Command.Parameters.Add("@ReadWriteStatus", SqlDbType.Bit).Value = uP.ReadWriteStatus;
                Sql_Command.Parameters.Add("@CanAccess", SqlDbType.Bit).Value = uP.CanAccess;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = uP.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = uP.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = uP.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = uP.LastUpdatedBy;

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

        public List<UserPermission> GetUserPermissionListByUserId(int UserId)
        {
            try
            {
                Sql_Query = "[user].[spUserPermissionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "details_user";
                Sql_Command.Parameters.Add("@PermissionId", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                Sql_Command.Parameters.Add("@ControllerId", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@ReadWriteStatus", SqlDbType.Bit).Value = null;
                Sql_Command.Parameters.Add("@CanAccess", SqlDbType.Bit).Value = null;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = null;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = null;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = null;


                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<UserPermission> userPermissionList = new List<UserPermission>();

                while (Data_Reader.Read())
                {
                    var uP = new UserPermission()
                    {
                        PermissionId = Convert.ToInt32(Data_Reader["PermissionId"]),
                        UserId = Convert.ToInt32(Data_Reader["UserId"]),
                        UserFullName = Convert.ToString(Data_Reader["UserFullName"]),
                        ControllerId = Convert.ToInt32(Data_Reader["ControllerId"]),
                        ControllerName = Convert.ToString(Data_Reader["ControllerName"]),
                        ReadWriteStatus = Data_Reader["ReadWriteStatus"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["ReadWriteStatus"]) : (bool?)null,
                        CanAccess = Data_Reader["CanAccess"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["CanAccess"]) : (bool?)null,
                        CreateDate = Data_Reader["CreateDate"] !=
                                                DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdated = Data_Reader["LastUpdated"] !=
                                                DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        CreatedByUserName = Convert.ToString(Data_Reader["CreatedByUserName"]),
                        UpdatedByUserName = Convert.ToString(Data_Reader["UpdatedByUserName"]),
                        Description = Convert.ToString(Data_Reader["Description"])
                    };
                    userPermissionList.Add(uP);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return userPermissionList;
            }
            catch
            {
                throw new Exception("No Access");
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                {
                    Sql_Connection.Close();
                }
            }
        }

        public List<UserPermission> GetUserPermissionList()
        {
            try
            {
                Sql_Query = "[user].[spUserPermissionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Select;
                Sql_Command.Parameters.Add("@PermissionId", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@UserId", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@ControllerId", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@ReadWriteStatus", SqlDbType.Bit).Value = null;
                Sql_Command.Parameters.Add("@CanAccess", SqlDbType.Bit).Value = null;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = null;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = null;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = null;


                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<UserPermission> userPermissionList = new List<UserPermission>();

                while (Data_Reader.Read())
                {
                    var uP = new UserPermission()
                    {
                        //PermissionId = Convert.ToInt32(Data_Reader["PermissionId"]),
                        UserId = Convert.ToInt32(Data_Reader["UserId"]),
                        UserFullName = Convert.ToString(Data_Reader["UserFullName"])
                       // ControllerId = Convert.ToInt32(Data_Reader["ControllerId"]),
                        //ControllerName = Convert.ToString(Data_Reader["ControllerName"]),
                        //ReadWriteStatus = Data_Reader["ReadWriteStatus"] !=
                        //                        DBNull.Value ? Convert.ToBoolean(Data_Reader["ReadWriteStatus"]) : (bool?)null,
                        //CanAccess = Data_Reader["CanAccess"] !=
                        //                        DBNull.Value ? Convert.ToBoolean(Data_Reader["CanAccess"]) : (bool?)null,
                        //CreateDate = Data_Reader["CreateDate"] !=
                        //                        DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        //CreatedBy = Data_Reader["CreatedBy"] !=
                        //                        DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        //LastUpdated = Data_Reader["LastUpdated"] !=
                        //                        DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        //LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                        //                        DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        //CreatedByUserName = Convert.ToString(Data_Reader["CreatedByUserName"]),
                        //UpdatedByUserName = Convert.ToString(Data_Reader["UpdatedByUserName"])
                    };
                    userPermissionList.Add(uP);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return userPermissionList;
            }
            catch
            {
                throw new Exception("No Access");
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                {
                    Sql_Connection.Close();
                }
            }
        }

        #endregion
    }
}