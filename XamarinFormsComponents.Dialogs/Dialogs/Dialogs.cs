namespace XamarinFormsComponents.Dialogs
{
    using System;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async Task<bool> Confirm(string message, string title, string acceptButton, string cancelButton)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, acceptButton, cancelButton);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async Task Information(string message, string title, string cancelButton)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancelButton);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async Task<int> Select(string[] items, string title, string cancel = null, string destruction = null)
        {
            var complete = new TaskCompletionSource<int>();

            var config = new ActionSheetConfig();
            if (!String.IsNullOrEmpty(title))
            {
                config.Title = title;
            }

            if (!String.IsNullOrEmpty(cancel))
            {
                config.Cancel = new ActionSheetOption(cancel, () => complete.TrySetResult(-1));
            }

            if (!String.IsNullOrEmpty(destruction))
            {
                config.Destructive = new ActionSheetOption(destruction, () => complete.TrySetResult(-2));
            }

            for (var i = 0; i < items.Length; i++)
            {
                var index = i;
                config.Options.Add(new ActionSheetOption(items[i], () => complete.TrySetResult(index)));
            }

            using (UserDialogs.Instance.ActionSheet(config))
            {
                await complete.Task;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async Task<DateDialogResult> Date(string title = null, DateTime? value = null, DateTime? minDate = null, DateTime? maxDate = null)
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async Task<TimeDialogResult> Time(string title = null, TimeSpan? value = null)
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
