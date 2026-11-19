using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RoomHub.Api.Features.Rooms.CreateBooking;

public sealed record CreateBookingRequest(
    string? BookedBy,
    DateTime StartUtc,
    DateTime EndUtc,
    int Attendees);

public sealed record CreatedBookingResponse(Guid Id, Guid RoomId, string Status);

public static class CreateBookingEndpoint
{
    public static void Map(RouteGroupBuilder group) =>
        group.MapPost("/{roomId:guid}/bookings", Handle).WithName("CreateBooking");

    private static async Task<Results<Created<CreatedBookingResponse>, ProblemHttpResult>> Handle(
        Guid roomId,
        CreateBookingRequest request,
        ICommandHandler<CreateBookingCommand, CreatedBookingResponse> handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(new CreateBookingCommand(
            roomId, request.BookedBy, request.StartUtc, request.EndUtc, request.Attendees), ct);

        return result.IsFailure
            ? result.Error.ToProblem()
            : TypedResults.Created($"/api/bookings/{result.Value.Id}", result.Value);
    }
}
