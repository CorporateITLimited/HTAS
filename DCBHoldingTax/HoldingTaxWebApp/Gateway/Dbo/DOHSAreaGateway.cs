using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HoldingTaxWebApp.Models.Dbo;
using HoldingTaxWebApp.Models;
using System.Data.SqlClient;
using System.Data;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.DBO;

namespace HoldingTaxWebApp.Gateway.DBO
{
    public class DOHSAreaGateway : DefaultGateway
    {
        public List<DOHSArea> GetAllDOHSArea()
        {
            try
            {
                Sql_Query = "[dbo].[spDOHSArea]";
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

                List<DOHSArea> dohsareaList = new List<DOHSArea>();

                while (Data_Reader.Read())
                {
                    DOHSArea dohsarea = new DOHSArea()
                    {
                        AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
                        AreaName = Data_Reader["AreaName"].ToString(),
                        TotalArea = Data_Reader["TotalArea"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalArea"]) : (Decimal?)null,
                        CurrentPlotNumber = Convert.ToInt32(Data_Reader["CurrentPlotNumber"]),
                        CurrentFlatNumber = Convert.ToInt32(Data_Reader["CurrentFlatNumber"]),
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedByUsername = Convert.ToString(Data_Reader["CreatedByUsername"]),
                        UpdatedByUsername = Convert.ToString(Data_Reader["UpdatedByUsername"])
                    };

                    dohsarea.StrCreateDate = $"{dohsarea.CreateDate:dd/MM/yyyy HH:mm:ss tt}";
                    dohsarea.StrLastUpdated = $"{dohsarea.LastUpdated:dd/MM/yyyy HH:mm:ss tt}";

                    dohsareaList.Add(dohsarea);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return dohsareaList;
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

        public DOHSArea GetDOHSAreaId(int id)
        {
            try
            {
                Sql_Query = "[dbo].[spDOHSArea]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@AreaId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                DOHSArea dohsarea = new DOHSArea();

                while (Data_Reader.Read())
                {
                    dohsarea.AreaId = Convert.ToInt32(Data_Reader["AreaId"]);
                    dohsarea.AreaName = Data_Reader["AreaName"].ToString();
                    dohsarea.CurrentPlotNumber = Convert.ToInt32(Data_Reader["CurrentPlotNumber"]);
                    dohsarea.CurrentFlatNumber = Convert.ToInt32(Data_Reader["CurrentFlatNumber"]);
                    dohsarea.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    dohsarea.CreatedBy = Data_Reader["CreatedBy"] !=
                                            DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    dohsarea.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    dohsarea.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                            DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    dohsarea.IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    dohsarea.IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    dohsarea.TotalArea = Data_Reader["TotalArea"] != DBNull.Value ?
                                               Convert.ToDecimal(Data_Reader["TotalArea"]) : (decimal?)null;
                    dohsarea.CreatedByUsername = Convert.ToString(Data_Reader["CreatedByUsername"]);
                    dohsarea.UpdatedByUsername = Convert.ToString(Data_Reader["UpdatedByUsername"]);
                };

                dohsarea.StrCreateDate = $"{dohsarea.CreateDate:dd/MM/yyyy HH:mm:ss tt}";
                dohsarea.StrLastUpdated = $"{dohsarea.LastUpdated:dd/MM/yyyy HH:mm:ss tt}";

                Data_Reader.Close();
                Sql_Connection.Close();

                return dohsarea;
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

        public int DOHSAreaInsert(DOHSArea DOHS)
        {
            try
            {
                Sql_Query = "[dbo].[spDOHSArea]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;

                Sql_Command.Parameters.Add("@AreaId", SqlDbType.Int).Value = DOHS.AreaId;
                Sql_Command.Parameters.Add("@AreaName", SqlDbType.NVarChar).Value = DOHS.AreaName;
                Sql_Command.Parameters.Add("@TotalArea", SqlDbType.Decimal).Value = DOHS.TotalArea;
                Sql_Command.Parameters.Add("@CurrentPlotNumber", SqlDbType.Int).Value = DOHS.CurrentPlotNumber;
                Sql_Command.Parameters.Add("@CurrentFlatNumber", SqlDbType.Int).Value = DOHS.CurrentFlatNumber;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = DOHS.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = DOHS.CreatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = DOHS.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = DOHS.IsDeleted;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = DOHS.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = DOHS.LastUpdatedBy;


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

        public int DOHSAreaUpdate(DOHSArea DOHS)
        {
            try
            {
                Sql_Query = "[dbo].[spDOHSArea]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@AreaId", SqlDbType.Int).Value = DOHS.AreaId;
                Sql_Command.Parameters.Add("@AreaName", SqlDbType.NVarChar).Value = DOHS.AreaName;
                Sql_Command.Parameters.Add("@TotalArea", SqlDbType.Decimal).Value = DOHS.TotalArea;
                Sql_Command.Parameters.Add("@CurrentPlotNumber", SqlDbType.Int).Value = DOHS.CurrentPlotNumber;
                Sql_Command.Parameters.Add("@CurrentFlatNumber", SqlDbType.Int).Value = DOHS.CurrentFlatNumber;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = DOHS.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = DOHS.CreatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = DOHS.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = DOHS.IsDeleted;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = DOHS.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = DOHS.LastUpdatedBy;

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