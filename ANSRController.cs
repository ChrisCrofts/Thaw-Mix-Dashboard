using System;
using System.IO;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Data.SqlClient;
using System.Linq;

namespace Thaw_Mix_Dashboard
{
    class ANSRController : IDisposable
    {
        private string connectionString = "Data Source=ANSRLTR;Initial Catalog=dbANSRLTR_PROD;Persist Security Info=True;User ID=SuppSrvcsIT;Password=p1sGMe#mnzNd0%cP";

        private AnsrConnectionDataContext ansrConnectionDataContext;

        public IQueryable<string> GetDepartmentCodeAndName()
        {
            return ansrConnectionDataContext.tblDepartments.Select(x => x.DEPT_ID.ToString() + " " + x.NAME);
        }

        public string GetEmployeeName(string lanId)
        {
            tblEmployee tblEmployee = ansrConnectionDataContext.tblEmployees.Where(i => i.LAN_ID == lanId).FirstOrDefault();

            if(tblEmployee != null)
            {
                return tblEmployee.LAST_NAME + ", " + tblEmployee.FIRST_NAME;
            }
            else
            {
                throw new Exception($"{lanId} is not a valid Employee ID");
            }
        }

        public IQueryable<string> GetEmployees()
        {
            return ansrConnectionDataContext.tblEmployees.Select(i => i.LAST_NAME + ", " + i.FIRST_NAME);
        }

        public string GetEmployeeDepartment(string lanId)
        {
            ansrConnectionDataContext = new AnsrConnectionDataContext(connectionString);

            tblEmployee tblEmployee = ansrConnectionDataContext.tblEmployees.Where(i => i.LAN_ID == lanId).FirstOrDefault();

            if (tblEmployee != null)
            {
                return tblEmployee.DEPT_ID.ToString();
            }
            else
            {
                return ($"{lanId} is not a valid Employee ID");
            }
        }

        public void Dispose()
        {
            ansrConnectionDataContext.Dispose();
        }
    }
}
