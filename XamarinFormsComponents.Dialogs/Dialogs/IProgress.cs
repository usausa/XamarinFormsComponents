namespace XamarinFormsComponents.Dialogs;

public interface IProgress : IDisposable
{
    void Update(int percent);
}
