using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewVoyager.Models;

public class ApplicationUser : IdentityUser
{
     public string? FirstName { get; set; }
     public string? LastName { get; set; }
     public ICollection<Plans>? Plans { get; set; }
}
