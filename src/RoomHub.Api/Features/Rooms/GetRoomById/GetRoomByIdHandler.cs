using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.GetRoomById;

public sealed record RoomResponse(Guid Id, string Name, int Capacity);

public sealed class GetRoomByIdHandler(
    IRoomRepository rooms) : IQueryHandler<GetRoomByIdQuery, RoomResponse>
{
    public async Task<Result<RoomResponse>> Handle(
        GetRoomByIdQuery query,
        CancellationToken cancellationToken)
    {
        var room = await rooms.GetByIdAsync(query.RoomId, cancellationToken);
        if (room is null)
            return RoomErrors.NotFound;

        return new RoomResponse(room.Id, room.Name, room.Capacity);
    }
}
