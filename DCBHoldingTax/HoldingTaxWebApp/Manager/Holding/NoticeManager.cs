using HoldingTaxWebApp.Gateway.Holding;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Holding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Holding
{
    public class NoticeManager
    {
        private readonly NoticeGateway _notice;
        public NoticeManager()
        {
            _notice = new NoticeGateway();
        }
        public List<Notice> GetAllNotice()
        {
            return _notice.GetAllNotice();
        }

        public List<Notice> GetAllNoticeForHolder(int id)
        {
            return _notice.GetAllNoticeForHolder(id);
        }

        public string PrepareNotice(Notice model)
        {
            int result = _notice.PrepareNotice(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 401)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }
        public string SendNotice(Notice model)
        {
            int result = _notice.SendNotice(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 401)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public List<Notice> GetAllNoticeFiltering(int? FinancialYearId, int? NoticeTypeId, int? AreaId, int? PlotId)
        {
            return _notice.GetAllNoticeFiltering(FinancialYearId, NoticeTypeId, AreaId, PlotId);
        }
    }
}