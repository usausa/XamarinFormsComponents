namespace XamarinFormsComponents
{
    using XamarinFormsComponents.Locations;

    public static class ResolverAdapterExtensions
    {
        public static IResolverAdapter AddDialogs(this IResolverAdapter adapter)
        {
            adapter.AddComponent<ILocationManager, LocationManager>();
            return adapter;
        }
    }
}
