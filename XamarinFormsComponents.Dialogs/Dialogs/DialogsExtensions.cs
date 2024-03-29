namespace XamarinFormsComponents.Dialogs;

public static class DialogsExtensions
{
    public static void Progress(this IDialogs dialog, Action<IProgress> action)
    {
        using var progress = dialog.Progress();
        action(progress);
    }

    public static void Progress(this IDialogs dialog, string title, Action<IProgress> action)
    {
        using var progress = dialog.Progress(title);
        action(progress);
    }

    public static async ValueTask Progress(this IDialogs dialog, Func<IProgress, ValueTask> action)
    {
        using var progress = dialog.Progress();
        await action(progress).ConfigureAwait(false);
    }

    public static async ValueTask Progress(this IDialogs dialog, string title, Func<IProgress, ValueTask> action)
    {
        using var progress = dialog.Progress(title);
        await action(progress).ConfigureAwait(false);
    }

    public static T Progress<T>(this IDialogs dialog, Func<IProgress, T> func)
    {
        using var progress = dialog.Progress();
        return func(progress);
    }

    public static T Progress<T>(this IDialogs dialog, string title, Func<IProgress, T> func)
    {
        using var progress = dialog.Progress(title);
        return func(progress);
    }

    public static async ValueTask<T> Progress<T>(this IDialogs dialog, Func<IProgress, ValueTask<T>> func)
    {
        using var progress = dialog.Progress();
        return await func(progress).ConfigureAwait(false);
    }

    public static async ValueTask<T> Progress<T>(this IDialogs dialog, string title, Func<IProgress, ValueTask<T>> func)
    {
        using var progress = dialog.Progress(title);
        return await func(progress).ConfigureAwait(false);
    }

    public static void Loading(this IDialogs dialog, Action<IProgress> action)
    {
        using var progress = dialog.Loading();
        action(progress);
    }

    public static void Loading(this IDialogs dialog, string title, Action<IProgress> action)
    {
        using var progress = dialog.Loading(title);
        action(progress);
    }

    public static async ValueTask Loading(this IDialogs dialog, Func<IProgress, ValueTask> action)
    {
        using var progress = dialog.Loading();
        await action(progress).ConfigureAwait(false);
    }

    public static async ValueTask Loading(this IDialogs dialog, string title, Func<IProgress, ValueTask> action)
    {
        using var progress = dialog.Loading(title);
        await action(progress).ConfigureAwait(false);
    }

    public static T Loading<T>(this IDialogs dialog, Func<IProgress, T> func)
    {
        using var progress = dialog.Loading();
        return func(progress);
    }

    public static T Loading<T>(this IDialogs dialog, string title, Func<IProgress, T> func)
    {
        using var progress = dialog.Loading(title);
        return func(progress);
    }

    public static async ValueTask<T> Loading<T>(this IDialogs dialog, Func<IProgress, ValueTask<T>> func)
    {
        using var progress = dialog.Loading();
        return await func(progress).ConfigureAwait(false);
    }

    public static async ValueTask<T> Loading<T>(this IDialogs dialog, string title, Func<IProgress, ValueTask<T>> func)
    {
        using var progress = dialog.Loading(title);
        return await func(progress).ConfigureAwait(false);
    }
}
