namespace XamarinFormsComponents
{
    public interface IResolverAdapter
    {
        IResolverAdapter AddComponent<TComponent>();

        IResolverAdapter AddComponent<TComponent, TImplement>()
            where TImplement : TComponent;

        IResolverAdapter AddComponent<TComponent>(TComponent component);
    }
}
