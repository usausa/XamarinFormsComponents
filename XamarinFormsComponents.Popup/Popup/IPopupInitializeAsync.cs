namespace XamarinFormsComponents.Popup;

public interface IPopupInitializeAsync<in T>
{
    ValueTask Initialize(T parameter);
}
