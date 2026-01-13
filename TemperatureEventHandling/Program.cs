namespace TemperatureEventHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HeatSensor sensor = new HeatSensor();
            Thermostat thermostat = new Thermostat(sensor);

            sensor.Run();
            Console.ReadKey();
        }
    }


    // event listener
    public class Thermostat
    {
        public Thermostat(HeatSensor sensor)
        {
            sensor.WarningReached += OnWarning;
            sensor.EmergencyReached += OnEmergency;
            sensor.TemperatureNormal += OnNormal;
        }

        private void OnWarning(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠ {e.Temperature} Warning: Cooling ON");
            Console.ResetColor();
        }

        private void OnEmergency(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("🚨 Emergency: Device Shutdown");
            Console.ResetColor();
        }

        private void OnNormal(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("ℹ Temperature Normal: Cooling OFF");
            Console.ResetColor();
        }
    }


    //2. event creator
    public class HeatSensor
    {
        // STEP 1: Delegate
        //public delegate void TemperatureChangedHandler(object sender, TemperatureEventArgs e);

        // STEP 2: Events
        //public event TemperatureChangedHandler WarningReached;
        //public event TemperatureChangedHandler EmergencyReached;
        //public event TemperatureChangedHandler TemperatureNormal;

        //optimized used
        public event EventHandler<TemperatureEventArgs> WarningReached;
        public event EventHandler<TemperatureEventArgs> EmergencyReached;
        public event EventHandler<TemperatureEventArgs> TemperatureNormal;

        private double warningLevel = 27;
        private double emergencyLevel = 75;
        private bool wasWarning = false;

        double[] data = { 16, 17, 16.5, 18, 19, 22, 24, 26.75, 28.7, 27.6, 26, 24, 22, 45, 68, 86, 45 };

        public void Run()
        {
            foreach (var temp in data)
            {
                Console.WriteLine($"Temperature: {temp}");

                TemperatureEventArgs e = new TemperatureEventArgs
                {
                    Temperature = temp,
                    Time = DateTime.Now
                };

                // STEP 3: Raise events
                if (temp >= emergencyLevel)
                {
                    EmergencyReached?.Invoke(this, e);
                }
                else if (temp >= warningLevel)
                {
                    wasWarning = true;
                    WarningReached?.Invoke(this, e);
                }
                else if (wasWarning)
                {
                    wasWarning = false;
                    TemperatureNormal?.Invoke(this, e);
                }

                Thread.Sleep(1000);
            }
        }
    }

    //1. create a event args for temperature
    public class TemperatureEventArgs : EventArgs
    {
        public double Temperature { get; set; }
        public DateTime Time { get; set; }
    }
}
