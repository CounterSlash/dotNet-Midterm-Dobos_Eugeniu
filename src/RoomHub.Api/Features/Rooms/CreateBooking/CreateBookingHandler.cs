using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.CreateBooking;

public sealed class CreateBookingHandler(
    IRoomRepository rooms,
    IBookingRepository bookings,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider) : ICommandHandler<CreateBookingCommand, CreatedBookingResponse>
{
    public async Task<Result<CreatedBookingResponse>> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        var room = await rooms.GetByIdAsync(command.RoomId, cancellationToken);
        if (room is null)
            return RoomErrors.NotFound;

        var result = Booking.Create(
            command.RoomId,
            command.BookedBy,
            command.StartUtc,
            command.EndUtc,
            command.Attendees,
            room,
            timeProvider.GetUtcNow().UtcDateTime);
        if (result.IsFailure)
            return result.Error;

        var existing = await bookings.ListForRoomAsync(command.RoomId, cancellationToken);
        foreach (var booking in existing)
        {
            if (booking.Status == BookingStatus.Active &&
                booking.StartUtc < result.Value.EndUtc &&
                result.Value.StartUtc < booking.EndUtc)
            {
                return BookingErrors.RoomOccupied;
            }
        }

        bookings.Add(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var createdBooking = result.Value;
        return new CreatedBookingResponse(createdBooking.Id, createdBooking.RoomId, createdBooking.Status.ToString());
    }
}
