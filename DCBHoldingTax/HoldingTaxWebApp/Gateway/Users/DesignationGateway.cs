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
    public class DesignationGateway : DefaultGateway
    {
        public List<Designation> GetAllDesignation()
        {
            try
            {
                Sql_Query = "[user].[spDesignationMaster]";
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

                List<Designation> DesignationListVM = new List<Designation>();

                while (Data_Reader.Read())
                {
                    Designation DesignationVM = new Designation
                    {
                       
                        IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                      
                        DesignationId = Convert.ToInt32(Data_Reader["DesignationId"]),
                        DesignationName = Data_Reader["DesignationName"].ToString(),
                        Description = Data_Reader["Description"].ToString()

                    };

                    DesignationListVM.Add(DesignationVM);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return DesignationListVM;
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

      

        public Designation GetDesignationById(int id)
        {
            try
            {
                Sql_Query = "[user].[spDesignationMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@DesignationId", SqlDbType.NVarChar).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                Designation DesignationVM = new Designation();

                while (Data_Reader.Read())
                {  
                    DesignationVM.IsActive = Data_Reader["IsActive"] !=
                                           DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    DesignationVM.IsDeleted = Data_Reader["IsDeleted"] !=
                                      DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    DesignationVM.Description = Data_Reader["Description"].ToString();
                    DesignationVM.DesignationId = Convert.ToInt32(Data_Reader["DesignationId"]);
                    DesignationVM.DesignationName = Data_Reader["DesignationName"].ToString();
                  
                };

                Data_Reader.Close();
                Sql_Connection.Close();

                return DesignationVM;
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

        //Create Designation 
        public int DesignationInsert(Designation Designation)
        {
            try
            {
                Sql_Query = "[user].[spDesignationMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;
                Sql_Command.Parameters.Add("@DesignationName", SqlDbType.NVarChar).Value = Designation.DesignationName;
                Sql_Command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Designation.Description;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Designation.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = Designation.IsDeleted;

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

        //Create Designation Update
        public int DesignationUpdate(Designation Designation)
        {
            try
            {
                Sql_Query = "[user].[spDesignationMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@DesignationName", SqlDbType.NVarChar).Value = Designation.DesignationName;
                Sql_Command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Designation.Description;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Designation.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = Designation.IsDeleted;

                Sql_Command.Parameters.Add("@DesignationId", SqlDbType.Int).Value = Designation.DesignationId;

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

        //Delete Designation
        //public int DesignationDelete(Designation Designation)
        //{
        //    try
        //    {
        //        Sql_Query = "[user].[spDesignationMaster]";
        //        Sql_Command = new SqlCommand
        //        {
        //            CommandText = Sql_Query,
        //            Connection = Sql_Connection,
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        Sql_Command.Parameters.Clear();

        //        Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Delete;

        //        Sql_Command.Parameters.Add("@DesignationId", SqlDbType.Int).Value = Designation.DesignationId;

        //        SqlParameter result = new SqlParameter
        //        {
        //            ParameterName = "@result",
        //            SqlDbType = SqlDbType.Int,
        //            Direction = ParameterDirection.Output
        //        };
        //        Sql_Command.Parameters.Add(result);

        //        Sql_Connection.Open();
        //        int rowAffected = Sql_Command.ExecuteNonQuery();
        //        Sql_Connection.Close();

        //        int resultOutPut = int.Parse(result.Value.ToString());

        //        return resultOutPut;
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

    }
}