namespace XamarinFormsComponents
{
    using System;

    using Smart.Resolver;

    public sealed class SmartActivator : IActivator
    {
        private readonly SmartResolver resolver;

        public SmartActivator(SmartResolver resolver)
        {
            this.resolver = resolver;
        }

        public object Create(Type type) => resolver.Get(type);
    }
}
