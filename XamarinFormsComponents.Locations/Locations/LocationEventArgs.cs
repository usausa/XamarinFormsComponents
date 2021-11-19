namespace XamarinFormsComponents.Locations;

using System;

public sealed class LocationEventArgs : EventArgs
{
    public LocationInformation Location { get; }

    public LocationEventArgs(LocationInformation location)
    {
        Location = location;
    }
}
