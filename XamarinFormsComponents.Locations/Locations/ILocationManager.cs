namespace XamarinFormsComponents.Locations
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ILocationManager
    {
        event EventHandler<LocationEventArgs> LocationChanged;

        int Interval { get; set; }

        void Start();

        void Stop();

        Task<LocationInformation> GetLastLocationAsync();

        Task<LocationInformation> GetLocationAsync(CancellationTokenSource cancellationTokenSource);

        Task<PlaceInformation[]> GetPlaceInformationAsync(double latitude, double longitude);
    }
}
