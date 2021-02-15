using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessGame.Business.Contracts.Models;
using ChessGame.Business.Contracts.Services;
using ChessGame.Business.InternalClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessGame.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ILogger<PlayerController> _logger;

        public GameController(IGameService gameService, ILogger<PlayerController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        public async Task<ActionResult> Index(string searchTerm)
        {
            ViewData["SearchTerm"] = searchTerm;

            var games = await _gameService.FindAsync(searchTerm);

            if (games == null)
            {
                return NotFound();
            }
            return View(games);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Date,WhitePlayerId,BlackPlayerId,OpeningClassification,Result")] Game game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _gameService.CreateAsync(game);
                    await _gameService.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (GameValidationException ex)
            {
                _logger.LogError(ex, "The game contains invalid player ids");
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error creating a new game");
                ModelState.AddModelError("", "Unable to create game");
            }
            return View(game);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
            {
                // either redirect to Index action, or display 404 (return NotFound() );
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }


        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost([Bind("Id,Date,WhitePlayerId,BlackPlayerId,OpeningClassification,Result")] Game game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _gameService.EditAsync(game);
                    await _gameService.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (GameValidationException ex)
            {
                _logger.LogError(ex, "The game contains invalid player ids");
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error editing a game");
                ModelState.AddModelError("", "Unable to edit game");
            }
            return View(game);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
            {
                // either redirect to Index action, or display 404 (return NotFound() );
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
            {
                // either redirect to Index action, or display 404 (return NotFound() );
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _gameService.DeleteAsync(id);
                await _gameService.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error deleting the game");
                ModelState.AddModelError("", "Unable to delete game");
            }

            return View(game);
        }
    }
}
