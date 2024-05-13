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
    public string? AppUserID { get; set; }
    public ApplicationUser? AppUser { get; set; }
    public ICollection<ApplicationUser>? Collaborators { get; set; }

    public ICollection<Trips>? Trips { get; set; }
    public ICollection<ChatMessage>? ChatMessages { get; set; } // Navigation property for chat messages

    public Plans()
    {
        // Initialize Collaborators collection in the constructor
        Collaborators = new List<ApplicationUser>();
        ChatMessages = new List<ChatMessage>();
    }

    
}