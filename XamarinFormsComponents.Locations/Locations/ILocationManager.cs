namespace XamarinFormsComponents.Locations;

public interface ILocationManager
{
    event EventHandler<LocationEventArgs> LocationChanged;

    int Interval { get; set; }

    void Start();

    void Stop();

    ValueTask<LocationInformation?> GetLastLocationAsync();

    ValueTask<LocationInformation?> GetLocationAsync(CancellationTokenSource cancellationTokenSource);

    ValueTask<PlaceInformation[]> GetPlaceInformationAsync(double latitude, double longitude);
}
