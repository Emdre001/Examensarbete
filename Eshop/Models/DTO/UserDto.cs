using System;
using System.Collections.Generic;

namespace Models.DTO;

public class UserDTO
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public string UserAddress { get; set; }
    public string UserPhoneNr { get; set; }
    public string UserRole { get; set; }

    public string UserSalt { get; set; } = null;

    public virtual List<Guid> OrdersId { get; set; } = null;
}
