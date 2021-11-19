namespace XamarinFormsComponents;

using System.Diagnostics.CodeAnalysis;

public interface IResolverAdapter
{
    IResolverAdapter AddComponent<TComponent>();

    IResolverAdapter AddComponent<TComponent, TImplement>()
        where TImplement : TComponent;

    IResolverAdapter AddComponent<TComponent>([DisallowNull] TComponent component);
}
