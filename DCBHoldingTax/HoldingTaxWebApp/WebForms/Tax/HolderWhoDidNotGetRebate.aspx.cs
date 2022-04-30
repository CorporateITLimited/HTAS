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
    public partial class HolderWhoDidNotGetRebate : System.Web.UI.Page
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
            cryRpt.Load(Server.MapPath("~/AppReports/Tax/crHolderWhoDidNotGetRebate.rpt"));
            cryRpt.SetDatabaseLogon("sa", "#PimsOne$1m#", @"119.18.146.107", "DCB_HTAS");

            CrystalReportViewer1.ReportSource = cryRpt;

            dsForRebate taxData = Getdata(); // datasetname
            cryRpt.SetDataSource(taxData);

            CrystalReportViewer1.Zoom(100);
            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer1.HasExportButton = false;
        }
        private dsForRebate Getdata()    /*-----Return type is Dataset--------*/
        {
            int? rptValueAreaId = Session["rebate_AreaID"] != null ? Convert.ToInt32(Session["rebate_AreaID"]) : (int?)null;
            int? rptFinancialYearId = Session["rebate_FYID"] != null ? Convert.ToInt32(Session["rebate_FYID"]) : (int?)null;
            int? rptPlotId = Session["rebate_PlotID"] != null ? Convert.ToInt32(Session["rebate_PlotID"]) : (int?)null;

            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStrHTAS"].ConnectionString);
            SqlCommand cmd = new SqlCommand("exec [rpt].[spHoldersWhoDidNotGetRebate] @FinancialYearId, @AreaId, @PlotId", con);
            cmd.CommandType = CommandType.Text; // always text
            cmd.Parameters.AddWithValue("@FinancialYearId", SqlDbType.Int).Value = rptFinancialYearId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@AreaId", SqlDbType.Int).Value = rptValueAreaId ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@PlotId", SqlDbType.Int).Value = rptPlotId ?? (object)DBNull.Value;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dsForRebate list = new dsForRebate(); // same as dataset
                sda.Fill(list, "dtHoldersWhoGetOrNotGetRebate");
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