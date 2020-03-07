using GraniteCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MCSample.Marvel.Domain
{
    public class AvengersTeam : BaseModel<int>
    {
        public string Name { get; set; }
        public ICollection<Hero> HeroRoster { get; set; } = new List<Hero>();

        public AvengersTeam()
        {
        }
    }
}
