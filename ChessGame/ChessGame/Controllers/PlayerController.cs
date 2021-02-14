using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessGame.Business.Contracts.Models;
using ChessGame.Business.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessGame.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(IPlayerService playerService, ILogger<PlayerController> logger)
        {
            _playerService = playerService;
            _logger = logger;
        }

        public async Task<ActionResult> Index(string searchTerm)
        {
            ViewData["SearchTerm"] = searchTerm;

            var players = await _playerService.FindAsync(searchTerm);

            if (players == null)
            {
                return NotFound();
            }
            return View(players);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Rating")] Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {                   
                    await _playerService.CreateAsync(player);
                    await _playerService.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error creating a new player");
                ModelState.AddModelError("", "Unable to create player");
            }
            return View(player);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var player = await _playerService.GetByIdAsync(id);
            if (player == null)
            {
                // either redirect to Index action, or display 404 (return NotFound() );
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }


        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost([Bind("Id, FirstName,LastName,Rating")] Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _playerService.EditAsync(player);
                    await _playerService.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error editing a player");
                ModelState.AddModelError("", "Unable to edit player");
            }
            return View(player);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerService.GetByIdAsync(id);
            if (player == null)
            {
                // either redirect to Index action, or display 404 (return NotFound() );
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var player = await _playerService.GetByIdAsync(id);
            if (player == null)
            {
                // either redirect to Index action, or display 404 (return NotFound() );
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _playerService.DeleteAsync(id);
                await _playerService.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error deleting the player");
                ModelState.AddModelError("", "Unable to delete player");
            }

            return View(player);
        }
    }
}
