using System;
using System.Collections.Generic;
using System.Text;

namespace LinkDevice
{
    public class Device
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Device(int x, int y)
        {
            X = x;
            Y = y;
        }

        public (LinkStation, double) GetBestLinkStationForDevice(List<LinkStation> linkStations)
        {
            var highestPower = 0D;
            LinkStation bestLinkStation = null;
            try
            {
                foreach (var linkStation in linkStations)
                {
                    var deltaX = linkStation.X - X;
                    var deltaY = linkStation.Y - Y;
                    var deviceDistanceFromLinkStation = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

                    if (deviceDistanceFromLinkStation < linkStation.Reach)
                    {
                        var power = Math.Pow(linkStation.Reach - deviceDistanceFromLinkStation, 2);
                        if (power > highestPower)
                        {
                            highestPower = power;
                            bestLinkStation = linkStation;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (bestLinkStation, highestPower);

        }
    }
}