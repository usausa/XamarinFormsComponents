namespace XamarinFormsComponents.Dialogs
{
    using System;

    public interface IProgress : IDisposable
    {
        void Update(int percent);
    }
}
