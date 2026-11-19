using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Rooms.GetRoomById;

public sealed record GetRoomByIdQuery(Guid RoomId) : IQuery<RoomResponse>;
