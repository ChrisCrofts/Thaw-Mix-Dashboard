using System;
using System.IO;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Data.SqlClient;
using System.Linq;

namespace Thaw_Mix_Dashboard
{
    class ESPController : IDisposable
    {
        private string connectionString = "Data Source=ESP-ODS;Initial Catalog=ContainerManagement-ODS;Persist Security Info=True;User ID=SuppSrvcsIT;Password=p1sGMe#mnzNd0%cP";

        private ESPConnectionDataContext espodsConnectionDataContext;

        public string GetTemperatureID(string trkNumber)
        {
            int trkNum = Convert.ToInt32(trkNumber);

            if (trkNumber.Length == 9 || trkNumber.Length == 10)
            {
                espodsConnectionDataContext = new ESPConnectionDataContext(connectionString);
                RoutedContainer routedContainer = espodsConnectionDataContext.RoutedContainers.Where(i => i.TrackingNumber == trkNum).FirstOrDefault();
                if (routedContainer == null)
                {
                    return ($"{trkNumber} is not a valid tracking number.");
                }
                return routedContainer.TemperatureId.ToString();
            }
            else
            {
                return ($"{trkNumber} is not a valid tracking number.");
            }
            
        }

        public string InLabStatus(string trkNumber)
        {
            int trkNum = Convert.ToInt32(trkNumber);

            if (trkNumber.Length == 9 || trkNumber.Length == 10)
            {
                espodsConnectionDataContext = new ESPConnectionDataContext(connectionString);
                RoutedContainer routedContainer = espodsConnectionDataContext.RoutedContainers.Where(i => i.TrackingNumber == trkNum).FirstOrDefault();
                long containerID = routedContainer.ContainerId;

                Container_OrderedTest container_OrderedTest = espodsConnectionDataContext.Container_OrderedTests.Where(i => i.ContainerId == containerID).FirstOrDefault();
                long orderedTestID = container_OrderedTest.OrderedTestId;

                OrderedTest orderedTest = espodsConnectionDataContext.OrderedTests.Where(i => i.OrderedTestId == orderedTestID).FirstOrDefault();

                if (routedContainer == null)
                {
                    return ($"{trkNumber} is not a valid tracking number.");
                }
                return orderedTest.LisStatusCodeId.ToString();
            }
            else
            {
                return ($"{trkNumber} is not a valid tracking number.");
            }
        }

        public void Dispose()
        {
            espodsConnectionDataContext.Dispose();
        }
    }
}