using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.UI.Xaml.Controls;

namespace IoT.Solutions.SmartIndustrialApplications.AnalogToDigitalSensor
{
    /// <summary>
    /// Program to read Analog Temperature and convert into equivalent Digital value
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const byte I2C_ADDR_PCF8591 = (0x90 >> 1); // PCF8591 address; The address has 127 bits, so 0x90 >> 1 = 0x48
        I2cDevice i2cPCF8591 = null;

        public MainPage()
        {
            this.InitializeComponent();
            InitI2C();
        }

        /// <summary>
        /// Initialize Device to read Analog data
        /// </summary>
        private async void InitI2C()
        {
            // Initialization I2C - device with RPI2
            string deviceSelector = I2cDevice.GetDeviceSelector();
            var i2cDeviceControllers = await DeviceInformation.FindAllAsync(deviceSelector);
            if (i2cDeviceControllers.Count == 0)
            {
                TempReadingBox.Text = "DeviceInformation objects is null";
                return;
            }

            //Settings for PCF8591
            var i2cSettings = new I2cConnectionSettings(I2C_ADDR_PCF8591);
            i2cSettings.BusSpeed = I2cBusSpeed.StandardMode;
            i2cPCF8591 = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, i2cSettings);
            if (i2cPCF8591 == null)
            {
                TempReadingBox.Text = "I2cDevice object is null";
                return;
            }

            var temperature = await GetTemperatureAsync();
            TempReadingBox.Text = temperature.ToString();
        }

        public async Task<double> GetTemperatureAsync()
        {
            byte[] tempCommand = new byte[1] { 0x40 };                          //0x40 is for function register to select DA
            i2cPCF8591.Write(tempCommand);
                        
            await Task.Delay(50);                                               // Per datasheet 14-bit temperature needs 10.8 msec

            byte[] tempData = new byte[2];
            i2cPCF8591.Read(tempData);

            var rawReading = tempData[0] << 8 | tempData[1];                    // Combine bytes

            var voltage = rawReading / 255.0 * 3.3;                             //calculate voltage
            var Rt = 10 * voltage / (3.3 - voltage);                            //calculate resistance value of thermistor
            var tempK = 1 / (1 / (273.15 + 25) + Math.Log(Rt / 10) / 3950.0);   //calculate temperature (Kelvin)
            var tempC = tempK - 273.15;                                         //calculate temperature (Celsius)

            return tempC;
        }
    }
}
