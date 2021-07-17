using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using HoldingTaxWebApp.AppDataSet;
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
    public partial class GetHoldingTax : System.Web.UI.Page
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
            cryRpt.Load(Server.MapPath("~/AppReports/Tax/rptGetHoldingTax.rpt"));
            //cryRpt.SetDatabaseLogon("sa", "CIL###MjR019", @"119.18.147.80", "BR_PIMS1_DB");

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

            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStrHTAS"].ConnectionString);
            SqlCommand cmd = new SqlCommand("exec [Tax].[spGetHoldingTaxDetails] @AreaId,@FinancialYearId,@HolderId", con);
            cmd.CommandType = CommandType.Text; // always text
            cmd.Parameters.AddWithValue("@AreaId", SqlDbType.Int).Value = 1;//Convert.ToInt32(Session["TenderDocId"]);
            cmd.Parameters.AddWithValue("@FinancialYearId", SqlDbType.Int).Value = 1;//Convert.ToInt32(Session["TenderDocId"]);
            cmd.Parameters.AddWithValue("@HolderId", SqlDbType.Int).Value = 2;//Convert.ToInt32(Session["TenderDocId"]);
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