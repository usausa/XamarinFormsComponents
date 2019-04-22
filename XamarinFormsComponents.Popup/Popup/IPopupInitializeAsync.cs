namespace XamarinFormsComponents.Popup
{
    using System.Threading.Tasks;

    public interface IPopupInitializeAsync<in T>
    {
        Task Initialize(T parameter);
    }
}
