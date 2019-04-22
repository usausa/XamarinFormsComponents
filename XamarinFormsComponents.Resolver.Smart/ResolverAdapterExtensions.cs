namespace XamarinFormsComponents
{
    using System;

    using Smart.Resolver;

    public static class ResolverAdapterExtensions
    {
        public static ResolverConfig UseXamarinFormsComponents(this ResolverConfig config, Action<SmartResolverAdapter> option)
        {
            var adapter = new SmartResolverAdapter(config);
            option(adapter);
            return config;
        }
    }
}
