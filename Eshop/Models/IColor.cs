using System.Collections.Generic;
using System;

namespace Models;

public interface IColor
{
    public Guid ColorId { get; set; }
    public string ColorName { get; set; }
}
