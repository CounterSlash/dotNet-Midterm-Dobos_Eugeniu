using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed record CreateRoomRequest(string? Name, int Capacity);
public sealed record CreatedRoomResponse(Guid Id, string Name, int Capacity);

public static class CreateRoomEndpoint
{
    public static void Map(RouteGroupBuilder group) =>
        group.MapPost("/", Handle).WithName("CreateRoom");

    private static async Task<Results<Created<CreatedRoomResponse>, ProblemHttpResult>> Handle(
        CreateRoomRequest request,
        ICommandHandler<CreateRoomCommand, CreatedRoomResponse> handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(new CreateRoomCommand(request.Name, request.Capacity), ct);
        return result.IsFailure
            ? result.Error.ToProblem()
            : TypedResults.Created($"/api/rooms/{result.Value.Id}", result.Value);
    }
}
