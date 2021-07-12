using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.DBO
{
    public class ConstructionStatusGateway : DefaultGateway
    {
        public List<ConstructionStatus> GetAllConstructionStatus()
        {
            try
            {
                Sql_Query = "[dbo].[sptConstructionStatusMaster]";
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

                List<ConstructionStatus> ConstructionStatusList = new List<ConstructionStatus>();

                while (Data_Reader.Read())
                {
                    ConstructionStatus ConstructionStatus = new ConstructionStatus
                    {

                        ConsStatusId = Convert.ToInt32(Data_Reader["ConsStatusId"]),
                        ConsStatusName = Data_Reader["ConsStatusName"].ToString(),
                       
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

                    ConstructionStatusList.Add(ConstructionStatus);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return ConstructionStatusList;
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