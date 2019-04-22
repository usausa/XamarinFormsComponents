namespace XamarinFormsComponents
{
    using XamarinFormsComponents.Locations;

    public static class ResolverAdapterExtensions
    {
        public static IResolverAdapter AddLocationManager(this IResolverAdapter adapter)
        {
            adapter.AddComponent<ILocationManager, LocationManager>();
            return adapter;
        }
    }
}
