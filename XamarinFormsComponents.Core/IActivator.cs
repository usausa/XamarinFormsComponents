namespace XamarinFormsComponents
{
    using System;

    public interface IActivator
    {
        object Create(Type type);
    }
}
