using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RoomHub.Api.Features.Bookings.GetBookingById;

public sealed record BookingCreatedResponse(Guid Id, Guid RoomId, string Status);

public sealed record BookingResponse(
    Guid Id,
    Guid RoomId,
    string BookedBy,
    DateTime StartUtc,
    DateTime EndUtc,
    int Attendees,
    string Status);

public static class GetBookingByIdEndpoint
{
    public static void Map(RouteGroupBuilder group) =>
        group.MapGet("/{id:guid}", Handle).WithName("GetBookingById");

    private static async Task<Results<Ok<BookingResponse>, ProblemHttpResult>> Handle(
        Guid id,
        IBookingRepository bookings,
        CancellationToken ct)
    {
        var booking = await bookings.GetByIdAsync(id, ct);
        if (booking is null)
            return BookingErrors.NotFound.ToProblem();

        return TypedResults.Ok(new BookingResponse(
            booking.Id,
            booking.RoomId,
            booking.BookedBy,
            booking.StartUtc,
            booking.EndUtc,
            booking.Attendees,
            booking.Status.ToString()));
    }
}
