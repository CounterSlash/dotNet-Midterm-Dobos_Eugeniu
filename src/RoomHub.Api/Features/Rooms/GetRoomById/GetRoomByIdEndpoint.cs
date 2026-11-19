using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RoomHub.Api.Features.Rooms.GetRoomById;

public static class GetRoomByIdEndpoint
{
    public static void Map(RouteGroupBuilder group) =>
        group.MapGet("/{id:guid}", Handle).WithName("GetRoomById");

    private static async Task<Results<Ok<RoomResponse>, ProblemHttpResult>> Handle(
        Guid id,
        IQueryHandler<GetRoomByIdQuery, RoomResponse> handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(new GetRoomByIdQuery(id), ct);
        return result.IsFailure
            ? result.Error.ToProblem()
            : TypedResults.Ok(result.Value);
    }
}
