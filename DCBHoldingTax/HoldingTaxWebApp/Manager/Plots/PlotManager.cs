using HoldingTaxWebApp.Gateway.Plots;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Plots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Plots
{
    public class PlotManager
    {
        private readonly PlotGateway _plotGateway;
        public PlotManager()
        {
            _plotGateway = new PlotGateway();
        }

        public List<Plot> GetAllPlot()
        {
            return _plotGateway.GetAllPlot();
        }

        public Plot GetPlotById(int id)
        {
            return _plotGateway.GetPlotById(id);
        }

        public string PlotGatewayUpdate(Plot Plot)
        {
            int result = _plotGateway.PlotGatewayUpdate(Plot);
            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 401)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string PlotGatewayInsert(Plot Plot)
        {
            int result = _plotGateway.PlotGatewayInsert(Plot);
            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 401)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }
    }
}
