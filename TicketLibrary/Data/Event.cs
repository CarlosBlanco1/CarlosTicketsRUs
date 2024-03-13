using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace TicketLibrary.Data;
[SQLite.Table("Event")]
public partial class Event
{
    [SQLite.PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string? EventName { get; set; }

    public DateTime? DateOfEvent { get; set; }

    [OneToMany]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
