namespace XamarinFormsComponents
{
    using Smart.Resolver;

    public sealed class SmartResolverAdapter : IResolverAdapter
    {
        private readonly ResolverConfig config;

        public SmartResolverAdapter(ResolverConfig config)
        {
            this.config = config;
        }

        public IResolverAdapter AddComponent<TComponent>()
        {
            config.Bind<TComponent>().ToSelf().InSingletonScope();
            return this;
        }

        public IResolverAdapter AddComponent<TComponent, TImplement>()
            where TImplement : TComponent
        {
            config.Bind<TComponent>().To<TImplement>().InSingletonScope();
            return this;
        }

        public IResolverAdapter AddComponent<TComponent>(TComponent component)
        {
            config.Bind<TComponent>().ToConstant(component).InSingletonScope();
            return this;
        }
    }
}
