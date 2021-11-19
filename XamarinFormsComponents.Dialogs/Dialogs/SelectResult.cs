namespace XamarinFormsComponents.Dialogs;

public class SelectResult<T>
{
    public static SelectResult<T> Cancel { get; } = new(false, default!);

    public bool Selected { get; internal set; }

    public T Value { get; internal set; }

    public SelectResult(T value)
        : this(true, value)
    {
    }

    private SelectResult(bool selected, T value)
    {
        Selected = selected;
        Value = value;
    }
}
