using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabWork1.Models;
using LabWork2.Services;


namespace LabWork1.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BookingContext _context;
        private readonly IFileService _fileService;

        public BookingsController(BookingContext context, IFileService fileService )
        {
            _context = context;
            _fileService = fileService;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
              return _context.Bookings != null ? 
                          View(await _context.Bookings.ToListAsync()) :
                          Problem("Entity set 'BookingContext.Bookings'  is null.");
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Photo,PhoneNumber,PickUpLocation,DropLocation")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        [HttpPost]
        public ActionResult AddBooking(Booking booking)
        {
            try
            {
                var saveImageResult = _fileService.SaveImage(booking.PhotoFile);
                if (saveImageResult.Item1 == 1)
                {
                    var oldImage = booking.Photo;
                    booking.Photo = saveImageResult.Item2;
                    var deleteResult = _fileService.DeleteImage(oldImage);
                }
                _context.Bookings.Add(booking);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit( Booking booking)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var saveImageResult = _fileService.SaveImage(booking.PhotoFile);
                    if (saveImageResult.Item1 == 1)
                    {
                        var oldImage = booking.Photo;
                        booking.Photo = saveImageResult.Item2;
                        var deleteResult = _fileService.DeleteImage(oldImage);
                    }
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'BookingContext.Bookings'  is null.");
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
          return (_context.Bookings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
