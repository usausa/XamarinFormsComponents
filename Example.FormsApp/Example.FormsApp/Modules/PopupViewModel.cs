namespace Example.FormsApp.Modules;

using Smart.Forms.Input;
using Smart.Navigation;

using XamarinFormsComponents.Dialogs;
using XamarinFormsComponents.Popup;

public class PopupViewModel : AppViewModelBase
{
    private readonly IDialogs dialogs;

    private readonly IPopupNavigator popupNavigator;

    public AsyncCommand BackCommand { get; }

    public AsyncCommand NumberCommand { get; }

    public PopupViewModel(
        ApplicationState applicationState,
        IDialogs dialogs,
        IPopupNavigator popupNavigator)
        : base(applicationState)
    {
        this.dialogs = dialogs;
        this.popupNavigator = popupNavigator;

        BackCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));

        NumberCommand = MakeAsyncCommand(Number);
    }

    private async Task Number()
    {
        var result = await popupNavigator.InputNumberAsync("Test", "123", 5);
        if (!String.IsNullOrEmpty(result))
        {
            await dialogs.Information(result, "Result");
        }
    }
}
