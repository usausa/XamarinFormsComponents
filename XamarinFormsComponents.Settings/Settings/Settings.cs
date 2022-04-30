namespace XamarinFormsComponents.Settings;

using Xamarin.Essentials;

public class Setting : ISetting
{
    public void Remove(string key) => Preferences.Remove(key);

    public bool ReadBool(string key, bool defaultValue = default) => Preferences.Get(key, defaultValue);

    public int ReadInteger(string key, int defaultValue = default) => Preferences.Get(key, defaultValue);

    public long ReadLong(string key, long defaultValue = default) => Preferences.Get(key, defaultValue);

    public double ReadDouble(string key, long defaultValue = default) => Preferences.Get(key, defaultValue);

    public string ReadString(string key, string? defaultValue = default) => Preferences.Get(key, defaultValue);

    public DateTime ReadDateTime(string key, DateTime defaultValue = default) => Preferences.Get(key, defaultValue);

    public void WriteBool(string key, bool value) => Preferences.Set(key, value);

    public void WriteInteger(string key, int value) => Preferences.Set(key, value);

    public void WriteLong(string key, long value) => Preferences.Set(key, value);

    public void WriteDouble(string key, double value) => Preferences.Set(key, value);

    public void WriteString(string key, string value) => Preferences.Set(key, value);

    public void WriteDateTime(string key, DateTime value) => Preferences.Set(key, value);
}
