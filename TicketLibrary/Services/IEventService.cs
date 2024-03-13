using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketLibrary.Data;

namespace TicketLibrary.Services;

public interface IEventService
{
    public Task<List<Event>> GetAllEventsAsync();

}
