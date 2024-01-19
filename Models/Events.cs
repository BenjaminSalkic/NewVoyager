using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NewVoyager.Models;

public class Events
{
    [Key]
    public int EventID { get; set; }

    public string? Opis {get; set;}
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    [ForeignKey("Trip")]
    public int? TripID { get; set; }
    public Trips? Trip { get; set; }
}