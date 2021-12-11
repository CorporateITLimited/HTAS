using HoldingTaxWebApp.Gateway.Plots;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Plots;
using HoldingTaxWebApp.ViewModels.Plots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Plots
{
    public class PlotOwnerManager
    {
        private readonly PlotOwnerGateway _PlotOwnerGateway;

        public PlotOwnerManager()
        {
            _PlotOwnerGateway = new PlotOwnerGateway();
        }


        #region PlotOwner & ConstructionProgress & UnauthPortion

        //Get All Plot Owner List
        public List<PlotOwner> GetAllPlotOwner()
        {
            return _PlotOwnerGateway.GetAllPlotOwner();

        }

        //Get PlotOwner By Id
        public PlotOwner GetPlotOwnerById(int id)
        {
            return _PlotOwnerGateway.GetPlotOwnerById(id);

        }

        //Get Construction Progress By Id
        public ConstructionProgress GetConstructionProgressById(int id)
        {
            return _PlotOwnerGateway.GetConstructionProgressById(id);

        }


        //Get Unauth Portion By Id
        public UnauthPortion GetUnauthPortionById(int id)
        {
            return _PlotOwnerGateway.GetUnauthPortionById(id);

        }

        //get Plot for select 
        public List<Plot> GetPlot()
        {
            return _PlotOwnerGateway.GetPlot();

        }

        //get present addres for plotno change event

        public Plot GetPresentAddress(int id)
        {
            return _PlotOwnerGateway.GetPresentAddress(id);
        }


        //Create Plot Owner Details
        public int PlotOwnerInsert(PlotOwnerCombineVM model)
        {
            return _PlotOwnerGateway.PlotOwnerInsert(model);

        }

        //Update Plot Owner Details
        public int PlotOwnerUpdate(PlotOwnerCombineVM model)
        {
            return _PlotOwnerGateway.PlotOwnerUpdate(model);
        }

        #endregion



        #region Details Portion  Other Plot Owner

        //Get All Plot Owner List
        public List<OthetPlotOwner> GetOthetPlotOwnerById(int id)
        {
            return _PlotOwnerGateway.GetOthetPlotOwnerById(id);
        }






        //Create Plot Owner Details
        public string OthetPlotOwnerInsert(OthetPlotOwner model)
        {
            int result = _PlotOwnerGateway.OthetPlotOwnerInsert(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }



        //Delete Plot Owner Details
        public string OthetPlotOwnerDelete(int id)
        {

            int result = _PlotOwnerGateway.OthetPlotOwnerDelete(id);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }

        #endregion


        #region Details Portion  Design Approval

        //Get All Design Approval List
        public List<DesignApproval> GetDesignApprovalById(int id)
        {
            return _PlotOwnerGateway.GetDesignApprovalById(id);

        }


        //Create Design Approval Details
        public string DesignApprovalInsert(DesignApproval model)
        {
            int result = _PlotOwnerGateway.DesignApprovalInsert(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }



        //Delete Design Approval Details
        public string DesignApprovalDelete(int id)
        {
            int result = _PlotOwnerGateway.DesignApprovalDelete(id);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }

        #endregion

        public PlotOwner GetPlotOwnerByPlotId(int id)
        {
            return _PlotOwnerGateway.GetPlotOwnerByPlotId(id);
        }

    }
}