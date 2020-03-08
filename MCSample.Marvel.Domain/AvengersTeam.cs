using GraniteCore;
using System;
using System.Collections.Generic;

namespace MCSample.Marvel.Domain
{
    public class AvengersTeam : BaseModel<Guid>
    {
        public string Name { get; set; }
        public ICollection<Hero> HeroRoster { get; set; } = new List<Hero>();

        public AvengersTeam()
        {
            ID = Guid.NewGuid();
        }
    }
}
