using GraniteCore;
using System;

namespace MCSample.Marvel.Domain
{
    public class Hero : BaseModel<Guid>
    {
        public string Name { get; set; }
    }
}
