using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Bookings.GetBookingById;

public sealed record GetBookingByIdQuery(Guid BookingId) : IQuery<BookingResponse>;
