namespace XamarinFormsComponents.Dialogs;

public class TimeDialogResult
{
    public bool Ok { get; }

    public TimeSpan Value { get; }

    public TimeDialogResult(bool ok, TimeSpan value)
    {
        Ok = ok;
        Value = value;
    }
}
