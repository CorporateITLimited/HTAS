using HoldingTaxWebApp.Gateway.DBO;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.DBO
{
    public class IssueManager
    {
        private readonly IssueGateway _IssueGateway;

        public IssueManager()
        {
            _IssueGateway = new IssueGateway();
        }

        #region Issue Portion
        //Get All Issue List
        public List<Issue> GetAllIssue()
        {
            return _IssueGateway.GetAllIssue();

        }


        //Get Top 5 Issue List
        public List<Issue> GetTopFiveIssue()
        {
            return _IssueGateway.GetTopFiveIssue();

        }






        //Get Issue By Id
        public Issue GetIssueById(int id)
        {
            return _IssueGateway.GetIssueById(id);

        }

        //Create Issue  
        public int IssueInsert(Issue model)
        {
            return _IssueGateway.IssueInsert(model);

        }

        //Update Issue  
        public int IssueUpdate(Issue model)
        {
            return _IssueGateway.IssueUpdate(model);
        }
        #endregion


        #region Issue Details Portion
        //Get All Issue Details by Issue Id
        public List<IssueDetails> GetAllIssueDetailsByIssueId(int Id)
        {
            return _IssueGateway.GetAllIssueDetailsByIssueId(Id);

        }

        //Create Issue  Details
        public string IssueDetailsInsert(IssueDetails model)
        {
            int result = _IssueGateway.IssueDetailsInsert(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }

        //Update Issue Details
        public string IssueDetailsUpdate(IssueDetails model)
        {
            int result = _IssueGateway.IssueDetailsUpdate(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        //Update Issue Details
        public string IssueDetailsDelete(int Id)
        {
            int result = _IssueGateway.IssueDetailsDelete(Id);

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

    }
}