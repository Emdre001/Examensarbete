using System;
using System.Collections.Generic;

namespace Models.DTO;

public class UserDTO
{
    public Guid UserID { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public string UserAddress { get; set; }
    public int UserPhoneNr { get; set; }
    public string UserRole { get; set; }
}
