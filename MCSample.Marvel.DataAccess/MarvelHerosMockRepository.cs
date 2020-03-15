using GraniteCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MCSample.Marvel.DataAccess
{
    public class MarvelHerosMockRepository<TDtoModel, TEntity, TPrimaryKey> : IBaseRepository<TDtoModel, TEntity, TPrimaryKey>
        where TDtoModel : class, IDto<TPrimaryKey>, new()
        where TEntity : class, IBaseIdentityModel<TPrimaryKey>, new()
    {
        private readonly IList<TEntity> _store = new List<TEntity>();
        protected readonly IGraniteMapper _mapper;

        public MarvelHerosMockRepository(
            IGraniteMapper mapper
            )
        {
            _mapper = mapper;
        }


        public Task<TDtoModel> Create(TDtoModel entity)
        {
            return Task.Run(() =>
            {
                _store.Add(_mapper.Map<TDtoModel, TEntity>(entity));

                return entity;
            });
        }

        public Task Delete(TPrimaryKey id)
        {
            return Task.Run(() =>
            {
                var ent = _store.FirstOrDefault(x => x.ID.Equals(id));
                if (ent != null)
                    _store.Remove(ent);
            });
        }

        public IQueryable<TDtoModel> GetAll()
        {
            return _mapper.Map<TEntity, TDtoModel>(_store.AsQueryable());
        }

        public Task<TDtoModel> GetByID(TPrimaryKey id)
        {
            return Task.Run(() =>
            {
                return _mapper.Map<TEntity, TDtoModel>(_store.FirstOrDefault(x => x.ID.Equals(id)));
            });
        }

        public Task<TDtoModel> GetByID(TPrimaryKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException("Parameter 'includeProperties' not supported");
        }

        public Task Update(TPrimaryKey id, TDtoModel dto)
        {
            return Task.Run(() =>
            {
                try
                {
                    var entity = _store.Single(x => x.ID.Equals(id));
                    var index = _store.IndexOf(entity);

                    _store[index] = _mapper.Map<TDtoModel, TEntity>(dto);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex); // Logger here
                }
            });
        }
    }
}
