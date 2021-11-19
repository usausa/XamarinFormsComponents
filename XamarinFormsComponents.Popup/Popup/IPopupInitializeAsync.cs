namespace XamarinFormsComponents.Popup;

using System.Threading.Tasks;

public interface IPopupInitializeAsync<in T>
{
    ValueTask Initialize(T parameter);
}
