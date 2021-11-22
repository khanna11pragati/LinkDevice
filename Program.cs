using LinkDevice;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkDevice
{
    class Program
    {
        private static List<LinkStation> _linkStations;
        
        static Program()
        {
            _linkStations = new List<LinkStation>();
        }

        static void Main()
        {
            int action;
            do
            {
                action = GetMenuAction();
                switch (action)
                {
                    case 0:
                        break;
                    case 1:
                        AddLinkStation();
                        break;
                    case 2:
                        var device = GetDeviceLocation();
                        if (device != null)
                        {
                            CalculatePowerForDevice(device);
                        }
                        break;
                    case 3:
                        CalculatePowerForPredefinedDevicesAndLinkStations();
                        break;
                    default:
                        Console.WriteLine("Invalid action. Enter correct action.");
                        break;
                }
            }
            while (action != 0);

            Console.WriteLine("Exiting program");
        }

        /// <summary>
        /// Calculates power for a pre-defined device and link stations locations
        /// </summary>
        private static void CalculatePowerForPredefinedDevicesAndLinkStations()
        {
            _linkStations = InitializeLinkStations();
            foreach (var d in InitializeDeviceLocations())
            {
                CalculatePowerForDevice(d);
            }
        }

        /// <summary>
        /// Calcuates power for a device and linkstations
        /// </summary>
        /// <param name="d"></param>
        private static void CalculatePowerForDevice(Device d)
        {
            try
            {
                var (linkStation, power) = d.GetBestLinkStationForDevice(_linkStations);
                Console.WriteLine(Output(d, linkStation, power));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while calculating best link station for device with error " + ex.Message);
            }
        }

        /// <summary>
        /// Format the output of the program
        /// </summary>
        /// <param name="d"></param>
        /// <param name="ls"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private static string Output(Device d, LinkStation ls, double p)
        {
            if (p == 0)
            {
                return $"No link station within reach for point [X:{d.X}, Y:{d.Y}]";
            }
            else
                return $"Best link station for point [X:{d.X}, Y:{d.Y}] is [X:{ls.X}, Y:{ls.Y}] with power {p:N3}";
        }

        /// <summary>
        /// Menu selection that allows users to either add link stations dynamically or let users input device locations manually
        /// You can also use option 3 to test the output based on pre-defined lits of link stations and test device locations
        /// </summary>
        /// <returns></returns>
        private static int GetMenuAction()
        {
            Console.WriteLine("Press 1 to enter link station");
            Console.WriteLine("Press 2 to fetch link station for device");
            Console.WriteLine("Press 3 to fetch link station for pre-defined device locations and link stations");
            Console.WriteLine("Press 0 to exit");

            var isValidAction = int.TryParse(Console.ReadLine(), out var action);
            return isValidAction ? action : -1;
        }

        private static void AddLinkStation()
        {
            bool isValidAction;
            int x, y, reach;
            do
            {
                Console.WriteLine("Enter Location-X");
                isValidAction = int.TryParse(Console.ReadLine(), out x);
                if (!isValidAction)
                {
                    Console.WriteLine("Invalid Location-X");
                }
            }
            while (!isValidAction);

            do
            {
                Console.WriteLine("Enter Location-Y");
                isValidAction = int.TryParse(Console.ReadLine(), out y);
                if (!isValidAction)
                {
                    Console.WriteLine("Invalid Location-Y");
                }
            }
            while (!isValidAction);

            do
            {
                Console.WriteLine("Enter Reach");
                isValidAction = int.TryParse(Console.ReadLine(), out reach);
                if (!isValidAction)
                {
                    Console.WriteLine("Invalid Reach");
                }
            }
            while (!isValidAction);
            try
            {
                _linkStations.Add(new LinkStation(x, y, reach));
                Console.WriteLine($"Added link location: [X:{x}, Y:{y}, Reach:{reach}]");
            }
            catch
            {
                Console.WriteLine("Unable to add location");
            }
        }

        private static Device GetDeviceLocation()
        {
            bool isvalidAction;
            int x, y;
            do
            {
                Console.WriteLine("Enter device Location-X");
                isvalidAction = int.TryParse(Console.ReadLine(), out x);
                if (!isvalidAction)
                {
                    Console.WriteLine("Invalid Device Location-X");
                }
            }
            while (!isvalidAction);

            do
            {
                Console.WriteLine("Enter device Location-Y");
                isvalidAction = int.TryParse(Console.ReadLine(), out y);
                if (!isvalidAction)
                {
                    Console.WriteLine("Invalid Device Location-Y");
                }
            }
            while (!isvalidAction);

            try
            {
                return new Device(x, y);
            }
            catch
            {
                Console.WriteLine("Unable to get Device location");
                return null;
            }
        }

        static List<Device> InitializeDeviceLocations()
        {
            var lstDeviceLoctaions = new List<Device>();
            lstDeviceLoctaions.Add(new Device(0, 0));
            lstDeviceLoctaions.Add(new Device(100, 100));
            lstDeviceLoctaions.Add(new Device(15, 10));
            lstDeviceLoctaions.Add(new Device(18, 18));
            return lstDeviceLoctaions;
        }

        static List<LinkStation> InitializeLinkStations()
        {
            var lstLinkStation = new List<LinkStation>();
            lstLinkStation.Add(new LinkStation(0, 0, 10));
            lstLinkStation.Add(new LinkStation(20, 20, 5));
            lstLinkStation.Add(new LinkStation(10, 0, 12));
            return lstLinkStation;
        }
    }
}