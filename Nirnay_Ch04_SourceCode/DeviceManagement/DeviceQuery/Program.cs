using Microsoft.Azure.Devices;
using System;
using System.Threading.Tasks;

namespace IoT.Solutions.SmartIndustrialApplications.DeviceTwin
{
    /// <summary>
    /// Program to query and retrive the devices
    /// </summary>
    class Program
    {
        //To get the IoT hub connection string go to shared access policies and click service. 
        private static readonly string connectionString = "<Connection string here>";

        /// <summary>
        /// Retrieves the next paged result as Twin objects
        /// </summary>
        /// <returns></returns>
        private static async Task QueryAsync()
        {
            //Creates a RegistryManager from the Iot Hub connection string.
            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);

            //Define the query to be executed
            var query = registryManager.CreateQuery("SELECT * FROM devices", 10);

            while (query.HasMoreResults)
            {
                // Retrieves the next paged result
                var page = await query.GetNextAsTwinAsync();
                foreach (var device in page)
                {
                    Console.WriteLine(device.DeviceId);
                }
            }
        }

        static async Task Main(string[] args)
        {
            await QueryAsync();

            Console.ReadKey();
        }
    }
}
