using HoldingTaxWebApp.Gateway.Dbo;
using HoldingTaxWebApp.Models.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.DBO
{
    public class BuildingTypeManager
    {
        private readonly BuildingTypeGateway _buildingTypeGateway;
        public BuildingTypeManager()
        {
            _buildingTypeGateway = new BuildingTypeGateway();
        }

        public List<BuildingType> GetAllBuildingType()
        {
            return _buildingTypeGateway.GetAllBuildingType();
        }
    }
}