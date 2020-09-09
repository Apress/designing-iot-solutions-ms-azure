using Microsoft.Azure.Devices;
using System;
using System.Threading.Tasks;

namespace IoT.Solutions.SmartIndustrialApplications.CreateDevice
{
    /// <summary>
    /// Program to create a new device identity in Azure IoT Hub.
    /// </summary>
    class Program
    {
        private static RegistryManager registryManager;

        //To get the IoT hub connection string go to shared access policies and click iothubowner. 
        private static readonly string connectionString = "<Connection string here>";
        
        private static readonly string deviceId = "<device id>";

        /// <summary>
        /// Register a new device with the system
        /// </summary>
        /// <returns>The device</returns>
        private async static Task<Device> AddDeviceAsync()
        {
            //Register a new device with the system
            var device = await registryManager.AddDeviceAsync(new Device(deviceId));

            Console.WriteLine("Generated Symmetric Key for Authentication: {0}", device.Authentication.SymmetricKey.PrimaryKey);

            return device;
        }

        /// <summary>
        /// Removes the supplied device from the Azure IoT Hub.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        public static async Task RemoveDevice(Device device)
        {
            //Deletes a previously registered device from the system
            await registryManager.RemoveDeviceAsync(device);

            Console.WriteLine($"Removed device with the id: {device.Id}.");
        }

        static async Task Main(string[] args)
        {
            //Creates a RegistryManager from the Iot Hub connection string.
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);

            var device = await AddDeviceAsync();

            //Cleanup
            //await RemoveDevice(device);

            Console.ReadKey();
        }
    }
}