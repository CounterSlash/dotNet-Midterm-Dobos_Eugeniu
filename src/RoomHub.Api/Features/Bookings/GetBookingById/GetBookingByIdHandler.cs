using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.GetBookingById;

public sealed class GetBookingByIdHandler(
    IBookingRepository bookings) : IQueryHandler<GetBookingByIdQuery, BookingResponse>
{
    public async Task<Result<BookingResponse>> Handle(
        GetBookingByIdQuery query,
        CancellationToken cancellationToken)
    {
        var booking = await bookings.GetByIdAsync(query.BookingId, cancellationToken);
        if (booking is null)
            return BookingErrors.NotFound;

        return new BookingResponse(
            booking.Id,
            booking.RoomId,
            booking.BookedBy,
            booking.StartUtc,
            booking.EndUtc,
            booking.Attendees,
            booking.Status.ToString());
    }
}
