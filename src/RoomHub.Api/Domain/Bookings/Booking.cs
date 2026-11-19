using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Domain.Bookings;

public enum BookingStatus
{
    Active,
    Cancelled
}

public sealed class Booking
{
    private Booking(Guid id, Guid roomId, string bookedBy, DateTime startUtc, DateTime endUtc, int attendees)
    {
        Id = id;
        RoomId = roomId;
        BookedBy = bookedBy;
        StartUtc = startUtc;
        EndUtc = endUtc;
        Attendees = attendees;
        Status = BookingStatus.Active;
    }

    private Booking()
    {
    }

    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = string.Empty;
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }

    public static Result<Booking> Create(
        Guid roomId,
        string? bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        Room room,
        DateTime nowUtc)
    {
        if (string.IsNullOrWhiteSpace(bookedBy))
            return BookingErrors.InvalidBookedBy;

        if (endUtc <= startUtc)
            return BookingErrors.InvalidRange;

        if (startUtc < nowUtc)
            return BookingErrors.StartInPast;

        var duration = endUtc - startUtc;
        var maximumDuration = room.Capacity > 10
            ? TimeSpan.FromHours(2)
            : TimeSpan.FromHours(4);

        if (duration > maximumDuration)
            return room.Capacity > 10
                ? BookingErrors.LargeRoomMaximumDurationExceeded
                : BookingErrors.MaximumDurationExceeded;

        if (attendees <= 0)
            return BookingErrors.InvalidAttendees;

        if (attendees > room.Capacity)
            return BookingErrors.AttendeesExceedCapacity;

        return new Booking(Guid.NewGuid(), roomId, bookedBy.Trim(), startUtc, endUtc, attendees);
    }

    public Result<Unit> Cancel(DateTime nowUtc)
    {
        if (Status == BookingStatus.Cancelled)
            return BookingErrors.AlreadyCancelled;

        if (StartUtc - nowUtc < TimeSpan.FromMinutes(60))
            return BookingErrors.CancellationDeadlineExceeded;

        Status = BookingStatus.Cancelled;
        return Unit.Value;
    }
}