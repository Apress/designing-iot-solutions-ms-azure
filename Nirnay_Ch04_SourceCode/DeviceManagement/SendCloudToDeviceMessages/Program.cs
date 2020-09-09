using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IoT.Solutions.SmartIndustrialApplications.SendC2DMessages
{
    /// <summary>
    /// Program to Send structural and non-structural data event to Azure IoT Hub.
    /// </summary>
    class Program
    {
        //To get the IoT hub connection string go to shared access policies and click service. 
        private static readonly string connectionString = "<Connection string here>";

        private static readonly string deviceId = "<device id>";

        /// <summary>
        /// Sends structural data event to Azure IoT Hub.
        /// </summary>
        /// <returns></returns>
        private async static Task SendSensorData()
        {
            //The message to send. Should be disposed after sending.
            var telemetryDataPoint = new
            {
                deviceId = deviceId,
                temperature = 30.3,
            };

            var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            var deviceClient = DeviceClient.CreateFromConnectionString(connectionString, deviceId, TransportType.Http1);
            await deviceClient.SendEventAsync(message);
        }

        /// <summary>
        /// Sends non-structural data event to Azure IoT Hub.
        /// </summary>
        /// <returns></returns>
        private static async Task SendUnstructuredDataAsync()
        {
            string fileName = "images.jpeg";

            var deviceClient = DeviceClient.CreateFromConnectionString(connectionString, deviceId, TransportType.Mqtt_Tcp_Only);

            using var sourceData = new FileStream(fileName, FileMode.Open);
            await deviceClient.UploadToBlobAsync(fileName, sourceData);
        }

        static async Task Main(string[] args)
        {
            //Sends structural data
            await SendSensorData();

            //Sends non-structural data
            await SendUnstructuredDataAsync();

            Console.ReadKey();
        }
    }
}
