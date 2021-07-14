using GameCatalogue.Entities;
using GameCatalogue.Exceptions;
using GameCatalogue.InputModel;
using GameCatalogue.Repositories;
using GameCatalogue.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogue.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task Delete(Guid id)
        {
            var game = await _gameRepository.Obtain(id);

            if (game == null)
                throw new GameNotRegisteredException();

            await _gameRepository.Delete(id);
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var gameEntity = await _gameRepository.Obtain(game.Name, game.Producer);

            if (gameEntity.Count > 0)
                throw new GameAlreadyRegisteredException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task<List<GameViewModel>> Obtain(int page, int quantity)
        {
            var games = await _gameRepository.Obtain(page, quantity);

            // I could have used an automapping to make this conversion
            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Obtain(Guid id)
        {
            var game = await _gameRepository.Obtain(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var gameEntity = await _gameRepository.Obtain(id);

            if (gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Name = game.Name;
            gameEntity.Producer = game.Producer;
            gameEntity.Price = game.Price;

            await _gameRepository.Update(gameEntity);
        }

        public async Task Update(Guid id, double price)
        {
            var gameEntity = await _gameRepository.Obtain(id);

            if (gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Price = price;

            await _gameRepository.Update(gameEntity);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();   //This will distruct the connections that the entity may have
        }
    }
}
