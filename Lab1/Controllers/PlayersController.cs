// we, group 14 (Austin Enes (000818994), Mingi Kang(000818677), Linh Nguyen(000800045)),, certify that this material is our
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Lab1.Controllers
{
    [Authorize(Roles = "Manager, Player")]
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlayersController> _logger;


        public PlayersController(ApplicationDbContext context, ILogger<PlayersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
              return View(await _context.Player.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        [Authorize(Roles = "Manager")] //Allow Manager only
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamName", "TeamName"); //added dropdown list

            string logMsg = $"User {User.Identity.Name} created a player record";
            _logger.LogInformation(logMsg);
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")] //Allow Manager only
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,TeamName,BirthDate")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamName", "TeamName"); //added dropdown list

            string logMsg = $"User {User.Identity.Name} created a player record";
            _logger.LogInformation(logMsg);
            return View(player);
        }

        // GET: Players/Edit/5
        [Authorize(Roles = "Manager")] //Allow Manager only
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamName", "TeamName", player.TeamName); //added dropdown list

            string logMsg = $"User {User.Identity.Name} player edit for id : {id}";
            _logger.LogInformation(logMsg);

            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")] //Allow Manager only
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,TeamName,BirthDate")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamName", "TeamName", player.TeamName); //added dropdown list

            string logMsg = $"User {User.Identity.Name} player edit for id : {id}";
            _logger.LogInformation(logMsg);
            return View(player);
        }

        // GET: Players/Delete/5
        [Authorize(Roles = "Manager")] //Allow Manager only
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            string logMsg = $"User {User.Identity.Name} player delete for id : {id}";
            _logger.LogInformation(logMsg);
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager")] //Allow Manager only
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Player == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Player'  is null.");
            }
            var player = await _context.Player.FindAsync(id);
            if (player != null)
            {
                _context.Player.Remove(player);
            }
            
            await _context.SaveChangesAsync();
            string logMsg = $"User {User.Identity.Name} player delete for id : {id}";
            _logger.LogInformation(logMsg);

            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
          return _context.Player.Any(e => e.Id == id);
        }
    }
}
