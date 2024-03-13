using Microsoft.AspNetCore.Mvc;
using TicketLibrary.Data;
using TicketLibrary.Services;

namespace BlazorTickets.contollers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet("getall")]
    public async Task<IEnumerable<Event>> GetEventsAsync()
    {
        return await _eventService.GetAllEventsAsync();
    }


}
