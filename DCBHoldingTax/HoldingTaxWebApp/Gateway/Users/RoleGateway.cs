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
    public class RoleGateway : DefaultGateway
    {
        public List<Role> GetAllRole()
        {
            try
            {
                Sql_Query = "[user].[spRoleMaster]";
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

                List<Role> roleListVM = new List<Role>();

                while (Data_Reader.Read())
                {
                    Role roleVM = new Role
                    {
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        RoleDetails = Data_Reader["RoleDetails"].ToString(),
                        RoleId = Convert.ToInt32(Data_Reader["RoleId"]),
                        RoleName = Data_Reader["RoleName"].ToString(),
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUserName = Data_Reader["CreatedByUserName"].ToString()
                    };

                    roleListVM.Add(roleVM);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return roleListVM;
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

        //public List<RoleVM> GetAllRoleNonAdmin()
        //{
        //    try
        //    {
        //        Sql_Query = "[dbo].[spRoleMaster]";
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

        //        List<RoleVM> roleListVM = new List<RoleVM>();

        //        while (Data_Reader.Read())
        //        {
        //            RoleVM roleVM = new RoleVM
        //            {
        //                LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
        //                                        Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
        //                IsActive = Data_Reader["IsActive"] !=
        //                                        DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
        //                IsDeleted = Data_Reader["IsDeleted"] !=
        //                                        DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
        //                RoleDetails = Data_Reader["RoleDetails"].ToString(),
        //                RoleId = Convert.ToInt32(Data_Reader["RoleId"]),
        //                RoleName = Data_Reader["RoleName"].ToString(),
        //                LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
        //                                        DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
        //                UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString()
        //            };

        //            roleListVM.Add(roleVM);
        //        }

        //        Data_Reader.Close();
        //        Sql_Connection.Close();

        //        return roleListVM;
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

        //Get Role By Id 

        public Role GetRoleById(int id)
        {
            try
            {
                Sql_Query = "[user].[spRoleMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@RoleId", SqlDbType.NVarChar).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                Role roleVM = new Role();

                while (Data_Reader.Read())
                {
                    roleVM.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    roleVM.IsActive = Data_Reader["IsActive"] !=
                                           DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    roleVM.IsDeleted = Data_Reader["IsDeleted"] !=
                                      DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    roleVM.RoleDetails = Data_Reader["RoleDetails"].ToString();
                    roleVM.RoleId = Convert.ToInt32(Data_Reader["RoleId"]);
                    roleVM.RoleName = Data_Reader["RoleName"].ToString();
                    roleVM.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                         DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    roleVM.UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString();
                    roleVM.CreatedBy = Data_Reader["CreatedBy"] !=
                                               DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    roleVM.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                            Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    roleVM.CreatedByUserName = Data_Reader["CreatedByUserName"].ToString();


                };

                Data_Reader.Close();
                Sql_Connection.Close();

                return roleVM;
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

        //Create Role 
        public int RoleInsert(Role role)
        {
            try
            {
                Sql_Query = "[user].[spRoleMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;
                Sql_Command.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = role.RoleName;
                Sql_Command.Parameters.Add("@RoleDetails", SqlDbType.NVarChar).Value = role.RoleDetails;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = role.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = role.CreateDate;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = role.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = role.LastUpdated;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = role.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = role.IsDeleted;

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

        //Create Role Update
        public int RoleUpdate(Role role)
        {
            try
            {
                Sql_Query = "[user].[spRoleMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = role.RoleName;
                Sql_Command.Parameters.Add("@RoleDetails", SqlDbType.NVarChar).Value = role.RoleDetails;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = role.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = role.LastUpdated;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = role.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = role.IsDeleted;

                Sql_Command.Parameters.Add("@RoleId", SqlDbType.Int).Value = role.RoleId;

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

        //Delete Role
        public int RoleDelete(Role role)
        {
            try
            {
                Sql_Query = "[user].[spRoleMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Delete;

                Sql_Command.Parameters.Add("@RoleId", SqlDbType.Int).Value = role.RoleId;

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