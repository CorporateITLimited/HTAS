using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HoldingTaxWebApp.Models.DBO;
using HoldingTaxWebApp.Models;
using System.Data.SqlClient;
using System.Data;
using HoldingTaxWebApp.Helpers;

namespace HoldingTaxWebApp.Gateway.DBO
{
    public class OfficialStatusGateway : DefaultGateway
    {
        public List<OfficialStatus> GetAllOfficialStatus()
        {
            try
            {
                Sql_Query = "[dbo].[spOfficialStatusMaster]";
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

                List<OfficialStatus> officialStatusList = new List<OfficialStatus>();

                while (Data_Reader.Read())
                {
                    OfficialStatus officialStatus = new OfficialStatus
                    {

                        OfficialStatusId = Convert.ToInt32(Data_Reader["OfficialStatusId"]),
                        Remarks = Data_Reader["Remarks"].ToString(),
                        OffStatusName = Data_Reader["OffStatusName"].ToString(),
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
                                                    DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null
                    };

                    officialStatusList.Add(officialStatus);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return officialStatusList;
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