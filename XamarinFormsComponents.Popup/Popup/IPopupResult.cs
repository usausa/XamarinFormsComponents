namespace XamarinFormsComponents.Popup
{
    public interface IPopupResult<out T>
    {
        T Result { get; }
    }
}
