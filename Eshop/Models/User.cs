using System;
using System.Collections.Generic;

namespace Models;

public class User
{
    public virtual Guid UserId { get; set; }
    public virtual string UserName { get; set; }
    public virtual string Email { get; set; }
    public virtual string Password { get; set; }
    public virtual string? Address { get; set; }
    public virtual string? PhoneNr { get; set; }
    public virtual string Role { get; set; }

    public virtual List<Order> Orders { get; set; }
}
