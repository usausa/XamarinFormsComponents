namespace XamarinFormsComponents
{
    using XamarinFormsComponents.Settings;

    public static class ResolverAdapterExtensions
    {
        public static IResolverAdapter AddSettings(this IResolverAdapter adapter)
        {
            adapter.AddComponent<ISetting, Setting>();
            return adapter;
        }
    }
}
