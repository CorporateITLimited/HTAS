using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models;
using HoldingTaxWebApp.Models.Constant;

namespace HoldingTaxWebApp.Gateway.Constant
{
    public class OwnTaxRateGateway : DefaultGateway
    {
        public List<OwnTaxRate> GetList()
        {
            try
            {
                Sql_Query = "[constant].[spOwnTaxRate]";
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

                List<OwnTaxRate> vmList = new List<OwnTaxRate>();

                while (Data_Reader.Read())
                {
                    OwnTaxRate model = new OwnTaxRate
                    {
                        Amount = Convert.ToDecimal(Data_Reader["Amount"]),
                        AreaSF = Convert.ToDecimal(Data_Reader["AreaSF"]),
                        Mill_Civil = Convert.ToInt32(Data_Reader["Mill_Civil"]),
                        OwnTaxRateId = Convert.ToInt32(Data_Reader["OwnTaxRateId"]),
                        PerSqfTax = Data_Reader["PerSqfTax"] !=
                                                    DBNull.Value ? Convert.ToDecimal(Data_Reader["PerSqfTax"]) : (decimal?)null,
                        TypeName = Data_Reader["TypeName"].ToString(),
                        Remarks = Data_Reader["Remarks"].ToString(),
                        CreatedByUsername = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUsername = Data_Reader["UpdatedByUsername"].ToString(),
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                    DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                    Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                    DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                    Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] !=
                                                    DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] !=
                                                    DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null
                    };
                    model.StrAmount = BanglaConvertionHelper.DecimalValueEnglish2Bangla(model.Amount);
                    model.StrAreaSF = BanglaConvertionHelper.DecimalValueEnglish2Bangla(model.AreaSF);
                    model.StrPerSqfRent = BanglaConvertionHelper.DecimalValueEnglish2Bangla(model.PerSqfTax);
                    model.StrCreateDate = $"{model.CreateDate:dd/MM/yyyy HH:mm:ss tt}";
                    model.StrLastUpdated = $"{model.LastUpdated:dd/MM/yyyy HH:mm:ss tt}";

                    vmList.Add(model);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return vmList;
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

        public OwnTaxRate GetById(int id)
        {
            try
            {
                Sql_Query = "[constant].[spOwnTaxRate]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@OwnTaxRateId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                OwnTaxRate vmList = new OwnTaxRate();

                while (Data_Reader.Read())
                {
                    vmList.Amount = Convert.ToDecimal(Data_Reader["Amount"]);
                    vmList.AreaSF = Convert.ToDecimal(Data_Reader["AreaSF"]);
                    vmList.Mill_Civil = Convert.ToInt32(Data_Reader["Mill_Civil"]);
                    vmList.OwnTaxRateId = Convert.ToInt32(Data_Reader["OwnTaxRateId"]);
                    vmList.PerSqfTax = Data_Reader["PerSqfTax"] !=
                                                DBNull.Value ? Convert.ToDecimal(Data_Reader["PerSqfTax"]) : (decimal?)null;
                    vmList.TypeName = Data_Reader["TypeName"].ToString();
                    vmList.Remarks = Data_Reader["Remarks"].ToString();
                    vmList.CreatedByUsername = Data_Reader["CreatedByUsername"].ToString();
                    vmList.UpdatedByUsername = Data_Reader["UpdatedByUsername"].ToString();
                    vmList.CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    vmList.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    vmList.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    vmList.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    vmList.IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    vmList.IsDeleted = Data_Reader["IsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    vmList.StrAmount = BanglaConvertionHelper.DecimalValueEnglish2Bangla(vmList.Amount);
                    vmList.StrAreaSF = BanglaConvertionHelper.DecimalValueEnglish2Bangla(vmList.AreaSF);
                    vmList.StrPerSqfRent = BanglaConvertionHelper.DecimalValueEnglish2Bangla(vmList.PerSqfTax);
                    vmList.StrCreateDate = $"{vmList.CreateDate:dd/MM/yyyy HH:mm:ss tt}";
                    vmList.StrLastUpdated = $"{vmList.LastUpdated:dd/MM/yyyy HH:mm:ss tt}";
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return vmList;
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


        public int Insert(OwnTaxRate rate)
        {
            try
            {
                Sql_Query = "[constant].[spOwnTaxRate]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;
                Sql_Command.Parameters.Add("@OwnTaxRateId", SqlDbType.Int).Value = rate.OwnTaxRateId;
                Sql_Command.Parameters.Add("@Mill_Civil", SqlDbType.Int).Value = rate.Mill_Civil;
                Sql_Command.Parameters.Add("@AreaSF", SqlDbType.Decimal).Value = rate.AreaSF;
                Sql_Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = rate.Amount;
                Sql_Command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = rate.Remarks;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = rate.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = rate.IsDeleted;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = rate.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = rate.LastUpdated;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = rate.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = rate.CreateDate;

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

        public int Update(OwnTaxRate rate)
        {
            try
            {
                Sql_Query = "[constant].[spOwnTaxRate]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@OwnTaxRateId", SqlDbType.Int).Value = rate.OwnTaxRateId;
                Sql_Command.Parameters.Add("@Mill_Civil", SqlDbType.Int).Value = rate.Mill_Civil;
                Sql_Command.Parameters.Add("@AreaSF", SqlDbType.Decimal).Value = rate.AreaSF;
                Sql_Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = rate.Amount;
                Sql_Command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = rate.Remarks;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = rate.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = rate.IsDeleted;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = rate.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = rate.LastUpdated;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = rate.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = rate.CreateDate;

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