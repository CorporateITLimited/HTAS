using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HoldingTaxWebApp.Models.Plots;
using HoldingTaxWebApp.Models;
using System.Data.SqlClient;
using System.Data;
using HoldingTaxWebApp.Helpers;


namespace HoldingTaxWebApp.Gateway.Plots
{
    public class PlotGateway : DefaultGateway
    {

        public List<Plot> GetAllPlot()
        {
            try
            {
                Sql_Query = "[Plot].[spPlot]";
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

                List<Plot> PlotList = new List<Plot>();

                while (Data_Reader.Read())
                {
                    Plot plot = new Plot
                    {

                        PlotId = Convert.ToInt32(Data_Reader["PlotId"]),
                        AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
                        TotalArea = Data_Reader["TotalArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["TotalArea"]) : (Decimal?)null,
                        RoadNo = Data_Reader["RoadNo"].ToString(),
                        PlotNo = Data_Reader["PlotNo"].ToString(),
                        PlotIdNumber = Data_Reader["PlotIdNumber"].ToString(),

                        //CreatedByName = Data_Reader["CreatedByName"].ToString(),
                        //LastUpdatedByName = Data_Reader["LastUpdatedByName"].ToString(),
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
                                                    DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        RoadName = Data_Reader["RoadName"].ToString(),
                    };

                    PlotList.Add(plot);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return PlotList;
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

        public Plot GetPlotById(int id)
        {
            try
            {
                Sql_Query = "[Plot].[spPlot]";
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

                Plot plot = new Plot();

                while (Data_Reader.Read())
                {
                    plot.PlotId = Convert.ToInt32(Data_Reader["PlotId"]);
                    plot.AreaId = Convert.ToInt32(Data_Reader["AreaId"]);
                    plot.TotalArea = Data_Reader["TotalArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["TotalArea"]) : (Decimal?)null;
                    plot.RoadNo = Data_Reader["RoadNo"].ToString();
                    plot.PlotNo = Data_Reader["PlotNo"].ToString();
                    plot.PlotIdNumber = Data_Reader["PlotIdNumber"].ToString();

                    //CreatedByName = Data_Reader["CreatedByName"].ToString(),
                    //LastUpdatedByName = Data_Reader["LastUpdatedByName"].ToString(),
                    plot.CreatedBy = Data_Reader["CreatedBy"] !=
                                                 DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    plot.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    plot.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                 DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    plot.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                 Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    plot.IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    plot.IsDeleted = Data_Reader["IsDeleted"] !=
                                                 DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    plot.RoadName = Data_Reader["RoadName"].ToString();

                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return plot;
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