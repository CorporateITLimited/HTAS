using HoldingTaxWebApp.Gateway.Users;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Users
{
    public class EmployeeManager
    {
        private readonly EmployeeGateway _employeeGateway;

        public EmployeeManager()
        {
            _employeeGateway = new EmployeeGateway();
        }

        public List<Employee> GetAllEmployeeList()
        {
            return _employeeGateway.GetAllEmployeeList();
        }

        public List<Employee> GetAllEmployeeListForSelect()
        {
            return _employeeGateway.GetAllEmployeeListForSelect();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeGateway.GetEmployeeById(id);
        }

        public string EmployeeInsert(Employee employee)
        {
            int result = _employeeGateway.EmployeeInsert(employee);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }

        public string EmployeeUpdate(Employee employee)
        {
            int result = _employeeGateway.EmployeeUpdate(employee);

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