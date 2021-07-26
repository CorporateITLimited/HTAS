using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Tax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Tax
{
    public class HoldingTaxGateway : DefaultGateway
    {
        //List
        public List<HoldingTax> GetAllHoldingTax()
        {
            try
            {
                Sql_Query = "[Tax].[spHoldingTax]";
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

                List<HoldingTax> holdingtaxList = new List<HoldingTax>();

                while (Data_Reader.Read())
                {
                    HoldingTax holdingtax = new HoldingTax()
                    {
                        HoldingTaxId = Convert.ToInt32(Data_Reader["HoldingTaxId"]),
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        FinancialYearId = Convert.ToInt32(Data_Reader["FinancialYearId"]),
                        FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),

                        TotalRent = Data_Reader["TotalRent"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalRent"]) : (Decimal?)null,

                        TaxFromRent = Data_Reader["TaxFromRent"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TaxFromRent"]) : (Decimal?)null,
                        TaxFromOwnProperty = Data_Reader["TaxFromOwnProperty"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TaxFromOwnProperty"]) : (Decimal?)null,

                        TotalHoldingTax = Data_Reader["TotalHoldingTax"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalHoldingTax"]) : (Decimal?)null,
                        Surcharge = Data_Reader["Surcharge"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Surcharge"]) : (Decimal?)null,

                        Rebate = Data_Reader["Rebate"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Rebate"]) : (Decimal?)null,
                        WrongInfoCharge = Data_Reader["WrongInfoCharge"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["WrongInfoCharge"]) : (Decimal?)null,

                        Date = Data_Reader["Date"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["Date"]) : (DateTime?)null,

                        EndDate = Data_Reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["EndDate"]) : (DateTime?)null,
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,

                        isFinalized = Data_Reader["isFinalized"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["isFinalized"]) : (bool?)null,

                        PaidAmount = Data_Reader["PaidAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["PaidAmount"]) : (Decimal?)null,

                        NetTaxPayableAmount = Data_Reader["NetTaxPayableAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["NetTaxPayableAmount"]) : (Decimal?)null,


                        AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
                        AreaName = Convert.ToString(Data_Reader["AreaName"]),
                        PlotIdNumber = Convert.ToString(Data_Reader["PlotIdNumber"]),
                        PlotNo = Convert.ToString(Data_Reader["PlotNo"]),
                        AreaPlotFlatData = Convert.ToString(Data_Reader["AreaPlotFlatData"]),
                        HolderName = Convert.ToString(Data_Reader["HolderName"]),
                        PaymentDate = Data_Reader["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["PaymentDate"]) : (DateTime?)null,
                        Remarks = Convert.ToString(Data_Reader["Remarks"])
                    };

                    holdingtaxList.Add(holdingtax);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return holdingtaxList;
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

        //List for Holder
        public List<HoldingTax> GetAllHoldingTaxForHolder(int HolderId)
        {
            try
            {
                Sql_Query = "[Tax].[spHoldingTax]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                // added to get holderid from session ===============================


                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "selectForHolder";
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.NVarChar).Value = HolderId;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<HoldingTax> holdingtaxList = new List<HoldingTax>();

                while (Data_Reader.Read())
                {
                    HoldingTax holdingtax = new HoldingTax()
                    {

                        HoldingTaxId = Convert.ToInt32(Data_Reader["HoldingTaxId"]),
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        FinancialYearId = Convert.ToInt32(Data_Reader["FinancialYearId"]),
                        FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),

                        TotalRent = Data_Reader["TotalRent"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalRent"]) : (Decimal?)null,

                        TaxFromRent = Data_Reader["TaxFromRent"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TaxFromRent"]) : (Decimal?)null,
                        TaxFromOwnProperty = Data_Reader["TaxFromOwnProperty"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TaxFromOwnProperty"]) : (Decimal?)null,

                        TotalHoldingTax = Data_Reader["TotalHoldingTax"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalHoldingTax"]) : (Decimal?)null,
                        Surcharge = Data_Reader["Surcharge"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Surcharge"]) : (Decimal?)null,

                        Rebate = Data_Reader["Rebate"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Rebate"]) : (Decimal?)null,
                        WrongInfoCharge = Data_Reader["WrongInfoCharge"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["WrongInfoCharge"]) : (Decimal?)null,

                        Date = Data_Reader["Date"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["Date"]) : (DateTime?)null,

                        EndDate = Data_Reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["EndDate"]) : (DateTime?)null,
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,

                        isFinalized = Data_Reader["isFinalized"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["isFinalized"]) : (bool?)null,

                        PaidAmount = Data_Reader["PaidAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["PaidAmount"]) : (Decimal?)null,

                        NetTaxPayableAmount = Data_Reader["NetTaxPayableAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["NetTaxPayableAmount"]) : (Decimal?)null,

                        AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
                        AreaName = Convert.ToString(Data_Reader["AreaName"]),
                        PlotIdNumber = Convert.ToString(Data_Reader["PlotIdNumber"]),
                        PlotNo = Convert.ToString(Data_Reader["PlotNo"]),
                        AreaPlotFlatData = Convert.ToString(Data_Reader["AreaPlotFlatData"]),
                        HolderName = Convert.ToString(Data_Reader["HolderName"]),
                        PaymentDate = Data_Reader[" PaymentDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader[" PaymentDate"]) : (DateTime?)null,
                        Remarks = Convert.ToString(Data_Reader["Remarks"])
                    };

                    holdingtaxList.Add(holdingtax);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return holdingtaxList;
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

        //Details
        public HoldingTax GetHoldingTaxById(int id)
        {
            try
            {
                Sql_Query = "[Tax].[spHoldingTax]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@HoldingTaxId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                HoldingTax holdingtaxList = new HoldingTax();

                while (Data_Reader.Read())
                {
                    HoldingTax holdingtax = new HoldingTax()
                    {
                        HoldingTaxId = Convert.ToInt32(Data_Reader["HoldingTaxId"]),
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        FinancialYearId = Convert.ToInt32(Data_Reader["FinancialYearId"]),
                        FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),

                        TotalRent = Data_Reader["TotalRent"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalRent"]) : (Decimal?)null,

                        TaxFromRent = Data_Reader["TaxFromRent"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TaxFromRent"]) : (Decimal?)null,
                        TaxFromOwnProperty = Data_Reader["TaxFromOwnProperty"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TaxFromOwnProperty"]) : (Decimal?)null,

                        TotalHoldingTax = Data_Reader["TotalHoldingTax"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalHoldingTax"]) : (Decimal?)null,
                        Surcharge = Data_Reader["Surcharge"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Surcharge"]) : (Decimal?)null,

                        Rebate = Data_Reader["Rebate"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Rebate"]) : (Decimal?)null,
                        WrongInfoCharge = Data_Reader["WrongInfoCharge"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["WrongInfoCharge"]) : (Decimal?)null,

                        Date = Data_Reader["Date"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["Date"]) : (DateTime?)null,

                        EndDate = Data_Reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["EndDate"]) : (DateTime?)null,
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,

                        isFinalized = Data_Reader["isFinalized"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["isFinalized"]) : (bool?)null,

                        PaidAmount = Data_Reader["PaidAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["PaidAmount"]) : (Decimal?)null,

                        NetTaxPayableAmount = Data_Reader["NetTaxPayableAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["NetTaxPayableAmount"]) : (Decimal?)null,


                        AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
                        AreaName = Convert.ToString(Data_Reader["AreaName"]),
                        PlotIdNumber = Convert.ToString(Data_Reader["PlotIdNumber"]),
                        PlotNo = Convert.ToString(Data_Reader["PlotNo"]),
                        AreaPlotFlatData = Convert.ToString(Data_Reader["AreaPlotFlatData"]),
                        HolderName = Convert.ToString(Data_Reader["HolderName"]),
                        PaymentDate = Data_Reader["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["PaymentDate"]) : (DateTime?)null,
                        Remarks = Convert.ToString(Data_Reader["Remarks"])
                    };

                    holdingtax.StringPaymentDate = $"{holdingtax.PaymentDate:dd/MM/yyyy}";

                    holdingtax.SubTotalHoldingTax = holdingtax.NetTaxPayableAmount;

                    holdingtaxList = holdingtax;
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return holdingtaxList;
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

        public HoldingTax GetRebateAndWrongInfoByHoldingTaxId(int id)
        {
            try
            {
                Sql_Query = "[Tax].[spGetRebateAndWrongInfoByHoldingTaxId]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@HoldingTaxId", SqlDbType.Int).Value = id;


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                HoldingTax holdingtax = new HoldingTax();

                while (Data_Reader.Read())
                {

                    holdingtax.SubTotalHoldingTax = Data_Reader["SubTotalHoldingTax"] != DBNull.Value ?
                                            Convert.ToDecimal(Data_Reader["SubTotalHoldingTax"]) : (Decimal?)null;
                    holdingtax.RebatePercent = Data_Reader["RebatePercent"] != DBNull.Value ?
                                             Convert.ToDecimal(Data_Reader["RebatePercent"]) : (Decimal?)null;
                    holdingtax.RebateValue = Data_Reader["RebateValue"] != DBNull.Value ?
                                             Convert.ToDecimal(Data_Reader["RebateValue"]) : (Decimal?)null;
                    holdingtax.WrongInfoChargePercent = Data_Reader["WrongInfoChargePercent"] != DBNull.Value ?
                                             Convert.ToDecimal(Data_Reader["WrongInfoChargePercent"]) : (Decimal?)null;
                    holdingtax.WrongInfoChargeValue = Data_Reader["WrongInfoChargeValue"] != DBNull.Value ?
                                            Convert.ToDecimal(Data_Reader["WrongInfoChargeValue"]) : (Decimal?)null;
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return holdingtax;
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


        public int UpdateTax(HoldingTax tax)
        {
            try
            {
                Sql_Query = "[Tax].[spHoldingTax]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "only_update";
                Sql_Command.Parameters.Add("@Rebate", SqlDbType.Decimal).Value = tax.Rebate;
                Sql_Command.Parameters.Add("@WrongInfoCharge", SqlDbType.Decimal).Value = tax.WrongInfoCharge;
                Sql_Command.Parameters.Add("@isFinalized", SqlDbType.Bit).Value = tax.isFinalized;
                Sql_Command.Parameters.Add("@PaidAmount", SqlDbType.Decimal).Value = tax.PaidAmount;
                Sql_Command.Parameters.Add("@NetTaxPayableAmount", SqlDbType.Decimal).Value = tax.NetTaxPayableAmount;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = tax.LastUpdatedBy;
                Sql_Command.Parameters.Add("@HoldingTaxId", SqlDbType.Int).Value = tax.HoldingTaxId;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = tax.LastUpdated;
                Sql_Command.Parameters.Add("@PaymentDate", SqlDbType.DateTime).Value = tax.PaymentDate;
                Sql_Command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = tax.Remarks;

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


        public int GenerateTax(int FinYearid)
        {
            try
            {
                Sql_Query = "[Tax].[spGenerateTax]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@FinancialyearId", SqlDbType.NVarChar).Value = FinYearid;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Sql_Command.ExecuteNonQuery();
                //int rowAffected = Sql_Command.ExecuteNonQuery();
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