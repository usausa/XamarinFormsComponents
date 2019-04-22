namespace XamarinFormsComponents
{
    using XamarinFormsComponents.Dialogs;

    public static class ResolverAdapterExtensions
    {
        public static IResolverAdapter AddDialogs(this IResolverAdapter adapter)
        {
            adapter.AddComponent<IDialogs, XamarinFormsComponents.Dialogs.Dialogs>();
            return adapter;
        }
    }
}
