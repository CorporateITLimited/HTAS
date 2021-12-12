using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Users
{
    public class ClusterGateway : DefaultGateway
    {
        public List<Cluster> GetAllCluster()
        {
            try
            {
                Sql_Query = "[user].[spCluster]";
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

                List<Cluster> clusterList = new List<Cluster>();

                while (Data_Reader.Read())
                {
                    Cluster roleVM = new Cluster
                    {
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        ClusterDetails = Data_Reader["ClusterDetails"].ToString(),
                        ClusterId = Convert.ToInt32(Data_Reader["ClusterId"]),
                        ClusterName = Data_Reader["ClusterName"].ToString(),
                        UserFullName = Data_Reader["UserFullName"].ToString(),
                        UserId = Data_Reader["UserId"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["UserId"]) : (int?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null
                    };

                    clusterList.Add(roleVM);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return clusterList;
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

        public List<Cluster> GetAllActiveCluster()
        {
            try
            {
                Sql_Query = "[user].[spCluster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "select_active";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<Cluster> clusterList = new List<Cluster>();

                while (Data_Reader.Read())
                {
                    Cluster roleVM = new Cluster
                    {
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] !=
                                                DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        ClusterDetails = Data_Reader["ClusterDetails"].ToString(),
                        ClusterId = Convert.ToInt32(Data_Reader["ClusterId"]),
                        ClusterName = Data_Reader["ClusterName"].ToString(),
                        UserFullName = Data_Reader["UserFullName"].ToString(),
                        UserId = Data_Reader["UserId"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["UserId"]) : (int?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null
                    };

                    clusterList.Add(roleVM);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return clusterList;
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

        public Cluster GetClusterById(int id)
        {
            try
            {
                Sql_Query = "[user].[spCluster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@ClusterId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                Cluster clusterVm = new Cluster();

                while (Data_Reader.Read())
                {
                    clusterVm.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    clusterVm.IsActive = Data_Reader["IsActive"] !=
                                           DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    clusterVm.IsDeleted = Data_Reader["IsDeleted"] !=
                                      DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    clusterVm.ClusterDetails = Data_Reader["ClusterDetails"].ToString();
                    clusterVm.ClusterId = Convert.ToInt32(Data_Reader["ClusterId"]);
                    clusterVm.ClusterName = Data_Reader["ClusterName"].ToString();
                    clusterVm.UserFullName = Data_Reader["UserFullName"].ToString();
                    clusterVm.UserFullName = Data_Reader["UserFullName"].ToString();
                    clusterVm.UserId = Data_Reader["UserId"] !=
                                            DBNull.Value ? Convert.ToInt32(Data_Reader["UserId"]) : (int?)null;
                    clusterVm.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                         DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    clusterVm.CreatedBy = Data_Reader["CreatedBy"] !=
                                               DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    clusterVm.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                            Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;

                };

                Data_Reader.Close();
                Sql_Connection.Close();

                return clusterVm;
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

        //Create Role 
        public int ClusterInsert(Cluster cluster)
        {
            try
            {
                Sql_Query = "[user].[spCluster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;
                Sql_Command.Parameters.Add("@ClusterId", SqlDbType.Int).Value = cluster.ClusterId;
                Sql_Command.Parameters.Add("@ClusterName", SqlDbType.NVarChar).Value = cluster.ClusterName;
                Sql_Command.Parameters.Add("@ClusterDetails", SqlDbType.NVarChar).Value = cluster.ClusterDetails;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = cluster.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = cluster.CreateDate;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = cluster.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = cluster.LastUpdated;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = cluster.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = cluster.IsDeleted;

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

        //Create Role Update
        public int ClusterUpdate(Cluster cluster)
        {
            try
            {
                Sql_Query = "[user].[spCluster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@ClusterId", SqlDbType.Int).Value = cluster.ClusterId;
                Sql_Command.Parameters.Add("@ClusterName", SqlDbType.NVarChar).Value = cluster.ClusterName;
                Sql_Command.Parameters.Add("@ClusterDetails", SqlDbType.NVarChar).Value = cluster.ClusterDetails;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = cluster.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = cluster.CreateDate;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = cluster.LastUpdatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = cluster.LastUpdated;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = cluster.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = cluster.IsDeleted;

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