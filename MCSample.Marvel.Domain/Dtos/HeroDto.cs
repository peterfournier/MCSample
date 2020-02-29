using GraniteCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSample.Marvel.Domain.Dtos
{
    public class HeroDto : BaseDto<int>
    {
        public string Name { get; set; }
    }
}
