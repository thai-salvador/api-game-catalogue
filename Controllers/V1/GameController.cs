using GameCatalogue.Exceptions;
using GameCatalogue.InputModel;
using GameCatalogue.Services;
using GameCatalogue.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogue.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Get all the games and return the paginated results
        /// </summary>
        /// <remarks>
        /// It's not possible to return results without pagination
        /// </remarks>
        /// <param name="page">It indicates which page are being consulted. Miniumum 1 page</param>
        /// <param name="quantity">It indicates the quantity of results per page. Miniumum 1 and maximum 50 results per page</param>
        /// <response code="200">It returns the game list</response>
        /// <response code="204">If doesn't have any games with the given Id</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Obtain([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Obtain(page, quantity);
            if (games.Count == 0)
                return NoContent();

            return Ok(games);
        }

        /// <summary>
        /// Search a game by its Id
        /// </summary>
        /// <param name="gameId">The Game's Id</param>
        /// <response code="200">It returns the game</response>
        /// <response code="204">If there's no game with the Id informed </response>
        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> Obtain([FromRoute] Guid gameId)
        {
            var game = await _gameService.Obtain(gameId);

            if (game == null)
                return NoContent();

            return Ok();
        }

        /// <summary>
        /// Add a new game
        /// </summary>
        /// <param name="gameInputModel">Game's information</param>
        /// <response code="200">In case of successful operation</response>
        /// <response code="422">If already exists a game with the same name and producer</response>
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> AddGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);
                return Ok(game);
            }
            catch (GameAlreadyRegisteredException ex)
            {
                return UnprocessableEntity("Already exists a game with this name for this producer"); //status code 422
            }
        }

        /// <summary>
        /// Update all game's information
        /// </summary>
        /// <param name="gameId">The Id of the game to be updated</param>
        /// <param name="gameInputModel">The new game's information</param>
        /// <response code="200">In case of successful operation</response>
        /// <response code="404">If doesn't have a game with this Id</response>
        [HttpPost("{gameId:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(gameId, gameInputModel);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game doesn't exist in the database");
            }
        }

        /// <summary>
        /// Update a game's price
        /// </summary>
        /// <param name="gameId">The Id of the game to be updated</param>
        /// <param name="price">The new game's price</param>
        /// <response code="200">In case of successful operation</response>
        /// <response code="404">If doesn't have a game with this Id</response>

        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(gameId, price);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game doesn't exist in the database");
            }
        }

        /// <summary>
        /// Delete a game
        /// </summary>
        /// <param name="gameId">The Id of the game to be deleted</param>
        /// <response code="200">In case of successful operation</response>
        /// <response code="404">If doesn't have a game with this Id</response>
        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid gameId)
        {
            try
            {
                await _gameService.Delete(gameId);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game doesn't exist in the database");
            }
        }
    }
}
