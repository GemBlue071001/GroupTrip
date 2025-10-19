
using GT.NotificationService.Application.Interfaces;
using GT.NotificationService.Application.Messaging;

namespace GT.Notification.Application.Services;


public class NotificationService : INotificationService
{
    private readonly UserLoginCache _userLoginCache;

    public NotificationService(UserLoginCache userLoginCache)
    {
        _userLoginCache = userLoginCache;
    }

    public string GetWelcomeMessage()
    {
        var latestLogin = _userLoginCache.GetLatestLogin();
        
        if (latestLogin == null)
        {
            return "No user has logged in yet.";
        }

        return $"Welcome {latestLogin.UserName}";
    }

    public IEnumerable<UserLoginInfo> GetAllUserLogins()
    {
        return _userLoginCache.GetAllLogins()
            .Select(e => new UserLoginInfo
            {
                UserId = e.UserId,
                UserName = e.UserName,
                Email = e.Email,
                LoggedInAt = e.LoggedInAt,
                Message = $"Welcome {e.UserName}"
            });
    }

    public UserLoginInfo? GetLatestUserLogin()
    {
        var latestLogin = _userLoginCache.GetLatestLogin();
        
        if (latestLogin == null) return null;

        return new UserLoginInfo
        {
            UserId = latestLogin.UserId,
            UserName = latestLogin.UserName,
            Email = latestLogin.Email,
            LoggedInAt = latestLogin.LoggedInAt,
            Message = $"Welcome {latestLogin.UserName}"
        };
    }
}

public class UserLoginInfo
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime LoggedInAt { get; set; }
    public string Message { get; set; } = string.Empty;
}