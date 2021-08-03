using HoldingTaxWebApp.Gateway.Plots;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Plots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Plots
{
    public class OldPlotOwnerManager
    {
        private readonly OldPlotOwnerGateway _OldPlotOwnerGateway;

        public OldPlotOwnerManager()
        {
            _OldPlotOwnerGateway = new OldPlotOwnerGateway();
        }

        #region Old Plot Owner 
        //Get All Plot Owner List
        public List<OldPlotOwner> GetAllOldPlotOwner()
        {

            return _OldPlotOwnerGateway.GetAllOldPlotOwner();
        }

        //Get PlotOwner By Id
        public OldPlotOwner GetOldPlotOwnerById(int id)
        {
            return _OldPlotOwnerGateway.GetOldPlotOwnerById(id);

        }


        //get Plot for select 
        public List<Plot> GetPlot()
        {
            return _OldPlotOwnerGateway.GetPlot();

        }



        //Get Data
        public PlotOwner GetPlotOwnerData(int id)
        {
            return _OldPlotOwnerGateway.GetPlotOwnerData(id);

        }


        //Create Plot Owner Details
        public int OldPlotOwnerInsert(OldPlotOwner model)
        {
            return _OldPlotOwnerGateway.OldPlotOwnerInsert(model);

        }

        //Update Plot Owner Details
        public string OldPlotOwnerUpdate(OldPlotOwner model)
        {
            int result = _OldPlotOwnerGateway.OldPlotOwnerUpdate(model);
            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 401)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        #endregion


        #region Details Portion Old Other Plot Owner

        //Get   All old Plot Owner List
        public List<OldOthetPlotOwner> GetOldOthetPlotOwnerById(int id)
        {
            return _OldPlotOwnerGateway.GetOldOthetPlotOwnerById(id);

        }


        //Create old Plot Owner Details
        public string OldOthetPlotOwnerInsert(int id, int oldid)
        {
            int result = _OldPlotOwnerGateway.OldOthetPlotOwnerInsert(id,oldid);
            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 401)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }



        //Delete Plot Owner Details
        public string OldOthetPlotOwnerDelete(int id)
        {
            int result = _OldPlotOwnerGateway.OldOthetPlotOwnerDelete(id);
            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 401)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        #endregion
    }
}