using GraniteCore;
using MCSample.Marvel.Domain;
using MCSample.Marvel.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCSample.Marvel.Services
{
    public interface IAvengersTeamService : IBaseService<AvengersTeamDto, AvengersTeam, int>
    {
        
    }
    public class AvengersTeamService : BaseService<AvengersTeamDto, AvengersTeam, int>, IAvengersTeamService
    {
        public AvengersTeamService(
            IBaseRepository<AvengersTeamDto, AvengersTeam, int> repository,
            IGraniteMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
