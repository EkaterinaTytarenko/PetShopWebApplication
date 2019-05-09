using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShopWebApplication.Data;
using PetShopWebApplication.Models;

namespace PetShopWebApplication.Controllers
{
    public class CagesController : Controller
    {
        private readonly AnimalsContext _context;
        private static int shopId;

        public CagesController(AnimalsContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Details(int? id)
        {
            shopId = (int)id;
            var animalsContext = _context.Cages.Where(c=>c.ShopID==shopId).Include(c => c.Shop);
            return View(await animalsContext.ToListAsync());
        }

        // GET: Cages/Create
        public IActionResult Create()
        {
            var cage = new Cage();
            cage.ShopID = shopId;
            cage.Shop = _context.Shops.Where(s => s.ID == shopId).First();
            return View(cage);
        }

        // POST: Cages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Square")] Cage cage)
        {
            cage.ID = Guid.NewGuid().GetHashCode();
            cage.ShopID = shopId;
            if (ModelState.IsValid)
            {
                _context.Add(cage);
                await _context.SaveChangesAsync();
                return Redirect("https://localhost:44324/Cages/Details/" + shopId);
            }
            return View(cage);
        }

        // GET: Cages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cage = await _context.Cages.FindAsync(id);
            if (cage == null)
            {
                return NotFound();
            }
            return View(cage);
        }

        // POST: Cages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ShopID,Number,Square")] Cage cage)
        {
            cage.ShopID = shopId;
            cage.Shop = _context.Shops.Where(s => s.ID == shopId).First();
            if (id != cage.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CageExists(cage.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("https://localhost:44324/Cages/Details/"+shopId);
            }
            return View(cage);
        }

        // GET: Cages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cage = await _context.Cages
                .Include(c => c.Shop)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cage == null)
            {
                return NotFound();
            }

            return View(cage);
        }

        // POST: Cages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cage = await _context.Cages.FindAsync(id);
            _context.Cages.Remove(cage);
            await _context.SaveChangesAsync();
            return Redirect("https://localhost:44324/Cages/Details/" + shopId);
        }

        private bool CageExists(int id)
        {
            return _context.Cages.Any(e => e.ID == id);
        }
    }
}
