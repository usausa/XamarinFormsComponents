namespace Example.FormsApp.Modules;

using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Smart.Forms.Input;
using Smart.Navigation;

using XamarinFormsComponents.Dialogs;

public class DialogViewModel : AppViewModelBase
{
    private readonly IDialogs dialogs;

    public AsyncCommand BackCommand { get; }

    public AsyncCommand ProgressCommand { get; }
    public AsyncCommand LoadingCommand { get; }
    public AsyncCommand DateCommand { get; }
    public AsyncCommand TimeCommand { get; }
    public AsyncCommand ConfirmCommand { get; }
    public AsyncCommand SelectCommand { get; }

    public DialogViewModel(
        ApplicationState applicationState,
        IDialogs dialogs)
        : base(applicationState)
    {
        this.dialogs = dialogs;

        BackCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));

        ProgressCommand = MakeAsyncCommand(Progress);
        LoadingCommand = MakeAsyncCommand(Loading);
        DateCommand = MakeAsyncCommand(Date);
        TimeCommand = MakeAsyncCommand(Time);
        ConfirmCommand = MakeAsyncCommand(Confirm);
        SelectCommand = MakeAsyncCommand(Select);
    }

    private async Task Progress()
    {
        using var progress = dialogs.Progress("Test");
        for (var i = 0; i < 100; i++)
        {
            await Task.Delay(50);

            progress.Update(i + 1);
        }
    }

    private async Task Loading()
    {
        using var _ = dialogs.Loading("Test");
        await Task.Delay(3000);
    }

    private async Task Date()
    {
        var result = await dialogs.Date("Test");
        if (result.Ok)
        {
            await dialogs.Information(result.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture));
        }
    }

    private async Task Time()
    {
        var result = await dialogs.Time("Test");
        if (result.Ok)
        {
            await dialogs.Information(result.Value.ToString(@"hh\:mm", CultureInfo.InvariantCulture));
        }
    }

    private async Task Confirm()
    {
        var result = await dialogs.Confirm("○○しますか？", "確認", "はい", "いいえ");
        await dialogs.Information(result.ToString(CultureInfo.InvariantCulture));
    }

    private async Task Select()
    {
        var result = await dialogs.Select(Enumerable.Range(1, 3), x => $"Item-{x}", "選択");
        if (result.Selected)
        {
            await dialogs.Information($"Selected={result.Value}");
        }
    }
}
