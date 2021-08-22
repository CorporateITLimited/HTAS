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

namespace HoldingTaxWebApp.WebForms.Administrative
{
    public partial class LoginHistory : System.Web.UI.Page
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
            cryRpt.Load(Server.MapPath("~/AppReports/Administrative/rptLoginHistory.rpt"));
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

            string rptuserName = Session["userName"] != null ? Convert.ToString(Session["userName"]) : null;
            DateTime? rptfromDate = Session["fromDate"] != null ? Convert.ToDateTime(Session["fromDate"]) : (DateTime?)null;
            DateTime? rpttoDate = Session["toDate"] != null ? Convert.ToDateTime(Session["toDate"]) : (DateTime?)null;
            int? rptuserType = Session["userType"] != null ? Convert.ToInt32(Session["userType"]) : (int?)null;

            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStrHTAS"].ConnectionString);
            SqlCommand cmd = new SqlCommand("exec [rpt].[spLoginHistoryAll] @userName, @fromDate, @toDate, @userType", con);
            cmd.CommandType = CommandType.Text; // always text

            cmd.Parameters.AddWithValue("@userName", SqlDbType.VarChar).Value = rptuserName ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@fromDate", SqlDbType.DateTime).Value = rptfromDate ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.DateTime).Value = rpttoDate ?? (object)DBNull.Value;
            cmd.Parameters.AddWithValue("@userType", SqlDbType.Int).Value = rptuserType ?? (object)DBNull.Value;

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dsTax list = new dsTax(); // same as dataset
                sda.Fill(list, "dtLoginHistory");
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