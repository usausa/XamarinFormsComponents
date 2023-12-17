namespace XamarinFormsComponents.Locations;

using System.Diagnostics;

using Xamarin.Essentials;

public sealed class LocationManager : ILocationManager, IDisposable
{
    public event EventHandler<LocationEventArgs>? LocationChanged;

    private bool running;

    private CancellationTokenSource? cts;

    public int Interval { get; set; } = 15000;

    public void Dispose()
    {
        cts?.Dispose();
    }

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
#pragma warning disable CA1031
        try
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request, cancellationTokenSource.Token).ConfigureAwait(false);
                if (location != null)
                {
                    LocationChanged?.Invoke(this, new LocationEventArgs(new LocationInformation(location.Latitude, location.Longitude, location.Timestamp)));
                }

                await Task.Delay(Interval, cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }
        catch (Exception e)
        {
            Trace.WriteLine(e);
        }
#pragma warning restore CA1031
    }

    public async ValueTask<LocationInformation?> GetLastLocationAsync()
    {
#pragma warning disable CA1031
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync().ConfigureAwait(false);
            if (location != null)
            {
                return new LocationInformation(location.Latitude, location.Longitude, location.Timestamp);
            }
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
        }
#pragma warning restore CA1031

        return null;
    }

    public async ValueTask<LocationInformation?> GetLocationAsync(CancellationTokenSource cancellationTokenSource)
    {
#pragma warning disable CA1031
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request, cancellationTokenSource.Token).ConfigureAwait(false);
            if (location != null)
            {
                return new LocationInformation(location.Latitude, location.Longitude, location.Timestamp);
            }
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
        }
#pragma warning restore CA1031

        return null;
    }

    public async ValueTask<PlaceInformation[]> GetPlaceInformationAsync(double latitude, double longitude)
    {
#pragma warning disable CA1031
        try
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude).ConfigureAwait(false);
            return placemarks.Select(x => new PlaceInformation(x)).ToArray();
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
        }
#pragma warning restore CA1031

        return Array.Empty<PlaceInformation>();
    }
}
