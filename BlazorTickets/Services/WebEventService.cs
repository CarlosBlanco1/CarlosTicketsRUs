using BlazorTickets.Data;
using Microsoft.EntityFrameworkCore;
using TicketLibrary.Data;
using TicketLibrary.Services;
namespace BlazorTickets.Services;

public class WebEventService : IEventService
{
    PostgresContext _context;
    public WebEventService(PostgresContext context)
    {
        _context = context;
    }
    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _context.Events.ToListAsync<Event>();
    }
}
