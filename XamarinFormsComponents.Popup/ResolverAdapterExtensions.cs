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
    }
}
