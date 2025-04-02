using System;
using System.Collections.Generic;

namespace Models;

public class User : IUser
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public string UserAddress { get; set; }
    public int UserPhoneNr { get; set; }
    public string UserRole { get; set; }

    public virtual List<IOrder> Orders { get; set; }
}
