using System;
using System.Collections.Generic;

namespace BaseAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }
}
