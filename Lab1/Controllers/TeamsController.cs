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

namespace Lab1.Controllers
{
    [Authorize(Roles = "Manager, Player")]
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TeamsController> _logger;


        public TeamsController(ApplicationDbContext context, ILogger<TeamsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
              return View(await _context.Team.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        [Authorize(Roles = "Manager")] //Allow Manager only
        // GET: Teams/Create
        public IActionResult Create()
        {
            string logMsg = $"User {User.Identity.Name} created a team";
            _logger.LogInformation(logMsg);
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")] //Allow Manager only
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamName,Email,EstablishedDate")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            string logMsg = $"User {User.Identity.Name} team created: {team}";
            _logger.LogInformation(logMsg);
            return View(team);
        }

        [Authorize(Roles = "Manager")] //Manager only
        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            string logMsg = $"User {User.Identity.Name} team edit for id : {id}";
            _logger.LogInformation(logMsg);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")] //Allow Manager only
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamName,Email,EstablishedDate")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            string logMsg = $"User {User.Identity.Name} team edit for id : {id}";
            _logger.LogInformation(logMsg);
            return View(team);
        }

        [Authorize(Roles = "Manager")] //Manager only
        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            string logMsg = $"User {User.Identity.Name} team delete for id : {id}";
            _logger.LogInformation(logMsg);
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager")] //Allow Manager only
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Team == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Team'  is null.");
            }
            var team = await _context.Team.FindAsync(id);
            if (team != null)
            {
                _context.Team.Remove(team);
            }
            
            await _context.SaveChangesAsync();

            string logMsg = $"User {User.Identity.Name} team delete confirmed for id : {id}";
            _logger.LogInformation(logMsg);

            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
          return _context.Team.Any(e => e.Id == id);
        }
    }
}
