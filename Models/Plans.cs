using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace NewVoyager.Models;

public class Plans
{
    [Key]
    public int PlanID { get; set; }
    public string PlanName { get; set; }

    public int VoyagerID { get; set; }
    public Voyager Voyager { get; set; }
    public List<int>? Attendees { get; set;}
    public ICollection<Trips>? Trips { get; set; }

    
}