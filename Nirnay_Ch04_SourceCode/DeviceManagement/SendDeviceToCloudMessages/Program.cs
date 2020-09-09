using Microsoft.Azure.Devices;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.Solutions.SmartIndustrialApplications.SendD2CMessages
{
    /// <summary>
    /// Program to send message from the cloud to device with IoT Hub
    /// </summary>
    class Program
    {
        //To get the IoT hub connection string go to shared access policies and click service. 
        private static readonly string connectionString = "<Connection string here>";

        private static readonly string deviceId = "<device id>";

        /// <summary>
        /// Sends a new cloud-to-device message to the device with the ID
        /// </summary>
        /// <returns></returns>
        private async static Task SendC2DMessageAsync()
        {
            var serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            var commandMessage = new Message(Encoding.ASCII.GetBytes("Cloud to device message."));

            await serviceClient.SendAsync(deviceId, commandMessage);
        }

        /// <summary>
        /// Request delivery (or expiration) acknowledgments from IoT Hub for each cloud-to-device message
        /// </summary>
        private async static Task ReceiveFeedbackAsync()
        {
            var serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            var feedbackReceiver = serviceClient.GetFeedbackReceiver();

            while (true)
            {
                var feedbackBatch = await feedbackReceiver.ReceiveAsync();
                if (feedbackBatch == null) continue;

                Console.WriteLine($"Received acknowledgement: {0}", string.Join(", ", feedbackBatch.Records.Select(f => f.StatusCode)));
                await feedbackReceiver.CompleteAsync(feedbackBatch);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<CloudToDeviceMethodResult> InvokeDirectMethodOnDevice()
        {
            var serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            var methodInvocation = new CloudToDeviceMethod("TriggerAction") { ResponseTimeout = TimeSpan.FromSeconds(300) };
            methodInvocation.SetPayloadJson("'Switch ON Light'");

            //Interactively invokes a method on device
            var response = await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);

            return response;
        }

        static async Task Main(string[] args)
        {
            await SendC2DMessageAsync();
            await ReceiveFeedbackAsync();
            await InvokeDirectMethodOnDevice();

            Console.ReadKey();
        }
    }
}
