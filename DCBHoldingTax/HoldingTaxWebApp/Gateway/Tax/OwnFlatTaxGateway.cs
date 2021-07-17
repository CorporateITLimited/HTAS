﻿using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Tax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Tax
{
    public class OwnFlatTaxGateway : DefaultGateway
    {

        public List<OwnFlatTax> GetAllOwnFlatTax()
        {
            try
            {
                Sql_Query = "[Tax].[tOwnFlatTax]";
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

                List<OwnFlatTax> ownflattaxList = new List<OwnFlatTax>();

                while (Data_Reader.Read())
                {
                    OwnFlatTax ownflattax = new OwnFlatTax()
                    {




                  





                       OwnFlatTaxId  = Convert.ToInt32(Data_Reader[" OwnFlatTaxId "]),

                       HoldingTaxId  = Convert.ToInt32(Data_Reader[" HoldingTaxId"]),

                        House_FlatNo  = Data_Reader[" House_FlatNo"].ToString(),
                        FlatNo = Data_Reader["FlatNo"].ToString(),
                       Area = Data_Reader["Area"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["Area"]) : (Decimal?)null,
                        PerSFTax = Data_Reader["PerSFTax"] != DBNull.Value ?
                                                Convert.ToDecimal(Data_Reader["PerSFRent"]) : (Decimal?)null,



                      
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,

                       







                    };

                    ownflattaxList.Add(ownflattax);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return ownflattaxList;
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