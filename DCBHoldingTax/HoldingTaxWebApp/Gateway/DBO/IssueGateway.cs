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
    public class IssueGateway : DefaultGateway
    {
        #region Issue Portion
        //Get All Issue List
        public List<Issue> GetAllIssue()
        {
            try
            {
                Sql_Query = "[dbo].[spIssue]";
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

                List<Issue> vm = new List<Issue>();

                while (Data_Reader.Read())
                {
                    Issue model = new Issue
                    {


                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUserName = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (Int32?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (Int32?)null,


                        IssueId = Convert.ToInt32(Data_Reader["IssueId"]),
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        StatusTypeId = Convert.ToInt32(Data_Reader["StatusTypeId"]),
                        HolderName = Data_Reader["HolderName"].ToString(),
                        Remarks = Data_Reader["Remarks"].ToString(),
                        SolvedDate = Data_Reader["SolvedDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["SolvedDate"]) : (DateTime?)null,
                        StatusName = Data_Reader["StatusName"].ToString(),
                        Subject = Data_Reader["Subject"].ToString(),

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

        //Get Issue By Id
        public Issue GetIssueById(int id)
        {
            try
            {
                Sql_Query = "[dbo].[spIssue]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "details";
                Sql_Command.Parameters.Add("@IssueId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                Issue vm = new Issue();

                while (Data_Reader.Read())
                {
                    vm.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    vm.CreatedByUserName = Data_Reader["CreatedByUsername"].ToString();
                    vm.UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString();
                    vm.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    vm.IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    vm.IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    vm.CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (Int32?)null;
                    vm.LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (Int32?)null;

                    vm.IssueId = Convert.ToInt32(Data_Reader["IssueId"]);

                    vm.HolderId = Convert.ToInt32(Data_Reader["HolderId"]);
                    vm.StatusTypeId = Convert.ToInt32(Data_Reader["StatusTypeId"]);
                    vm.HolderName = Data_Reader["HolderName"].ToString();
                    vm.Remarks = Data_Reader["Remarks"].ToString();
                    vm.SolvedDate = Data_Reader["SolvedDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["SolvedDate"]) : (DateTime?)null;
                    vm.StatusName = Data_Reader["StatusName"].ToString();
                    vm.Subject = Data_Reader["Subject"].ToString();


                };

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

        //Create Issue  
        public int IssueInsert(Issue model)
        {
            try
            {
                Sql_Query = "[dbo].[spIssue]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;


                //Sql_Command.Parameters.Add("@IssueId", SqlDbType.Int).Value = model.IssueId;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = model.HolderId;
                Sql_Command.Parameters.Add("@StatusTypeId", SqlDbType.Int).Value = model.StatusTypeId;

                Sql_Command.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = model.Subject;
                Sql_Command.Parameters.Add("@SolvedDate", SqlDbType.DateTime).Value = model.SolvedDate;
                Sql_Command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = model.Remarks;
              
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = model.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = model.LastUpdatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = model.IsDeleted;


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

        //Update Issue  
        public int IssueUpdate(Issue model)
        {
            try
            {
                Sql_Query = "[dbo].[spIssue]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@IssueId", SqlDbType.Int).Value = model.IssueId;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = model.HolderId;
                Sql_Command.Parameters.Add("@StatusTypeId", SqlDbType.Int).Value = model.StatusTypeId;

                Sql_Command.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = model.Subject;
                Sql_Command.Parameters.Add("@SolvedDate", SqlDbType.DateTime).Value = model.SolvedDate;
                Sql_Command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = model.Remarks;

                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = model.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = model.LastUpdatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = model.IsDeleted;


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


        #region Issue Details Portion
        //Get All Issue Details by Issue Id
        public List<IssueDetails> GetAllIssueDetailsByIssueId(int Id)
        {
            try
            {
                Sql_Query = "[dbo].[spIssueDetails]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@IssueId", SqlDbType.Int).Value = Id;


                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<IssueDetails> vm = new List<IssueDetails>();

                while (Data_Reader.Read())
                {
                    IssueDetails model = new IssueDetails
                    {
                        IssueId = Convert.ToInt32(Data_Reader["IssueId"]),
                        IssueDetailsId = Convert.ToInt32(Data_Reader["IssueDetailsId"]),
                        MsgDetails = Data_Reader["MsgDetails"].ToString(),
                        Doc1 = Data_Reader["Doc1"].ToString(),
                        Doc2 = Data_Reader["Doc2"].ToString(),
                        MessageSenderName = Data_Reader["MessageSenderName"].ToString(),
                        MessageSender = Convert.ToInt32(Data_Reader["MessageSender"]),
                        MessageSenderType = Convert.ToInt32(Data_Reader["MessageSenderType"]),
                        MsgDate = Data_Reader["MsgDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["MsgDate"]) : (DateTime?)null,
                        IsRead = Data_Reader["IsRead"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsRead"]) : (bool?)null,
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

        //Create Issue  Details
        public int IssueDetailsInsert(IssueDetails model)
        {
            try
            {
                Sql_Query = "[dbo].[spIssueDetails]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;

                Sql_Command.Parameters.Add("@IssueId", SqlDbType.Int).Value = model.IssueId;
                Sql_Command.Parameters.Add("@MsgDate", SqlDbType.DateTime).Value = model.MsgDate;

                Sql_Command.Parameters.Add("@MsgDetails", SqlDbType.NVarChar).Value = model.MsgDetails;
                Sql_Command.Parameters.Add("@Doc1", SqlDbType.DateTime).Value = model.Doc1;
                Sql_Command.Parameters.Add("@Doc2", SqlDbType.NVarChar).Value = model.Doc2;
                Sql_Command.Parameters.Add("@IsRead", SqlDbType.NVarChar).Value = model.IsRead;
                Sql_Command.Parameters.Add("@MessageSender", SqlDbType.Int).Value = model.MessageSender;
                //Sql_Command.Parameters.Add("@MessageSenderName", SqlDbType.NVarChar).Value = model.MessageSenderName;



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

        //Update Issue Details
        public int IssueDetailsUpdate(IssueDetails model)
        {
            try
            {
                Sql_Query = "[dbo].[spIssueDetails]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@IssueDetailsId", SqlDbType.Int).Value = model.IssueDetailsId;
                Sql_Command.Parameters.Add("@IssueId", SqlDbType.Int).Value = model.IssueId;
                Sql_Command.Parameters.Add("@MsgDate", SqlDbType.DateTime).Value = model.MsgDate;

                Sql_Command.Parameters.Add("@MsgDetails", SqlDbType.NVarChar).Value = model.MsgDetails;
                Sql_Command.Parameters.Add("@Doc1", SqlDbType.DateTime).Value = model.Doc1;
                Sql_Command.Parameters.Add("@Doc2", SqlDbType.NVarChar).Value = model.Doc2;
                Sql_Command.Parameters.Add("@IsRead", SqlDbType.NVarChar).Value = model.IsRead;
                Sql_Command.Parameters.Add("@MessageSender", SqlDbType.Int).Value = model.MessageSender;
                Sql_Command.Parameters.Add("@MessageSenderType", SqlDbType.Int).Value = model.MessageSenderType;

                //Sql_Command.Parameters.Add("@MessageSenderName", SqlDbType.NVarChar).Value = model.MessageSenderName;


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

        //Update Issue Details
        public int IssueDetailsDelete(int Id)
        {
            try
            {
                Sql_Query = "[dbo].[spIssueDetails]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "delete";
                Sql_Command.Parameters.Add("@IssueId", SqlDbType.Int).Value = Id;

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