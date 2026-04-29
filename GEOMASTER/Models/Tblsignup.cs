using System;
using System.Collections.Generic;

namespace GEOMASTER.Models;

public partial class Tblsignup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Role { get; set; }
}
