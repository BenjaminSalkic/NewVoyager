using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NewVoyager.Models;

public class Trips
{
    [Key]
    public int TripID { get; set; }
    public string TripName { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public ICollection<Events>? Events { get; set; }
    [ForeignKey("Plan")]

    public int? PlanID { get; set; }
    public Plans? Plan { get; set; }
}