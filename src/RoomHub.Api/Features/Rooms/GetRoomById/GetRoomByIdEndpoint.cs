using RoomHub.Api.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Features.Rooms.CreateRoom;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RoomHub.Api.Features.Rooms.GetRoomById;

public static class GetRoomByIdEndpoint
{
    public static void Map(RouteGroupBuilder group) =>
        group.MapGet("/{id:guid}", Handle).WithName("GetRoomById");

    private static async Task<Results<Ok<RoomResponse>, ProblemHttpResult>> Handle(
        Guid id,
        IRoomRepository rooms,
        CancellationToken ct)
    {
        var room = await rooms.GetByIdAsync(id, ct);
        if (room is null)
            return RoomErrors.NotFound.ToProblem();

        return TypedResults.Ok(new RoomResponse(room.Id, room.Name, room.Capacity));
    }
}
