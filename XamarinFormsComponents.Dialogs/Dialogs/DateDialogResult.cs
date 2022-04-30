namespace XamarinFormsComponents.Dialogs;

public class DateDialogResult
{
    public bool Ok { get; }

    public DateTime Value { get; }

    public DateDialogResult(bool ok, DateTime value)
    {
        Ok = ok;
        Value = value;
    }
}
