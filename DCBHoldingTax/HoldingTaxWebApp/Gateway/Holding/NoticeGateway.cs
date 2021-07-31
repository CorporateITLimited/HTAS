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
    public class NoticeGateway : DefaultGateway
    {
        public List<Notice> GetAllNotice()
        {
            try
            {
                Sql_Query = "[Holding].[spNoticeMaster]";
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

                List<Notice> vm = new List<Notice>();

                while (Data_Reader.Read())
                {
                    Notice model = new Notice
                    {
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        HolderName = Convert.ToString(Data_Reader["HolderName"]),
                        AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
                        AreaName = Convert.ToString(Data_Reader["AreaName"]),
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUsername = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUsername = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        AreaPlotFlatData = Convert.ToString(Data_Reader["AreaPlotFlatData"]),
                        FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),
                        FinancialYearId = Convert.ToInt32(Data_Reader["FinancialYearId"]),
                        IsNoticeSent = Data_Reader["IsNoticeSent"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsNoticeSent"]) : (bool?)null,
                        NoticeId = Convert.ToInt64(Data_Reader["NoticeId"]),
                        NoticeLinkName = Convert.ToString(Data_Reader["NoticeLinkName"]),
                        NoticeName = "",
                        NoticeSentDate = Data_Reader["NoticeSentDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["NoticeSentDate"]) : (DateTime?)null,
                        NoticeTypeId = Convert.ToInt32(Data_Reader["NoticeTypeId"]),
                        EmployeeName = Convert.ToString(Data_Reader["EmployeeName"]),
                        DesignationName = Convert.ToString(Data_Reader["DesignationName"]),
                    };
                    if (model.NoticeTypeId == 1)
                        model.NoticeName = "গৃহকরের প্রাথমিক বিজ্ঞপ্তি দেখুন";
                    else if (model.NoticeTypeId == 2)
                        model.NoticeName = "রিবেটসহ গৃহকর প্রাপ্তির বিজ্ঞপ্তি দেখুন";
                    else if (model.NoticeTypeId == 3)
                        model.NoticeName = "গৃহকরের চূড়ান্ত বিজ্ঞপ্তি দেখুন";

                    model.StringNoticeSentDate = $"{model.NoticeSentDate:dd/MM/yyyy HH:mm:ss tt}";

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

        //public List<Notice> GetAllNotice_()
        //{
        //    try
        //    {
        //        Sql_Query = "[Holding].[spNoticeMaster]";
        //        Sql_Command = new SqlCommand
        //        {
        //            CommandText = Sql_Query,
        //            Connection = Sql_Connection,
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        Sql_Command.Parameters.Clear();

        //        Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Select;

        //        SqlParameter result = new SqlParameter
        //        {
        //            ParameterName = "@result",
        //            SqlDbType = SqlDbType.Int,
        //            Direction = ParameterDirection.Output
        //        };
        //        Sql_Command.Parameters.Add(result);


        //        Sql_Connection.Open();
        //        Data_Reader = Sql_Command.ExecuteReader();

        //        List<Notice> vm = new List<Notice>();

        //        while (Data_Reader.Read())
        //        {
        //            Notice model = new Notice
        //            {
        //                HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
        //                HolderName = Convert.ToString(Data_Reader["HolderName"]),
        //                AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
        //                AreaName = Convert.ToString(Data_Reader["AreaName"]),
        //                AreaPlotFlatData = Convert.ToString(Data_Reader["AreaPlotFlatData"]),
        //                FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),
        //                FinancialYearId = Convert.ToInt32(Data_Reader["FinancialYearId"]),
        //                IsNoticeSent = Data_Reader["IsNoticeSent"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsNoticeSent"]) : (bool?)null,
        //                //  NoticeTypeId = Convert.ToInt32(Data_Reader["NoticeTypeId"]),
        //                Notice_1 = Data_Reader["1"] != DBNull.Value ? Convert.ToInt32(Data_Reader["1"]) : (int?)null,
        //                Notice_2 = Data_Reader["2"] != DBNull.Value ? Convert.ToInt32(Data_Reader["2"]) : (int?)null,
        //                Notice_3 = Data_Reader["3"] != DBNull.Value ? Convert.ToInt32(Data_Reader["3"]) : (int?)null
        //            };
        //            model.NoticeName_1 = model.Notice_1 == 1 ? "১ নম্বর বিজ্ঞপ্তি দেখুন" : null;
        //            model.NoticeName_2 = model.Notice_2 == 2 ? "২ নম্বর বিজ্ঞপ্তি দেখুন " : null;
        //            model.NoticeName_3 = model.Notice_3 == 3 ? "৩ নম্বর বিজ্ঞপ্তি দেখুন " : null;
        //            model.StringNoticeSentDate = $"{model.NoticeSentDate:dd/MM/yyyy HH:mm:ss tt}";

        //            vm.Add(model);
        //        }

        //        Data_Reader.Close();
        //        Sql_Connection.Close();

        //        return vm;
        //    }
        //    catch (SqlException exception)
        //    {
        //        for (int i = 0; i < exception.Errors.Count; i++)
        //        {
        //            ErrorMessages.Append("Index #" + i + "\n" +
        //                "Message: " + exception.Errors[i].Message + "\n" +
        //                "Error Number: " + exception.Errors[i].Number + "\n" +
        //                "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
        //                "Source: " + exception.Errors[i].Source + "\n" +
        //                "Procedure: " + exception.Errors[i].Procedure + "\n");
        //        }
        //        throw new Exception(ErrorMessages.ToString());
        //    }
        //    finally
        //    {
        //        if (Sql_Connection.State == ConnectionState.Open)
        //            Sql_Connection.Close();
        //    }
        //}

        //public List<Notice> GetAllNoticeForHolder_(int id)
        //{
        //    try
        //    {
        //        Sql_Query = "[Holding].[spNoticeMaster]";
        //        Sql_Command = new SqlCommand
        //        {
        //            CommandText = Sql_Query,
        //            Connection = Sql_Connection,
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        Sql_Command.Parameters.Clear();

        //        Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "select_by_holderid";
        //        Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = id;

        //        SqlParameter result = new SqlParameter
        //        {
        //            ParameterName = "@result",
        //            SqlDbType = SqlDbType.Int,
        //            Direction = ParameterDirection.Output
        //        };
        //        Sql_Command.Parameters.Add(result);


        //        Sql_Connection.Open();
        //        Data_Reader = Sql_Command.ExecuteReader();

        //        List<Notice> vm = new List<Notice>();

        //        while (Data_Reader.Read())
        //        {
        //            Notice model = new Notice
        //            {
        //                HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
        //                HolderName = Convert.ToString(Data_Reader["HolderName"]),
        //                AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
        //                AreaName = Convert.ToString(Data_Reader["AreaName"]),
        //                AreaPlotFlatData = Convert.ToString(Data_Reader["AreaPlotFlatData"]),
        //                FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),
        //                FinancialYearId = Convert.ToInt32(Data_Reader["FinancialYearId"]),
        //                IsNoticeSent = Data_Reader["IsNoticeSent"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsNoticeSent"]) : (bool?)null,
        //                //  NoticeTypeId = Convert.ToInt32(Data_Reader["NoticeTypeId"]),
        //                Notice_1 = Data_Reader["1"] != DBNull.Value ? Convert.ToInt32(Data_Reader["1"]) : (int?)null,
        //                Notice_2 = Data_Reader["2"] != DBNull.Value ? Convert.ToInt32(Data_Reader["2"]) : (int?)null,
        //                Notice_3 = Data_Reader["3"] != DBNull.Value ? Convert.ToInt32(Data_Reader["3"]) : (int?)null
        //            };
        //            model.NoticeName_1 = model.Notice_1 == 1 ? "১ নম্বর বিজ্ঞপ্তি দেখুন " : null;
        //            model.NoticeName_2 = model.Notice_2 == 2 ? "২ নম্বর বিজ্ঞপ্তি দেখুন " : null;
        //            model.NoticeName_3 = model.Notice_3 == 3 ? "৩ নম্বর বিজ্ঞপ্তি দেখুন " : null;
        //            model.StringNoticeSentDate = $"{model.NoticeSentDate:dd/MM/yyyy HH:mm:ss tt}";

        //            vm.Add(model);
        //        }

        //        Data_Reader.Close();
        //        Sql_Connection.Close();

        //        return vm;
        //    }
        //    catch (SqlException exception)
        //    {
        //        for (int i = 0; i < exception.Errors.Count; i++)
        //        {
        //            ErrorMessages.Append("Index #" + i + "\n" +
        //                "Message: " + exception.Errors[i].Message + "\n" +
        //                "Error Number: " + exception.Errors[i].Number + "\n" +
        //                "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
        //                "Source: " + exception.Errors[i].Source + "\n" +
        //                "Procedure: " + exception.Errors[i].Procedure + "\n");
        //        }
        //        throw new Exception(ErrorMessages.ToString());
        //    }
        //    finally
        //    {
        //        if (Sql_Connection.State == ConnectionState.Open)
        //            Sql_Connection.Close();
        //    }

        //}

        public List<Notice> GetAllNoticeForHolder(int id)
        {
            try
            {
                Sql_Query = "[Holding].[spNoticeMaster]";
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

                List<Notice> vm = new List<Notice>();

                while (Data_Reader.Read())
                {
                    Notice model = new Notice
                    {
                        HolderId = Convert.ToInt32(Data_Reader["HolderId"]),
                        HolderName = Convert.ToString(Data_Reader["HolderName"]),
                        AreaId = Convert.ToInt32(Data_Reader["AreaId"]),
                        AreaName = Convert.ToString(Data_Reader["AreaName"]),
                        CreateDate = Data_Reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["CreateDate"]) : (DateTime?)null,
                        CreatedByUsername = Data_Reader["CreatedByUsername"].ToString(),
                        UpdatedByUsername = Data_Reader["UpdatedByUserName"].ToString(),
                        LastUpdated = Data_Reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["LastUpdated"]) : (DateTime?)null,
                        IsActive = Data_Reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsActive"]) : (bool?)null,
                        IsDeleted = Data_Reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsDeleted"]) : (bool?)null,
                        CreatedBy = Data_Reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["CreatedBy"]) : (int?)null,
                        LastUpdatedBy = Data_Reader["LastUpdatedBy"] != DBNull.Value ? Convert.ToInt32(Data_Reader["LastUpdatedBy"]) : (int?)null,
                        AreaPlotFlatData = Convert.ToString(Data_Reader["AreaPlotFlatData"]),
                        FinancialYear = Convert.ToString(Data_Reader["FinancialYear"]),
                        FinancialYearId = Convert.ToInt32(Data_Reader["FinancialYearId"]),
                        IsNoticeSent = Data_Reader["IsNoticeSent"] != DBNull.Value ? Convert.ToBoolean(Data_Reader["IsNoticeSent"]) : (bool?)null,
                        NoticeId = Convert.ToInt64(Data_Reader["NoticeId"]),
                        NoticeLinkName = Convert.ToString(Data_Reader["NoticeLinkName"]),
                        NoticeName = "",
                        NoticeSentDate = Data_Reader["NoticeSentDate"] != DBNull.Value ? Convert.ToDateTime(Data_Reader["NoticeSentDate"]) : (DateTime?)null,
                        NoticeTypeId = Convert.ToInt32(Data_Reader["NoticeTypeId"]),
                        EmployeeName = Convert.ToString(Data_Reader["EmployeeName"]),
                        DesignationName = Convert.ToString(Data_Reader["DesignationName"]),
                    };
                    if (model.NoticeTypeId == 1)
                        model.NoticeName = "গৃহকরের প্রাথমিক বিজ্ঞপ্তি দেখুন";
                    else if (model.NoticeTypeId == 2)
                        model.NoticeName = "রিবেটসহ গৃহকর প্রাপ্তির বিজ্ঞপ্তি দেখুন";
                    else if (model.NoticeTypeId == 3)
                        model.NoticeName = "গৃহকরের চূড়ান্ত বিজ্ঞপ্তি দেখুন";

                    model.StringNoticeSentDate = $"{model.NoticeSentDate:dd/MM/yyyy HH:mm:ss tt}";

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

        public int SendNotice(Notice model)
        {
            try
            {
                Sql_Query = "[Holding].[spNoticeMaster]";
                Sql_Command = new SqlCommand
                {
                    CommandText = Sql_Query,
                    Connection = Sql_Connection,
                    CommandType = CommandType.StoredProcedure
                };

                Sql_Command.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = CommonConstantHelper.Insert;

                Sql_Command.Parameters.Add("@NoticeId", SqlDbType.BigInt).Value = 0;
                Sql_Command.Parameters.Add("@FinancialYearId", SqlDbType.Int).Value = model.FinancialYearId;
                Sql_Command.Parameters.Add("@AreaId", SqlDbType.Int).Value = model.AreaId;
                Sql_Command.Parameters.Add("@HolderId", SqlDbType.Int).Value = 0;
                Sql_Command.Parameters.Add("@NoticeTypeId", SqlDbType.Int).Value = model.NoticeTypeId;
                Sql_Command.Parameters.Add("@IsNoticeSent", SqlDbType.Bit).Value = model.IsNoticeSent;
                Sql_Command.Parameters.Add("@NoticeSentDate", SqlDbType.DateTime).Value = model.NoticeSentDate;
                Sql_Command.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = model.CreateDate;
                Sql_Command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                Sql_Command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = model.LastUpdated;
                Sql_Command.Parameters.Add("@LastUpdatedBy", SqlDbType.Int).Value = model.LastUpdatedBy;
                Sql_Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                Sql_Command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = model.IsDeleted;
                Sql_Command.Parameters.Add("@EmpolyeeId", SqlDbType.Int).Value = model.EmpolyeeId;

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