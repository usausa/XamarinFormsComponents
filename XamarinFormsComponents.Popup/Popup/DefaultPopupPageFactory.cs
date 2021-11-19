namespace XamarinFormsComponents.Popup;

using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;

public sealed class DefaultPopupPageFactory : IPopupPageFactory
{
    public PopupPage Create(View content)
    {
        return new PopupPage
        {
            Content = content,
            CloseWhenBackgroundIsClicked = false,
            HasSystemPadding = true,
            Padding = PopupProperty.GetThickness(content)
        };
    }
}
