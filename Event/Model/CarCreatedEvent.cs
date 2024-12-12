namespace Service.Event.Event.Model;

public class CarCreatedEvent : CarEvent
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
}