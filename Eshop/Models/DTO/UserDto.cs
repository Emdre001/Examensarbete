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
    public int UserPhoneNr { get; set; }
    public string UserRole { get; set; }

    public virtual List<Guid> OrdersId { get; set; } = null;

    public UserDTO() {}
    public UserDTO (IUser org)
    {
        UserId = org.UserId;
        UserName = org.UserName;
        UserEmail = org.UserEmail;
        UserPassword = org.UserPassword;
        UserAddress = org.UserAddress;
        UserPhoneNr = org.UserPhoneNr;
        UserRole = org.UserRole;

        OrdersId = org.Orders?.Select(o => o.OrderId).ToList();
    }
}
