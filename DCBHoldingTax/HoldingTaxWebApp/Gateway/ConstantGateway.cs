using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models;
using HoldingTaxWebApp.Models.Constant;

namespace HoldingTaxWebApp.Gateway
{
    public class ConstantGateway : DefaultGateway
    {
        public List<RentTaxRate> GetAllRentTaxRates()
        {

            try
            {
                Sql_Query = "[dbo].[spRentTaxRate]";
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

                List<RentTaxRate> RentTaxRateList = new List<RentTaxRate>();

                while (Data_Reader.Read())
                {
                    RentTaxRate rentTaxRate = new RentTaxRate
                    {
                        RentTaxRateId = Convert.ToInt32(Data_Reader["RentTaxRateId"]),
                        AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
                        BuildingTypeId = Convert.ToInt32(Data_Reader["BuildingTypeId"]),
                        PerSqfRent = Convert.ToDecimal(Data_Reader["PerSqfRent"]),
                        Remarks = Data_Reader["Remarks"].ToString(),
                        AreaName = Data_Reader["AreaName"].ToString(),
                        BuildingTypeName = Data_Reader["BuildingTypeName"].ToString(),
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

                    rentTaxRate.StrCreateDate = $"{rentTaxRate.CreateDate:dd/MM/yyyy HH:mm:ss tt}";
                    rentTaxRate.StrLastUpdated = $"{rentTaxRate.LastUpdated:dd/MM/yyyy HH:mm:ss tt}";

                    RentTaxRateList.Add(rentTaxRate);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return RentTaxRateList;
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

        public RentTaxRate GetAllRentTaxRatesById(int id)
        {
            try
            {
                Sql_Query = "[dbo].[spRentTaxRate]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@rentTaxRateId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                RentTaxRate model = new RentTaxRate();

                while (Data_Reader.Read())
                {
                    model.RentTaxRateId = Convert.ToInt32(Data_Reader["RentTaxRateId"]);
                    model.AreaId = Convert.ToInt32(Data_Reader["AreaId"]);
                    model.BuildingTypeId = Convert.ToInt32(Data_Reader["BuildingTypeId"]);
                    model.PerSqfRent = Convert.ToDecimal(Data_Reader["PerSqfRent"]);
                    model.Remarks = Data_Reader["Remarks"].ToString();
                    model.AreaName = Data_Reader["AreaName"].ToString();
                    model.BuildingTypeName = Data_Reader["BuildingTypeName"].ToString();
                    model.CreatedByUsername = Data_Reader["CreatedByUsername"].ToString();
                    model.UpdatedByUsername = Data_Reader["UpdatedByUsername"].ToString();
                    model.CreatedBy = Data_Reader["CreatedBy"] !=
                                               DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    model.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                               Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    model.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                               DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    model.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                               Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    model.IsActive = Data_Reader["IsActive"] !=
                                               DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    model.IsDeleted = Data_Reader["IsDeleted"] !=
                                               DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                }


                model.StrCreateDate = $"{model.CreateDate:dd/MM/yyyy HH:mm:ss tt}";
                model.StrLastUpdated = $"{model.LastUpdated:dd/MM/yyyy HH:mm:ss tt}";
                model.PerSqfRent = Convert.ToDecimal(string.Format("{0:0.00}", model.PerSqfRent));
                model.StrPerSqfRent = BanglaConvertionHelper.DecimalValueEnglish2Bangla(model.PerSqfRent);

                Data_Reader.Close();
                Sql_Connection.Close();

                return model;
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

        public int RentTaxRatesInsert(RentTaxRate rate)
        {
            try
            {
                Sql_Query = "[dbo].[spRentTaxRate]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;
                Sql_Command.Parameters.Add("@rentTaxRateId", SqlDbType.Int).Value = rate.RentTaxRateId;
                Sql_Command.Parameters.Add("@areaId", SqlDbType.Int).Value = rate.AreaId;
                Sql_Command.Parameters.Add("@buildingTypeId", SqlDbType.Int).Value = rate.BuildingTypeId;
                Sql_Command.Parameters.Add("@perSqfRent", SqlDbType.Decimal).Value = rate.PerSqfRent;
                Sql_Command.Parameters.Add("@remarks", SqlDbType.NVarChar).Value = rate.Remarks;
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

        public int RentTaxRatesUpdate(RentTaxRate rate)
        {
            try
            {
                Sql_Query = "[dbo].[spRentTaxRate]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();
                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@rentTaxRateId", SqlDbType.Int).Value = rate.RentTaxRateId;
                Sql_Command.Parameters.Add("@areaId", SqlDbType.Int).Value = rate.AreaId;
                Sql_Command.Parameters.Add("@buildingTypeId", SqlDbType.Int).Value = rate.BuildingTypeId;
                Sql_Command.Parameters.Add("@perSqfRent", SqlDbType.Decimal).Value = rate.PerSqfRent;
                Sql_Command.Parameters.Add("@remarks", SqlDbType.NVarChar).Value = rate.Remarks;
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