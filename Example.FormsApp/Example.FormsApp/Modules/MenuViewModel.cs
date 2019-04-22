namespace Example.FormsApp.Modules
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class MenuViewModel : AppViewModelBase
    {
        public static MenuViewModel DesignInstance { get; } = null; // For design

        public AsyncCommand<ViewId> ForwardCommand { get; }

        public MenuViewModel(
            ApplicationState applicationState)
            : base(applicationState)
        {
            ForwardCommand = MakeAsyncCommand<ViewId>(async x =>
            {
                if (await Permissions.IsPermissionRequired() &&
                    !await Permissions.RequestPermissions())
                {
                    return;
                }

                await Navigator.ForwardAsync(x);
            });
        }
    }
}
