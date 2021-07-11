using HoldingTaxWebApp.Gateway.Plots;
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
    }
}