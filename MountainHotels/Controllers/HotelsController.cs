using Microsoft.AspNetCore.Mvc;
using MountainHotels.DataAccess;
using MountainHotels.Models;

namespace MountainHotels.Controllers
{
    public class HotelsController : Controller
    {
        private readonly MountainHotelsContext _context;

        public HotelsController(MountainHotelsContext context)
        {
            _context = context;
        }

        // GET: /Hotels
        public IActionResult Index(string? location, string? year)
        {
            var hotels = _context.Hotels.AsEnumerable();
            if (location != null)
            {
                hotels = _context.Hotels.Where(e => e.Location == location);
                ViewData["SearchLocation"] = location;
            }
            ViewData["AllLocations"] = _context.Hotels.Select(e => e.Location).Distinct().ToList();
            
            return View(hotels);
        }

        [Route("/hotels/{id:int}")]
        public IActionResult Show(int id)
        {
            var hotel = _context.Hotels.Find(id);
            return View(hotel);
        }

        // GET: /Hotels/New
        public IActionResult New()
        {
            return View();
        }

        // POST: /Hotels
        [HttpPost]
        public IActionResult Index(Hotel hotel)
        {
            //Take the movie sent in the request and save it to the database
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            // Redirect back to the index page with all hotels
            return RedirectToAction("Index");
        }

        [Route("/hotels/{id:int}/edit")]
        public IActionResult Edit(int id)
        {
            var hotel = _context.Hotels.Find(id);
            return View(hotel);
        }

        [HttpPost]
        [Route("/hotels/{id:int}")]
        public IActionResult Update(int id, Hotel hotel)
        {
            hotel.Id = id;
            _context.Hotels.Update(hotel);
            _context.SaveChanges();

            return Redirect($"/hotels/{hotel.Id}");
        }
    }
}
