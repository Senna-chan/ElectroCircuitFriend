using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectroCircuitFriendRemake.Data;
using ElectroCircuitFriendRemake.Models;

namespace ElectroCircuitFriendRemake.Controllers
{
    public class ResistorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResistorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Resistors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Resistors.ToListAsync());
        }

        // GET: Resistors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resistor = await _context.Resistors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resistor == null)
            {
                return NotFound();
            }

            return View(resistor);
        }

        // GET: Resistors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resistors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Band1,Band2,Band3,Band4,Band5,Use4Bands,Use5Bands,Amount")] Resistor resistor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resistor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resistor);
        }

        // GET: Resistors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resistor = await _context.Resistors.SingleOrDefaultAsync(m => m.Id == id);
            if (resistor == null)
            {
                return NotFound();
            }
            return View(resistor);
        }

        // POST: Resistors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Band1,Band2,Band3,Band4,Band5,Use4Bands,Use5Bands,Amount")] Resistor resistor)
        {
            if (id != resistor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resistor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResistorExists(resistor.Id))
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
            return View(resistor);
        }

        // GET: Resistors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resistor = await _context.Resistors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resistor == null)
            {
                return NotFound();
            }

            return View(resistor);
        }

        // POST: Resistors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resistor = await _context.Resistors.SingleOrDefaultAsync(m => m.Id == id);
            _context.Resistors.Remove(resistor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResistorExists(int id)
        {
            return _context.Resistors.Any(e => e.Id == id);
        }
    }
}
