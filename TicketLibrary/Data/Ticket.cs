using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections;

namespace TicketLibrary.Data;
[SQLite.Table("Ticket")]
public partial class Ticket
{
    [SQLite.PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [SQLiteNetExtensions.Attributes.ForeignKey(typeof(Event))]
    public int? EventId { get; set; }

    public string? UserEmail { get; set; }

    public bool? IsScanned { get; set; }

    public string Ticketnumber { get; set; } = null!;

    [ManyToOne]
    public virtual Event? Event { get; set; }
}
