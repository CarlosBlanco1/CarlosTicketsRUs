using Microsoft.AspNetCore.Mvc;
using TicketLibrary.Data;
using TicketLibrary.Services;

namespace BlazorTickets.contollers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet("getall")]
    public async Task<IEnumerable<Ticket>> GetTicketsAsync()
    {
        return await _ticketService.GetAllTicketsAsync();
    }

    [HttpPost("addticket")]
    public async Task AddATicket([FromBody] Ticket ticket)
    {
        await _ticketService.AddATicket(ticket);
    }

    [HttpPut("updateticket")]
    public async Task UpdateATicket([FromBody] Ticket ticket)
    {
        await _ticketService.UpdateATicket(ticket);
    }

}
