using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Dbo
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
                    DOHSArea dohsarea = new Dohsarea
                    {

                        

                         AreaId  = Convert.ToInt32(Data_Reader["AreaId"]),
                         AreaName = Data_Reader["AreaName "].ToString(),
                        MedicalAmount = Convert.ToInt32(Data_Reader["MedicalAmount"]),
                        TotalArea = Data_Reader["TotalArea"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["TotalArea"]) : (Decimal?)null,
                       CurrentPlotNumber  = Convert.ToInt32(Data_Reader["CurrentPlotNumber"]),
                       CurrentFlatNumber  = Convert.ToInt32(Data_Reader["CurrentFlatNumber"]),
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,

                    };

                    dohsareaList.Add(dohsarea);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return constantvalueList;
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