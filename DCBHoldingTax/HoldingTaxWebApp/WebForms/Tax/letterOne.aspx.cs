using CrystalDecisions.CrystalReports.Engine;
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
    public partial class letterOne : System.Web.UI.Page
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
            cryRpt.Load(Server.MapPath("~/AppReports/Tax/rptletterOne.rpt"));
            cryRpt.SetDatabaseLogon("sa", "#PimsOne$1m#", @"119.18.146.107", "DCB_HTAS");

            CrystalReportViewer1.ReportSource = cryRpt;

            dsTax tenderdata = Getdata(); // datasetname
            cryRpt.SetDataSource(tenderdata);

            CrystalReportViewer1.Zoom(100);
            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer1.HasExportButton = false;
        }
        private dsTax Getdata()    /*-----Return type is Dataset--------*/
        {
            //PMISEntities con = new PMISEntities();
            //Session["PCaseId"] = 2;
            int? rptValueAreaId = null;//Session[CommonConstantHelper.AreaId] != null ? Convert.ToInt32(Session[CommonConstantHelper.AreaId]) : (int?)null;
            int? rptFinancialYearId = Session["FinacialYearID"] != null ? Convert.ToInt32(Session["FinacialYearID"]) : (int?)null;
            int? rptHolderId = Session["HolderID"] != null ? Convert.ToInt32(Session["HolderID"]) : (int?)null;
            int? rptHoldingTaxId = null;//Session["HoldingTaxId"] != null ? Convert.ToInt32(Session["HoldingTaxId"]) : (int?)null;

            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStrHTAS"].ConnectionString);
            SqlCommand cmd = new SqlCommand("exec [Tax].[spGetHoldingTaxDetails] @AreaId, @FinancialYearId, @HolderId, @HoldingTaxId", con);
            cmd.CommandType = CommandType.Text; // always text
            cmd.Parameters.AddWithValue("@AreaId", SqlDbType.Int).Value = rptValueAreaId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@FinancialYearId", SqlDbType.Int).Value = rptFinancialYearId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@HolderId", SqlDbType.Int).Value = rptHolderId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@HoldingTaxId", SqlDbType.Int).Value = rptHoldingTaxId ?? (object)DBNull.Value;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dsTax list = new dsTax(); // same as dataset
                sda.Fill(list, "dtHoldingTax");
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