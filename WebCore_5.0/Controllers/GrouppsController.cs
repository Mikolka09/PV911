using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebCore_5._0.Data;
using WebCore_5._0.Entities.School;

namespace WebCore_5._0.Controllers
{
    public class GrouppsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GrouppsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Groupps
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Groups.Include(g => g.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Groupps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupp = await _context.Groups
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupp == null)
            {
                return NotFound();
            }

            return View(groupp);
        }

        // GET: Groupps/Create
        public IActionResult Create()
        {
            //в этой точке необходимо передать только тех учителей, у которых нет групп
            ViewData["TeacherId"] = new SelectList(_context.Teachers.Where(t => t.Groupp == null), "Id", "Name");
            return View();
        }

        // POST: Groupps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TeacherId")] Groupp groupp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", groupp.TeacherId);
            return View(groupp);
        }

        // GET: Groupps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupp = await _context.Groups.FindAsync(id);
            if (groupp == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", groupp.TeacherId);
            return View(groupp);
        }

        // POST: Groupps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TeacherId")] Groupp groupp)
        {
            if (id != groupp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrouppExists(groupp.Id))
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
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", groupp.TeacherId);
            return View(groupp);
        }

        // GET: Groupps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupp = await _context.Groups
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupp == null)
            {
                return NotFound();
            }

            return View(groupp);
        }

        // POST: Groupps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupp = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(groupp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrouppExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
