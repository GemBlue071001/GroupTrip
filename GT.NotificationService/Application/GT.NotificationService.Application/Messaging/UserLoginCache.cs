using System.Collections.Concurrent;

namespace GT.NotificationService.Application.Messaging
{
    public class UserLoginCache
    {
        private readonly ConcurrentBag<UserLoginSuccessful> _loginEvents = new();

        public void AddUserLogin(UserLoginSuccessful loginEvent)
        {
            _loginEvents.Add(loginEvent);
        }

        public IEnumerable<UserLoginSuccessful> GetAllLogins()
        {
            return _loginEvents.OrderByDescending(e => e.LoggedInAt);
        }

        public UserLoginSuccessful? GetLatestLogin()
        {
            return _loginEvents.OrderByDescending(e => e.LoggedInAt).FirstOrDefault();
        }

        public IEnumerable<UserLoginSuccessful> GetLoginsByUserName(string userName)
        {
            return _loginEvents
                .Where(e => e.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(e => e.LoggedInAt);
        }
    }
}