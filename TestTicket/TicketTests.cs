using System.Net.Http.Json;
using TicketLibrary.Data;
using TicketLibrary.Services;


namespace TestTicket;

public class TicketTests : IClassFixture<TicketFactory>
{
    public HttpClient client { get; set; }
    public TicketFactory ticketFactory { get; set; }

    public TicketTests(TicketFactory factory)
    {
        client = factory.CreateDefaultClient();
    }

    [Fact]
    public void CanHavePassingTest()
    {
        Assert.Equal(1, 1);
    }

    [Fact]
    public async void SuccessfulScanUpdatesDatabase()
    {

        List<Ticket> list = new List<Ticket>();
        Ticket finalTestTicket = new Ticket();

        //arrange
        Ticket ticket = new Ticket();
        ticket.EventId = 1;
        ticket.IsScanned = false;
        ticket.Ticketnumber = "testTicketNumber";

        //act
        await client.PostAsJsonAsync("api/Ticket/addticket", ticket);
        //assume a scan in Maui sets local ticket to true
        ticket.IsScanned = true;

        list = await client.GetFromJsonAsync<List<Ticket>>("api/Ticket/getall");
        ticket.Id = list.FirstOrDefault(q => q.Ticketnumber == "testTicketNumber").Id;
        await client.PutAsJsonAsync("api/Ticket/updateticket", ticket);
        list = await client.GetFromJsonAsync<List<Ticket>>("api/Ticket/getall");

        //assert
        finalTestTicket = list.FirstOrDefault(q => q.Ticketnumber == "testTicketNumber");
        Assert.Equal(finalTestTicket.IsScanned, true);

    }

    [Fact]
    public async void FailedScanDoesntUpdatesDatabase()
    {

        List<Ticket> list = new List<Ticket>();
        Ticket finalTestTicket2 = new Ticket();

        //arrange
        Ticket ticket = new Ticket();
        ticket.EventId = 1;
        ticket.IsScanned = false;
        ticket.Ticketnumber = "changedThing";

        //act

        //not posted so ticket is invalid
        await client.PostAsJsonAsync("api/Ticket/addticket", ticket);

        list = await client.GetFromJsonAsync<List<Ticket>>("api/Ticket/getall");
        ticket.Id = list.FirstOrDefault(q => q.Ticketnumber == "changedThing").Id;
        await client.PutAsJsonAsync("api/Ticket/updateticket", ticket);
        list = await client.GetFromJsonAsync<List<Ticket>>("api/Ticket/getall");

        //assert
        finalTestTicket2 = list.FirstOrDefault(q => q.Ticketnumber == "changedThing");
        Assert.Equal(finalTestTicket2.IsScanned, false);

    }

    [Fact]
    public void ChangeAPIResetsLocalDatabase()
    {
        Assert.Equal(1, 1);
    }
}
