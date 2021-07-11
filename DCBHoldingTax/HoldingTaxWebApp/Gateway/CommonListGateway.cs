using HoldingTaxWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway
{
    public class CommonListGateway : DefaultGateway
    {
        public List<CommonList> GetAllGender()
        {

            try
            {
                Sql_Query = "[dbo].[spGetAllCommonTypeList]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "gender";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<CommonList> RentTaxRateList = new List<CommonList>();

                while (Data_Reader.Read())
                {
                    CommonList rentTaxRate = new CommonList
                    {
                        TypeId = Convert.ToInt32(Data_Reader["TypeId"]),
                        TypeName = Data_Reader["TypeName"].ToString()
                    };

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

        public List<CommonList> GetAllMaritalStatus()
        {

            try
            {
                Sql_Query = "[dbo].[spGetAllCommonTypeList]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "maritalstatus";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<CommonList> RentTaxRateList = new List<CommonList>();

                while (Data_Reader.Read())
                {
                    CommonList rentTaxRate = new CommonList
                    {
                        TypeId = Convert.ToInt32(Data_Reader["TypeId"]),
                        TypeName = Data_Reader["TypeName"].ToString()
                    };

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

        public List<CommonList> GetAllOwnerShipType()
        {

            try
            {
                Sql_Query = "[dbo].[spGetAllCommonTypeList]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "ownershiptype";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<CommonList> RentTaxRateList = new List<CommonList>();

                while (Data_Reader.Read())
                {
                    CommonList rentTaxRate = new CommonList
                    {
                        TypeId = Convert.ToInt32(Data_Reader["TypeId"]),
                        TypeName = Data_Reader["TypeName"].ToString()
                    };

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

        public List<CommonList> GetAllFloor()
        {

            try
            {
                Sql_Query = "[dbo].[spGetAllCommonTypeList]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "floor";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<CommonList> RentTaxRateList = new List<CommonList>();

                while (Data_Reader.Read())
                {
                    CommonList rentTaxRate = new CommonList
                    {
                        TypeId = Convert.ToInt32(Data_Reader["TypeId"]),
                        TypeName = Data_Reader["TypeName"].ToString()
                    };

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

        public List<CommonList> GetAllOwnOrRent()
        {

            try
            {
                Sql_Query = "[dbo].[spGetAllCommonTypeList]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "ownorrent";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<CommonList> RentTaxRateList = new List<CommonList>();

                while (Data_Reader.Read())
                {
                    CommonList rentTaxRate = new CommonList
                    {
                        TypeId = Convert.ToInt32(Data_Reader["TypeId"]),
                        TypeName = Data_Reader["TypeName"].ToString()
                    };

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

        public List<CommonList> GetAllOwnType()
        {
            try
            {
                Sql_Query = "[dbo].[spGetAllCommonTypeList]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "owntype";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<CommonList> RentTaxRateList = new List<CommonList>();

                while (Data_Reader.Read())
                {
                    CommonList rentTaxRate = new CommonList
                    {
                        TypeId = Convert.ToInt32(Data_Reader["TypeId"]),
                        TypeName = Data_Reader["TypeName"].ToString()
                    };

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
    }
}