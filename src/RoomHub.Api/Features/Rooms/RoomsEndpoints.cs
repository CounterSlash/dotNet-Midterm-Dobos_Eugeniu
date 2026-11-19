using RoomHub.Api.Features.Rooms.CreateRoom;
using RoomHub.Api.Features.Rooms.GetRoomById;
using RoomHub.Api.Features.Rooms.CreateBooking;

namespace RoomHub.Api.Features.Rooms;

public static class RoomsEndpoints
{
    public static void MapRooms(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/rooms").WithTags("Rooms");
        CreateRoomEndpoint.Map(group);
        GetRoomByIdEndpoint.Map(group);
        CreateBookingEndpoint.Map(group);
    }
}
