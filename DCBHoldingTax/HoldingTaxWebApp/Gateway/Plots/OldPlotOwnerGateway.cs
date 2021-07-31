using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Plots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Plots
{
    public class OldPlotOwnerGateway: DefaultGateway
    {
        #region Old Plot Owner 
        //Get All Plot Owner List
        public List<OldPlotOwner> GetAllOldPlotOwner()
        {
            try
            {
                Sql_Query = "[Plot].[spOldPlotOwnerMaster]";
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

                List<OldPlotOwner> vm = new List<OldPlotOwner>();

                while (Data_Reader.Read())
                {
                    OldPlotOwner model = new OldPlotOwner
                    {


                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUserName = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (Int32?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (Int32?)null,


                        OldPlotOwnerId = Convert.ToInt32(Data_Reader["OldPlotOwnerId"]),
                        PlotOwnerId = Convert.ToInt32(Data_Reader["PlotOwnerId"]),
                        //PlotId = Convert.ToInt32(Data_Reader["PlotId"]),
                        //PlotIdNumber = Data_Reader["PlotIdNumber"].ToString(),
                        OldPlotOwnerName = Data_Reader["PlotOwnerName"].ToString(),
                        IsAlive = Data_Reader["IsAlive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsAlive"]) : (bool?)null,
                        OfficialStatusId = Convert.ToInt32(Data_Reader["OfficialStatusId"]),
                        OffStatusName = Data_Reader["OffStatusName"].ToString(),
                        PresentAdd = Data_Reader["PresentAdd"].ToString(),
                        PermanentAdd = Data_Reader["PermanentAdd"].ToString(),
                        PhoneNumber = Data_Reader["PhoneNumber"].ToString(),
                        Email = Data_Reader["Email"].ToString(),
                       
                        //TotalArea = Data_Reader["TotalArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["TotalArea"]) : (Decimal?)null,

                    };

                    vm.Add(model);
                }

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

        //Get PlotOwner By Id
        public OldPlotOwner GetOldPlotOwnerById(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spOldPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "detailsPlotOwner";
                Sql_Command.Parameters.Add("@OldPlotOwnerId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                OldPlotOwner vm = new OldPlotOwner();

                while (Data_Reader.Read())
                {
                    vm.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    vm.CreatedByUserName = Data_Reader["CreatedByUsername"].ToString();
                    vm.UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString();
                    vm.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    vm.IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    vm.IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    vm.CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (Int32?)null;
                    vm.LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (Int32?)null;

                    vm.OldPlotOwnerId = Convert.ToInt32(Data_Reader["OldPlotOwnerId"]);
                    vm.PlotOwnerId = Convert.ToInt32(Data_Reader["PlotOwnerId"]);
                    //vm.PlotId = Convert.ToInt32(Data_Reader["PlotId"]);
                    //vm.PlotIdNumber = Data_Reader["PlotIdNumber"].ToString();
                    vm.OldPlotOwnerName = Data_Reader["OldPlotOwnerName"].ToString();
                    vm.IsAlive = Data_Reader["IsAlive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsAlive"]) : (bool?)null;
                    vm.OfficialStatusId = Convert.ToInt32(Data_Reader["OfficialStatusId"]);
                    vm.OffStatusName = Data_Reader["OffStatusName"].ToString();
                    vm.PresentAdd = Data_Reader["PresentAdd"].ToString();
                    vm.PermanentAdd = Data_Reader["PermanentAdd"].ToString();
                    vm.PhoneNumber = Data_Reader["PhoneNumber"].ToString();
                    vm.Email = Data_Reader["Email"].ToString();
                  
                    //vm.TotalArea = Data_Reader["TotalArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["TotalArea"]) : (Decimal?)null;
                };

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

       
        //Create Plot Owner Details
        public int OldPlotOwnerInsert(OldPlotOwner model)
        {
            try
            {
                Sql_Query = "[Plot].[spOldPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;


                Sql_Command.Parameters.Add("@PlotOwnerId", SqlDbType.Int).Value = model.PlotOwnerId;
                //Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = model.PlotId;
                Sql_Command.Parameters.Add("@OldPlotOwnerName ", SqlDbType.NVarChar).Value = model.OldPlotOwnerName;
                Sql_Command.Parameters.Add("@IsAlive ", SqlDbType.Bit).Value = model.IsAlive;
                Sql_Command.Parameters.Add("@OfficialStatusId", SqlDbType.Int).Value = model.OfficialStatusId;
                Sql_Command.Parameters.Add("@PresentAdd", SqlDbType.NVarChar).Value = model.PresentAdd;
                Sql_Command.Parameters.Add("@PermanentAdd", SqlDbType.NVarChar).Value = model.PermanentAdd;
                Sql_Command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = model.PhoneNumber;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
               


                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = model.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = model.LastUpdatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = model.IsDeleted;


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

        //Update Plot Owner Details
        public int OldPlotOwnerUpdate(OldPlotOwner model)
        {
            try
            {
                Sql_Query = "[Plot].[spOldPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@OldPlotOwnerId", SqlDbType.Int).Value = model.OldPlotOwnerId;
                Sql_Command.Parameters.Add("@PlotOwnerId", SqlDbType.Int).Value = model.PlotOwnerId;
                //Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = model.PlotId;
                Sql_Command.Parameters.Add("@OldPlotOwnerName ", SqlDbType.NVarChar).Value = model.OldPlotOwnerName;
                Sql_Command.Parameters.Add("@IsAlive ", SqlDbType.Bit).Value = model.IsAlive;
                Sql_Command.Parameters.Add("@OfficialStatusId", SqlDbType.Int).Value = model.OfficialStatusId;
                Sql_Command.Parameters.Add("@PresentAdd", SqlDbType.NVarChar).Value = model.PresentAdd;
                Sql_Command.Parameters.Add("@PermanentAdd", SqlDbType.NVarChar).Value = model.PermanentAdd;
                Sql_Command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = model.PhoneNumber;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;



                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = model.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = model.LastUpdatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = model.IsDeleted;


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


        #region Details Portion Old Other Plot Owner

        //Get All old Plot Owner List
        public List<OldOthetPlotOwner> GetOldOthetPlotOwnerById(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spOldOthetPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@OldPlotOwnerId", SqlDbType.Int).Value = id;
                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<OldOthetPlotOwner> vm = new List<OldOthetPlotOwner>();

                while (Data_Reader.Read())
                {
                    OldOthetPlotOwner model = new OldOthetPlotOwner
                    {




                        OldPlotOwnerId = Convert.ToInt32(Data_Reader["OldPlotOwnerId"]),
                        OldOthetPlotOwnerId = Convert.ToInt32(Data_Reader["OldOthetPlotOwnerId"]),
                        OldOthetOwneeName = Data_Reader["OldOthetOwneeName"].ToString(),

                        Address = Data_Reader["Address"].ToString(),
                        Remarks = Data_Reader["Remarks"].ToString(),

                    };

                    vm.Add(model);
                }

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


        //Create old Plot Owner Details
        public int OldOthetPlotOwnerInsert(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spOthetPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;


                Sql_Command.Parameters.Add("@PlotOwnerId", SqlDbType.Int).Value = id;
              

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



        //Delete Plot Owner Details
        public int OldOthetPlotOwnerDelete(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spOldOthetPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Delete;
                Sql_Command.Parameters.Add("@OldPlotOwnerId", SqlDbType.Int).Value = id;


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