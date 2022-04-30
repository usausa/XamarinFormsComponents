namespace XamarinFormsComponents.Locations;

public sealed class LocationEventArgs : EventArgs
{
    public LocationInformation Location { get; }

    public LocationEventArgs(LocationInformation location)
    {
        Location = location;
    }
}
