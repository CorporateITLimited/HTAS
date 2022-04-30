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
    public class OtpHistoryGateway : DefaultGateway
    {
        //get Otp details by Otp
        public OtpHistory GetOtpHistoryById(int otp)
        {
            try
            {
                Sql_Query = "[user].[spOtpHistoryMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "detailsbyOtp";
                Sql_Command.Parameters.Add("@Otp", SqlDbType.Int).Value = otp;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                OtpHistory OtpHistoryVM = new OtpHistory();

                while (Data_Reader.Read())
                {
                    OtpHistoryVM.CreateDate = Data_Reader["CreateDate"] !=
                                           DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    OtpHistoryVM.HistoryId = Convert.ToInt32(Data_Reader["HistoryId"]);

                    OtpHistoryVM.LogInCredentialId = Data_Reader["LogInCredentialId"] !=
                                           DBNull.Value ? Convert.ToInt32(Data_Reader["LogInCredentialId"]) : (Int32?)null;
                    OtpHistoryVM.UserName = Data_Reader["UserName"].ToString();

                    OtpHistoryVM.Otp = Convert.ToInt32(Data_Reader["Otp"]);
                    OtpHistoryVM.Purpose = Data_Reader["Purpose"].ToString();
                    OtpHistoryVM.responseString = Data_Reader["responseString"].ToString();

                };

                Data_Reader.Close();
                Sql_Connection.Close();

                return OtpHistoryVM;
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

        //Create OtpHistory 
        public int OtpHistoryInsert(OtpHistory OtpHistory)
        {
            try
            {
                Sql_Query = "[user].[spOtpHistoryMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;
                Sql_Command.Parameters.Add("@LogInCredentialId", SqlDbType.Int).Value = OtpHistory.LogInCredentialId;
                Sql_Command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = OtpHistory.UserName;
                Sql_Command.Parameters.Add("@Purpose", SqlDbType.NVarChar).Value = OtpHistory.Purpose;
                Sql_Command.Parameters.Add("@responseString", SqlDbType.NVarChar).Value = OtpHistory.responseString;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = OtpHistory.CreateDate;
                Sql_Command.Parameters.Add("@Otp", SqlDbType.Int).Value = OtpHistory.Otp;


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

        //update Otp History
        public int OtpHistoryUpdate(OtpHistory OtpHistory)
        {
            try
            {
                Sql_Query = "[user].[spOtpHistoryMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@HistoryId", SqlDbType.Int).Value = OtpHistory.HistoryId;
                Sql_Command.Parameters.Add("@LogInCredentialId", SqlDbType.Int).Value = OtpHistory.LogInCredentialId;
                Sql_Command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = OtpHistory.UserName;
                Sql_Command.Parameters.Add("@Purpose", SqlDbType.NVarChar).Value = OtpHistory.Purpose;
                Sql_Command.Parameters.Add("@responseString", SqlDbType.NVarChar).Value = OtpHistory.responseString;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = OtpHistory.CreateDate;
                Sql_Command.Parameters.Add("@Otp", SqlDbType.Int).Value = OtpHistory.Otp;


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