namespace XamarinFormsComponents.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Acr.UserDialogs;

    using Xamarin.Forms;

    public sealed class Dialogs : IDialogs
    {
        private sealed class ProgressWrapper : IProgress
        {
            private readonly IProgressDialog dialog;

            public ProgressWrapper(IProgressDialog dialog)
            {
                this.dialog = dialog;
            }

            public void Dispose()
            {
                dialog.Dispose();
            }

            public void Update(int percent)
            {
                dialog.PercentComplete = percent;
            }
        }

        public async ValueTask<bool> Confirm(string message, string title, string acceptButton, string cancelButton)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, acceptButton, cancelButton);
        }

        public async ValueTask Information(string message, string title, string cancelButton)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancelButton);
        }

        public async ValueTask<SelectResult<string>> Select(IEnumerable<string> items, string title = null, string cancel = null)
        {
            var complete = new TaskCompletionSource<SelectResult<string>>();

            var config = new ActionSheetConfig();
            if (!String.IsNullOrEmpty(title))
            {
                config.Title = title;
            }

            if (!String.IsNullOrEmpty(cancel))
            {
                config.Cancel = new ActionSheetOption(cancel, () => complete.TrySetResult(SelectResult<string>.Cancel));
            }

            foreach (var item in items)
            {
                config.Options.Add(new ActionSheetOption(item ?? string.Empty, () => complete.TrySetResult(new SelectResult<string>(item))));
            }

            using (UserDialogs.Instance.ActionSheet(config))
            {
                await complete.Task.ConfigureAwait(false);
                return complete.Task.Result;
            }
        }

        public async ValueTask<SelectResult<T>> Select<T>(IEnumerable<T> items, Func<T, string> formatter, string title = null, string cancel = null)
        {
            var complete = new TaskCompletionSource<SelectResult<T>>();

            var config = new ActionSheetConfig();
            if (!String.IsNullOrEmpty(title))
            {
                config.Title = title;
            }

            if (!String.IsNullOrEmpty(cancel))
            {
                config.Cancel = new ActionSheetOption(cancel, () => complete.TrySetResult(SelectResult<T>.Cancel));
            }

            foreach (var item in items)
            {
                var text = item is null ? string.Empty : formatter(item);
                config.Options.Add(new ActionSheetOption(text, () => complete.TrySetResult(new SelectResult<T>(item))));
            }

            using (UserDialogs.Instance.ActionSheet(config))
            {
                await complete.Task.ConfigureAwait(false);
                return complete.Task.Result;
            }
        }

        public IProgress Progress(string title = null)
        {
            return new ProgressWrapper(UserDialogs.Instance.Progress(title));
        }

        public IProgress Loading(string title = null)
        {
            return new ProgressWrapper(UserDialogs.Instance.Loading(title));
        }

        public async ValueTask<DateDialogResult> Date(string title = null, DateTime? value = null, DateTime? minDate = null, DateTime? maxDate = null)
        {
            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                Title = title,
                SelectedDate = value,
                MaximumDate = maxDate,
                MinimumDate = minDate
            });

            return new DateDialogResult(result.Ok, result.SelectedDate);
        }

        public async ValueTask<TimeDialogResult> Time(string title = null, TimeSpan? value = null)
        {
            var result = await UserDialogs.Instance.TimePromptAsync(new TimePromptConfig
            {
                Title = title,
                SelectedTime = value
            });

            return new TimeDialogResult(result.Ok, result.SelectedTime);
        }
    }
}
