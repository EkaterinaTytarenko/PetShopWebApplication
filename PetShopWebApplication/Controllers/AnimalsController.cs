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
    public class AnimalsController : Controller
    {
        private readonly AnimalsContext _context;

        public AnimalsController(AnimalsContext context)
        {
            _context = context;
        }
        
        // GET: Animals
        public async Task<IActionResult> Index(string searchString)
        {   ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
            var animalsContext1 = _context.Animals.Include(a => a.Cage).Include(a => a.Color).Include(a => a.Shop).Include(a => a.Species).Where(a => a.Species.Name.StartsWith(searchString));
            return View(await animalsContext1.ToListAsync());
            }
            var animalsContext = _context.Animals.Include(a => a.Cage).Include(a => a.Color).Include(a => a.Shop).Include(a => a.Species);
            return View(await animalsContext.ToListAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Cage)
                .Include(a => a.Color)
                .Include(a => a.Shop)
                .Include(a => a.Species)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ColorsDropDownList();
            ShopsDropDownList();
            SpeciesDropDownList();
            SexDropDownList();
            //CagesDropDownList();
            return View();
        }

        public IActionResult Choosecage([Bind("Name,Sex,ShopID,CageID,SpeciesID,Date,ColorID,Price")] Animal animal)
        {
            if (animal.CageID == null&&animal.Name!=null)
            {
                animal1 = animal;
                CagesDropDownList(animal.ShopID);
                return View(animal);
            }
            animal1.CageID = animal.CageID;
            if(animal1.CageID==null)
            {
                Animal animal2 = animal1;
                return Choosecage(animal2);
            }
            _context.Add(animal1);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task< IActionResult> Choosecage1([Bind("ID,Name,Sex,ShopID,CageID,SpeciesID,Date,ColorID,Price")] Animal animal)
        {
 
            if (animal.CageID == null && animal.Name != null)
            {
                animal1 = animal;
                CagesDropDownList(animal.ShopID);
                return View(animal);
            }
            animal1.CageID = animal.CageID;
            if (animal1.CageID == null)
            {
                Animal animal2 = animal1;
                return Choosecage(animal2);
            }
            var animalToUpdate = _context.Animals
                           .FirstOrDefault(a => a.ID == animal1.ID);
            
                try
                {
                    animalToUpdate.CageID = animal1.CageID;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }

            return Redirect("https://localhost:44324/Animals/Edit/" + animalToUpdate.ID);

        }

        static Animal animal1;

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Sex,ShopID,CageID,SpeciesID,Date,ColorID,Price")] Animal animal)
        {
            animal.ID = Guid.NewGuid().GetHashCode();
            if (ModelState.IsValid)
            {
                    return RedirectToAction(nameof(Choosecage),animal);

            }       
        ColorsDropDownList(animal.ColorID);
        ShopsDropDownList(animal.ShopID);
        SpeciesDropDownList(animal.SpeciesID);
        SexDropDownList(animal.Sex);
        return View(animal);
        }


        private void ShopsDropDownList(object selectedShop = null)
        {
            var shopsQuery = from s in _context.Shops
                                   orderby s.Name
                                   select s;
            ViewBag.ShopID = new SelectList(shopsQuery.AsNoTracking(), "ID", "Name", selectedShop);
        }

        private void SexDropDownList(Char? sex=null)
        {
            List<char> sexes = new List<char>()
            {'Ж',
            'Ч',
            '-'
            };
            ViewBag.Sex = new SelectList(sexes,sex);
        }
       
        private void CagesDropDownList(Int32 ShopID,object selectedCage=null)
        {
            var cagesQuery = from c in _context.Cages
                             where c.ShopID==ShopID
                             orderby c.Number
                             select c;
            ViewBag.CageID = new SelectList(cagesQuery.AsNoTracking(), "ID", "Number", selectedCage);
        }

        private void ColorsDropDownList(object selectedColor = null)
        {
            var colorsQuery = from c in _context.Colors
                             orderby c.Name
                             select c;
            ViewBag.ColorID = new SelectList(colorsQuery.AsNoTracking(), "ID", "Name", selectedColor);
        }

        private void SpeciesDropDownList(object selectedSpecies = null)
        {
            var speciesQuery = from s in _context.Species
                              orderby s.Name
                              select s;
            ViewBag.SpeciesID = new SelectList(speciesQuery.AsNoTracking(), "ID", "Name", selectedSpecies);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .AsNoTracking()
                .FirstOrDefaultAsync(a=>a.ID==id);
            if (animal == null)
            {
                return NotFound();
            }
            ColorsDropDownList(animal.ColorID);
            ShopsDropDownList(animal.ShopID);
            SpeciesDropDownList(animal.SpeciesID);
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Sex,ShopID,CageID,SpeciesID,Date,ColorID,Price")] Animal animal)
        {
            if (id != animal.ID)
            {
                return NotFound();
            }
            var animalToUpdate = await _context.Animals
                           .FirstOrDefaultAsync(a => a.ID == animal.ID);
            if (await TryUpdateModelAsync<Animal>(
        animalToUpdate,
        "",
        a => a.Name, a => a.Sex, a => a.Date, a => a.Price, a => a.ShopID, a => a.Color, a => a.SpeciesID, a => a.Shop, a => a.Species))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    animalToUpdate.CageID = null;
                    return RedirectToAction(nameof(Choosecage1),animalToUpdate);
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return RedirectToAction(nameof(Edit), animal.ID);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Cage)
                .Include(a => a.Color)
                .Include(a => a.Shop)
                .Include(a => a.Species)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.ID == id);
        }
    }
}
