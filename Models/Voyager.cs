using System;
using System.Collections.Generic;

namespace NewVoyager.Models;
public class Voyager
{
    public int ID { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }

    public ICollection<Plans>? Plans { get; set; }
}