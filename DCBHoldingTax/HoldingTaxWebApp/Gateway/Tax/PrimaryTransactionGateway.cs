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
    public class PrimaryTransactionGateway : DefaultGateway
    {
        public List<PrimaryTransaction> GetAllPrimaryTransaction()
        {
            try
            {
                Sql_Query = "[Tax].[spPrimaryTransactionMaster]";
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

                List<PrimaryTransaction> PrimaryTransactionList = new List<PrimaryTransaction>();

                while (Data_Reader.Read())
                {
                    PrimaryTransaction PrimaryTransaction = new PrimaryTransaction
                    {
                        PrimaryTransactionId = Convert.ToInt64(Data_Reader["PrimaryTransactionId"]),

                        Status = Data_Reader["Status"].ToString(),
                        TranDate = Data_Reader["TranDate"] != DBNull.Value ?
                                                    Convert.ToDateTime(Data_Reader["TranDate"]) : (DateTime?)null,
                        TranId = Data_Reader["TranId"].ToString(),
                        ValId = Data_Reader["ValId"].ToString(),
                        Amount = Data_Reader["Amount"] != DBNull.Value ?
                                                    Convert.ToDecimal(Data_Reader["Amount"]) : (Decimal?)null,
                        StoreAmount = Data_Reader["StoreAmount"] != DBNull.Value ?
                                                    Convert.ToDecimal(Data_Reader["StoreAmount"]) : (Decimal?)null,
                        CardType = Data_Reader["CardType"].ToString(),
                        CardNo = Data_Reader["CardNo"].ToString(),
                        Currency = Data_Reader["Currency"].ToString(),
                        BankTranId = Data_Reader["BankTranId"].ToString(),
                        CardIssuer = Data_Reader["CardIssuer"].ToString(),
                        CardBrand = Data_Reader["CardBrand"].ToString(),
                        CardIssuerCountry = Data_Reader["CardIssuerCountry"].ToString(),
                        CardIssuerCountryCode = Data_Reader["CardIssuerCountryCode"].ToString(),
                        CurrencyType = Data_Reader["CurrencyType"].ToString(),
                        CurrencyAmount = Data_Reader["CurrencyAmount"] != DBNull.Value ?
                                                    Convert.ToDecimal(Data_Reader["CurrencyAmount"]) : (Decimal?)null,
                        EmiInstalment = Data_Reader["EmiInstalment"] != DBNull.Value ?
                                                    Convert.ToInt32(Data_Reader["EmiInstalment"]) : (int?)null,
                        EmiAmount = Data_Reader["EmiAmount"] != DBNull.Value ?
                                                    Convert.ToDecimal(Data_Reader["EmiAmount"]) : (Decimal?)null,
                        DiscountAmount = Data_Reader["DiscountAmount"] != DBNull.Value ?
                                                    Convert.ToDecimal(Data_Reader["DiscountAmount"]) : (Decimal?)null,
                        DiscountPercentage = Data_Reader["DiscountPercentage"] != DBNull.Value ?
                                                    Convert.ToDecimal(Data_Reader["DiscountPercentage"]) : (Decimal?)null,
                        DiscountRemarks = Data_Reader["DiscountRemarks"].ToString(),
                        ValueA = Data_Reader["ValueA"].ToString(),
                        ValueB = Data_Reader["ValueB"].ToString(),
                        ValueC = Data_Reader["ValueC"].ToString(),
                        ValueD = Data_Reader["ValueD"].ToString(),
                        RiskLevel = Data_Reader["RiskLevel"] != DBNull.Value ?
                                                    Convert.ToInt32(Data_Reader["RiskLevel"]) : (int?)null,
                        SecondaryStatus = Data_Reader["SecondaryStatus"].ToString(),
                        RiskTitle = Data_Reader["RiskTitle"].ToString(),
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                    Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        HolderName = Data_Reader["HolderName"].ToString(),
                        FinancialYear = Data_Reader["FinancialYear"].ToString(),
                        VendorCharge = Data_Reader["VendorCharge"] != DBNull.Value ?
                                                    Convert.ToDecimal(Data_Reader["VendorCharge"]) : (Decimal?)null,

                    };

                    PrimaryTransaction.StringTranDate = $"{PrimaryTransaction.TranDate:dd/MM/yyyy hh:mm:ss tt}";
                    PrimaryTransaction.StringCreateDate = $"{PrimaryTransaction.CreateDate:dd/MM/yyyy hh:mm:ss tt}";

                    PrimaryTransactionList.Add(PrimaryTransaction);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return PrimaryTransactionList;
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

        public PrimaryTransaction GetPrimaryTransactionById(int id)
        {
            try
            {
                Sql_Query = "[Tax].[spPrimaryTransactionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@PrimaryTransactionId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                PrimaryTransaction PrimaryTransaction = new PrimaryTransaction();

                while (Data_Reader.Read())
                {
                    PrimaryTransaction.PrimaryTransactionId = Convert.ToInt64(Data_Reader["PrimaryTransactionId"]);

                    PrimaryTransaction.Status = Data_Reader["Status"].ToString();
                    PrimaryTransaction.TranDate = Data_Reader["TranDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["TranDate"]) : (DateTime?)null;
                    PrimaryTransaction.TranId = Data_Reader["TranId"].ToString();
                    PrimaryTransaction.ValId = Data_Reader["ValId"].ToString();
                    PrimaryTransaction.Amount = Data_Reader["Amount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Amount"]) : (Decimal?)null;
                    PrimaryTransaction.StoreAmount = Data_Reader["StoreAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["StoreAmount"]) : (Decimal?)null;
                    PrimaryTransaction.CardType = Data_Reader["CardType"].ToString();
                    PrimaryTransaction.CardNo = Data_Reader["CardNo"].ToString();
                    PrimaryTransaction.Currency = Data_Reader["Currency"].ToString();
                    PrimaryTransaction.BankTranId = Data_Reader["BankTranId"].ToString();
                    PrimaryTransaction.CardIssuer = Data_Reader["CardIssuer"].ToString();
                    PrimaryTransaction.CardBrand = Data_Reader["CardBrand"].ToString();
                    PrimaryTransaction.CardIssuerCountry = Data_Reader["CardIssuerCountry"].ToString();
                    PrimaryTransaction.CardIssuerCountryCode = Data_Reader["CardIssuerCountryCode"].ToString();
                    PrimaryTransaction.CurrencyType = Data_Reader["CurrencyType"].ToString();
                    PrimaryTransaction.CurrencyAmount = Data_Reader["CurrencyAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["CurrencyAmount"]) : (Decimal?)null;
                    PrimaryTransaction.EmiInstalment = Data_Reader["EmiInstalment"] != DBNull.Value ?
                                                Convert.ToInt32(Data_Reader["EmiInstalment"]) : (int?)null;
                    PrimaryTransaction.EmiAmount = Data_Reader["EmiAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["EmiAmount"]) : (Decimal?)null;
                    PrimaryTransaction.DiscountAmount = Data_Reader["DiscountAmount"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["DiscountAmount"]) : (Decimal?)null;
                    PrimaryTransaction.DiscountPercentage = Data_Reader["DiscountPercentage"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["DiscountPercentage"]) : (Decimal?)null;
                    PrimaryTransaction.DiscountRemarks = Data_Reader["DiscountRemarks"].ToString();
                    PrimaryTransaction.ValueA = Data_Reader["ValueA"].ToString();
                    PrimaryTransaction.ValueB = Data_Reader["ValueB"].ToString();
                    PrimaryTransaction.ValueC = Data_Reader["ValueC"].ToString();
                    PrimaryTransaction.ValueD = Data_Reader["ValueD"].ToString();
                    PrimaryTransaction.RiskLevel = Data_Reader["RiskLevel"] != DBNull.Value ?
                                                Convert.ToInt32(Data_Reader["RiskLevel"]) : (int?)null;
                    PrimaryTransaction.SecondaryStatus = Data_Reader["SecondaryStatus"].ToString();
                    PrimaryTransaction.RiskTitle = Data_Reader["RiskTitle"].ToString();
                    PrimaryTransaction.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    PrimaryTransaction.HolderName = Data_Reader["HolderName"].ToString();
                    PrimaryTransaction.FinancialYear = Data_Reader["FinancialYear"].ToString();
                    PrimaryTransaction.VendorCharge = Data_Reader["VendorCharge"] != DBNull.Value ?
                                                     Convert.ToDecimal(Data_Reader["VendorCharge"]) : (Decimal?)null;

                }

                PrimaryTransaction.StringTranDate = $"{PrimaryTransaction.TranDate:dd/MM/yyyy hh:mm:ss tt}";
                PrimaryTransaction.StringCreateDate = $"{PrimaryTransaction.CreateDate:dd/MM/yyyy hh:mm:ss tt}";

                Data_Reader.Close();
                Sql_Connection.Close();

                return PrimaryTransaction;
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

        public int PrimaryTransactionGatewayInsert(PrimaryTransaction PrimaryTransaction)
        {
            try
            {
                Sql_Query = "[Tax].[spPrimaryTransactionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;
                Sql_Command.Parameters.Add("@Status", SqlDbType.VarChar).Value = PrimaryTransaction.Status;
                Sql_Command.Parameters.Add("@TranDate", SqlDbType.DateTime).Value = PrimaryTransaction.TranDate;
                Sql_Command.Parameters.Add("@TranId", SqlDbType.VarChar).Value = PrimaryTransaction.TranId;
                Sql_Command.Parameters.Add("@ValId", SqlDbType.VarChar).Value = PrimaryTransaction.ValId;
                Sql_Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = PrimaryTransaction.Amount;
                Sql_Command.Parameters.Add("@StoreAmount", SqlDbType.Decimal).Value = PrimaryTransaction.StoreAmount;
                Sql_Command.Parameters.Add("@CardType", SqlDbType.VarChar).Value = PrimaryTransaction.CardType;
                Sql_Command.Parameters.Add("@CardNo", SqlDbType.VarChar).Value = PrimaryTransaction.CardNo;
                Sql_Command.Parameters.Add("@Currency", SqlDbType.VarChar).Value = PrimaryTransaction.Currency;
                Sql_Command.Parameters.Add("@BankTranId", SqlDbType.VarChar).Value = PrimaryTransaction.BankTranId;
                Sql_Command.Parameters.Add("@CardIssuer", SqlDbType.VarChar).Value = PrimaryTransaction.CardIssuer;
                Sql_Command.Parameters.Add("@CardBrand", SqlDbType.VarChar).Value = PrimaryTransaction.CardBrand;
                Sql_Command.Parameters.Add("@CardIssuerCountry", SqlDbType.VarChar).Value = PrimaryTransaction.CardIssuerCountry;
                Sql_Command.Parameters.Add("@CardIssuerCountryCode", SqlDbType.VarChar).Value = PrimaryTransaction.CardIssuerCountryCode;
                Sql_Command.Parameters.Add("@CurrencyType", SqlDbType.VarChar).Value = PrimaryTransaction.CurrencyType;
                Sql_Command.Parameters.Add("@CurrencyAmount", SqlDbType.Decimal).Value = PrimaryTransaction.CurrencyAmount;
                Sql_Command.Parameters.Add("@EmiInstalment", SqlDbType.Int).Value = PrimaryTransaction.EmiInstalment;
                Sql_Command.Parameters.Add("@EmiAmount", SqlDbType.Decimal).Value = PrimaryTransaction.EmiAmount;
                Sql_Command.Parameters.Add("@DiscountAmount", SqlDbType.Decimal).Value = PrimaryTransaction.DiscountAmount;
                Sql_Command.Parameters.Add("@DiscountPercentage", SqlDbType.Decimal).Value = PrimaryTransaction.DiscountPercentage;
                Sql_Command.Parameters.Add("@DiscountRemarks", SqlDbType.VarChar).Value = PrimaryTransaction.DiscountRemarks;
                Sql_Command.Parameters.Add("@ValueA", SqlDbType.VarChar).Value = PrimaryTransaction.ValueA;
                Sql_Command.Parameters.Add("@ValueB", SqlDbType.VarChar).Value = PrimaryTransaction.ValueB;
                Sql_Command.Parameters.Add("@ValueC", SqlDbType.VarChar).Value = PrimaryTransaction.ValueC;
                Sql_Command.Parameters.Add("@ValueD", SqlDbType.VarChar).Value = PrimaryTransaction.ValueD;
                Sql_Command.Parameters.Add("@RiskLevel", SqlDbType.Int).Value = PrimaryTransaction.RiskLevel;
                Sql_Command.Parameters.Add("@SecondaryStatus", SqlDbType.VarChar).Value = PrimaryTransaction.SecondaryStatus;
                Sql_Command.Parameters.Add("@RiskTitle", SqlDbType.VarChar).Value = PrimaryTransaction.RiskTitle;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = PrimaryTransaction.CreateDate;



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

        public int PrimaryTransactionGatewayUpdate(PrimaryTransaction PrimaryTransaction)
        {
            try
            {
                Sql_Query = "[Tax].[spPrimaryTransactionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@Status", SqlDbType.VarChar).Value = PrimaryTransaction.Status;
                Sql_Command.Parameters.Add("@TranDate", SqlDbType.DateTime).Value = PrimaryTransaction.TranDate;
                Sql_Command.Parameters.Add("@TranId", SqlDbType.VarChar).Value = PrimaryTransaction.TranId;
                Sql_Command.Parameters.Add("@ValId", SqlDbType.VarChar).Value = PrimaryTransaction.ValId;
                Sql_Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = PrimaryTransaction.Amount;
                Sql_Command.Parameters.Add("@StoreAmount", SqlDbType.Decimal).Value = PrimaryTransaction.StoreAmount;
                Sql_Command.Parameters.Add("@CardType", SqlDbType.VarChar).Value = PrimaryTransaction.CardType;
                Sql_Command.Parameters.Add("@CardNo", SqlDbType.VarChar).Value = PrimaryTransaction.CardNo;
                Sql_Command.Parameters.Add("@Currency", SqlDbType.VarChar).Value = PrimaryTransaction.Currency;
                Sql_Command.Parameters.Add("@BankTranId", SqlDbType.VarChar).Value = PrimaryTransaction.BankTranId;
                Sql_Command.Parameters.Add("@CardIssuer", SqlDbType.VarChar).Value = PrimaryTransaction.CardIssuer;
                Sql_Command.Parameters.Add("@CardBrand", SqlDbType.VarChar).Value = PrimaryTransaction.CardBrand;
                Sql_Command.Parameters.Add("@CardIssuerCountry", SqlDbType.VarChar).Value = PrimaryTransaction.CardIssuerCountry;
                Sql_Command.Parameters.Add("@CardIssuerCountryCode", SqlDbType.VarChar).Value = PrimaryTransaction.CardIssuerCountryCode;
                Sql_Command.Parameters.Add("@CurrencyType", SqlDbType.VarChar).Value = PrimaryTransaction.CurrencyType;
                Sql_Command.Parameters.Add("@CurrencyAmount", SqlDbType.Decimal).Value = PrimaryTransaction.CurrencyAmount;
                Sql_Command.Parameters.Add("@EmiInstalment", SqlDbType.Int).Value = PrimaryTransaction.EmiInstalment;
                Sql_Command.Parameters.Add("@EmiAmount", SqlDbType.Decimal).Value = PrimaryTransaction.EmiAmount;
                Sql_Command.Parameters.Add("@DiscountAmount", SqlDbType.Decimal).Value = PrimaryTransaction.DiscountAmount;
                Sql_Command.Parameters.Add("@DiscountPercentage", SqlDbType.Decimal).Value = PrimaryTransaction.DiscountPercentage;
                Sql_Command.Parameters.Add("@DiscountRemarks", SqlDbType.VarChar).Value = PrimaryTransaction.DiscountRemarks;
                Sql_Command.Parameters.Add("@ValueA", SqlDbType.VarChar).Value = PrimaryTransaction.ValueA;
                Sql_Command.Parameters.Add("@ValueB", SqlDbType.VarChar).Value = PrimaryTransaction.ValueB;
                Sql_Command.Parameters.Add("@ValueC", SqlDbType.VarChar).Value = PrimaryTransaction.ValueC;
                Sql_Command.Parameters.Add("@ValueD", SqlDbType.VarChar).Value = PrimaryTransaction.ValueD;
                Sql_Command.Parameters.Add("@RiskLevel", SqlDbType.Int).Value = PrimaryTransaction.RiskLevel;
                Sql_Command.Parameters.Add("@SecondaryStatus", SqlDbType.VarChar).Value = PrimaryTransaction.SecondaryStatus;
                Sql_Command.Parameters.Add("@RiskTitle", SqlDbType.VarChar).Value = PrimaryTransaction.RiskTitle;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = PrimaryTransaction.CreateDate;



                Sql_Command.Parameters.Add("@PrimaryTransactionId", SqlDbType.BigInt).Value = PrimaryTransaction.PrimaryTransactionId;



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