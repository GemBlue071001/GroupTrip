using GT.NotificationService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GT.NotificationService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    /// <summary>
    /// Get welcome message for the latest logged in user
    /// </summary>
    [HttpGet("welcome")]
    public IActionResult GetWelcomeMessage()
    {
        var message = _notificationService.GetWelcomeMessage();
        return Ok(new { message });
    }

    /// <summary>
    /// Get the latest user login information
    /// </summary>
    [HttpGet("latest-login")]
    public IActionResult GetLatestLogin()
    {
        var login = _notificationService.GetLatestUserLogin();
        
        if (login == null)
        {
            return NotFound(new { message = "No user has logged in yet." });
        }

        return Ok(login);
    }

    /// <summary>
    /// Get all user login events
    /// </summary>
    [HttpGet("all-logins")]
    public IActionResult GetAllLogins()
    {
        var logins = _notificationService.GetAllUserLogins();
        return Ok(logins);
    }
}