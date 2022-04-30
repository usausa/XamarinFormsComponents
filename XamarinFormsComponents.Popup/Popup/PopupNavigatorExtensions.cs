namespace XamarinFormsComponents.Popup;

using System.Reflection;

public static class PopupNavigatorExtensions
{
    public static void AutoRegister(this IPopupNavigator navigator, IEnumerable<Type> types)
    {
        foreach (var type in types)
        {
            foreach (var attr in type.GetTypeInfo().GetCustomAttributes<PopupAttribute>())
            {
                navigator.Register(attr.Id, type);
            }
        }
    }
}
