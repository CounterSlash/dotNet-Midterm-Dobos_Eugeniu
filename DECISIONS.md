# DECISIONS

Answer **yourself**, in your own words, in 3–5 sentences per question.
Refer to concrete files and classes from your own repo (for example `Features/Bookings/CreateBooking/...`).
An unanswered question gives 0 points.

## 1. A rule you moved
Pick one business rule (R1–R8). In which file did it live in the starter and in which file does it live now? Why there?

_Rule 1 (the non-empty room name and >0 capacity one) was originally in Services/RoomService.cs. Now it lives in Domain/Rooms/Room.cs as part of the Create method. I moved it there because it's better practice than what the original project did (put it in the service). No rooms can ever have empty names or <0 capacity so it's better for the restrictions to be in the class_

## 2. Result instead of an exception
Why is `Result` a better choice than an exception for "the room is occupied"? What happens in your code when an `Error` is returned?

_Result is better because "the room is occupied" is an error that will definitely happen at some point. Because we know it will happen, it's better to explicitly handle it, and leave the exceptions for unexpected errors._

_In my code, a returned Error will get grabbed by the handler for that API operation (CreateRoomHandler for example) and returned up until it gets converted to a TypedResults.Problem._

## 3. Who knows about EF Core?
Which classes in your project know about EF Core and which do not? What do you gain and what do you pay with Repository + Unit of Work?

_EF Core is known by the repositories in `Infrastructure/Persistence/Repositories/`, the configurations for the db in `Infrastructure/Persistence/Configuration/`, DatabaseInitializer adn RoomHubDbContext in `Infrastructure/Persistence/`. The domain and endpoints are not._

_With Repository + Unit of Work, the domain and database are more separate and the repositories do not directly expose the DB but use operations like GetByIdAsync_

_The tradeoff is a lot of classes and interfaces to keep track of._

## 4. The flow of a request
Draw in text (ASCII arrows) the path of a `POST /api/rooms/{roomId}/bookings` request through your project, with the **real** names of files and classes, from the endpoint to the database.

_Request -> `Features/Rooms/CreateBooking/CreateBookingEndpoint.cs` -> `Features/Rooms/CreateBooking/CreateBookingHandler.cs` -> `Infrastructure/Persistence/Repositories/BookingRepository.cs` -> `Infrastructure/Persistence/RoomHubDbContext.cs`_

## 5. What you rejected from what the AI gave you
Give a concrete example of code or an idea proposed by the AI that you corrected or rejected, and say why.

_Initially, the proposed GetRoomByIdEndpoint (and get for bookings) had all the logic in the endpoint file, while the other endpoints were structured properly with separate endpoint and handler files. I corrected this by telling it to follow the structure of the other endpoints. This way it would respect the structure and be consistent even if an endpoint is simple._

## 6. If you had 30 more minutes
What would you improve and what risk did you leave in the project?

_If I had more time, I'd make more tests and probably a collection in Postman that I can run and look at outside of Rider._

_A risk would be that the schema for the database is rigid so if I want to add something extra like `quality` to `RoomConfiguration`, I'd have to delete the DB and regenerate._
