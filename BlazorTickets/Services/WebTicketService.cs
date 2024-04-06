using BlazorTickets.Data;
using Microsoft.EntityFrameworkCore;
using TicketLibrary.Data;
using TicketLibrary.Services;
namespace BlazorTickets.Services;

public class WebTicketService(IDbContextFactory<PostgresContext> dbContextFactory) : ITicketService
{
    public async Task AddATicket(Ticket t)
    {
        var _context = await dbContextFactory.CreateDbContextAsync();
        try
        {
            t.Id = _context.Tickets.Max(t => t.Id) + 1;
            _context.Tickets.Add(t);
            _context.SaveChanges();
        }
        catch (Exception ex) { throw; }
    }

    public async Task ChangeBaseAddress(string newBaseAddress)
    {
        throw new NotImplementedException();
    }

    public void ChangeConnectivity(bool _IsConnected)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        var _context = await dbContextFactory.CreateDbContextAsync();
        return await _context.Tickets.ToListAsync<Ticket>();
    }

    public Task ResetLocalTicketsDB()
    {
        throw new NotImplementedException();
    }

    public Task SyncDatabases()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateATicket(Ticket t)
    {
        var _context = await dbContextFactory.CreateDbContextAsync();
        _context.Tickets.Update(t);
        _context.SaveChanges();
    }

    Task ITicketService.SetTimer(int seconds)
    {
        throw new NotImplementedException();
    }
}
