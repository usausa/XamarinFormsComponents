namespace XamarinFormsComponents.Dialogs;

public interface IDialogs
{
    ValueTask<bool> Confirm(string message, string? title = null, string acceptButton = "OK", string cancelButton = "Cancel");

    ValueTask Information(string message, string? title = null, string cancelButton = "OK");

    ValueTask<SelectResult<string>> Select(IEnumerable<string> items, string? title = null, string? cancel = null);

    ValueTask<SelectResult<T>> Select<T>(IEnumerable<T> items, Func<T, string> formatter, string? title = null, string? cancel = null);

    IProgress Progress(string? title = null);

    IProgress Loading(string? title = null);

    ValueTask<DateDialogResult> Date(string? title = null, DateTime? value = null, DateTime? minDate = null, DateTime? maxDate = null);

    ValueTask<TimeDialogResult> Time(string? title = null, TimeSpan? value = null);
}
