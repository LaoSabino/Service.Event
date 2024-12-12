using Service.Event.Event.Model;
using Service.Event.Interface;
using Service.Event.Model;

namespace Service.Event.Service;

public class CarService(IEventStore eventStore)
{
    public async Task<Car> GetAsync(string carId)
    {
        var events = await eventStore.GetEventsAsync(carId);
        return Apply(events);
    }

    public async Task CreateAsync(Car car)
    {
        var @event = new CarCreatedEvent
        {
            CarId = car.Id,
            Make = car.Make,
            Model = car.Model,
            Year = car.Year,
            Price = car.Price,
            OccurredAt = DateTime.UtcNow
        };
        await eventStore.AppendEventAsync(@event);
    }

    public async Task UpdateAsync(string carId, Car car)
    {
        var @event = new CarUpdatedEvent
        {
            CarId = carId,
            Make = car.Make,
            Model = car.Model,
            Year = car.Year,
            Price = car.Price,
            OccurredAt = DateTime.UtcNow
        };
        await eventStore.AppendEventAsync(@event);
    }

    public async Task DeleteAsync(string carId)
    {
        var @event = new CarDeletedEvent
        {
            CarId = carId,
            OccurredAt = DateTime.UtcNow
        };
        await eventStore.AppendEventAsync(@event);
    }

    private static Car Apply(IEnumerable<CarEvent> events)
    {
        var car = new Car();
        foreach (var @event in events)
        {
            switch (@event)
            {
                case CarCreatedEvent e:
                    car.Id = e.CarId;
                    car.Make = e.Make;
                    car.Model = e.Model;
                    car.Year = e.Year;
                    car.Price = e.Price;
                    break;
                case CarUpdatedEvent e:
                    car.Make = e.Make;
                    car.Model = e.Model;
                    car.Year = e.Year;
                    car.Price = e.Price;
                    break;
                case CarDeletedEvent _:
                    car = null;
                    break;
            }
        }
        return car;
    }
}
