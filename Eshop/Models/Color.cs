using System.Collections.Generic;

namespace Models;

public class Color : IColor
{
    public Guid ColorID { get; set; }
    public string ColorName { get; set; }
}
