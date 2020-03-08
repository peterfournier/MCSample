using GraniteCore;
using MCSample.Marvel.Domain;
using MCSample.Marvel.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCSample.Marvel.Services
{
    public interface IAvengersTeamService : IBaseService<AvengersTeamDto, AvengersTeam, Guid>
    {
        
    }
    public class AvengersTeamService : BaseService<AvengersTeamDto, AvengersTeam, Guid>, IAvengersTeamService
    {
        public AvengersTeamService(
            IBaseRepository<AvengersTeamDto, AvengersTeam, Guid> repository,
            IGraniteMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
