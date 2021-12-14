using HoldingTaxWebApp.ViewModels;
using HoldingTaxWebApp.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HoldingTaxWebApp.Models.Users;

namespace HoldingTaxWebApp.Gateway
{
    public class AccountGateway : DefaultGateway
    {

        public UserLogInCredentialVM LogIn(LogInVM user)
        {
            try
            {
                Sql_Query = "[user].[spUserLogIn]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                Sql_Command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                Sql_Command.Parameters.Add("@LogInTime", SqlDbType.DateTime).Value = user.LogInTime;
                Sql_Command.Parameters.Add("@LogOutTime", SqlDbType.DateTime).Value = user.LogOutTime;
                Sql_Command.Parameters.Add("@UserLogDetails", SqlDbType.NVarChar).Value = user.UserLogDetails;


                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                UserLogInCredentialVM credentialVM = new UserLogInCredentialVM();

                while (Data_Reader.Read())
                {
                    credentialVM.LogInCredentialId = Convert.ToInt32(Data_Reader["LogInCredentialId"]);
                    credentialVM.UserName = Data_Reader["UserName"].ToString();
                    credentialVM.UserTypeId = Convert.ToInt32(Data_Reader["UserTypeId"]);
                    credentialVM.LogIsActive = Convert.ToBoolean(Data_Reader["LogIsActive"]);
                    credentialVM.LogIsDeleted = Convert.ToBoolean(Data_Reader["LogIsDeleted"]);
                    
                    // user portion
                    credentialVM.UserId = Data_Reader["UserId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["UserId"]) : (int?)null;
                    credentialVM.RoleId = Data_Reader["RoleId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["RoleId"]) : (int?)null;
                    credentialVM.RoleName = Data_Reader["RoleName"].ToString();
                    credentialVM.UserFullName = Data_Reader["UserFullName"].ToString();
                    
                    // holder portion
                    credentialVM.HolderName = Data_Reader["HolderName"].ToString();
                    credentialVM.AreaPlotFlatData = Data_Reader["AreaPlotFlatData"].ToString();
                    credentialVM.HolderId = Data_Reader["HolderId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["HolderId"]) : (int?)null;
                    credentialVM.AreaId = Data_Reader["AreaId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["AreaId"]) : (int?)null;
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                credentialVM.CommonEntity.Result = int.Parse(result.Value.ToString());

                return credentialVM;
            }
            catch
            {
                throw new Exception("Connecting to server is failed.");
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                {
                    Sql_Connection.Close();
                }
            }
        }

        public int LogOut(LogInVM user)
        {
            try
            {
                Sql_Query = "[user].[spUserLogOut]";

                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                Sql_Command.Parameters.Add("@LogInTime", SqlDbType.DateTime).Value = user.LogInTime;
                Sql_Command.Parameters.Add("@LogOutTime", SqlDbType.DateTime).Value = user.LogOutTime;


                //SqlParameter result = new SqlParameter
                //{
                //    ParameterName = "@result",
                //    SqlDbType = SqlDbType.Int,
                //    Direction = ParameterDirection.Output
                //};
                //Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                int rowAffect = Sql_Command.ExecuteNonQuery();
                Sql_Connection.Close();

                //int resultCode = int.Parse(result.Value.ToString());

                return rowAffect;
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
                {
                    Sql_Connection.Close();
                }
            }

        }


        public UserPermission GetUserPermissionByUserAndController(int UserId, int ControllerId)
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

                Sql_Command.Parameters.Add("@PermissionId", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                Sql_Command.Parameters.Add("@ControllerId", SqlDbType.Int).Value = ControllerId;
                Sql_Command.Parameters.Add("@ReadWriteStatus", SqlDbType.Bit).Value = null;
                Sql_Command.Parameters.Add("@CanAccess", SqlDbType.Bit).Value = null;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = null;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = null;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = null;
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.VarChar).Value = "details_config";


                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                UserPermission userPermission = new UserPermission();

                while (Data_Reader.Read())
                {
                    userPermission.CanAccess = Data_Reader["CanAccess"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["CanAccess"]) : (bool?)null;
                    //userPermission.ControllerId = Convert.ToInt32(Data_Reader["ControllerId"]);
                    //userPermission.ControllerName = Convert.ToString(Data_Reader["ControllerName"]);
                    userPermission.ReadWriteStatus = Data_Reader["ReadWriteStatus"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["ReadWriteStatus"]) : (bool?)null;
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return userPermission;
            }
            catch
            {
                throw new Exception("No Access.");
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                {
                    Sql_Connection.Close();
                }
            }
        }


        #region Forget Password Portion

        public ChangePassword findUserName(string UserName)
        {
            try
            {
                Sql_Query = "[user].[spForgetPassword]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "findUserName";
                Sql_Command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                Sql_Command.Parameters.Add("@HashPassword", SqlDbType.NVarChar).Value = null;
                Sql_Command.Parameters.Add("@LogInCredentialId", SqlDbType.Int).Value = null;
                

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                ChangePassword credentialVM = new ChangePassword();

                while (Data_Reader.Read())
                {
                    credentialVM.LogInCredentialId = Convert.ToInt32(Data_Reader["LogInCredentialId"]);
                    credentialVM.UserName = Data_Reader["UserName"].ToString();
                    credentialVM.MobileNumber = Data_Reader["MobileNumber"].ToString();
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                //credentialVM.CommonEntity.Result = int.Parse(result.Value.ToString());

                return credentialVM;
            }
            catch
            {
                throw new Exception("Connecting to server is failed.");
            }
            finally
            {
                if (Sql_Connection.State == ConnectionState.Open)
                {
                    Sql_Connection.Close();
                }
            }
        }


        public int passwordUpdate(ChangePassword cp)
        {
            try
            {
                Sql_Query = "[user].[spForgetPassword]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "updatePassword";

                Sql_Command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = cp.UserName;
                Sql_Command.Parameters.Add("@HashPassword", SqlDbType.NVarChar).Value = cp.HashPassword;
                Sql_Command.Parameters.Add("@LogInCredentialId", SqlDbType.Int).Value = cp.LogInCredentialId;


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


        #endregion

    }
}