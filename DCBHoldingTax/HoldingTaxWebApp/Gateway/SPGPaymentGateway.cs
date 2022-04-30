using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway
{
    public class SPGPaymentGateway : DefaultGateway
    {
        public List<SPGTransaction> GetSPGTransaction()
        {
            try
            {
                Sql_Query = "[paygateway].[spSPGTransactionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "select";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<SPGTransaction> trnx_list = new List<SPGTransaction>();

                while (Data_Reader.Read())
                {
                    SPGTransaction trnx = new SPGTransaction
                    {
                        Id = Convert.ToInt64(Data_Reader["Id"]),
                        RequestId = Data_Reader["RequestId"].ToString(),
                        RefTranNo = Data_Reader["RefTranNo"].ToString(),
                        RefTranDate = Data_Reader["RefTranDate"] != DBNull.Value ?
                                                    Convert.ToDateTime(Data_Reader["RefTranDate"]) : (DateTime?)null,
                        TranAmount = Data_Reader["TranAmount"].ToString(),
                        ContactName = Data_Reader["ContactName"].ToString(),
                        ContactNo = Data_Reader["ContactNo"].ToString(),
                        Address = Data_Reader["Address"].ToString(),
                        PayerId = Data_Reader["PayerId"].ToString(),
                        CreditAccounts = Data_Reader["CreditAccounts"].ToString(),
                        CrAmount = Data_Reader["CrAmount"].ToString(),
                        Purpose = Data_Reader["Purpose"].ToString(),
                        OnBehalf = Data_Reader["OnBehalf"].ToString(),
                        TranactionId = Data_Reader["TranactionId"].ToString(),
                        TranDateTime = Data_Reader["TranDateTime"] != DBNull.Value ?
                                                    Convert.ToDateTime(Data_Reader["TranDateTime"]) : (DateTime?)null,
                        PayAmount = Data_Reader["PayAmount"].ToString(),
                        PayMode = Data_Reader["PayMode"].ToString(),
                        OrgiBrCode = Data_Reader["OrgiBrCode"].ToString(),
                        StatusMsg = Data_Reader["StatusMsg"].ToString(),
                        TransactionStatus = Data_Reader["TransactionStatus"].ToString(),
                        IPAddressDetails = Data_Reader["IPAddressDetails"].ToString(),
                        ApiSessionKey = Data_Reader["ApiSessionKey"].ToString(),
                        ApiTokenKey = Data_Reader["ApiTokenKey"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                    Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ?
                                                    Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        HolderId = Data_Reader["HolderId"] != DBNull.Value ?
                                                    Convert.ToInt32(Data_Reader["HolderId"]) : (int?)null,
                        HolderUserName = Data_Reader["HolderUserName"].ToString(),
                        FinancialYear = Data_Reader["FinancialYear"].ToString(),
                        HolderName = Data_Reader["HolderName"].ToString(),

                        RankId = Data_Reader["RankId"] != DBNull.Value ?
                                                    Convert.ToInt32(Data_Reader["RankId"]) : (int?)null,
                        HolderNamecon = Data_Reader["HolderNamecon"].ToString(),
                        RankName = Data_Reader["RankName"].ToString(),




                    };
                    trnx.strRefTranDate = $"{trnx.RefTranDate:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.strTranDateTime = $"{trnx.TranDateTime:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.strLastUpdated = $"{trnx.LastUpdated:dd/MM/yyyy hh:mm:ss tt}";

                    trnx_list.Add(trnx);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return trnx_list;
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

        public SPGTransaction GetSPGTransactionById(long id)
        {
            try
            {
                Sql_Query = "[paygateway].[spSPGTransactionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "details";
                Sql_Command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                SPGTransaction trnx = new SPGTransaction();

                while (Data_Reader.Read())
                {
                    trnx.Id = Convert.ToInt64(Data_Reader["Id"]);
                    trnx.RequestId = Data_Reader["RequestId"].ToString();
                    trnx.RefTranNo = Data_Reader["RefTranNo"].ToString();
                    trnx.RefTranDate = Data_Reader["RefTranDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["RefTranDate"]) : (DateTime?)null;
                    trnx.TranAmount = Data_Reader["TranAmount"].ToString();
                    trnx.ContactName = Data_Reader["ContactName"].ToString();
                    trnx.ContactNo = Data_Reader["ContactNo"].ToString();
                    trnx.Address = Data_Reader["Address"].ToString();
                    trnx.PayerId = Data_Reader["PayerId"].ToString();
                    trnx.CreditAccounts = Data_Reader["CreditAccounts"].ToString();
                    trnx.CrAmount = Data_Reader["CrAmount"].ToString();
                    trnx.Purpose = Data_Reader["Purpose"].ToString();
                    trnx.OnBehalf = Data_Reader["OnBehalf"].ToString();
                    trnx.TranactionId = Data_Reader["TranactionId"].ToString();
                    trnx.TranDateTime = Data_Reader["TranDateTime"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["TranDateTime"]) : (DateTime?)null;
                    trnx.PayAmount = Data_Reader["PayAmount"].ToString();
                    trnx.PayMode = Data_Reader["PayMode"].ToString();
                    trnx.OrgiBrCode = Data_Reader["OrgiBrCode"].ToString();
                    trnx.StatusMsg = Data_Reader["StatusMsg"].ToString();
                    trnx.TransactionStatus = Data_Reader["TransactionStatus"].ToString();
                    trnx.IPAddressDetails = Data_Reader["IPAddressDetails"].ToString();
                    trnx.ApiSessionKey = Data_Reader["ApiSessionKey"].ToString();
                    trnx.ApiTokenKey = Data_Reader["ApiTokenKey"].ToString();
                    trnx.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    trnx.LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ?
                                                Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    trnx.HolderId = Data_Reader["HolderId"] != DBNull.Value ?
                                                 Convert.ToInt32(Data_Reader["HolderId"]) : (int?)null;
                    trnx.HolderUserName = Data_Reader["HolderUserName"].ToString();
                    trnx.strRefTranDate = $"{trnx.RefTranDate:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.strTranDateTime = $"{trnx.TranDateTime:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.strLastUpdated = $"{trnx.LastUpdated:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.FinancialYear = Data_Reader["FinancialYear"].ToString();
                    trnx.HolderName = Data_Reader["HolderName"].ToString();
                    trnx.RankId = Data_Reader["RankId"] != DBNull.Value ?
                                                    Convert.ToInt32(Data_Reader["RankId"]) : (int?)null;
                    trnx.HolderNamecon = Data_Reader["HolderNamecon"].ToString();
                    trnx.RankName = Data_Reader["RankName"].ToString();
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return trnx;
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


        public List<SPGTransaction> GetSPGTransactionByHolderId(int id)
        {
            try
            {
                Sql_Query = "[paygateway].[spSPGTransactionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "by_holder_id";
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<SPGTransaction> trnx_list = new List<SPGTransaction>();

                while (Data_Reader.Read())
                {
                    SPGTransaction trnx = new SPGTransaction();
                    trnx.Id = Convert.ToInt64(Data_Reader["Id"]);
                    trnx.RequestId = Data_Reader["RequestId"].ToString();
                    trnx.RefTranNo = Data_Reader["RefTranNo"].ToString();
                    trnx.RefTranDate = Data_Reader["RefTranDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["RefTranDate"]) : (DateTime?)null;
                    trnx.TranAmount = Data_Reader["TranAmount"].ToString();
                    trnx.ContactName = Data_Reader["ContactName"].ToString();
                    trnx.ContactNo = Data_Reader["ContactNo"].ToString();
                    trnx.Address = Data_Reader["Address"].ToString();
                    trnx.PayerId = Data_Reader["PayerId"].ToString();
                    trnx.CreditAccounts = Data_Reader["CreditAccounts"].ToString();
                    trnx.CrAmount = Data_Reader["CrAmount"].ToString();
                    trnx.Purpose = Data_Reader["Purpose"].ToString();
                    trnx.OnBehalf = Data_Reader["OnBehalf"].ToString();
                    trnx.TranactionId = Data_Reader["TranactionId"].ToString();
                    trnx.TranDateTime = Data_Reader["TranDateTime"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["TranDateTime"]) : (DateTime?)null;
                    trnx.PayAmount = Data_Reader["PayAmount"].ToString();
                    trnx.PayMode = Data_Reader["PayMode"].ToString();
                    trnx.OrgiBrCode = Data_Reader["OrgiBrCode"].ToString();
                    trnx.StatusMsg = Data_Reader["StatusMsg"].ToString();
                    trnx.TransactionStatus = Data_Reader["TransactionStatus"].ToString();
                    trnx.IPAddressDetails = Data_Reader["IPAddressDetails"].ToString();
                    trnx.ApiSessionKey = Data_Reader["ApiSessionKey"].ToString();
                    trnx.ApiTokenKey = Data_Reader["ApiTokenKey"].ToString();
                    trnx.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    trnx.LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ?
                                                Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    trnx.HolderId = Data_Reader["HolderId"] != DBNull.Value ?
                                                 Convert.ToInt32(Data_Reader["HolderId"]) : (int?)null;
                    trnx.HolderUserName = Data_Reader["HolderUserName"].ToString();
                    trnx.strRefTranDate = $"{trnx.RefTranDate:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.strTranDateTime = $"{trnx.TranDateTime:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.strLastUpdated = $"{trnx.LastUpdated:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.FinancialYear = Data_Reader["FinancialYear"].ToString();
                    trnx.HolderName = Data_Reader["HolderName"].ToString();
                    trnx.RankId = Data_Reader["RankId"] != DBNull.Value ?
                                                   Convert.ToInt32(Data_Reader["RankId"]) : (int?)null;
                    trnx.HolderNamecon = Data_Reader["HolderNamecon"].ToString();
                    trnx.RankName = Data_Reader["RankName"].ToString();

                    trnx_list.Add(trnx);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return trnx_list;
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

        public SPGTransaction GetSPGTransactionByTrnxDetails(SPGTransaction spg)
        {
            try
            {
                Sql_Query = "[paygateway].[spSPGTransactionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "tran_details";
                Sql_Command.Parameters.Add("@RefTranNo", SqlDbType.NVarChar).Value = spg.RefTranNo;
               // Sql_Command.Parameters.Add("@RefTranDate", SqlDbType.DateTime).Value = spg.RefTranDate;
                Sql_Command.Parameters.Add("@TranAmount", SqlDbType.NVarChar).Value = spg.TranAmount;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                SPGTransaction trnx = new SPGTransaction();

                while (Data_Reader.Read())
                {
                    trnx.Id = Convert.ToInt64(Data_Reader["Id"]);
                    trnx.RequestId = Data_Reader["RequestId"].ToString();
                    trnx.RefTranNo = Data_Reader["RefTranNo"].ToString();
                    trnx.RefTranDate = Data_Reader["RefTranDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["RefTranDate"]) : (DateTime?)null;
                    trnx.TranAmount = Data_Reader["TranAmount"].ToString();
                    trnx.ContactName = Data_Reader["ContactName"].ToString();
                    trnx.ContactNo = Data_Reader["ContactNo"].ToString();
                    trnx.Address = Data_Reader["Address"].ToString();
                    trnx.PayerId = Data_Reader["PayerId"].ToString();
                    trnx.CreditAccounts = Data_Reader["CreditAccounts"].ToString();
                    trnx.CrAmount = Data_Reader["CrAmount"].ToString();
                    trnx.Purpose = Data_Reader["Purpose"].ToString();
                    trnx.OnBehalf = Data_Reader["OnBehalf"].ToString();
                    trnx.TranactionId = Data_Reader["TranactionId"].ToString();
                    trnx.TranDateTime = Data_Reader["TranDateTime"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["TranDateTime"]) : (DateTime?)null;
                    trnx.PayAmount = Data_Reader["PayAmount"].ToString();
                    trnx.PayMode = Data_Reader["PayMode"].ToString();
                    trnx.OrgiBrCode = Data_Reader["OrgiBrCode"].ToString();
                    trnx.StatusMsg = Data_Reader["StatusMsg"].ToString();
                    trnx.TransactionStatus = Data_Reader["TransactionStatus"].ToString();
                    trnx.IPAddressDetails = Data_Reader["IPAddressDetails"].ToString();
                    trnx.ApiSessionKey = Data_Reader["ApiSessionKey"].ToString();
                    trnx.ApiTokenKey = Data_Reader["ApiTokenKey"].ToString();
                    trnx.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    trnx.LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ?
                                                Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    trnx.HolderId = Data_Reader["HolderId"] != DBNull.Value ?
                                                 Convert.ToInt32(Data_Reader["HolderId"]) : (int?)null;
                    trnx.HolderUserName = Data_Reader["HolderUserName"].ToString();
                    trnx.strRefTranDate = $"{trnx.RefTranDate:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.strTranDateTime = $"{trnx.TranDateTime:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.strLastUpdated = $"{trnx.LastUpdated:dd/MM/yyyy hh:mm:ss tt}";
                    trnx.FinancialYear = Data_Reader["FinancialYear"].ToString();
                    trnx.HolderName = Data_Reader["HolderName"].ToString();
                    trnx.RankId = Data_Reader["RankId"] != DBNull.Value ?
                                                   Convert.ToInt32(Data_Reader["RankId"]) : (int?)null;
                    trnx.HolderNamecon = Data_Reader["HolderNamecon"].ToString();
                    trnx.RankName = Data_Reader["RankName"].ToString();
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return trnx;
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

        public int SPGTransactionInsert(SPGTransaction trnx)
        {
            try
            {
                Sql_Query = "[paygateway].[spSPGTransactionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "insert";
                Sql_Command.Parameters.Add("@Id", SqlDbType.BigInt).Value = trnx.Id;
                Sql_Command.Parameters.Add("@RequestId", SqlDbType.NVarChar).Value = trnx.RequestId;
                Sql_Command.Parameters.Add("@RefTranNo", SqlDbType.NVarChar).Value = trnx.RefTranNo;
                Sql_Command.Parameters.Add("@RefTranDate", SqlDbType.DateTime).Value = trnx.RefTranDate;
                Sql_Command.Parameters.Add("@TranAmount", SqlDbType.NVarChar).Value = trnx.TranAmount;
                Sql_Command.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = trnx.ContactName;
                Sql_Command.Parameters.Add("@ContactNo", SqlDbType.NVarChar).Value = trnx.ContactNo;
                Sql_Command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = trnx.Address;
                Sql_Command.Parameters.Add("@PayerId", SqlDbType.NVarChar).Value = trnx.PayerId;
                Sql_Command.Parameters.Add("@CreditAccounts", SqlDbType.NVarChar).Value = trnx.CreditAccounts;
                Sql_Command.Parameters.Add("@CrAmount", SqlDbType.NVarChar).Value = trnx.CrAmount;
                Sql_Command.Parameters.Add("@Purpose", SqlDbType.NVarChar).Value = trnx.Purpose;
                Sql_Command.Parameters.Add("@OnBehalf", SqlDbType.NVarChar).Value = trnx.OnBehalf;
                Sql_Command.Parameters.Add("@TranactionId", SqlDbType.NVarChar).Value = trnx.TranactionId;
                Sql_Command.Parameters.Add("@TranDateTime", SqlDbType.DateTime).Value = trnx.TranDateTime;
                Sql_Command.Parameters.Add("@PayAmount", SqlDbType.NVarChar).Value = trnx.PayAmount;
                Sql_Command.Parameters.Add("@PayMode", SqlDbType.NVarChar).Value = trnx.PayMode;
                Sql_Command.Parameters.Add("@OrgiBrCode", SqlDbType.NVarChar).Value = trnx.OrgiBrCode;
                Sql_Command.Parameters.Add("@StatusMsg", SqlDbType.NVarChar).Value = trnx.StatusMsg;
                Sql_Command.Parameters.Add("@TransactionStatus", SqlDbType.NVarChar).Value = trnx.TransactionStatus;
                Sql_Command.Parameters.Add("@IPAddressDetails", SqlDbType.NVarChar).Value = trnx.IPAddressDetails;
                Sql_Command.Parameters.Add("@ApiSessionKey", SqlDbType.NVarChar).Value = trnx.ApiSessionKey;
                Sql_Command.Parameters.Add("@ApiTokenKey", SqlDbType.VarChar).Value = trnx.ApiTokenKey;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = trnx.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = trnx.LastUpdatedBy;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = trnx.HolderId;
                Sql_Command.Parameters.Add("@HolderUserName", SqlDbType.NVarChar).Value = trnx.HolderUserName;

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

        public int SPGTransactionUpdate(SPGTransaction trnx)
        {
            try
            {
                Sql_Query = "[paygateway].[spSPGTransactionMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "update";
                Sql_Command.Parameters.Add("@Id", SqlDbType.BigInt).Value = trnx.Id;
                Sql_Command.Parameters.Add("@RequestId", SqlDbType.NVarChar).Value = trnx.RequestId;
                Sql_Command.Parameters.Add("@RefTranNo", SqlDbType.NVarChar).Value = trnx.RefTranNo;
                Sql_Command.Parameters.Add("@RefTranDate", SqlDbType.DateTime).Value = trnx.RefTranDate;
                Sql_Command.Parameters.Add("@TranAmount", SqlDbType.NVarChar).Value = trnx.TranAmount;
                Sql_Command.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = trnx.ContactName;
                Sql_Command.Parameters.Add("@ContactNo", SqlDbType.NVarChar).Value = trnx.ContactNo;
                Sql_Command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = trnx.Address;
                Sql_Command.Parameters.Add("@PayerId", SqlDbType.NVarChar).Value = trnx.PayerId;
                Sql_Command.Parameters.Add("@CreditAccounts", SqlDbType.NVarChar).Value = trnx.CreditAccounts;
                Sql_Command.Parameters.Add("@CrAmount", SqlDbType.NVarChar).Value = trnx.CrAmount;
                Sql_Command.Parameters.Add("@Purpose", SqlDbType.NVarChar).Value = trnx.Purpose;
                Sql_Command.Parameters.Add("@OnBehalf", SqlDbType.NVarChar).Value = trnx.OnBehalf;
                Sql_Command.Parameters.Add("@TranactionId", SqlDbType.NVarChar).Value = trnx.TranactionId;
                Sql_Command.Parameters.Add("@TranDateTime", SqlDbType.DateTime).Value = trnx.TranDateTime;
                Sql_Command.Parameters.Add("@PayAmount", SqlDbType.NVarChar).Value = trnx.PayAmount;
                Sql_Command.Parameters.Add("@PayMode", SqlDbType.NVarChar).Value = trnx.PayMode;
                Sql_Command.Parameters.Add("@OrgiBrCode", SqlDbType.NVarChar).Value = trnx.OrgiBrCode;
                Sql_Command.Parameters.Add("@StatusMsg", SqlDbType.NVarChar).Value = trnx.StatusMsg;
                Sql_Command.Parameters.Add("@TransactionStatus", SqlDbType.NVarChar).Value = trnx.TransactionStatus;
                Sql_Command.Parameters.Add("@IPAddressDetails", SqlDbType.NVarChar).Value = trnx.IPAddressDetails;
                Sql_Command.Parameters.Add("@ApiSessionKey", SqlDbType.NVarChar).Value = trnx.ApiSessionKey;
                Sql_Command.Parameters.Add("@ApiTokenKey", SqlDbType.VarChar).Value = trnx.ApiTokenKey;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = trnx.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = trnx.LastUpdatedBy;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = trnx.HolderId;
                Sql_Command.Parameters.Add("@HolderUserName", SqlDbType.NVarChar).Value = trnx.HolderUserName;

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
                Sql_Query = "SELECT [RefTranNo] FROM [paygateway].[tSPGTransaction] WHERE RefTranNo = @TransactionCode";
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
    }
}