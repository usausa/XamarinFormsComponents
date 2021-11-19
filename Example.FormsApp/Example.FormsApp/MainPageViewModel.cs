namespace Example.FormsApp;

using System.Threading.Tasks;

using Example.FormsApp.Shell;

using Smart.ComponentModel;
using Smart.Forms.ViewModels;
using Smart.Navigation;

public class MainPageViewModel : ViewModelBase, IShellControl
{
    public NotificationValue<string> Title { get; } = new();

    public ApplicationState ApplicationState { get; }

    public INavigator Navigator { get; }

    public MainPageViewModel(
        ApplicationState applicationState,
        INavigator navigator)
        : base(applicationState)
    {
        ApplicationState = applicationState;
        Navigator = navigator;
    }

    protected virtual Task<bool> OnNotifyBackAsync()
    {
        return Task.FromResult(true);
    }
}
