using System.Collections.Generic;

namespace Models;
{
    public interface IColor
    {
         public Guid ColorID { get; set; }
         public string ColorName { get; set; }
    }
}
