﻿using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using HoldingTaxWebApp.AppDataSet;
using HoldingTaxWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HoldingTaxWebApp.WebForms.Tax
{
    public partial class leterThree : System.Web.UI.Page
    {
        private ReportDocument cryRpt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                cryreportshow();
            else
                cryreportshow();
        }

        private void cryreportshow()
        {
            cryRpt = new ReportDocument();
            cryRpt.Load(Server.MapPath("~/AppReports/Tax/rptleterThree.rpt"));
            cryRpt.SetDatabaseLogon("sa", "#PimsOne$1m#", @"119.18.146.107", "DCB_HTAS");

            CrystalReportViewer2.ReportSource = cryRpt;

            dsTax tenderdata = Getdata(); // datasetname
            cryRpt.SetDataSource(tenderdata);

            CrystalReportViewer2.Zoom(100);
            CrystalReportViewer2.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer2.HasExportButton = false;
        }
        private dsTax Getdata()    /*-----Return type is Dataset--------*/
        {
            
            
            //int? rptValueAreaId = Session[CommonConstantHelper.AreaId] != null ? Convert.ToInt32(Session[CommonConstantHelper.AreaId]) : (int?)null;
            //int? rptFinancialYearId = Session["FinancialYearId"] != null ? Convert.ToInt32(Session["FinancialYearId"]) : (int?)null;
            //int? rptHolderId = Session[CommonConstantHelper.HolderId] != null ? Convert.ToInt32(Session[CommonConstantHelper.HolderId]) : (int?)null;

            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStrHTAS"].ConnectionString);
            SqlCommand cmd = new SqlCommand("exec [Tax].[spletterThree] @FinancialYearId,@HolderId", con);
            cmd.CommandType = CommandType.Text; // always text
            //cmd.Parameters.AddWithValue("@AreaId", SqlDbType.Int).Value = rptValueAreaId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@FinancialYearId", SqlDbType.Int).Value = 2; //rptFinancialYearId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@HolderId", SqlDbType.Int).Value = 10;//rptHolderId ?? (object)DBNull.Value;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dsTax list = new dsTax(); // same as dataset
                sda.Fill(list, "dtLetterThree");
                //for (int i = 0; i < list.dtListOfQuotedItems.Count(); i++)
                //{
                //    list.dtListOfQuotedItems.Rows[i][13] = "List of offered Lowest Quoted Items";
                //}

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}