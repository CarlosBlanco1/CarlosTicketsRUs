using BlazorTickets.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TicketLibrary.Data;
using TicketLibrary.Services;
namespace BlazorTickets.Services;

public class WebEventService(IDbContextFactory<PostgresContext> dbContextFactory) : IEventService
{
    public async Task<List<Event>> GetAllEventsAsync()
    {
        var _context = await dbContextFactory.CreateDbContextAsync();
        return await _context.Events.ToListAsync<Event>();
    }
}
