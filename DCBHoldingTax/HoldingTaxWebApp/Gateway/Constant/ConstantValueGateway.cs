using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Constant
{
    public class ConstantValueGateway : DefaultGateway
    {
        #region Constant Value Portion
        //Get All ConstantValue List
        public List<ConstantValue> GetAllConstantValue()
        {
            try
            {
                Sql_Query = "[constant].[spConstantValue]";
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

                List<ConstantValue> vm = new List<ConstantValue>();

                while (Data_Reader.Read())
                {
                    ConstantValue model = new ConstantValue
                    {
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUserName = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUserName = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (Int32?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (Int32?)null,
                        constantValueId = Convert.ToInt32(Data_Reader["constantValueId"]),
                        DueCharge = Data_Reader["DueCharge"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["DueCharge"]) : (Decimal?)null,
                        DueChargeRef = Data_Reader["DueChargeRef"].ToString(),
                        Rebate = Convert.ToDecimal(Data_Reader["Rebate"]),
                        RebateRef = Data_Reader["RebateRef"].ToString(),
                        RentMonth = Convert.ToInt32(Data_Reader["RentMonth"]),
                        RentMonthRef = Data_Reader["RentMonthRef"].ToString(),
                        RentTaxRate = Convert.ToDecimal(Data_Reader["RentTaxRate"]),
                        RentTaxRateRef = Data_Reader["RentTaxRateRef"].ToString(),
                        Surcharge = Convert.ToDecimal(Data_Reader["Surcharge"]),
                        SurchargeRef = Data_Reader["SurchargeRef"].ToString(),
                        WrongInfoCharge = Convert.ToDecimal(Data_Reader["WrongInfoCharge"]),
                        WrongInfoChargeRef = Data_Reader["WrongInfoChargeRef"].ToString(),
                        OwnFlatDiscount = Data_Reader["OwnFlatDiscount"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["OwnFlatDiscount"]) : (Decimal?)null,
                        OwnFlatDiscountRef = Data_Reader["OwnFlatDiscountRef"].ToString(),
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

        //Get ConstantValue By Id
        public ConstantValue GetConstantValueById()
        {
            try
            {
                Sql_Query = "[constant].[spConstantValue]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "details";
                //Sql_Command.Parameters.Add("@constantValueId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                ConstantValue vm = new ConstantValue();

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
                    vm.constantValueId = Convert.ToInt32(Data_Reader["constantValueId"]);
                    vm.DueCharge = Data_Reader["DueCharge"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["DueCharge"]) : (Decimal?)null;
                    vm.DueChargeRef = Data_Reader["DueChargeRef"].ToString();
                    vm.Rebate = Convert.ToDecimal(Data_Reader["Rebate"]);
                    vm.RebateRef = Data_Reader["RebateRef"].ToString();
                    vm.RentMonth = Convert.ToInt32(Data_Reader["RentMonth"]);
                    vm.RentMonthRef = Data_Reader["RentMonthRef"].ToString();
                    vm.RentTaxRate = Convert.ToDecimal(Data_Reader["RentTaxRate"]);
                    vm.RentTaxRateRef = Data_Reader["RentTaxRateRef"].ToString();
                    vm.Surcharge = Convert.ToDecimal(Data_Reader["Surcharge"]);
                    vm.SurchargeRef = Data_Reader["SurchargeRef"].ToString();
                    vm.WrongInfoCharge = Convert.ToDecimal(Data_Reader["WrongInfoCharge"]);
                    vm.WrongInfoChargeRef = Data_Reader["WrongInfoChargeRef"].ToString();
                    vm.OwnFlatDiscount = Data_Reader["OwnFlatDiscount"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["OwnFlatDiscount"]) : (Decimal?)null;
                    vm.OwnFlatDiscountRef = Data_Reader["OwnFlatDiscountRef"].ToString();
                    
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

        //Create ConstantValue  
        public int ConstantValueInsert(ConstantValue model)
        {
            try
            {
                Sql_Query = "[constant].[spConstantValue]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;


                
                //Sql_Command.Parameters.Add("@constantValueId", SqlDbType.Int).Value = model.constantValueId;
                Sql_Command.Parameters.Add("@RentMonth", SqlDbType.Int).Value = model.RentMonth;
                Sql_Command.Parameters.Add("@RentMonthRef", SqlDbType.NVarChar).Value = model.RentMonthRef;
                Sql_Command.Parameters.Add("@RentTaxRate", SqlDbType.Decimal).Value = model.RentTaxRate;
                Sql_Command.Parameters.Add("@RentTaxRateRef", SqlDbType.NVarChar).Value = model.RentTaxRateRef;
                Sql_Command.Parameters.Add("@Surcharge", SqlDbType.Decimal).Value = model.Surcharge;
                Sql_Command.Parameters.Add("@SurchargeRef", SqlDbType.NVarChar).Value = model.SurchargeRef;
                Sql_Command.Parameters.Add("@WrongInfoCharge", SqlDbType.Decimal).Value = model.WrongInfoCharge;
                Sql_Command.Parameters.Add("@WrongInfoChargeRef", SqlDbType.NVarChar).Value = model.WrongInfoChargeRef;
                Sql_Command.Parameters.Add("@Rebate", SqlDbType.Decimal).Value = model.Rebate;
                Sql_Command.Parameters.Add("@RebateRef", SqlDbType.NVarChar).Value = model.RebateRef;
                Sql_Command.Parameters.Add("@DueCharge", SqlDbType.Decimal).Value = model.DueCharge;
                Sql_Command.Parameters.Add("@DueChargeRef", SqlDbType.NVarChar).Value = model.DueChargeRef;
                Sql_Command.Parameters.Add("@OwnFlatDiscount", SqlDbType.Decimal).Value = model.OwnFlatDiscount;
                Sql_Command.Parameters.Add("@OwnFlatDiscountRef", SqlDbType.NVarChar).Value = model.OwnFlatDiscountRef;


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

        //Update ConstantValue  
        public int ConstantValueUpdate(ConstantValue model)
        {
            try
            {
                Sql_Query = "[constant].[spConstantValue]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@constantValueId", SqlDbType.Int).Value = model.constantValueId;
                Sql_Command.Parameters.Add("@RentMonth", SqlDbType.Int).Value = model.RentMonth;
                Sql_Command.Parameters.Add("@RentMonthRef", SqlDbType.NVarChar).Value = model.RentMonthRef;
                Sql_Command.Parameters.Add("@RentTaxRate", SqlDbType.Decimal).Value = model.RentTaxRate;
                Sql_Command.Parameters.Add("@RentTaxRateRef", SqlDbType.NVarChar).Value = model.RentTaxRateRef;
                Sql_Command.Parameters.Add("@Surcharge", SqlDbType.Decimal).Value = model.Surcharge;
                Sql_Command.Parameters.Add("@SurchargeRef", SqlDbType.NVarChar).Value = model.SurchargeRef;
                Sql_Command.Parameters.Add("@WrongInfoCharge", SqlDbType.Decimal).Value = model.WrongInfoCharge;
                Sql_Command.Parameters.Add("@WrongInfoChargeRef", SqlDbType.NVarChar).Value = model.WrongInfoChargeRef;
                Sql_Command.Parameters.Add("@Rebate", SqlDbType.Decimal).Value = model.Rebate;
                Sql_Command.Parameters.Add("@RebateRef", SqlDbType.NVarChar).Value = model.RebateRef;
                Sql_Command.Parameters.Add("@DueCharge", SqlDbType.Decimal).Value = model.DueCharge;
                Sql_Command.Parameters.Add("@DueChargeRef", SqlDbType.NVarChar).Value = model.DueChargeRef;
                Sql_Command.Parameters.Add("@OwnFlatDiscount", SqlDbType.Decimal).Value = model.OwnFlatDiscount;
                Sql_Command.Parameters.Add("@OwnFlatDiscountRef", SqlDbType.NVarChar).Value = model.OwnFlatDiscountRef;


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
    }
}