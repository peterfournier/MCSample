using GraniteCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSample.Marvel.Domain.Dtos
{
    public class AvengersTeamDto : BaseDto<int>
    {
        public string Name { get; set; }
        public ICollection<HeroDto> HeroRoster { get; set; } = new List<HeroDto>();
    }
}
