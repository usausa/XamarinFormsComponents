namespace XamarinFormsComponents.Locations;

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Essentials;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ignore")]
public sealed class LocationManager : ILocationManager
{
    public event EventHandler<LocationEventArgs>? LocationChanged;

    private bool running;

    private CancellationTokenSource? cts;

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

    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Ignore")]
    private async ValueTask GetLocationLoop(CancellationTokenSource cancellationTokenSource)
    {
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
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Ignore")]
    public async ValueTask<LocationInformation?> GetLastLocationAsync()
    {
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

        return null;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Ignore")]
    public async ValueTask<LocationInformation?> GetLocationAsync(CancellationTokenSource cancellationTokenSource)
    {
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

        return null;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Ignore")]
    public async ValueTask<PlaceInformation[]> GetPlaceInformationAsync(double latitude, double longitude)
    {
        try
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude).ConfigureAwait(false);
            return placemarks.Select(x => new PlaceInformation(x)).ToArray();
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
        }

        return Array.Empty<PlaceInformation>();
    }
}
