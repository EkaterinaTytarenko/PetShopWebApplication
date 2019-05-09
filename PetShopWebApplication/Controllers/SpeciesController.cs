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
    public class SpeciesController : Controller
    {
        private readonly AnimalsContext _context;

        public SpeciesController(AnimalsContext context)
        {
            _context = context;
        }

        // GET: Species
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            try
            {
                int n = int.Parse(searchString);
                var species = _context.Species.Where(s => s.LifeTime >= n);
                return View(await species.ToListAsync());
            }
            catch (Exception)
            {
                return View(await _context.Species.ToListAsync());
            }
        }

        // GET: Species/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var species = await _context.Species
                .Include(s=>s.SpeciesFood)
                .ThenInclude(sf=>sf.Food)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (species == null)
            {
                return NotFound();
            }

            return View(species);
        }

        // GET: Species/Create
        public IActionResult Create()
        {
            var species = new Species();
            species.SpeciesFood = new List<SpeciesFood>();
            PopulateAssignedFoodData(species);
            return View();
        }

        // POST: Species/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,LifeTime,Temperature")] Species species,string[]selectedFood)
        {
            species.ID = Guid.NewGuid().GetHashCode();
            if (selectedFood != null)
            {
                species.SpeciesFood = new List<SpeciesFood>();
                foreach (var food in selectedFood)
                {
                    var foodToAdd = new SpeciesFood { SpeciesID = species.ID, FoodID = int.Parse(food), ID = Guid.NewGuid().GetHashCode() };
                    species.SpeciesFood.Add(foodToAdd);
                }
            }
                if (ModelState.IsValid)
            {
                _context.Add(species);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(species);
        }

        // GET: Species/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var species = await _context.Species
        .Include(s => s.SpeciesFood).ThenInclude(sf => sf.Food)
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.ID == id);
            if (species == null)
            {
                return NotFound();
            }
            PopulateAssignedFoodData(species);
            return View(species);
        }
        private void PopulateAssignedFoodData(Species species)
        {
            var allFood = _context.Food;
            var speciesFood = new HashSet<int>(species.SpeciesFood.Select(sf => sf.FoodID));
            var viewModel = new List<AssignedFoodData>();
            foreach (var food in allFood)
            {
                viewModel.Add(new AssignedFoodData
                {
                    FoodID = food.ID,
                    Name = food.Name,
                    Assigned = speciesFood.Contains(food.ID)
                });
            }
            ViewData["Food"] = viewModel;
        }

        // POST: Species/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedFood)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciesToUpdate = await _context.Species
                .Include(s => s.SpeciesFood).ThenInclude(sf => sf.Food)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);


            if (await TryUpdateModelAsync<Species>(
                speciesToUpdate,
                "",
                i => i.Name, i => i.LifeTime, i => i.Temperature, i => i.SpeciesFood))
            {
               
                UpdateSpeciesFood(selectedFood, speciesToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateSpeciesFood(selectedFood, speciesToUpdate);
            PopulateAssignedFoodData(speciesToUpdate);
            return View(speciesToUpdate);
        }

        private void UpdateSpeciesFood(string[] selectedFood, Species speciesToUpdate)
        {
            if (selectedFood == null)
            {
                speciesToUpdate.SpeciesFood = new List<SpeciesFood>();
                return;
            }

            var selectedFoodHS = new HashSet<string>(selectedFood);
            var speciesFood= new HashSet<int>
                (speciesToUpdate.SpeciesFood.Select(sf => sf.Food.ID));
            foreach (var food in _context.Food)
            {
                if (selectedFoodHS.Contains(food.ID.ToString()))
                {
                    if (!speciesFood.Contains(food.ID))
                    {
                        SpeciesFood newElement = new SpeciesFood { SpeciesID = speciesToUpdate.ID, FoodID = food.ID, ID = Guid.NewGuid().GetHashCode() };
                        speciesToUpdate.SpeciesFood.Add(newElement);
                        _context.SpeciesFood.Add(newElement);
                        
                    }
                }
                else
                {

                    if (speciesFood.Contains(food.ID))
                    {
                        SpeciesFood foodToRemove = speciesToUpdate.SpeciesFood.FirstOrDefault(i => i.FoodID == food.ID);
                        _context.Remove(foodToRemove);
                    }
                }
            }
            _context.SaveChanges();
        }
        // GET: Species/Delete/5
        public async Task<IActionResult> Delete(int? id, string errorMessage)
        {
            if (id == null)
            {
                return NotFound();
            }

            var species = await _context.Species
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (species == null)
            {
                return NotFound();
            }
            ViewData["ErrorMessage"] = errorMessage;
            return View(species);
        }

        // POST: Species/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var species = await _context.Species.FindAsync(id);
            int animalsCount = _context.Animals.Where(a => a.SpeciesID == id).Count();
            if (animalsCount == 0)
            {
                var speciesfood = _context.SpeciesFood.Where(sf => sf.SpeciesID == id).ToList();
                foreach (SpeciesFood sf in speciesfood)
                    _context.SpeciesFood.Remove(sf);
                _context.Species.Remove(species);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                return RedirectToAction(nameof(Delete), new { id = id, errorMessage = "Неможливо видалити вид, представники якого є у системі" });
        }

        private bool SpeciesExists(int id)
        {
            return _context.Species.Any(e => e.ID == id);
        }
    }
}
