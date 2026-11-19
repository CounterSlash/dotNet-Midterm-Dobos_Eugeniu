using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence.Repositories;

public sealed class BookingRepository : IBookingRepository
{
    private readonly Dictionary<Guid, Booking> _bookings = new();
    private readonly Lock _lock = new();

    public void Add(Booking booking)
    {
        lock (_lock)
            _bookings[booking.Id] = booking;
    }

    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        lock (_lock)
            return Task.FromResult(_bookings.GetValueOrDefault(id));
    }

    public Task<IReadOnlyList<Booking>> ListForRoomAsync(Guid roomId, CancellationToken cancellationToken)
    {
        lock (_lock)
            return Task.FromResult<IReadOnlyList<Booking>>(
                _bookings.Values.Where(b => b.RoomId == roomId).ToList());
    }
}
