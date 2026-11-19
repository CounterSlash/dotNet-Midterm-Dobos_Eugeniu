using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Rooms.CreateBooking;

public sealed record CreateBookingCommand(
    Guid RoomId,
    string? BookedBy,
    DateTime StartUtc,
    DateTime EndUtc,
    int Attendees) : ICommand<CreatedBookingResponse>;
