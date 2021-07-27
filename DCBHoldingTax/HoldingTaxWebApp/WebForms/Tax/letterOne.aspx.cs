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

            //done for two separate subreport ==================================================
            int? rptValueAreaId = 1;
            int? rptFinancialYearId = 2;
            int? rptHolderId = 1;
            int? rptHoldingTaxId = null;
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStrHTAS"].ConnectionString);
            SqlCommand cmd = new SqlCommand("exec [Tax].[spGetHoldingTaxDetails] @AreaId, @FinancialYearId, @HolderId, @HoldingTaxId", con);
            cmd.CommandType = CommandType.Text; // always text
            cmd.Parameters.AddWithValue("@AreaId", SqlDbType.Int).Value = rptValueAreaId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@FinancialYearId", SqlDbType.Int).Value = rptFinancialYearId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@HolderId", SqlDbType.Int).Value = rptHolderId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@HoldingTaxId", SqlDbType.Int).Value = rptHoldingTaxId ?? (object)DBNull.Value;

            dsTax list = new dsTax(); // same as dataset
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                
                sda.Fill(list, "dtHoldingTax");
            }
            catch (Exception ex)
            {
                throw ex;
            }





            SqlCommand cmd2 = new SqlCommand("exec [constant].[spRentTaxRate] @StatementType=@StatementType,@result=Null", con);
            cmd2.CommandType = CommandType.Text; // always text

            cmd2.Parameters.AddWithValue("@StatementType", SqlDbType.NVarChar).Value = "selectForResidential";
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                
                sda.Fill(list, "dtAreaRentRate");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }




            CrystalReportViewer1.ReportSource = cryRpt;
            cryRpt.SetDataSource(list);

            CrystalReportViewer1.Zoom(100);
            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer1.HasExportButton = false;
        }

    }
}