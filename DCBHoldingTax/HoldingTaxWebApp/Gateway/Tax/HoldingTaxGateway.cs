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
                Sql_Query = "[Tax].[spHoldingTax] ";
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
                        
                        TotalRent = Data_Reader["TotalRent"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalRent"]) : (Decimal?)null,

                         TaxFromRent = Data_Reader["TaxFromRent"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TaxFromRent"]) : (Decimal?)null,
                         TaxFromOwnProperty = Data_Reader["TaxFromOwnProperty"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TaxFromOwnProperty"]) : (Decimal?)null,

                          TotalHoldingTax= Data_Reader["TotalHoldingTax"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalHoldingTax"]) : (Decimal?)null,
                          Surcharge= Data_Reader["Surcharge"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Surcharge"]) : (Decimal?)null,

                          Rebate= Data_Reader["Rebate"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Rebate"]) : (Decimal?)null,
                            WrongInfoCharge= Data_Reader["WrongInfoCharge"] != DBNull.Value ?
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

                        isFinalized = Data_Reader["isFinalized"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,

                             PaidAmount = Data_Reader["PaidAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["PaidAmount"]) : (Decimal?)null,

                                    NetTaxPayableAmount = Data_Reader["NetTaxPayableAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["NetTaxPayableAmount"]) : (Decimal?)null,







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
                Sql_Query = "[Tax].[spHoldingTax] ";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@HoldingTaxId", SqlDbType.NVarChar).Value = id;

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

                        isFinalized = Data_Reader["isFinalized"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,

                        PaidAmount = Data_Reader["PaidAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["PaidAmount"]) : (Decimal?)null,

                        NetTaxPayableAmount = Data_Reader["NetTaxPayableAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["NetTaxPayableAmount"]) : (Decimal?)null,







                    };

                    holdingtaxList=holdingtax;
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
    }
}