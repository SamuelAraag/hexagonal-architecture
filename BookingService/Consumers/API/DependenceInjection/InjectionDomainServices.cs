using Application.Bookings;
using Application.Bookings.Posts;
using Application.Guests;
using Application.Guests.Ports;
using Application.Rooms;
using Application.Rooms.Ports;
using Data.Bookings;
using Data.Guests;
using Data.Rooms;
using Domain.Ports;

namespace API.DependenceInjection;

public static class InjectionDomainServices
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
    {
        services.AddScoped<IGuestManager, GuestManager>();
        services.AddScoped<IGuestRepository, GuestRepositoryInMemory>();
        services.AddScoped<IRoomManager, RoomManager>();
        services.AddScoped<IRoomRepository, RoomsRepositoryInMemory>();
        services.AddScoped<IBookingManager, BookingManager>();
        services.AddScoped<IBookingRepository, BookingRepositoryInMemory>();

        return services;
    }
}