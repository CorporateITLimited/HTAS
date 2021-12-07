using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Plots;
using HoldingTaxWebApp.ViewModels.Plots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Plots
{
    public class PlotOwnerGateway : DefaultGateway
    {


        #region PlotOwner & ConstructionProgress & UnauthPortion

        //Get All Plot Owner List
        public List<PlotOwner> GetAllPlotOwner()
        {
            try
            {
                Sql_Query = "[Plot].[spPlotOwnerMaster]";
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

                List<PlotOwner> vm = new List<PlotOwner>();

                while (Data_Reader.Read())
                {
                    PlotOwner model = new PlotOwner
                    {


                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUserName = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (Int32?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (Int32?)null,


                        PlotOwnerId = Convert.ToInt32(Data_Reader["PlotOwnerId"]),
                        PlotId = Convert.ToInt32(Data_Reader["PlotId"]),
                        PlotIdNumber = Data_Reader["PlotIdNumber"].ToString(),
                        PlotOwnerName = Data_Reader["PlotOwnerName"].ToString(),
                        IsAlive = Data_Reader["IsAlive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsAlive"]) : (bool?)null,
                        OfficialStatusId = Convert.ToInt32(Data_Reader["OfficialStatusId"]),
                        OffStatusName = Data_Reader["OffStatusName"].ToString(),
                        PresentAdd = Data_Reader["PresentAdd"].ToString(),
                        PermanentAdd = Data_Reader["PermanentAdd"].ToString(),
                        PhoneNumber = Data_Reader["PhoneNumber"].ToString(),
                        Email = Data_Reader["Email"].ToString(),
                        LeaveDate = Data_Reader["LeaveDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LeaveDate"]) : (DateTime?)null,
                        LeaseAuthority = Data_Reader["LeaseAuthority"].ToString(),
                        LeaseType = Data_Reader["LeaseType"].ToString(),
                        LeasePeriod = Data_Reader["LeasePeriod"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LeasePeriod"]) : (Int32?)null,
                        LeaseQuotaId = Convert.ToInt32(Data_Reader["LeaseQuotaId"]),
                        HandOverOffice = Data_Reader["HandOverOffice"].ToString(),
                        HandOverLetterNo = Data_Reader["HandOverLetterNo"].ToString(),
                        LandDevelopChange = Data_Reader["LandDevelopChange"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["LandDevelopChange"]) : (Decimal?)null,
                        ConsStatusId = Convert.ToInt32(Data_Reader["ConsStatusId"]),
                        Doc1 = Data_Reader["Doc1"].ToString(),
                        Doc2 = Data_Reader["Doc2"].ToString(),
                        Doc3 = Data_Reader["Doc3"].ToString(),
                        Doc4 = Data_Reader["Doc4"].ToString(),
                        Doc5 = Data_Reader["Doc5"].ToString(),
                        Doc6 = Data_Reader["Doc6"].ToString(),
                        ConsStatusName = Data_Reader["ConsStatusName"].ToString(),
                        LeaseQuotaName = Data_Reader["LeaseQuotaName"].ToString(),
                        TotalArea = Data_Reader["TotalArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["TotalArea"]) : (Decimal?)null,

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
        public PlotOwner GetPlotOwnerById(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "detailsPlotOwner";
                Sql_Command.Parameters.Add("@PlotOwnerId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                PlotOwner vm = new PlotOwner();

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

                    vm.PlotOwnerId = Convert.ToInt32(Data_Reader["PlotOwnerId"]);
                    vm.PlotId = Convert.ToInt32(Data_Reader["PlotId"]);
                    vm.PlotIdNumber = Data_Reader["PlotIdNumber"].ToString();
                    vm.PlotOwnerName = Data_Reader["PlotOwnerName"].ToString();
                    vm.IsAlive = Data_Reader["IsAlive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsAlive"]) : (bool?)null;
                    vm.OfficialStatusId = Convert.ToInt32(Data_Reader["OfficialStatusId"]);
                    vm.OffStatusName = Data_Reader["OffStatusName"].ToString();
                    vm.PresentAdd = Data_Reader["PresentAdd"].ToString();
                    vm.PermanentAdd = Data_Reader["PermanentAdd"].ToString();
                    vm.PhoneNumber = Data_Reader["PhoneNumber"].ToString();
                    vm.Email = Data_Reader["Email"].ToString();
                    vm.LeaveDate = Data_Reader["LeaveDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LeaveDate"]) : (DateTime?)null;
                    vm.LeaseAuthority = Data_Reader["LeaseAuthority"].ToString();
                    vm.LeaseType = Data_Reader["LeaseType"].ToString();
                    vm.LeasePeriod = Data_Reader["LeasePeriod"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LeasePeriod"]) : (Int32?)null;
                    vm.LeaseQuotaId = Convert.ToInt32(Data_Reader["LeaseQuotaId"]);
                    vm.HandOverOffice = Data_Reader["HandOverOffice"].ToString();
                    vm.HandOverLetterNo = Data_Reader["HandOverLetterNo"].ToString();
                    vm.LandDevelopChange = Data_Reader["LandDevelopChange"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["LandDevelopChange"]) : (Decimal?)null;
                    vm.ConsStatusId = Convert.ToInt32(Data_Reader["ConsStatusId"]);
                    vm.Doc1 = Data_Reader["Doc1"].ToString();
                    vm.Doc2 = Data_Reader["Doc2"].ToString();
                    vm.Doc3 = Data_Reader["Doc3"].ToString();
                    vm.Doc4 = Data_Reader["Doc4"].ToString();
                    vm.Doc5 = Data_Reader["Doc5"].ToString();
                    vm.Doc6 = Data_Reader["Doc6"].ToString();
                    vm.ConsStatusName = Data_Reader["ConsStatusName"].ToString();
                    vm.LeaseQuotaName = Data_Reader["LeaseQuotaName"].ToString();
                    vm.TotalArea = Data_Reader["TotalArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["TotalArea"]) : (Decimal?)null;
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

        //Get Construction Progress By Id
        public ConstructionProgress GetConstructionProgressById(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "detailsConstructionProgress";
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                ConstructionProgress vm = new ConstructionProgress();

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

                    vm.PlotId = Convert.ToInt32(Data_Reader["PlotId"]);
                    vm.PlotIdNumber = Data_Reader["PlotIdNumber"].ToString();

                    vm.ConsProgressId = Convert.ToInt32(Data_Reader["ConsProgressId"]);
                    vm.OwnerDeclaration = Data_Reader["OwnerDeclaration"].ToString();
                    vm.RealBuilder = Data_Reader["RealBuilder"].ToString();
                    vm.DevelopDeposit = Data_Reader["DevelopDeposit"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["DevelopDeposit"]) : (Decimal?)null;
                    vm.FloorNumber = Data_Reader["FloorNumber"] != DBNull.Value ? Convert.ToInt32(Data_Reader["FloorNumber"]) : (Int32?)null;
                    vm.CompletionDate = Data_Reader["CompletionDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CompletionDate"]) : (DateTime?)null;
                    vm.GroundFCDate = Data_Reader["GroundFCDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["GroundFCDate"]) : (DateTime?)null;
                    vm.FirstFCDate = Data_Reader["FirstFCDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["FirstFCDate"]) : (DateTime?)null;
                    vm.SccFCDate = Data_Reader["SccFCDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["SccFCDate"]) : (DateTime?)null;
                    vm.ThirdFCDate = Data_Reader["ThirdFCDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["ThirdFCDate"]) : (DateTime?)null;
                    vm.ForthFCDate = Data_Reader["ForthFCDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["ForthFCDate"]) : (DateTime?)null;
                    vm.FivthFCDate = Data_Reader["FivthFCDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["FivthFCDate"]) : (DateTime?)null;
                    vm.SixFCDate = Data_Reader["SixFCDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["SixFCDate"]) : (DateTime?)null;
                    vm.OtherFCDate = Data_Reader["OtherFCDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["OtherFCDate"]) : (DateTime?)null;
                    vm.OwnerPortion = Data_Reader["OwnerPortion"].ToString();
                    vm.DeveloperPortion = Data_Reader["DeveloperPortion"].ToString();
                    vm.BuyerPortion = Data_Reader["BuyerPortion"].ToString();
                    vm.SubmittedPortion = Data_Reader["SubmittedPortion"].ToString();

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


        //Get Unauth Portion By Id
        public UnauthPortion GetUnauthPortionById(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "detailsUnauthPortion";
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                UnauthPortion vm = new UnauthPortion();

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

                    vm.PlotId = Convert.ToInt32(Data_Reader["PlotId"]);
                    vm.PlotIdNumber = Data_Reader["PlotIdNumber"].ToString();

                    vm.UnauthComId = Convert.ToInt32(Data_Reader["UnauthComId"]);
                    vm.TotalUnauthArea = Data_Reader["TotalUnauthArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["TotalUnauthArea"]) : (Decimal?)null;
                    vm.FineFreeArea = Data_Reader["FineFreeArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["FineFreeArea"]) : (Decimal?)null;
                    vm.WithFineUnauth = Data_Reader["WithFineUnauth"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["WithFineUnauth"]) : (Decimal?)null;
                    vm.RemovedUnauthArea = Data_Reader["RemovedUnauthArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["RemovedUnauthArea"]) : (Decimal?)null;
                    vm.NonRemovedUnauth = Data_Reader["NonRemovedUnauth"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["NonRemovedUnauth"]) : (Decimal?)null;
                    vm.FineRate = Data_Reader["FineRate"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["FineRate"]) : (Decimal?)null;
                    vm.FineAmount = Data_Reader["FineAmount"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["FineAmount"]) : (Decimal?)null;

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

        //get Plot for select 
        public List<Plot> GetPlot()
        {
            try
            {
                Sql_Query = "[Plot].[spPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "selectPlot";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<Plot> vm = new List<Plot>();

                while (Data_Reader.Read())
                {
                    Plot model = new Plot
                    {


                       


                      
                        PlotId = Convert.ToInt32(Data_Reader["PlotId"]),
                        PlotIdNumber = Data_Reader["PlotIdNumber"].ToString(),
                        PlotNo = Data_Reader["PlotNo"].ToString()


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

        //get present addres for plotno change event

        public Plot GetPresentAddress(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "presentAddressByPlotId";
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                Plot vm = new Plot();

                while (Data_Reader.Read())
                {

                    vm.PlotId = Convert.ToInt32(Data_Reader["PlotId"]);
                    vm.PlotIdNumber = Data_Reader["PlotIdNumber"].ToString();
                    vm.PlotNo = Data_Reader["PlotNo"].ToString();
                    vm.AreaName = Data_Reader["AreaName"].ToString();
                    vm.AreaId = Convert.ToInt32(Data_Reader["AreaId"]);
                    

                   
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


        //Create Plot Owner Details
        public int PlotOwnerInsert(PlotOwnerCombineVM model)
        {
            try
            {
                Sql_Query = "[Plot].[spPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;


                //Sql_Command.Parameters.Add("@PlotOwnerId", SqlDbType.Int).Value = model.PlotOwnerId;
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = model.PlotId;
                Sql_Command.Parameters.Add("@PlotOwnerName ", SqlDbType.NVarChar).Value = model.PlotOwnerName;
                Sql_Command.Parameters.Add("@IsAlive ", SqlDbType.Bit).Value = model.IsAlive;
                Sql_Command.Parameters.Add("@OfficialStatusId", SqlDbType.Int).Value = model.OfficialStatusId;
                Sql_Command.Parameters.Add("@PresentAdd", SqlDbType.NVarChar).Value = model.PresentAdd;
                Sql_Command.Parameters.Add("@PermanentAdd", SqlDbType.NVarChar).Value = model.PermanentAdd;
                Sql_Command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = model.PhoneNumber;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
                Sql_Command.Parameters.Add("@LeaveDate", SqlDbType.DateTime).Value = model.LeaveDate;
                Sql_Command.Parameters.Add("@LeaseAuthority", SqlDbType.NVarChar).Value = model.LeaseAuthority;
                Sql_Command.Parameters.Add("@LeaseType", SqlDbType.NVarChar).Value = model.LeaseType;
                Sql_Command.Parameters.Add("@LeasePeriod ", SqlDbType.Int).Value = model.LeasePeriod;
                Sql_Command.Parameters.Add("@LeaseQuotaId", SqlDbType.Int).Value = model.LeaseQuotaId;
                Sql_Command.Parameters.Add("@HandOverOffice", SqlDbType.NVarChar).Value = model.HandOverOffice;
                Sql_Command.Parameters.Add("@HandOverLetterNo", SqlDbType.NVarChar).Value = model.HandOverLetterNo;
                Sql_Command.Parameters.Add("@LandDevelopChange", SqlDbType.Decimal).Value = model.LandDevelopChange;
                Sql_Command.Parameters.Add("@ConsStatusId", SqlDbType.Int).Value = model.ConsStatusId;
                Sql_Command.Parameters.Add("@Doc1", SqlDbType.NVarChar).Value = model.Doc1;
                Sql_Command.Parameters.Add("@Doc2", SqlDbType.NVarChar).Value = model.Doc2;
                Sql_Command.Parameters.Add("@Doc3", SqlDbType.NVarChar).Value = model.Doc3;
                Sql_Command.Parameters.Add("@Doc4", SqlDbType.NVarChar).Value = model.Doc4;
                Sql_Command.Parameters.Add("@Doc5", SqlDbType.NVarChar).Value = model.Doc5;
                Sql_Command.Parameters.Add("@Doc6", SqlDbType.NVarChar).Value = model.Doc6;
                //Sql_Command.Parameters.Add("@ConsProgressId", SqlDbType.Int).Value = model.ConsProgressId;
                Sql_Command.Parameters.Add("@OwnerDeclaration", SqlDbType.NVarChar).Value = model.OwnerDeclaration;
                Sql_Command.Parameters.Add("@RealBuilder", SqlDbType.NVarChar).Value = model.RealBuilder;
                Sql_Command.Parameters.Add("@DevelopDeposit", SqlDbType.Decimal).Value = model.DevelopDeposit;
                Sql_Command.Parameters.Add("@FloorNumber", SqlDbType.Int).Value = model.FloorNumber;
                Sql_Command.Parameters.Add("@CompletionDate", SqlDbType.DateTime).Value = model.CompletionDate;
                Sql_Command.Parameters.Add("@GroundFCDate", SqlDbType.DateTime).Value = model.GroundFCDate;
                Sql_Command.Parameters.Add("@FirstFCDate", SqlDbType.DateTime).Value = model.FirstFCDate;
                Sql_Command.Parameters.Add("@SccFCDate", SqlDbType.DateTime).Value = model.SccFCDate;
                Sql_Command.Parameters.Add("@ThirdFCDate", SqlDbType.DateTime).Value = model.ThirdFCDate;
                Sql_Command.Parameters.Add("@ForthFCDate", SqlDbType.DateTime).Value = model.ForthFCDate;
                Sql_Command.Parameters.Add("@FivthFCDate", SqlDbType.DateTime).Value = model.FivthFCDate;
                Sql_Command.Parameters.Add("@SixFCDate", SqlDbType.DateTime).Value = model.SixFCDate;
                Sql_Command.Parameters.Add("@OtherFCDate", SqlDbType.DateTime).Value = model.OtherFCDate;
                Sql_Command.Parameters.Add("@OwnerPortion", SqlDbType.NVarChar).Value = model.OwnerPortion;
                Sql_Command.Parameters.Add("@DeveloperPortion", SqlDbType.NVarChar).Value = model.DeveloperPortion;
                Sql_Command.Parameters.Add("@BuyerPortion", SqlDbType.NVarChar).Value = model.BuyerPortion;
                Sql_Command.Parameters.Add("@SubmittedPortion", SqlDbType.NVarChar).Value = model.SubmittedPortion;

                //Sql_Command.Parameters.Add("@UnauthComId", SqlDbType.Int).Value = model.UnauthComId;
                Sql_Command.Parameters.Add("@TotalUnauthArea", SqlDbType.Decimal).Value = model.TotalUnauthArea;
                Sql_Command.Parameters.Add("@FineFreeArea", SqlDbType.Decimal).Value = model.FineFreeArea;
                Sql_Command.Parameters.Add("@WithFineUnauth", SqlDbType.Decimal).Value = model.WithFineUnauth;
                Sql_Command.Parameters.Add("@RemovedUnauthArea", SqlDbType.Decimal).Value = model.RemovedUnauthArea;
                Sql_Command.Parameters.Add("@NonRemovedUnauth", SqlDbType.Decimal).Value = model.NonRemovedUnauth;
                Sql_Command.Parameters.Add("@FineRate", SqlDbType.Decimal).Value = model.FineRate;
                Sql_Command.Parameters.Add("@FineAmount", SqlDbType.Decimal).Value = model.FineAmount;


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
        public int PlotOwnerUpdate(PlotOwnerCombineVM model)
        {
            try
            {
                Sql_Query = "[Plot].[spPlotOwnerMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@PlotOwnerId", SqlDbType.Int).Value = model.PlotOwnerId;
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = model.PlotId;
                Sql_Command.Parameters.Add("@PlotOwnerName ", SqlDbType.NVarChar).Value = model.PlotOwnerName;
                Sql_Command.Parameters.Add("@IsAlive ", SqlDbType.Bit).Value = model.IsAlive;
                Sql_Command.Parameters.Add("@OfficialStatusId", SqlDbType.Int).Value = model.OfficialStatusId;
                Sql_Command.Parameters.Add("@PresentAdd", SqlDbType.NVarChar).Value = model.PresentAdd;
                Sql_Command.Parameters.Add("@PermanentAdd", SqlDbType.NVarChar).Value = model.PermanentAdd;
                Sql_Command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = model.PhoneNumber;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
                Sql_Command.Parameters.Add("@LeaveDate", SqlDbType.DateTime).Value = model.LeaveDate;
                Sql_Command.Parameters.Add("@LeaseAuthority", SqlDbType.NVarChar).Value = model.LeaseAuthority;
                Sql_Command.Parameters.Add("@LeaseType", SqlDbType.NVarChar).Value = model.LeaseType;
                Sql_Command.Parameters.Add("@LeasePeriod ", SqlDbType.Int).Value = model.LeasePeriod;
                Sql_Command.Parameters.Add("@LeaseQuotaId", SqlDbType.Int).Value = model.LeaseQuotaId;
                Sql_Command.Parameters.Add("@HandOverOffice", SqlDbType.NVarChar).Value = model.HandOverOffice;
                Sql_Command.Parameters.Add("@HandOverLetterNo", SqlDbType.NVarChar).Value = model.HandOverLetterNo;
                Sql_Command.Parameters.Add("@LandDevelopChange", SqlDbType.Decimal).Value = model.LandDevelopChange;
                Sql_Command.Parameters.Add("@ConsStatusId", SqlDbType.Int).Value = model.ConsStatusId;
                Sql_Command.Parameters.Add("@Doc1", SqlDbType.NVarChar).Value = model.Doc1;
                Sql_Command.Parameters.Add("@Doc2", SqlDbType.NVarChar).Value = model.Doc2;
                Sql_Command.Parameters.Add("@Doc3", SqlDbType.NVarChar).Value = model.Doc3;
                Sql_Command.Parameters.Add("@Doc4", SqlDbType.NVarChar).Value = model.Doc4;
                Sql_Command.Parameters.Add("@Doc5", SqlDbType.NVarChar).Value = model.Doc5;
                Sql_Command.Parameters.Add("@Doc6", SqlDbType.NVarChar).Value = model.Doc6;
                Sql_Command.Parameters.Add("@ConsProgressId", SqlDbType.Int).Value = model.ConsProgressId;
                Sql_Command.Parameters.Add("@OwnerDeclaration", SqlDbType.NVarChar).Value = model.OwnerDeclaration;
                Sql_Command.Parameters.Add("@RealBuilder", SqlDbType.NVarChar).Value = model.RealBuilder;
                Sql_Command.Parameters.Add("@DevelopDeposit", SqlDbType.Decimal).Value = model.DevelopDeposit;
                Sql_Command.Parameters.Add("@FloorNumber", SqlDbType.Int).Value = model.FloorNumber;
                Sql_Command.Parameters.Add("@CompletionDate", SqlDbType.DateTime).Value = model.CompletionDate;
                Sql_Command.Parameters.Add("@GroundFCDate", SqlDbType.DateTime).Value = model.GroundFCDate;
                Sql_Command.Parameters.Add("@FirstFCDate", SqlDbType.DateTime).Value = model.FirstFCDate;
                Sql_Command.Parameters.Add("@SccFCDate", SqlDbType.DateTime).Value = model.SccFCDate;
                Sql_Command.Parameters.Add("@ThirdFCDate", SqlDbType.DateTime).Value = model.ThirdFCDate;
                Sql_Command.Parameters.Add("@ForthFCDate", SqlDbType.DateTime).Value = model.ForthFCDate;
                Sql_Command.Parameters.Add("@FivthFCDate", SqlDbType.DateTime).Value = model.FivthFCDate;
                Sql_Command.Parameters.Add("@SixFCDate", SqlDbType.DateTime).Value = model.SixFCDate;
                Sql_Command.Parameters.Add("@OtherFCDate", SqlDbType.DateTime).Value = model.OtherFCDate;
                Sql_Command.Parameters.Add("@OwnerPortion", SqlDbType.NVarChar).Value = model.OwnerPortion;
                Sql_Command.Parameters.Add("@DeveloperPortion", SqlDbType.NVarChar).Value = model.DeveloperPortion;
                Sql_Command.Parameters.Add("@BuyerPortion", SqlDbType.NVarChar).Value = model.BuyerPortion;
                Sql_Command.Parameters.Add("@SubmittedPortion", SqlDbType.NVarChar).Value = model.SubmittedPortion;

                Sql_Command.Parameters.Add("@UnauthComId", SqlDbType.Int).Value = model.UnauthComId;
                Sql_Command.Parameters.Add("@TotalUnauthArea", SqlDbType.Decimal).Value = model.TotalUnauthArea;
                Sql_Command.Parameters.Add("@FineFreeArea", SqlDbType.Decimal).Value = model.FineFreeArea;
                Sql_Command.Parameters.Add("@WithFineUnauth", SqlDbType.Decimal).Value = model.WithFineUnauth;
                Sql_Command.Parameters.Add("@RemovedUnauthArea", SqlDbType.Decimal).Value = model.RemovedUnauthArea;
                Sql_Command.Parameters.Add("@NonRemovedUnauth", SqlDbType.Decimal).Value = model.NonRemovedUnauth;
                Sql_Command.Parameters.Add("@FineRate", SqlDbType.Decimal).Value = model.FineRate;
                Sql_Command.Parameters.Add("@FineAmount", SqlDbType.Decimal).Value = model.FineAmount;


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



        #region Details Portion  Other Plot Owner

        //Get All Plot Owner List
        public List<OthetPlotOwner> GetOthetPlotOwnerById(int id)
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

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@PlotOwnerId", SqlDbType.Int).Value = id;
                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<OthetPlotOwner> vm = new List<OthetPlotOwner>();

                while (Data_Reader.Read())
                {
                    OthetPlotOwner model = new OthetPlotOwner
                    {


                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUserName = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (Int32?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (Int32?)null,

                        PlotOwnerId = Convert.ToInt32(Data_Reader["PlotOwnerId"]),
                        OthetPlotOwnerId = Convert.ToInt32(Data_Reader["OthetPlotOwnerId"]),
                        OthetOwneeName = Data_Reader["OthetOwneeName"].ToString(),

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


        //Create Plot Owner Details
        public int OthetPlotOwnerInsert(OthetPlotOwner model)
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


                Sql_Command.Parameters.Add("@PlotOwnerId", SqlDbType.Int).Value = model.PlotOwnerId;
                //Sql_Command.Parameters.Add("@OthetPlotOwnerId ", SqlDbType.Int).Value = model.OthetPlotOwnerId;
                Sql_Command.Parameters.Add("@OthetOwneeName ", SqlDbType.NVarChar).Value = model.OthetOwneeName;
                Sql_Command.Parameters.Add("@Address ", SqlDbType.NVarChar).Value = model.Address;
                Sql_Command.Parameters.Add("@Remarks ", SqlDbType.NVarChar).Value = model.Remarks;
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



        //Delete Plot Owner Details
        public int OthetPlotOwnerDelete(int id)
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

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Delete;
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

        #endregion


        #region Details Portion  Design Approval

        //Get All Design Approval List
        public List<DesignApproval> GetDesignApprovalById(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spDesignApprovalMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = id;
                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<DesignApproval> vm = new List<DesignApproval>();

                while (Data_Reader.Read())
                {
                    DesignApproval model = new DesignApproval
                    {
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUserName = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (Int32?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (Int32?)null,

                        DesignAppId = Convert.ToInt32(Data_Reader["DesignAppId"]),
                        PlotId = Convert.ToInt32(Data_Reader["PlotId"]),
                        MEO_NCCDate = Data_Reader["MEO_NCCDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["MEO_NCCDate"]) : (DateTime?)null,
                        Reference = Data_Reader["Reference"].ToString(),
                        ApprovalDate = Convert.ToDateTime(Data_Reader["ApprovalDate"]),
                        ApprovalLetterNo = Data_Reader["ApprovalLetterNo"].ToString(),
                        FlorNumber = Data_Reader["FlorNumber"] != DBNull.Value ? Convert.ToInt32(Data_Reader["FlorNumber"]) : (Int32?)null,
                        GroundFlorArea = Data_Reader["GroundFlorArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["GroundFlorArea"]) : (Decimal?)null,
                        OtherFlorArea = Data_Reader["OtherFlorArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["OtherFlorArea"]) : (Decimal?)null,
                        ApprovalNo = Convert.ToInt32(Data_Reader["ApprovalNo"]),

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


        //Create Design Approval Details
        public int DesignApprovalInsert(DesignApproval model)
        {
            try
            {
                Sql_Query = "[Plot].[spDesignApprovalMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;


                Sql_Command.Parameters.Add("@DesignAppId", SqlDbType.Int).Value = model.DesignAppId;
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = model.PlotId;
                Sql_Command.Parameters.Add("@MEO_NCCDate", SqlDbType.DateTime).Value = model.MEO_NCCDate;
                Sql_Command.Parameters.Add("@Reference", SqlDbType.NVarChar).Value = model.Reference;
                Sql_Command.Parameters.Add("@ApprovalDate", SqlDbType.Date).Value = model.ApprovalDate;
                Sql_Command.Parameters.Add("@ApprovalLetterNo", SqlDbType.NVarChar).Value = model.ApprovalLetterNo;
                Sql_Command.Parameters.Add("@FlorNumber", SqlDbType.Int).Value = model.FlorNumber;
                Sql_Command.Parameters.Add("@GroundFlorArea", SqlDbType.Decimal).Value = model.GroundFlorArea;
                Sql_Command.Parameters.Add("@OtherFlorArea", SqlDbType.Decimal).Value = model.OtherFlorArea;
                Sql_Command.Parameters.Add("@ApprovalNo", SqlDbType.Int).Value = model.ApprovalNo;




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



        //Delete Design Approval Details
        public int DesignApprovalDelete(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spDesignApprovalMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Delete;
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = id;


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