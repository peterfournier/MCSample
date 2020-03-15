using GraniteCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSample.Marvel.Domain.Dtos
{
    public class AvengersTeamDto : BaseDto<Guid>
    {
        public string Name { get; set; }
        public ICollection<HeroDto> HeroRoster { get; set; } = new List<HeroDto>();

        public AvengersTeamDto() : base()
        {
            this.ID = Guid.NewGuid();  // for demo purposes only
        }
    }
}
