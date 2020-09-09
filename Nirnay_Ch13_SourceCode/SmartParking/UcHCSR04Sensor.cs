using System;
using System.Diagnostics;
using System.Threading;
using Windows.Devices.Gpio;

namespace SmartParking
{
    class UcHCSR04Sensor
    {
        GpioController gpio = GpioController.GetDefault();
        readonly GpioPin TriggerPin;
        readonly GpioPin EchoPin;

        public UcHCSR04Sensor(int triggerPin, int echoPin)
        {
            this.TriggerPin = gpio.OpenPin(triggerPin);
            this.EchoPin = gpio.OpenPin(echoPin);

            this.TriggerPin.SetDriveMode(GpioPinDriveMode.Output);
            this.EchoPin.SetDriveMode(GpioPinDriveMode.Input);

            this.TriggerPin.Write(GpioPinValue.Low);
        }

        public double GetDistance()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            mre.WaitOne(500);
            Stopwatch pulseLength = new Stopwatch();

            //Send pulse
            this.TriggerPin.Write(GpioPinValue.High);
            mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
            this.TriggerPin.Write(GpioPinValue.Low);

            //Recieve pusle
            while (this.EchoPin.Read() == GpioPinValue.Low) { }
            pulseLength.Start();


            while (this.EchoPin.Read() == GpioPinValue.High) { }
            pulseLength.Stop();

            //Calculating distance (speed: 17,000 miles/hour = 27,350 kilometers/hour)
            double distance = pulseLength.Elapsed.TotalSeconds * 17000;

            return distance;
        }
    }
}
