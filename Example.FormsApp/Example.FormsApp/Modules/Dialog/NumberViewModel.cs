namespace Example.FormsApp.Modules.Dialog
{
    using System.Threading.Tasks;

    using Example.FormsApp.Models;

    using Smart.ComponentModel;
    using Smart.Forms.Input;

    using XamarinFormsComponents.Popup;

    public class NumberViewModel : AppDialogViewModelBase, IPopupResult<string>, IPopupInitialize<InputParameter<string>>
    {
        public static NumberViewModel DesignInstance => null; // For design

        public NotificationValue<string> Title { get; } = new NotificationValue<string>();

        public TextInputModel Input { get; } = new TextInputModel();

        public string Result { get; private set; }

        public AsyncCommand<bool> CloseCommand { get; }

        public DelegateCommand ClearCommand { get; }
        public DelegateCommand PopCommand { get; }
        public DelegateCommand<string> PushCommand { get; }

        public NumberViewModel()
        {
            CloseCommand = MakeAsyncCommand<bool>(Close);
            ClearCommand = MakeDelegateCommand(() => Input.Clear());
            PopCommand = MakeDelegateCommand(() => Input.Pop());
            PushCommand = MakeDelegateCommand<string>(x => Input.Push(x));
        }

        public void Initialize(InputParameter<string> parameter)
        {
            Title.Value = parameter.Title;
            Input.MaxLength = parameter.MaxLength;
            Input.Text = parameter.Value;
        }

        private async Task Close(bool commit)
        {
            if (commit)
            {
                Result = Input.Text;
            }

            await PopupNavigator.PopAsync();
        }
    }
}
