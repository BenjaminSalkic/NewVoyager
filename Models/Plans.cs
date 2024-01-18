using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NewVoyager.Models;

public class Plans
{
    [Key]
    public int PlanID { get; set; }
    public string PlanName { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string AppUserID { get; set; }
    public ApplicationUser AppUser { get; set; }
    public List<string>? Attendees { get; set; }
    public ICollection<Trips>? Trips { get; set; }

    
}