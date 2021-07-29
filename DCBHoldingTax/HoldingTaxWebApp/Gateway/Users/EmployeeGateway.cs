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
    public class EmployeeGateway : DefaultGateway
    {
        public List<Employee> GetAllEmployeeList()
        {
            try
            {
                Sql_Query = "[user].[spEmployeeMaster]";
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
                List<Employee> EmployeeListVM = new List<Employee>();

                while (Data_Reader.Read())
                {
                    Employee Employee = new Employee
                    {
                        ContactNo = Data_Reader["ContactNo"].ToString(),
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        //Description = Data_Reader["Description"].ToString(),
                        DesignationId = Convert.ToInt32(Data_Reader["DesignationId"]),
                        DesignationName = Data_Reader["DesignationName"].ToString(),
                        DOB = Data_Reader["DOB"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["DOB"]) : (DateTime?)null,

                        EmployeeName = Data_Reader["EmployeeName"].ToString(),
                        EmpolyeeId = Convert.ToInt32(Data_Reader["EmpolyeeId"]),
                        FatherName = Data_Reader["FatherName"].ToString(),
                        MotherName = Data_Reader["MotherName"].ToString(),
                        NID = Data_Reader["NID"].ToString(),
                        Remarks = Data_Reader["ContactNo"].ToString(),
                        Sex = Data_Reader["Sex"].ToString(),
                        CreatedByUserName = Data_Reader["CreatedByUserName"].ToString(),
                        CreatedBy = Data_Reader["CreatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        Email = Data_Reader["Email"].ToString(),
                        IsActive = Convert.ToBoolean(Data_Reader["IsActive"]),
                        IsDeleted = Convert.ToBoolean(Data_Reader["IsDeleted"]),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                                Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                                DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        //EmpDesignationName = Data_Reader["EmpDesignationName"].ToString(),
                        EmployeeAddress = Data_Reader["EmployeeAddress"].ToString()

                    };

                    EmployeeListVM.Add(Employee);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return EmployeeListVM;
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


        public List<Employee> GetAllEmployeeListForSelect()
        {
            try
            {
                Sql_Query = "[user].[spEmployeeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "select_employee";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();
                List<Employee> EmployeeListVM = new List<Employee>();

                while (Data_Reader.Read())
                {
                    Employee Employee = new Employee
                    {
                        EmployeeName = Data_Reader["EmployeeName"].ToString(),
                        EmpolyeeId = Convert.ToInt32(Data_Reader["EmpolyeeId"]),
                    };

                    EmployeeListVM.Add(Employee);
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return EmployeeListVM;
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

        public Employee GetEmployeeById(int id)
        {
            try
            {
                Sql_Query = "[user].[spEmployeeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@EmpolyeeId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                Employee Employee = new Employee();

                while (Data_Reader.Read())
                {
                    Employee.ContactNo = Data_Reader["ContactNo"].ToString();
                    Employee.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ?
                                           Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    //Employee.Description = Data_Reader["Description"].ToString();
                    Employee.DesignationId = Convert.ToInt32(Data_Reader["DesignationId"]);
                    Employee.DesignationName = Data_Reader["DesignationName"].ToString();
                    Employee.DOB = Data_Reader["DOB"] != DBNull.Value ?
                                          Convert.ToDateTime(Data_Reader["DOB"]) : (DateTime?)null;
                    Employee.EmployeeName = Data_Reader["EmployeeName"].ToString();
                    Employee.EmpolyeeId = Convert.ToInt32(Data_Reader["EmpolyeeId"]);
                    Employee.FatherName = Data_Reader["FatherName"].ToString();
                    Employee.MotherName = Data_Reader["MotherName"].ToString();
                    Employee.NID = Data_Reader["NID"].ToString();
                    Employee.Remarks = Data_Reader["Remarks"].ToString();
                    Employee.Sex = Data_Reader["Sex"].ToString();
                    Employee.CreatedByUserName = Data_Reader["CreatedByUserName"].ToString();
                    Employee.CreatedBy = Data_Reader["CreatedBy"] !=
                                            DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    Employee.Email = Data_Reader["Email"].ToString();
                    Employee.IsActive = Convert.ToBoolean(Data_Reader["IsActive"]);
                    Employee.IsDeleted = Convert.ToBoolean(Data_Reader["IsDeleted"]);
                    Employee.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ?
                                         Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    Employee.LastUpdatedBy = Data_Reader["LastUpdatedBy"] !=
                                       DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    Employee.UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString();
                    Employee.EmployeeAddress = Data_Reader["EmployeeAddress"].ToString();
                }

                Data_Reader.Close();
                Sql_Connection.Close();

                return Employee;
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


        public int EmployeeInsert(Employee employee)
        {
            try
            {
                Sql_Query = "[user].[spEmployeeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;

                Sql_Command.Parameters.Add("@ContactNo", SqlDbType.NVarChar).Value = employee.ContactNo;
                Sql_Command.Parameters.Add("@EmployeeName", SqlDbType.NVarChar).Value = employee.EmployeeName;
                Sql_Command.Parameters.Add("@DesignationId", SqlDbType.Int).Value = employee.DesignationId;
                Sql_Command.Parameters.Add("@FatherName", SqlDbType.NVarChar).Value = employee.FatherName;
                Sql_Command.Parameters.Add("@MotherName", SqlDbType.NVarChar).Value = employee.MotherName;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = employee.Email;
                Sql_Command.Parameters.Add("@DOB", SqlDbType.DateTime).Value = employee.DOB;
                Sql_Command.Parameters.Add("@Sex", SqlDbType.NVarChar).Value = employee.Sex;
                Sql_Command.Parameters.Add("@NID", SqlDbType.NVarChar).Value = employee.NID;
                Sql_Command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = employee.Remarks;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = employee.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = employee.CreateDate;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = employee.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = employee.IsDeleted;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = employee.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = employee.LastUpdatedBy;
                Sql_Command.Parameters.Add("@EmployeeAddress", SqlDbType.NVarChar).Value = employee.EmployeeAddress;


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


        public int EmployeeUpdate(Employee employee)
        {
            try
            {
                Sql_Query = "[user].[spEmployeeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;
                Sql_Command.Parameters.Add("@ContactNo", SqlDbType.NVarChar).Value = employee.ContactNo;
                Sql_Command.Parameters.Add("@EmployeeName", SqlDbType.NVarChar).Value = employee.EmployeeName;
                Sql_Command.Parameters.Add("@DesignationId", SqlDbType.Int).Value = employee.DesignationId;
                Sql_Command.Parameters.Add("@FatherName", SqlDbType.NVarChar).Value = employee.FatherName;
                Sql_Command.Parameters.Add("@MotherName", SqlDbType.NVarChar).Value = employee.MotherName;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = employee.Email;
                Sql_Command.Parameters.Add("@DOB", SqlDbType.DateTime).Value = employee.DOB;
                Sql_Command.Parameters.Add("@Sex", SqlDbType.NVarChar).Value = employee.Sex;
                Sql_Command.Parameters.Add("@NID", SqlDbType.NVarChar).Value = employee.NID;
                Sql_Command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = employee.Remarks;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = employee.CreatedBy;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = employee.CreateDate;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = employee.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = employee.IsDeleted;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = employee.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = employee.LastUpdatedBy;
                Sql_Command.Parameters.Add("@EmpolyeeId", SqlDbType.Int).Value = employee.EmpolyeeId;
                Sql_Command.Parameters.Add("@EmployeeAddress", SqlDbType.NVarChar).Value = employee.EmployeeAddress;

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