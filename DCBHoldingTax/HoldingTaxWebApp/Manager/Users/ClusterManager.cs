using HoldingTaxWebApp.Gateway.Users;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Users
{
    public class ClusterManager
    {
        private readonly ClusterGateway _gateway;

        public ClusterManager()
        {
            _gateway = new ClusterGateway();
        }

        public List<Cluster> GetAllCluster()
        {
            return _gateway.GetAllCluster();
        }

        public List<Cluster> GetAllActiveCluster()
        {
            return _gateway.GetAllActiveCluster();
        }

        public Cluster GetClusterById(int id)
        {
            return _gateway.GetClusterById(id);
        }


        public string ClusterInsert(Cluster cluster)
        {
            int result = _gateway.ClusterInsert(cluster);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string ClusterUpdate(Cluster cluster)
        {
            int result = _gateway.ClusterUpdate(cluster);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }


    }
}