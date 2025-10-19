using GT.Notification.Application.Services;

namespace GT.NotificationService.Application.Interfaces;

public interface INotificationService
{
    string GetWelcomeMessage();
    IEnumerable<UserLoginInfo> GetAllUserLogins();
    UserLoginInfo? GetLatestUserLogin();
}
