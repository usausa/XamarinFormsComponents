namespace XamarinFormsComponents.Locations
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Xamarin.Essentials;

    public sealed class LocationManager : ILocationManager
    {
        public event EventHandler<LocationEventArgs> LocationChanged;

        private bool running;

        private CancellationTokenSource cts;

        public int Interval { get; set; } = 15000;

        public void Start()
        {
            if (running)
            {
                return;
            }

            running = true;

            cts = new CancellationTokenSource();
            Task.Run(() => GetLocationLoop(cts));
        }

        public void Stop()
        {
            if (!running)
            {
                return;
            }

            running = false;

            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }
        }

        private async ValueTask GetLocationLoop(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request, cancellationTokenSource.Token);
                    if (location != null)
                    {
                        LocationChanged?.Invoke(this, new LocationEventArgs(new LocationInformation(location.Latitude, location.Longitude, location.Timestamp)));
                    }

                    await Task.Delay(Interval, cancellationTokenSource.Token);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        public async ValueTask<LocationInformation> GetLastLocationAsync()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    return new LocationInformation(location.Latitude, location.Longitude, location.Timestamp);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return null;
        }

        public async ValueTask<LocationInformation> GetLocationAsync(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request, cancellationTokenSource.Token);
                if (location != null)
                {
                    return new LocationInformation(location.Latitude, location.Longitude, location.Timestamp);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return null;
        }

        public async ValueTask<PlaceInformation[]> GetPlaceInformationAsync(double latitude, double longitude)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
                return placemarks.Select(x => new PlaceInformation(x)).ToArray();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return Array.Empty<PlaceInformation>();
        }
    }
}
