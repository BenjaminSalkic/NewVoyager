using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace NewVoyager.Models;

public class Trips
{
    [Key]
    public int TripID { get; set; }
    public string TripName { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public ICollection<Events>? Events { get; set; }
    public int? PlanID { get; set; }
    public Plans? Plan { get; set; }
}