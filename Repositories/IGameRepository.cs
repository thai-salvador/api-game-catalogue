using GameCatalogue.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogue.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Obtain(int page, int quantity);
        Task<Game> Obtain(Guid id);
        Task<List<Game>> Obtain(string name, string producer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);

    }
}
