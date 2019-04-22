namespace Example.FormsApp.Modules
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    using XamarinFormsComponents.Dialogs;
    using XamarinFormsComponents.Settings;

    public class SettingViewModel : AppViewModelBase
    {
        public static SettingViewModel DesignInstance { get; } = null; // For design

        public AsyncCommand BackCommand { get; }

        public AsyncCommand WriteCommand { get; }
        public AsyncCommand ReadCommand { get; }
        public AsyncCommand RemoveCommand { get; }

        public SettingViewModel(
            ApplicationState applicationState,
            IDialogs dialogs,
            ISetting setting)
            : base(applicationState)
        {
            BackCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));

            WriteCommand = MakeAsyncCommand(async () =>
            {
                setting.WriteString("Test", "123");
                await dialogs.Information("Write");
            });
            ReadCommand = MakeAsyncCommand(async () =>
            {
                var value = setting.ReadString("Test");
                await dialogs.Information($"Read={value}");
            });
            RemoveCommand = MakeAsyncCommand(async () =>
            {
                setting.Remove("Test");
                await dialogs.Information("Remove");
            });
        }
    }
}
