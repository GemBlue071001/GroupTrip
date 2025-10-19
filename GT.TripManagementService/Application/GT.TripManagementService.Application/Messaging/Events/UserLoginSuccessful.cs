namespace GT.TripManagementService.Application.Messaging.Events;

public record UserLoginSuccesful(Guid UserId, string UserName, string Email, DateTime LoggedInAt);

