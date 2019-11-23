namespace XamarinFormsComponents.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDialogs
    {
        Task<bool> Confirm(string message, string title = null, string acceptButton = "OK", string cancelButton = "Cancel");

        Task Information(string message, string title = null, string cancelButton = "OK");

        Task<SelectResult<string>> Select(IEnumerable<string> items, string title = null, string cancel = null);

        Task<SelectResult<T>> Select<T>(IEnumerable<T> items, Func<T, string> formatter, string title = null, string cancel = null);

        IProgress Progress(string title = null);

        IProgress Loading(string title = null);

        Task<DateDialogResult> Date(string title = null, DateTime? value = null, DateTime? minDate = null, DateTime? maxDate = null);

        Task<TimeDialogResult> Time(string title = null, TimeSpan? value = null);
    }
}
