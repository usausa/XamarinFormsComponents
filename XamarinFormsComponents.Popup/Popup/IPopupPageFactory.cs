namespace XamarinFormsComponents.Popup
{
    using Rg.Plugins.Popup.Pages;

    using Xamarin.Forms;

    public interface IPopupPageFactory
    {
        PopupPage Create(View content);
    }
}
