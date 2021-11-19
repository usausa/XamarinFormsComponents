namespace Example.FormsApp.Models;

public class InputParameter<T>
{
    public string Title { get; }

    public T Value { get; }

    public int MaxLength { get; }

    public InputParameter(string title, T value, int maxLength)
    {
        Title = title;
        Value = value;
        MaxLength = maxLength;
    }
}
