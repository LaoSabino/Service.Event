using Service.Event.Event.Model;

namespace Service.Event.Interface;

public interface IEventStore
{
    Task AppendEventAsync(CarEvent @event);
    Task<IEnumerable<CarEvent>> GetEventsAsync(string carId);
}