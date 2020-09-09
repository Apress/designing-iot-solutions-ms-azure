using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace rfidauditor
{
    public static class Function1
    {
        private static string logicAppUri = @"<logic app http post url here>";
        private static string connectionString = @"SQL Connection string";

        private static HttpClient httpClient = new HttpClient();

        [FunctionName("Function1")]
        public static async Task Run([IoTHubTrigger("messages/events", Connection = "iothubconnectionstring")] EventData myIoTHubMessage, ILogger log)
        {
            log.LogInformation($"IoT Hub trigger with message: {myIoTHubMessage}");

            //connect to SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT '1' FROM [RFIDReader] WHERE ReaderId = {myIoTHubMessage.Body.ToString()}", connection);
                connection.Open();
                command.ExecuteNonQuery();

                //Select if coming RFID EPC tag is a valid RFID EPC. If not Trigger Logic app
                if (1)
                {
                    await httpClient.PostAsync(logicAppUri, new StringContent(myIoTHubMessage.Body.ToString(), Encoding.UTF8, "application/json"));
                }
            }
        }
    }
}