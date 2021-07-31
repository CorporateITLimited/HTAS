using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Holding;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Gateway.Holding
{
    public class HoldingGateway : DefaultGateway
    {
        #region Holder
        public List<Holder> GetAllHolder()
        {
            try
            {
                Sql_Query = "[Holding].[spHolderMaster]";
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

                List<Holder> vm = new List<Holder>();

                while (Data_Reader.Read())
                {
                    Holder model = new Holder
                    {
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        HolderName = Convert.ToString(Data_Reader["HolderName"]),
                        AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
                        AreaName = Convert.ToString(Data_Reader["AreaName"]),
                        PlotId = Convert.ToInt32(Data_Reader["PlotId"]),
                        PlotIdNumber = Convert.ToString(Data_Reader["PlotIdNumber"]),
                        PlotNo = Convert.ToString(Data_Reader["PlotNo"]),
                        NID = Convert.ToString(Data_Reader["NID"]),
                        Gender = Data_Reader["Gender"] != DBNull.Value ? Convert.ToInt32(Data_Reader["Gender"]) : (int?)null,
                        GenderType = Convert.ToString(Data_Reader["GenderType"]),
                        MaritialStatus = Data_Reader["MaritialStatus"] != DBNull.Value ? Convert.ToInt32(Data_Reader["MaritialStatus"]) : (int?)null,
                        MaritialStatusType = Convert.ToString(Data_Reader["MaritialStatusType"]),
                        Father = Convert.ToString(Data_Reader["Father"]),
                        Mother = Convert.ToString(Data_Reader["Mother"]),
                        Spouse = Convert.ToString(Data_Reader["Spouse"]),
                        Contact1 = Convert.ToString(Data_Reader["Contact1"]),
                        Contact2 = Convert.ToString(Data_Reader["Contact2"]),
                        Email = Convert.ToString(Data_Reader["Email"]),
                        PresentAdd = Convert.ToString(Data_Reader["PresentAdd"]),
                        PermanentAdd = Convert.ToString(Data_Reader["PermanentAdd"]),
                        ContactAdd = Convert.ToString(Data_Reader["ContactAdd"]),
                        OwnershipSourceId = Convert.ToInt32(Data_Reader["OwnershipSourceId"]),
                        SourceName = Convert.ToString(Data_Reader["SourceName"]),
                        OwnerType = Data_Reader["OwnerType"] != DBNull.Value ? Convert.ToInt32(Data_Reader["OwnerType"]) : (int?)null,
                        OwnerTypeName = Convert.ToString(Data_Reader["OwnerTypeName"]),
                        BuildingTypeId = Convert.ToInt32(Data_Reader["BuildingTypeId"]),
                        BuildingTypeName = Convert.ToString(Data_Reader["BuildingTypeName"]),
                        AmountOfLand = Data_Reader["AmountOfLand"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["AmountOfLand"]) : (decimal?)null,
                        TotalFloor = Data_Reader["TotalFloor"] != DBNull.Value ? Convert.ToInt32(Data_Reader["TotalFloor"]) : (int?)null,
                        EachFloorArea = Data_Reader["EachFloorArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["EachFloorArea"]) : (decimal?)null,
                        TotalFlat = Data_Reader["TotalFlat"] != DBNull.Value ? Convert.ToInt32(Data_Reader["TotalFlat"]) : (int?)null,
                        HoldersFlatNumber = Data_Reader["HoldersFlatNumber"] != DBNull.Value ? Convert.ToInt32(Data_Reader["HoldersFlatNumber"]) : (int?)null,
                        PreviousDueTax = Data_Reader["PreviousDueTax"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["PreviousDueTax"]) : (decimal?)null,
                        ImageLocation = Convert.ToString(Data_Reader["ImageLocation"]),
                        Document1 = Convert.ToString(Data_Reader["Document1"]),
                        Document2 = Convert.ToString(Data_Reader["Document2"]),
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUsername = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUsername = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,

                        //this part done by Masum  ====================
                        AllocationLetterNo = Convert.ToString(Data_Reader["AllocationLetterNo"]),
                        NamjariLetterNo = Convert.ToString(Data_Reader["NamjariLetterNo"]),
                        AllocationDate = Data_Reader["AllocationDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["AllocationDate"]) : (DateTime?)null,
                        NamjariDate = Data_Reader["NamjariDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["NamjariDate"]) : (DateTime?)null,
                        RecordCorrectionDate = Data_Reader["RecordCorrectionDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["RecordCorrectionDate"]) : (DateTime?)null,
                        AreaPlotFlatData = Convert.ToString(Data_Reader["AreaPlotFlatData"]),
                        IsHolderAnOwner = Data_Reader["IsHolderAnOwner"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsHolderAnOwner"]) : (bool?)null
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

        public Holder GetHolderById(int id)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Details;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                Holder vm = new Holder();

                while (Data_Reader.Read())
                {
                    vm.HolderId = Convert.ToInt32(Data_Reader["HolderId"]);
                    vm.HolderName = Convert.ToString(Data_Reader["HolderName"]);
                    vm.AreaId = Convert.ToInt32(Data_Reader["AreaId"]);
                    vm.AreaName = Convert.ToString(Data_Reader["AreaName"]);
                    vm.PlotId = Convert.ToInt32(Data_Reader["PlotId"]);
                    vm.PlotIdNumber = Convert.ToString(Data_Reader["PlotIdNumber"]);
                    vm.PlotNo = Convert.ToString(Data_Reader["PlotNo"]);
                    vm.NID = Convert.ToString(Data_Reader["NID"]);
                    vm.Gender = Data_Reader["Gender"] != DBNull.Value ? Convert.ToInt32(Data_Reader["Gender"]) : (int?)null;
                    vm.GenderType = Convert.ToString(Data_Reader["GenderType"]);
                    vm.MaritialStatus = Data_Reader["MaritialStatus"] != DBNull.Value ? Convert.ToInt32(Data_Reader["MaritialStatus"]) : (int?)null;
                    vm.MaritialStatusType = Convert.ToString(Data_Reader["MaritialStatusType"]);
                    vm.Father = Convert.ToString(Data_Reader["Father"]);
                    vm.Mother = Convert.ToString(Data_Reader["Mother"]);
                    vm.Spouse = Convert.ToString(Data_Reader["Spouse"]);
                    vm.Contact1 = Convert.ToString(Data_Reader["Contact1"]);
                    vm.Contact2 = Convert.ToString(Data_Reader["Contact2"]);
                    vm.Email = Convert.ToString(Data_Reader["Email"]);
                    vm.PresentAdd = Convert.ToString(Data_Reader["PresentAdd"]);
                    vm.PermanentAdd = Convert.ToString(Data_Reader["PermanentAdd"]);
                    vm.ContactAdd = Convert.ToString(Data_Reader["ContactAdd"]);
                    vm.OwnershipSourceId = Convert.ToInt32(Data_Reader["OwnershipSourceId"]);
                    vm.SourceName = Convert.ToString(Data_Reader["SourceName"]);
                    vm.OwnerType = Data_Reader["OwnerType"] != DBNull.Value ? Convert.ToInt32(Data_Reader["OwnerType"]) : (int?)null;
                    vm.OwnerTypeName = Convert.ToString(Data_Reader["OwnerTypeName"]);
                    vm.BuildingTypeId = Convert.ToInt32(Data_Reader["BuildingTypeId"]);
                    vm.BuildingTypeName = Convert.ToString(Data_Reader["BuildingTypeName"]);
                    vm.AmountOfLand = Data_Reader["AmountOfLand"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["AmountOfLand"]) : (decimal?)null;
                    vm.TotalFloor = Data_Reader["TotalFloor"] != DBNull.Value ? Convert.ToInt32(Data_Reader["TotalFloor"]) : (int?)null;
                    vm.EachFloorArea = Data_Reader["EachFloorArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["EachFloorArea"]) : (decimal?)null;
                    vm.TotalFlat = Data_Reader["TotalFlat"] != DBNull.Value ? Convert.ToInt32(Data_Reader["TotalFlat"]) : (int?)null;
                    vm.HoldersFlatNumber = Data_Reader["HoldersFlatNumber"] != DBNull.Value ? Convert.ToInt32(Data_Reader["HoldersFlatNumber"]) : (int?)null;
                    vm.PreviousDueTax = Data_Reader["PreviousDueTax"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["PreviousDueTax"]) : (decimal?)null;
                    vm.ImageLocation = Convert.ToString(Data_Reader["ImageLocation"]);
                    vm.Document1 = Convert.ToString(Data_Reader["Document1"]);
                    vm.Document2 = Convert.ToString(Data_Reader["Document2"]);
                    vm.CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null;
                    vm.CreatedByUsername = Data_Reader["CreatedByUsername"].ToString();
                    vm.UpdatedByUsername = Data_Reader["UpdatedByUserName"].ToString();
                    vm.LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null;
                    vm.IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null;
                    vm.IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null;
                    vm.CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null;
                    vm.LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null;
                    vm.RoadNo = Convert.ToString(Data_Reader["RoadNo"]);
                    vm.RoadName = Convert.ToString(Data_Reader["RoadName"]);

                    //done by masum =====================
                    vm.AllocationLetterNo = Convert.ToString(Data_Reader["AllocationLetterNo"]);
                    vm.NamjariLetterNo = Convert.ToString(Data_Reader["NamjariLetterNo"]);
                    vm.AllocationDate = Data_Reader["AllocationDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["AllocationDate"]) : (DateTime?)null;
                    vm.NamjariDate = Data_Reader["NamjariDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["NamjariDate"]) : (DateTime?)null;
                    vm.RecordCorrectionDate = Data_Reader["RecordCorrectionDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["RecordCorrectionDate"]) : (DateTime?)null;
                    vm.AreaPlotFlatData = Convert.ToString(Data_Reader["AreaPlotFlatData"]);
                    vm.IsHolderAnOwner = Data_Reader["IsHolderAnOwner"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsHolderAnOwner"]) : (bool?)null;
                };

                vm.StringCreateDate = $"{vm.CreateDate:dd/MM/yyyy HH:mm:ss tt}";
                vm.StringLastUpdated = $"{vm.LastUpdated:dd/MM/yyyy HH:mm:ss tt}";
                vm.StringAllocationDate = $"{vm.AllocationDate:dd/MM/yyyy}";
                vm.StringNamjariDate = $"{vm.NamjariDate:dd/MM/yyyy}";
                vm.StringRecordCorrectionDate = $"{vm.RecordCorrectionDate:dd/MM/yyyy}";
                vm.StrPreviousDueTax = BanglaConvertionHelper.DecimalValueEnglish2Bangla(vm.PreviousDueTax);
                vm.StrAmountOfLand = BanglaConvertionHelper.DecimalValueEnglish2Bangla(vm.EachFloorArea);
                vm.StrEachFloorArea = BanglaConvertionHelper.DecimalValueEnglish2Bangla(vm.AmountOfLand);
                vm.StrHoldersFlatNumber = BanglaConvertionHelper.IntegerValueEnglish2Bangla(vm.HoldersFlatNumber);
                vm.StrTotalFlat = BanglaConvertionHelper.IntegerValueEnglish2Bangla(vm.TotalFlat);
                vm.StrTotalFloor = BanglaConvertionHelper.IntegerValueEnglish2Bangla(vm.TotalFloor);

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

        public int InsertHolder(Holder model)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;

                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = model.HolderId;
                Sql_Command.Parameters.Add("@HolderName", SqlDbType.NVarChar).Value = model.HolderName;
                Sql_Command.Parameters.Add("@AreaId", SqlDbType.Int).Value = model.AreaId;
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = model.PlotId;
                Sql_Command.Parameters.Add("@NID", SqlDbType.NVarChar).Value = model.NID;
                Sql_Command.Parameters.Add("@Gender", SqlDbType.Int).Value = model.Gender;
                Sql_Command.Parameters.Add("@MaritialStatus", SqlDbType.Int).Value = model.MaritialStatus;
                Sql_Command.Parameters.Add("@Father", SqlDbType.NVarChar).Value = model.Father;
                Sql_Command.Parameters.Add("@Mother", SqlDbType.NVarChar).Value = model.Mother;
                Sql_Command.Parameters.Add("@Spouse", SqlDbType.NVarChar).Value = model.Spouse;
                Sql_Command.Parameters.Add("@Contact1", SqlDbType.NVarChar).Value = model.Contact1;
                Sql_Command.Parameters.Add("@Contact2", SqlDbType.NVarChar).Value = model.Contact2;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
                Sql_Command.Parameters.Add("@PresentAdd", SqlDbType.NVarChar).Value = model.PresentAdd;
                Sql_Command.Parameters.Add("@PermanentAdd", SqlDbType.NVarChar).Value = model.PermanentAdd;
                Sql_Command.Parameters.Add("@ContactAdd", SqlDbType.NVarChar).Value = model.ContactAdd;
                Sql_Command.Parameters.Add("@OwnershipSourceId", SqlDbType.Int).Value = model.OwnershipSourceId;
                Sql_Command.Parameters.Add("@OwnerType", SqlDbType.Int).Value = model.OwnerType;
                Sql_Command.Parameters.Add("@BuildingTypeId", SqlDbType.Int).Value = model.BuildingTypeId;
                Sql_Command.Parameters.Add("@AmountOfLand", SqlDbType.Decimal).Value = model.AmountOfLand;
                Sql_Command.Parameters.Add("@TotalFloor", SqlDbType.Int).Value = model.TotalFloor;
                Sql_Command.Parameters.Add("@EachFloorArea", SqlDbType.Decimal).Value = model.EachFloorArea;
                Sql_Command.Parameters.Add("@TotalFlat", SqlDbType.Int).Value = model.TotalFlat;
                Sql_Command.Parameters.Add("@HoldersFlatNumber", SqlDbType.Int).Value = model.HoldersFlatNumber;
                Sql_Command.Parameters.Add("@PreviousDueTax", SqlDbType.Decimal).Value = model.PreviousDueTax;


                Sql_Command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar).Value = model.ImageLocation;
                Sql_Command.Parameters.Add("@Document1", SqlDbType.NVarChar).Value = model.Document1;
                Sql_Command.Parameters.Add("@Document2", SqlDbType.NVarChar).Value = model.Document2;

                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = model.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = model.LastUpdatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = model.IsDeleted;
                Sql_Command.Parameters.Add("@IsHolderAnOwner", SqlDbType.Bit).Value = model.IsHolderAnOwner;

                //created by Masum ===================
                Sql_Command.Parameters.Add("@AllocationLetterNo", SqlDbType.NVarChar).Value = model.AllocationLetterNo;
                Sql_Command.Parameters.Add("@NamjariLetterNo", SqlDbType.NVarChar).Value = model.NamjariLetterNo;
                Sql_Command.Parameters.Add("@AllocationDate", SqlDbType.DateTime).Value = model.AllocationDate;
                Sql_Command.Parameters.Add("@NamjariDate", SqlDbType.DateTime).Value = model.NamjariDate;
                Sql_Command.Parameters.Add("@RecordCorrectionDate", SqlDbType.DateTime).Value = model.RecordCorrectionDate;

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

        public int UpdateHolder(Holder model)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = model.HolderId;
                Sql_Command.Parameters.Add("@HolderName", SqlDbType.NVarChar).Value = model.HolderName;
                Sql_Command.Parameters.Add("@AreaId", SqlDbType.Int).Value = model.AreaId;
                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = model.PlotId;
                Sql_Command.Parameters.Add("@NID", SqlDbType.NVarChar).Value = model.NID;
                Sql_Command.Parameters.Add("@Gender", SqlDbType.Int).Value = model.Gender;
                Sql_Command.Parameters.Add("@MaritialStatus", SqlDbType.Int).Value = model.MaritialStatus;
                Sql_Command.Parameters.Add("@Father", SqlDbType.NVarChar).Value = model.Father;
                Sql_Command.Parameters.Add("@Mother", SqlDbType.NVarChar).Value = model.Mother;
                Sql_Command.Parameters.Add("@Spouse", SqlDbType.NVarChar).Value = model.Spouse;
                Sql_Command.Parameters.Add("@Contact1", SqlDbType.NVarChar).Value = model.Contact1;
                Sql_Command.Parameters.Add("@Contact2", SqlDbType.NVarChar).Value = model.Contact2;
                Sql_Command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
                Sql_Command.Parameters.Add("@PresentAdd", SqlDbType.NVarChar).Value = model.PresentAdd;
                Sql_Command.Parameters.Add("@PermanentAdd", SqlDbType.NVarChar).Value = model.PermanentAdd;
                Sql_Command.Parameters.Add("@ContactAdd", SqlDbType.NVarChar).Value = model.ContactAdd;
                Sql_Command.Parameters.Add("@OwnershipSourceId", SqlDbType.Int).Value = model.OwnershipSourceId;
                Sql_Command.Parameters.Add("@OwnerType", SqlDbType.Int).Value = model.OwnerType;
                Sql_Command.Parameters.Add("@BuildingTypeId", SqlDbType.Int).Value = model.BuildingTypeId;
                Sql_Command.Parameters.Add("@AmountOfLand", SqlDbType.Decimal).Value = model.AmountOfLand;
                Sql_Command.Parameters.Add("@TotalFloor", SqlDbType.Int).Value = model.TotalFloor;
                Sql_Command.Parameters.Add("@EachFloorArea", SqlDbType.Decimal).Value = model.EachFloorArea;
                Sql_Command.Parameters.Add("@TotalFlat", SqlDbType.Int).Value = model.TotalFlat;
                Sql_Command.Parameters.Add("@HoldersFlatNumber", SqlDbType.Int).Value = model.HoldersFlatNumber;
                Sql_Command.Parameters.Add("@PreviousDueTax", SqlDbType.Decimal).Value = model.PreviousDueTax;
                Sql_Command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar).Value = model.ImageLocation;
                Sql_Command.Parameters.Add("@Document1", SqlDbType.NVarChar).Value = model.Document1;
                Sql_Command.Parameters.Add("@Document2", SqlDbType.NVarChar).Value = model.Document2;

                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = model.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = model.LastUpdatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = model.IsDeleted;
                Sql_Command.Parameters.Add("@IsHolderAnOwner", SqlDbType.Bit).Value = model.IsHolderAnOwner;


                //created by Masum ===================
                Sql_Command.Parameters.Add("@AllocationLetterNo", SqlDbType.NVarChar).Value = model.AllocationLetterNo;
                Sql_Command.Parameters.Add("@NamjariLetterNo", SqlDbType.NVarChar).Value = model.NamjariLetterNo;
                Sql_Command.Parameters.Add("@AllocationDate", SqlDbType.DateTime).Value = model.AllocationDate;
                Sql_Command.Parameters.Add("@NamjariDate", SqlDbType.DateTime).Value = model.NamjariDate;
                Sql_Command.Parameters.Add("@RecordCorrectionDate", SqlDbType.DateTime).Value = model.RecordCorrectionDate;
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

        #region holder flat

        public List<HolderFlat> GetHoldersFlatByHolderId(int id)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderFlatMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "select_by_holderid";
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);


                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                List<HolderFlat> vm = new List<HolderFlat>();

                while (Data_Reader.Read())
                {
                    HolderFlat model = new HolderFlat
                    {
                        HolderFlatId = Convert.ToInt32(Data_Reader["HolderFlatId"]),
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        FlorNo = Data_Reader["FlorNo"] != DBNull.Value ? Convert.ToInt32(Data_Reader["FlorNo"]) : (int?)null,
                        FlatNo = Convert.ToString(Data_Reader["FlatNo"]),
                        FlatArea = Data_Reader["FlatArea"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["FlatArea"]) : (decimal?)null,
                        OwnOrRent = Data_Reader["OwnOrRent"] != DBNull.Value ? Convert.ToInt32(Data_Reader["OwnOrRent"]) : (int?)null,
                        OwnOrRentType = Convert.ToString(Data_Reader["OwnOrRentType"]),
                        IsSelfOwned = Data_Reader["IsSelfOwned"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsSelfOwned"]) : (bool?)null,
                        MonthlyRent = Data_Reader["MonthlyRent"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["MonthlyRent"]) : (decimal?)null,
                        OwnerName = Convert.ToString(Data_Reader["OwnerName"]),
                        SelfOwn = Data_Reader["SelfOwn"] != DBNull.Value ? Convert.ToInt32(Data_Reader["SelfOwn"]) : (int?)null,
                        SelfOwnType = Convert.ToString(Data_Reader["SelfOwnType"]),
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        // CreatedByUsername = Data_Reader["CreatedByUsername"].ToString(),
                        //UpdatedByUsername = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        FloorTypeName = Data_Reader["FloorTypeName"].ToString()
                    };

                    model.StrFlatArea = BanglaConvertionHelper.DecimalValueEnglish2Bangla(model.FlatArea);
                    model.StrMonthlyRent = BanglaConvertionHelper.DecimalValueEnglish2Bangla(model.MonthlyRent);

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

        public int HoldersFlatInsert(HolderFlat model)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderFlatMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;

                Sql_Command.Parameters.Add("@HolderFlatId", SqlDbType.Int).Value = model.HolderFlatId;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = model.HolderId;
                Sql_Command.Parameters.Add("@FlorNo", SqlDbType.Int).Value = model.FlorNo;
                Sql_Command.Parameters.Add("@FlatNo", SqlDbType.NVarChar).Value = model.FlatNo;
                Sql_Command.Parameters.Add("@FlatArea", SqlDbType.Decimal).Value = model.FlatArea;
                Sql_Command.Parameters.Add("@OwnOrRent", SqlDbType.Int).Value = model.OwnOrRent;
                Sql_Command.Parameters.Add("@IsSelfOwned", SqlDbType.Bit).Value = model.IsSelfOwned;
                Sql_Command.Parameters.Add("@OwnerName", SqlDbType.NVarChar).Value = model.OwnerName;
                Sql_Command.Parameters.Add("@MonthlyRent", SqlDbType.Decimal).Value = model.MonthlyRent;
                Sql_Command.Parameters.Add("@SelfOwn", SqlDbType.Int).Value = model.SelfOwn;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = model.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = model.LastUpdatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = model.IsDeleted;
                Sql_Command.Parameters.Add("@IsCheckedByHolder", SqlDbType.Bit).Value = model.IsCheckedByHolder;

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

        public int HoldersFlatUpdate(HolderFlat model)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderFlatMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Update;

                Sql_Command.Parameters.Add("@HolderFlatId", SqlDbType.Int).Value = model.HolderFlatId;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = model.HolderId;
                Sql_Command.Parameters.Add("@FlorNo", SqlDbType.Int).Value = model.FlorNo;
                Sql_Command.Parameters.Add("@FlatNo", SqlDbType.NVarChar).Value = model.FlatNo;
                Sql_Command.Parameters.Add("@FlatArea", SqlDbType.Decimal).Value = model.FlatArea;
                Sql_Command.Parameters.Add("@OwnOrRent", SqlDbType.Int).Value = model.OwnOrRent;
                Sql_Command.Parameters.Add("@IsSelfOwned", SqlDbType.Bit).Value = model.IsSelfOwned;
                Sql_Command.Parameters.Add("@OwnerName", SqlDbType.NVarChar).Value = model.OwnerName;
                Sql_Command.Parameters.Add("@MonthlyRent", SqlDbType.Decimal).Value = model.MonthlyRent;
                Sql_Command.Parameters.Add("@SelfOwn", SqlDbType.Int).Value = model.SelfOwn;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = model.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = model.LastUpdatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = model.IsDeleted;
                Sql_Command.Parameters.Add("@IsCheckedByHolder", SqlDbType.Bit).Value = model.IsCheckedByHolder;

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

        public int DeleteHoldersFlatDataByHolderId(int HolderId)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderFlatMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };
                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Delete;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = HolderId;

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

        #region frontend queries

        public decimal GetPerSqrFeetPrice(int areaId, int buildingTypeId)
        {
            try
            {
                Sql_Query = "[Holding].[spHolderMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "per_square_rate";
                Sql_Command.Parameters.Add("@AreaId", SqlDbType.Int).Value = areaId;
                Sql_Command.Parameters.Add("@BuildingTypeId", SqlDbType.Int).Value = buildingTypeId;

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                decimal perSqrFeetPrice = 1;
                decimal? perSqrFeetPriceNull = null;

                while (Data_Reader.Read())
                {
                    perSqrFeetPriceNull = Data_Reader["PerSqfRent"] != DBNull.Value ? Convert.ToDecimal(Data_Reader["PerSqfRent"]) : (decimal?)null;
                };

                Data_Reader.Close();
                Sql_Connection.Close();

                perSqrFeetPrice = perSqrFeetPriceNull ?? default(decimal);

                return perSqrFeetPrice;
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

        public int GetMAXId()
        {
            try
            {
                Sql_Query = "[Holding].[spHolderMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "max_id";

                SqlParameter result = new SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                Sql_Command.Parameters.Add(result);

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                int id = 0;
                int? nullId = null;

                while (Data_Reader.Read())
                {
                    nullId = Data_Reader["MaxId"] != DBNull.Value ? Convert.ToInt32(Data_Reader["MaxId"]) : (int?)null;
                };

                Data_Reader.Close();
                Sql_Connection.Close();

                id = nullId ?? default(int);

                return id;
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

        public HolderVM GetAllotmentNamjariDesignByPlotId(int PlotId)
        {
            try
            {
                Sql_Query = "[dbo].[spGetAllotmentNamjariDesignByPlotId]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Clear();

                Sql_Command.Parameters.Add("@PlotId", SqlDbType.Int).Value = PlotId;

                Sql_Connection.Open();
                Data_Reader = Sql_Command.ExecuteReader();

                HolderVM vm = new HolderVM();

                while (Data_Reader.Read())
                {
                    vm.PlotId = Convert.ToInt32(Data_Reader["PlotId"]);
                    vm.PlotOwnerName = Convert.ToString(Data_Reader["PlotOwnerName"]);
                    vm.LeaseDate = Data_Reader["LeaseDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LeaseDate"]) : (DateTime?)null;
                    vm.LeasePeriod = Data_Reader["LeasePeriod"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LeasePeriod"]) : (int?)null;
                    vm.LeaseExpiryDate = Data_Reader["LeaseExpiryDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LeaseExpiryDate"]) : (DateTime?)null;
                    vm.FirstApprovalDate = Data_Reader["FirstApprovalDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["FirstApprovalDate"]) : (DateTime?)null;
                    vm.FirstApprovalLetterNo = Convert.ToString(Data_Reader["FirstApprovalLetterNo"]);
                    vm.LastApprovalDate = Data_Reader["LastApprovalDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastApprovalDate"]) : (DateTime?)null;
                    vm.LastApprovalLetterNo = Convert.ToString(Data_Reader["LastApprovalLetterNo"]);
                };

                vm.StringLeaseDate = $"{vm.LeaseDate:dd/MM/yyyy}";
                vm.StringLeaseExpiryDate = $"{vm.LeaseExpiryDate:dd/MM/yyyy}";
                vm.StringFirstApprovalDate = $"{vm.FirstApprovalDate:dd/MM/yyyy}";
                vm.StringLastApprovalDate = $"{vm.LastApprovalDate:dd/MM/yyyy}";

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


        #endregion
    }
}