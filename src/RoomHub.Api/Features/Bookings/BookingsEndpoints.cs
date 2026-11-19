using RoomHub.Api.Features.Bookings.GetBookingById;
using RoomHub.Api.Features.Bookings.CancelBooking;

namespace RoomHub.Api.Features.Bookings;

public static class BookingsEndpoints
{
    public static void MapBookings(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bookings").WithTags("Bookings");
        GetBookingByIdEndpoint.Map(group);
        CancelBookingEndpoint.Map(group);
    }
}
