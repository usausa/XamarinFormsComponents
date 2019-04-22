namespace XamarinFormsComponents
{
    using XamarinFormsComponents.Popup;

    public static class ResolverAdapterExtensions
    {
        public static IResolverAdapter AddDialogs(this IResolverAdapter adapter)
        {
            adapter.AddComponent<IPopupNavigator, PopupNavigator>();
            return adapter;
        }
    }
}
