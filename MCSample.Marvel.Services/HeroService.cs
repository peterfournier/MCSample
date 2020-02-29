using GraniteCore;
using MCSample.Marvel.Domain;
using MCSample.Marvel.Domain.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace MCSample.Marvel.Services
{
    public interface IHeroService : IBaseService<HeroDto, Hero, int>
    {
        IList<HeroDto> GetTopCars(int take = 5);
    }

    public class HeroService : BaseService<HeroDto, Hero, int>, IHeroService
    {
        public HeroService(
            IBaseRepository<HeroDto, Hero, int> repository,
            IGraniteMapper mapper)
            : base(repository, mapper)
        {
        }

        public IList<HeroDto> GetTopCars(int take = 5)
        {
            // maintain encapsulation of the generic Repository
            return Repository.GetAll()
                             .Take(take)
                             .ToList();
        }
    }
}
