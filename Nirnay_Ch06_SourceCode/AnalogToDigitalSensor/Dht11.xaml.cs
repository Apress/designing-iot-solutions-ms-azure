using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using System.Collections.Generic;
using Sensors.Dht;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPI2_I2C
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dht11 : Page
    {
        private GpioController gpio;
        private Sensors.Dht.Dht11 _dht;

        private GpioPin _pin;

        private DhtReading reading;

        static DeviceClient deviceClient;

        public Dht11()
        {
            InitializeComponent();

            GpioInit();
        }

        private async void GpioInit()
        {
            gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                return;
            }

            _pin = gpio.OpenPin(5);
            _dht = new Dht11(_pin, GpioPinDriveMode.Input);
        }

        private async void Timer_Tick(object sender, object e)
        {
            await ReadData();
        }

        private async Task ReadData()
        {
            reading = new DhtReading();

            try
            {
                reading = await _dht.GetReadingAsync();
            }
            catch (Exception e)
            {

            }
            if (reading.IsValid)
            {
                var Temperature = Convert.ToSingle(reading.Temperature);
                var Humidity = Convert.ToSingle(reading.Humidity);
            }
            else
            {
                throw new Exception("DHT Device did not answer");
            }
        }
      

        public async Task<List<byte>> FindDevicesAsync()
        {
            List<byte> returnValue = new List<byte>();

            // Get a selector string that will return all I2C controllers on the system 
            string aqs = I2cDevice.GetDeviceSelector();

            // Find the I2C bus controller device with our selector string 
            var dis = await DeviceInformation.FindAllAsync(aqs).AsTask();

            if (dis.Count > 0)
            {
                const int minimumAddress = 8;
                const int maximumAddress = 77;
                for (byte address = minimumAddress; address <= maximumAddress; address++)
                {
                    var settings = new I2cConnectionSettings(address);
                    settings.BusSpeed = I2cBusSpeed.FastMode;
                    settings.SharingMode = I2cSharingMode.Shared;

                    // Create an I2cDevice with our selected bus controller and I2C settings 
                    using (I2cDevice device = await I2cDevice.FromIdAsync(dis[0].Id, settings))
                    {
                        if (device != null)
                        {
                            try
                            {
                                byte[] writeBuffer = new byte[1] { 0 };
                                device.Write(writeBuffer);
                                // If no exception is thrown, there is 
                                // a device at this address. 
                                returnValue.Add(address);
                            }
                            catch
                            {
                                // If the address is invalid, an exception will be thrown. 
                            }
                        }
                    }
                }
            }
            return returnValue;
        }
    }
}
