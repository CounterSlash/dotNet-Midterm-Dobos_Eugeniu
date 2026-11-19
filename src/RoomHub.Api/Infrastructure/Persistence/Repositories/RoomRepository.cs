using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Infrastructure.Persistence.Repositories;

public sealed class RoomRepository(RoomHubDbContext db) : IRoomRepository
{
    public void Add(Room room)
        => db.Rooms.Add(room);

    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => db.Rooms.AsNoTracking().FirstOrDefaultAsync(room => room.Id == id, cancellationToken);
}
