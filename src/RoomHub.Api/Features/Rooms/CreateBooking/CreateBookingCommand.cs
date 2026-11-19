using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Features.Bookings.GetBookingById;

namespace RoomHub.Api.Features.Rooms.CreateBooking;

public sealed record CreateBookingCommand(
    Guid RoomId,
    string? BookedBy,
    DateTime StartUtc,
    DateTime EndUtc,
    int Attendees) : ICommand<BookingCreatedResponse>;
