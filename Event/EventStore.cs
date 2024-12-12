using Service.Event.Event.Model;
using Service.Event.Interface;

namespace Service.Event.Event;

public class EventStore : IEventStore
{
    private readonly List<CarEvent> _events = new();
    public Task AppendEventAsync(CarEvent @event)
    {
        _events.Add(@event);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<CarEvent>> GetEventsAsync(string carId)
    {
        var events = _events.Where(e => e.CarId == carId);
        return Task.FromResult(events);
    }
}