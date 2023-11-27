using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace NewVoyager.Models;

public class Events
{
    [Key]
    public int EventID { get; set; }

    public string? Opis {get; set;}
    public int? OrderNum {get; set;}

    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    public Trips? Trips { get; set; }
}