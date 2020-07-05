namespace Example.FormsApp.Modules
{
    using System;

    using Example.FormsApp.Models;

    using Smart.Forms.Input;
    using Smart.Navigation;

    using XamarinFormsComponents.Dialogs;
    using XamarinFormsComponents.Serializers;

    public class SerializerViewModel : AppViewModelBase
    {
        public static SerializerViewModel DesignInstance { get; } = null; // For design

        public AsyncCommand BackCommand { get; }

        public AsyncCommand SerializeCommand { get; }

        public SerializerViewModel(
            ApplicationState applicationState,
            IDialogs dialogs,
            ISerializer serializer)
            : base(applicationState)
        {
            BackCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));

            SerializeCommand = MakeAsyncCommand(async () =>
            {
                var obj = new SerializeObject { IntValue = 100, StringValue = "abc", BoolValue = true, DateTimeValue = DateTime.Now };
                var text = serializer.Serialize(obj);
                await dialogs.Information(text);
            });
        }
    }
}
