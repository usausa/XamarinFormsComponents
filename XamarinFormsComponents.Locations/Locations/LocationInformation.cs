namespace XamarinFormsComponents.Locations
{
    using System;

    public class LocationInformation
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public DateTimeOffset Timestamp { get; }

        public LocationInformation(double latitude, double longitude, DateTimeOffset timestamp)
        {
            Latitude = latitude;
            Longitude = longitude;
            Timestamp = timestamp;
        }
    }
}
