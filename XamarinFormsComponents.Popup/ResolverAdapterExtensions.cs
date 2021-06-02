namespace XamarinFormsComponents
{
    using XamarinFormsComponents.Popup;

    public static class ResolverAdapterExtensions
    {
        public static IResolverAdapter AddPopupNavigator(this IResolverAdapter adapter)
        {
            adapter.AddComponent<IPopupPageFactory, DefaultPopupPageFactory>();
            adapter.AddComponent<IPopupNavigator, PopupNavigator>();
            return adapter;
        }

        public static IResolverAdapter UsePopupPageFactory<T>(this IResolverAdapter adapter)
            where T : IPopupPageFactory
        {
            adapter.AddComponent<IPopupPageFactory, T>();
            return adapter;
        }

        public static IResolverAdapter UsePopupPageFactory(this IResolverAdapter adapter, IPopupPageFactory popupPageFactory)
        {
            adapter.AddComponent(popupPageFactory);
            return adapter;
        }
    }
}
