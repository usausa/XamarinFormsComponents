namespace XamarinFormsComponents.Popup;

using System;
using System.Threading.Tasks;

public interface IPopupNavigator
{
    void Register(object id, Type type);

    ValueTask<TResult> PopupAsync<TResult>(object id);

    ValueTask<TResult> PopupAsync<TParameter, TResult>(object id, TParameter parameter);

    ValueTask PopupAsync(object id);

    ValueTask PopupAsync<TParameter>(object id, TParameter parameter);

    ValueTask PopAsync();
}
