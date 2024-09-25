namespace Domain;

public record Point
{
    public Point(double longitude, double latitude)
    {
        Verify(longitude);
        Verify(latitude);
        Longitude = longitude;
        Latitude = latitude;
    }

    /// <summary>
    /// Longitude
    /// </summary>
    public double Longitude { get; init; }

    /// <summary>
    /// Latitude
    /// </summary>
    public double Latitude { get; init; }

    private static void Verify(double value)
    {
        if (value is < -180 or > 180)
            throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between -180 and 180.");
    }
}