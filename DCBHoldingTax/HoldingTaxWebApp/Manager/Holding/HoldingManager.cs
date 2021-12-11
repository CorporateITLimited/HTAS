using HoldingTaxWebApp.Gateway.Holding;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Holding;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Holding
{
    public class HoldingManager
    {
        public readonly HoldingGateway _holdingGateway;
        public HoldingManager()
        {
            _holdingGateway = new HoldingGateway();
        }

        #region Holder
        public List<Holder> GetAllHolder()
        {
            return _holdingGateway.GetAllHolder();
        }

        public List<Holder> GetAllUnapproveHolder()
        {
            return _holdingGateway.GetAllUnapproveHolder();
        }

        public Holder GetHolderById(int id)
        {
            return _holdingGateway.GetHolderById(id);
        }

        public List<Holder> GetHolderByAreaIdAndPlotId(int AreaId, int PlotId)
        {
            return _holdingGateway.GetHolderByAreaIdAndPlotId(AreaId, PlotId);
        }

        public int InsertHolder(Holder model)
        {
            return _holdingGateway.InsertHolder(model);
        }

        public int UpdateApprove()
        {
            return _holdingGateway.UpdateApprove();
        }

        public int ApproveInformation(Holder model)
        {
            return _holdingGateway.ApproveInformation(model);
        }

        public bool IsHolderNoExist(string HolderNo, int HolderId)
        {
            return _holdingGateway.IsHolderNoExist(HolderNo, HolderId);
        }
        public int UpdateHolder(Holder model)
        {
            return _holdingGateway.UpdateHolder(model);
        }

        public int UpdateHolderProfile(Holder model)
        {
            return _holdingGateway.UpdateHolderProfile(model);
        }

        #endregion

        #region holder flat
        public HolderFlat GetHoldersFlatByHolderFlatId(int id)
        {
            return _holdingGateway.GetHoldersFlatByHolderFlatId(id);
        }
        public List<HolderFlat> GetHoldersFlatByHolderId(int id)
        {
            return _holdingGateway.GetHoldersFlatByHolderId(id);
        }

        public List<HolderFlat> GetHoldersFlatByMainHolderId(int id)
        {
            return _holdingGateway.GetHoldersFlatByMainHolderId(id);
        }

        public string HoldersFlatInsert(HolderFlat model)
        {
            int result = _holdingGateway.HoldersFlatInsert(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public int HoldersFlatTransfer(HolderFlat model)
        {
            return _holdingGateway.HoldersFlatTransfer(model);
        }

        public string HoldersFlatInActive(HolderFlat model)
        {
            int result = _holdingGateway.HoldersFlatInActive(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string HoldersFlatUpdate(HolderFlat model)
        {
            int result = _holdingGateway.HoldersFlatUpdate(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string HoldersFlatUpdateForMainHolder(HolderFlat model)
        {
            int result = _holdingGateway.HoldersFlatUpdateForMainHolder(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string HoldersFlatUpdateForMainHolderProfile(HolderFlat model)
        {
            int result = _holdingGateway.HoldersFlatUpdateForMainHolderProfile(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public int DeleteHoldersFlatDataByHolderId(int HolderId)
        {
            return _holdingGateway.DeleteHoldersFlatDataByHolderId(HolderId);
        }

        #endregion

        #region frontend queries
        public List<HolderFlat> GetAllFlatByPlotId(int PlotId)
        {
            return _holdingGateway.GetAllFlatByPlotId(PlotId);
        }

        public List<HolderFlat> GetAllFlatByPlotIdForEdit(int PlotId, int HolderId)
        {
            return _holdingGateway.GetAllFlatByPlotIdForEdit(PlotId, HolderId);
        }
        public List<HolderFlat> GetAllFlatByAreaAndPlotId(int AreaId, int PlotId)
        {
            return _holdingGateway.GetAllFlatByAreaAndPlotId(AreaId, PlotId);
        }
        public List<HolderFlat> GetAllFlatByHolderId(int HolderId)
        {
            return _holdingGateway.GetAllFlatByHolderId(HolderId);
        }
        public decimal GetPerSqrFeetPrice(int areaId, int buildingTypeId)
        {
            return _holdingGateway.GetPerSqrFeetPrice(areaId, buildingTypeId);
        }

        public int GetMAXId()
        {
            return _holdingGateway.GetMAXId();
        }

        public HolderVM GetAllotmentNamjariDesignByPlotId(int PlotId)
        {
            return _holdingGateway.GetAllotmentNamjariDesignByPlotId(PlotId);
        }

        public List<Holder> GetHolderIndexData(int? AreaId, int? PlotId)
        {
            return _holdingGateway.GetHolderIndexData(AreaId, PlotId);
        }
        #endregion
    }
}