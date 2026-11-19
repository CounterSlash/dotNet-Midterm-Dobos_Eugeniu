using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RoomHub.Api.Features.Bookings.GetBookingById;

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
        IQueryHandler<GetBookingByIdQuery, BookingResponse> handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(new GetBookingByIdQuery(id), ct);
        return result.IsFailure
            ? result.Error.ToProblem()
            : TypedResults.Ok(result.Value);
    }
}
