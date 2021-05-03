namespace Example.FormsApp.Modules
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;

    using Xamarin.Essentials;

    using XamarinFormsComponents.Locations;

    public class LocationViewModel : AppViewModelBase
    {
        private readonly ILocationManager locationManager;

        public NotificationValue<LocationInformation?> LastLocation { get; } = new();

        public NotificationValue<LocationInformation?> CurrentLocation { get; } = new();

        public NotificationValue<string> Address { get; } = new();

        public AsyncCommand BackCommand { get; }

        public AsyncCommand OpenCommand { get; }

        public AsyncCommand GeocodeCommand { get; }

        public LocationViewModel(
            ApplicationState applicationState,
            ILocationManager locationManager)
            : base(applicationState)
        {
            this.locationManager = locationManager;

            BackCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));

            OpenCommand = MakeAsyncCommand(OpenMap, () => LastLocation.Value != null || CurrentLocation.Value != null)
                .Observe(LastLocation)
                .Observe(CurrentLocation);
            GeocodeCommand = MakeAsyncCommand(Geocode, () => LastLocation.Value != null || CurrentLocation.Value != null)
                .Observe(LastLocation)
                .Observe(CurrentLocation);
        }

        public override async void OnNavigatedTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                LastLocation.Value = await locationManager.GetLastLocationAsync();

                Disposables.Add(Observable
                    .FromEvent<EventHandler<LocationEventArgs>, LocationEventArgs>(h => (_, e) => h(e), h => locationManager.LocationChanged += h, h => locationManager.LocationChanged -= h)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(x => CurrentLocation.Value = x.Location));
            }

            locationManager.Start();
        }

        public override void OnNavigatingFrom(INavigationContext context)
        {
            locationManager.Stop();
        }

        private async Task OpenMap()
        {
            var location = CurrentLocation.Value ?? LastLocation.Value;
            if (location is null)
            {
                return;
            }

            await Map.OpenAsync(location.Latitude, location.Longitude);
        }

        private async Task Geocode()
        {
            var location = CurrentLocation.Value ?? LastLocation.Value;
            if (location is null)
            {
                return;
            }

            var placemarks = await locationManager.GetPlaceInformationAsync(location.Latitude, location.Longitude);
            var placemark = placemarks.FirstOrDefault();
            if (placemark != null)
            {
                Address.Value = $"CountryCode: {placemark.CountryCode}\n" +
                                $"CountryName: {placemark.CountryName}\n" +
                                $"FeatureName: {placemark.FeatureName}\n" +
                                $"PostalCode: {placemark.PostalCode}\n" +
                                $"AdminArea: {placemark.AdminArea}\n" +
                                $"Locality: {placemark.Locality}\n" +
                                $"Thoroughfare: {placemark.Thoroughfare}\n" +
                                $"SubAdminArea: {placemark.SubAdminArea}\n" +
                                $"SubLocality: {placemark.SubLocality}\n" +
                                $"SubThoroughfare: {placemark.SubThoroughfare}";
            }
            else
            {
                Address.Value = "(Unknown)";
            }
        }
    }
}
