using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed class CreateRoomHandler(
    IRoomRepository rooms,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateRoomCommand, RoomResponse>
{
    public async Task<Result<RoomResponse>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var result = Room.Create(command.Name, command.Capacity);
        if (result.IsFailure)
            return result.Error;

        rooms.Add(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var room = result.Value;
        return new RoomResponse(room.Id, room.Name, room.Capacity);
    }
}
