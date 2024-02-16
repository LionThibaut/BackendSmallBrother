namespace Domain;

public class Reporting
{
    public int IdReporting { get; set; }
    public float? Longitude { get; set; }
    public float? Latitude { get; set; }
    public string DescriptionReporting { get; set; }
    public string LastSeenDate { get; set; }
    public string LastSeenHour { get; set; }
    
    public Post PostReporting { get; set; }
}