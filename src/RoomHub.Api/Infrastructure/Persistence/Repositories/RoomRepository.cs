using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Infrastructure.Persistence.Repositories;

public sealed class RoomRepository : IRoomRepository
{
    private readonly Dictionary<Guid, Room> _rooms = new();
    private readonly Lock _lock = new();

    public void Add(Room room)
    {
        lock (_lock)
            _rooms[room.Id] = room;
    }

    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        lock (_lock)
            return Task.FromResult(_rooms.GetValueOrDefault(id));
    }
}
