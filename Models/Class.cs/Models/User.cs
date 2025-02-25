using System;
using System.Collections.Generic;
using Models;

namespace Models;

public class User : IUser
{
    public Guid UserID { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public string UserAddress { get; set; }
    public int UserPhoneNr { get; set; }
    public string UserRole { get; set; }
}
