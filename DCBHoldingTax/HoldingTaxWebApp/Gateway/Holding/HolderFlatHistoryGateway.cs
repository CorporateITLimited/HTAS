using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Holding;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Holding
{
    public class HolderFlatHistoryGateway : DefaultGateway
    {
        #region HolderFlatHistory
        public List<HolderFlatHistory> GetAllHolderFlatHistory()
        {
            try
            {
                Sql_Query = "[Holding].[spHolderFlatHistoryMaster]";
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

                List<HolderFlatHistory> vm = new List<HolderFlatHistory>();

                while (Data_Reader.Read())
                {
                    HolderFlatHistory model = new HolderFlatHistory
                    {
                       
                        AreaId = Data_Reader["AreaId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["AreaId"]) : (int?)null,
                        AreaName = Convert.ToString(Data_Reader["AreaName"]),
                        PlotId = Data_Reader["PlotId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["PlotId"]) : (int?)null,
                        PlotNo = Convert.ToString(Data_Reader["PlotNo"]),
                        FlatHistoryId = Convert.ToInt32(Data_Reader["FlatHistoryId"]),
                        NewFlatNo = Convert.ToString(Data_Reader["NewFlatNo"]),
                        OldFlatNo = Convert.ToString(Data_Reader["OldFlatNo"]),
                        NewHolderName = Convert.ToString(Data_Reader["NewHolderName"]),
                        OldHolderName = Convert.ToString(Data_Reader["OldHolderName"]),
                        TransactionByUserName = Convert.ToString(Data_Reader["TransactionByUserName"]),
                        NewHolderFlatId = Data_Reader["NewHolderFlatId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["NewHolderFlatId"]) : (int?)null,
                        OldHolderFlatId = Data_Reader["OldHolderFlatId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["OldHolderFlatId"]) : (int?)null,
                        NewHolderId = Data_Reader["NewHolderId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["NewHolderId"]) : (int?)null,
                        OldHolderId = Data_Reader["OldHolderId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["OldHolderId"]) : (int?)null,
                        ReferenceDate = Data_Reader["ReferenceDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["ReferenceDate"]) : (DateTime?)null,
                        TransactionDate = Data_Reader["TransactionDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["TransactionDate"]) : (DateTime?)null,
                        TransactionBy = Data_Reader["TransactionBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["TransactionBy"]) : (int?)null,
                        ReferenceNo = Convert.ToString(Data_Reader["ReferenceNo"]),

                        
                    };

                    vm.Add(model);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return vm;
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

        public HolderFlatHistory GetHolderFlatHistoryById(int id)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderFlatHistoryMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@FlatHistoryId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                HolderFlatHistory vm = new HolderFlatHistory();

                while (Data_Reader.Read())
                {
                    vm.AreaId = Data_Reader["AreaId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["AreaId"]) : (int?)null;
                    vm.AreaName = Convert.ToString(Data_Reader["AreaName"]);
                    vm.PlotId = Data_Reader["PlotId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["PlotId"]) : (int?)null;
                    vm.PlotNo = Convert.ToString(Data_Reader["PlotNo"]);
                    vm.FlatHistoryId = Convert.ToInt32(Data_Reader["FlatHistoryId"]);
                    vm.NewFlatNo = Convert.ToString(Data_Reader["NewFlatNo"]);
                    vm.OldFlatNo = Convert.ToString(Data_Reader["OldFlatNo"]);
                    vm.NewHolderName = Convert.ToString(Data_Reader["NewHolderName"]);
                    vm.OldHolderName = Convert.ToString(Data_Reader["OldHolderName"]);
                    vm.TransactionByUserName = Convert.ToString(Data_Reader["TransactionByUserName"]);
                    vm.NewHolderFlatId = Data_Reader["NewHolderFlatId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["NewHolderFlatId"]) : (int?)null;
                    vm.OldHolderFlatId = Data_Reader["OldHolderFlatId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["OldHolderFlatId"]) : (int?)null;
                    vm.NewHolderId = Data_Reader["NewHolderId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["NewHolderId"]) : (int?)null;
                    vm.OldHolderId = Data_Reader["OldHolderId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["OldHolderId"]) : (int?)null;
                    vm.ReferenceDate = Data_Reader["ReferenceDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["ReferenceDate"]) : (DateTime?)null;
                    vm.TransactionDate = Data_Reader["TransactionDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["TransactionDate"]) : (DateTime?)null;
                    vm.TransactionBy = Data_Reader["TransactionBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["TransactionBy"]) : (int?)null;
                    vm.ReferenceNo = Convert.ToString(Data_Reader["ReferenceNo"]);
                };

                vm.StringReferenceDate = $"{vm.ReferenceDate:dd/MM/yyyy}";
                //vm.StringLastUpdated = $"{vm.LastUpdated:dd/MM/yyyy HH:mm:ss tt}";

                Data_Reader.Close();
                Sql_Connection.Close();

                return vm;
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

        public int InsertHolderFlatHistory(HolderFlatHistory model)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderFlatHistoryMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;

                Sql_Command.Parameters.Add("@AreaId", SqlDbType.Int).Value = model.AreaId;
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = model.PlotId;
                Sql_Command.Parameters.Add("@OldHolderFlatId", SqlDbType.Int).Value = model.OldHolderFlatId;
                Sql_Command.Parameters.Add("@NewHolderFlatId", SqlDbType.Int).Value = model.NewHolderFlatId;
                Sql_Command.Parameters.Add("@OldHolderId", SqlDbType.Int).Value = model.OldHolderId;
                Sql_Command.Parameters.Add("@NewHolderId", SqlDbType.Int).Value = model.NewHolderId;
               
                Sql_Command.Parameters.Add("@ReferenceNo", SqlDbType.NVarChar).Value = model.ReferenceNo;
                Sql_Command.Parameters.Add("@TransactionBy", SqlDbType.Int).Value = model.TransactionBy;


                Sql_Command.Parameters.Add("@ReferenceDate", SqlDbType.DateTime).Value = model.ReferenceDate;
                Sql_Command.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = model.TransactionDate;




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

        public int UpdateHolderFlatHistory(HolderFlatHistory model)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderFlatHistoryMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@OldHolderFlatId", SqlDbType.Int).Value = model.OldHolderFlatId;
                Sql_Command.Parameters.Add("@AreaId", SqlDbType.Int).Value = model.AreaId;
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = model.PlotId;
                Sql_Command.Parameters.Add("@OldHolderFlatId", SqlDbType.Int).Value = model.OldHolderFlatId;
                Sql_Command.Parameters.Add("@NewHolderFlatId", SqlDbType.Int).Value = model.NewHolderFlatId;
                Sql_Command.Parameters.Add("@OldHolderId", SqlDbType.Int).Value = model.OldHolderId;
                Sql_Command.Parameters.Add("@NewHolderId", SqlDbType.Int).Value = model.NewHolderId;

                Sql_Command.Parameters.Add("@ReferenceNo", SqlDbType.NVarChar).Value = model.ReferenceNo;
                Sql_Command.Parameters.Add("@TransactionBy", SqlDbType.Int).Value = model.TransactionBy;


                Sql_Command.Parameters.Add("@ReferenceDate", SqlDbType.DateTime).Value = model.ReferenceDate;
                Sql_Command.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = model.TransactionDate;


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
        #endregion
    }
}