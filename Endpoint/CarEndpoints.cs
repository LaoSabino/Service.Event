namespace Service.Event.Endpoint;

using global::Service.Event.Model;
using global::Service.Event.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

public static class CarEndpoints
{
    public static void MapCarEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/cars");

        group.MapGet("/{id}", async ([FromServices] CarService service, string id) =>
        {
            var car = await service.GetAsync(id);
            return car is not null ? Results.Ok(car) : Results.NotFound();
        }).WithName("GetCarById");

        group.MapPost("/", async ([FromServices] CarService service, Car car) =>
        {
            car.Id = Guid.NewGuid().ToString();
            await service.CreateAsync(car);
            return Results.Created($"/cars/{car.Id}", car);
        }).WithName("CreateCar");

        group.MapPut("/{id}", async ([FromServices] CarService service, string id, Car updatedCar) =>
        {
            var car = await service.GetAsync(id);
            if (car is not null)
            {
                updatedCar.Id = car.Id;
                await service.UpdateAsync(id, updatedCar);
                return Results.NoContent();
            }
            return Results.NotFound();
        }).WithName("UpdateCar");

        group.MapDelete("/{id}", async ([FromServices] CarService service, string id) =>
        {
            var car = await service.GetAsync(id);
            if (car is not null)
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            }
            return Results.NotFound();
        }).WithName("DeleteCar");
    }
}
