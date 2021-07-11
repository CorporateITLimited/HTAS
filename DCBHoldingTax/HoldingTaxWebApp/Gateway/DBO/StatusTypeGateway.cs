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
    public class StatusTypeGateway:DefaultGateway
    {
        //get all StatusTypes
        public List<StatusType> GetAllStatusType()
        {
            try
            {
                Sql_Query = "[dbo].[spStatusType]";
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

                List<StatusType> StatusTypeList = new List<StatusType>();

                while (Data_Reader.Read())
                {
                    StatusType statusType = new StatusType
                    {


                        StatusTypeId = Convert.ToInt32(Data_Reader["StatusTypeId"]),
                        StatusName = Data_Reader["StatusName"].ToString(),

                    };

                    StatusTypeList.Add(statusType);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return StatusTypeList;
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