namespace Example.FormsApp.Modules
{
    using System.Threading.Tasks;

    using Example.FormsApp.Models;

    using XamarinFormsComponents.Popup;

    public static class PopupNavigatorExtensions
    {
        public static ValueTask<string> InputNumberAsync(this IPopupNavigator popupNavigator, string title, string value, int maxLength)
        {
            return popupNavigator.PopupAsync<InputParameter<string>, string>(
                DialogId.InputNumber,
                new InputParameter<string>(title, value, maxLength));
        }
    }
}
