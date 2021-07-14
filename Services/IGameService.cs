using GameCatalogue.InputModel;
using GameCatalogue.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogue.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> Obtain(int page, int quantity);   // we'll use pagination, otherwise we'll have some problems when showing all games we have in the database
        Task<GameViewModel> Obtain(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Delete(Guid id);

    }
}
