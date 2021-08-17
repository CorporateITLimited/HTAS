using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Holding;
using HoldingTaxWebApp.Models.Tax;
using HoldingTaxWebApp.ViewModels.Tax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Tax
{
    public class TransactionGateway : DefaultGateway
    {
        public List<TransactionPayment> GetAllTranscation()
        {
            try
            {
                Sql_Query = "[Tax].[spTransaction]";
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

                List<TransactionPayment> vm = new List<TransactionPayment>();

                while (Data_Reader.Read())
                {
                    TransactionPayment model = new TransactionPayment()
                    {
                        HoldingTaxId = Convert.ToInt32(Data_Reader["HoldingTaxId"]),
                        //  FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        // HolderName = Convert.ToString(Data_Reader["HolderName"]),
                        IPAddressDetails = Convert.ToString(Data_Reader["IPAddressDetails"]),
                        IsSuccessfulTransaction = Data_Reader["IsSuccessfulTransaction"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsSuccessfulTransaction"]) : (bool?)null,
                        //LastUpdatedByUsername = Convert.ToString(Data_Reader["LastUpdatedByUsername"]),
                        ProductName = Convert.ToString(Data_Reader["ProductName"]),
                        RequestValidationID = Convert.ToString(Data_Reader["RequestValidationID"]),
                        TransactionAmount = Data_Reader["TransactionAmount"] !=
                                                DBNull.Value ? Convert.ToDecimal(Data_Reader["TransactionAmount"]) : (decimal?)null,
                        TransactionCode = Convert.ToString(Data_Reader["TransactionCode"]),
                        TransactionCurrency = Convert.ToString(Data_Reader["TransactionCurrency"]),
                        TransactionDate = Data_Reader["TransactionDate"] !=
                                                DBNull.Value ? Convert.ToDateTime(Data_Reader["TransactionDate"]) : (DateTime?)null,
                        TransactionId = Convert.ToInt64(Data_Reader["TransactionId"])
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

        public TransactionPayment GetTranscationById(int id)
        {
            try
            {
                Sql_Query = "[Tax].[spTransaction]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@TransactionId", SqlDbType.BigInt).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                TransactionPayment vm = new TransactionPayment();

                while (Data_Reader.Read())
                {
                    vm.HoldingTaxId = Convert.ToInt32(Data_Reader["HoldingTaxId"]);
                    //  FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),
                    vm.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    vm.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    // HolderName = Convert.ToString(Data_Reader["HolderName"]),
                    vm.IPAddressDetails = Convert.ToString(Data_Reader["IPAddressDetails"]);
                    vm.IsSuccessfulTransaction = Data_Reader["IsSuccessfulTransaction"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["IsSuccessfulTransaction"]) : (bool?)null;
                    //LastUpdatedByUsername = Convert.ToString(Data_Reader["LastUpdatedByUsername"]),
                    vm.ProductName = Convert.ToString(Data_Reader["ProductName"]);
                    vm.RequestValidationID = Convert.ToString(Data_Reader["RequestValidationID"]);
                    vm.TransactionAmount = Data_Reader["TransactionAmount"] !=
                                            DBNull.Value ? Convert.ToDecimal(Data_Reader["TransactionAmount"]) : (decimal?)null;
                    vm.TransactionCode = Convert.ToString(Data_Reader["TransactionCode"]);
                    vm.TransactionCurrency = Convert.ToString(Data_Reader["TransactionCurrency"]);
                    vm.TransactionDate = Data_Reader["TransactionDate"] !=
                                            DBNull.Value ? Convert.ToDateTime(Data_Reader["TransactionDate"]) : (DateTime?)null;
                    vm.TransactionId = Convert.ToInt64(Data_Reader["TransactionId"]);
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

        public TransactionPayment GetTranscationByTransactionCode(string TransactionCode)
        {
            try
            {
                Sql_Query = "[Tax].[spTransaction]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "details_trnxcode";
                Sql_Command.Parameters.Add("@TransactionCode", SqlDbType.NVarChar).Value = TransactionCode;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                TransactionPayment vm = new TransactionPayment();

                while (Data_Reader.Read())
                {
                    vm.HoldingTaxId = Convert.ToInt32(Data_Reader["HoldingTaxId"]);
                    //  FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),
                    vm.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    vm.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    // HolderName = Convert.ToString(Data_Reader["HolderName"]),
                    vm.IPAddressDetails = Convert.ToString(Data_Reader["IPAddressDetails"]);
                    vm.IsSuccessfulTransaction = Data_Reader["IsSuccessfulTransaction"] !=
                                            DBNull.Value ? Convert.ToBoolean(Data_Reader["IsSuccessfulTransaction"]) : (bool?)null;
                    //LastUpdatedByUsername = Convert.ToString(Data_Reader["LastUpdatedByUsername"]),
                    vm.ProductName = Convert.ToString(Data_Reader["ProductName"]);
                    vm.RequestValidationID = Convert.ToString(Data_Reader["RequestValidationID"]);
                    vm.TransactionAmount = Data_Reader["TransactionAmount"] !=
                                            DBNull.Value ? Convert.ToDecimal(Data_Reader["TransactionAmount"]) : (decimal?)null;
                    vm.TransactionCode = Convert.ToString(Data_Reader["TransactionCode"]);
                    vm.TransactionCurrency = Convert.ToString(Data_Reader["TransactionCurrency"]);
                    vm.TransactionDate = Data_Reader["TransactionDate"] !=
                                            DBNull.Value ? Convert.ToDateTime(Data_Reader["TransactionDate"]) : (DateTime?)null;
                    vm.TransactionId = Convert.ToInt64(Data_Reader["TransactionId"]);
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

        public int InsertTranscation(TransactionPayment trnx)
        {
            try
            {
                Sql_Query = "[Tax].[spTransaction]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;

                Sql_Command.Parameters.Add("@TransactionId", SqlDbType.BigInt).Value = trnx.TransactionId;
                Sql_Command.Parameters.Add("@TransactionCode", SqlDbType.NVarChar).Value = trnx.TransactionCode;
                Sql_Command.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = trnx.TransactionDate;
                Sql_Command.Parameters.Add("@TransactionAmount", SqlDbType.Decimal).Value = trnx.TransactionAmount;
                Sql_Command.Parameters.Add("@TransactionCurrency", SqlDbType.VarChar).Value = trnx.TransactionCurrency;
                Sql_Command.Parameters.Add("@IsSuccessfulTransaction", SqlDbType.Int).Value = trnx.IsSuccessfulTransaction;
                Sql_Command.Parameters.Add("@HoldingTaxId", SqlDbType.Int).Value = trnx.HoldingTaxId;
                Sql_Command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = trnx.ProductName;
                Sql_Command.Parameters.Add("@RequestValidationID", SqlDbType.NVarChar).Value = trnx.RequestValidationID;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = trnx.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = trnx.LastUpdated;
                Sql_Command.Parameters.Add("@IPAddressDetails", SqlDbType.NVarChar).Value = trnx.IPAddressDetails;

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

        public bool IsTransactionCodeExist(string TransactionCode)
        {
            try
            {
                Sql_Query = "SELECT [TransactionCode] FROM [Tax].[tTransaction] WHERE [TransactionCode]=@TransactionCode";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.Text
                };
                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@TransactionCode", SqlDbType.NVarChar).Value = TransactionCode;
                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();
                bool isExist = Data_Reader.HasRows;
                Data_Reader.Close();
                Sql_Connection.Close();
                return isExist;
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


        public int UpdateTranscation(TransactionPayment trnx)
        {
            try
            {
                Sql_Query = "[Tax].[spTransaction]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@TransactionId", SqlDbType.BigInt).Value = trnx.TransactionId;
                Sql_Command.Parameters.Add("@TransactionCode", SqlDbType.NVarChar).Value = null;//trnx.TransactionCode;
                Sql_Command.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = null;//trnx.TransactionDate;
                Sql_Command.Parameters.Add("@TransactionAmount", SqlDbType.Decimal).Value = null; //trnx.TransactionAmount;
                Sql_Command.Parameters.Add("@TransactionCurrency", SqlDbType.VarChar).Value = null;// trnx.TransactionCurrency;
                Sql_Command.Parameters.Add("@IsSuccessfulTransaction", SqlDbType.Int).Value = trnx.IsSuccessfulTransaction;
                Sql_Command.Parameters.Add("@HoldingTaxId", SqlDbType.Int).Value = null; //trnx.HoldingTaxId;
                Sql_Command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = null;// trnx.ProductName;
                Sql_Command.Parameters.Add("@RequestValidationID", SqlDbType.NVarChar).Value = trnx.RequestValidationID;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = trnx.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = trnx.LastUpdated;
                Sql_Command.Parameters.Add("@IPAddressDetails", SqlDbType.NVarChar).Value = null;// trnx.IPAddressDetails;

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