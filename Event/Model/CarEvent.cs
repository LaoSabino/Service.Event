namespace Service.Event.Event.Model;

public abstract class CarEvent
{
    public string CarId { get; set; }
    public DateTime OccurredAt { get; set; }
}