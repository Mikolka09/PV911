using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUserAjax.Data;
using WebUserAjax.Entities;

namespace WebUserAjax.Controllers.Admin.Slider
{
    public class SliderItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SliderItemsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: SliderItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SliderItems.Include(s => s.SliderGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SliderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderItem = await _context.SliderItems
                .Include(s => s.SliderGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderItem == null)
            {
                return NotFound();
            }

            return View(sliderItem);
        }

        // GET: SliderItems/Create
        public IActionResult Create()
        {
            ViewData["SliderGroupId"] = new SelectList(_context.SliderGroups, "Id", "Name");
            return View();
        }

        // POST: SliderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImgUrl,Title,Text,Url,SliderGroupId")] SliderItem sliderItem, IFormFile ImgFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sliderItem);

                #region Обработка изображения

                var wwwRootPath = _environment.WebRootPath; // URL - для сайта
                var fileName = Path.GetRandomFileName().Replace('.', '_')
                     + Path.GetExtension(ImgFile.FileName);
                var filePath = Path.Combine(wwwRootPath + "\\storage\\sliders\\", fileName); // Реальный путь 

                sliderItem.ImgUrl = "/storage/sliders/" + fileName; // ссылка на картинку

                using (var stream = System.IO.File.Create(filePath))
                {
                    await ImgFile.CopyToAsync(stream);
                }

                #endregion
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SliderGroupId"] = new SelectList(_context.SliderGroups, "Id", "Name", sliderItem.SliderGroupId);
            return View(sliderItem);
        }

        // GET: SliderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderItem = await _context.SliderItems.FindAsync(id);
            if (sliderItem == null)
            {
                return NotFound();
            }
            ViewData["SliderGroupId"] = new SelectList(_context.SliderGroups, "Id", "Name", sliderItem.SliderGroupId);
            return View(sliderItem);
        }

        // POST: SliderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImgUrl,Title,Text,Url,SliderGroupId")] SliderItem sliderItem, IFormFile ImgFile)
        {
            if (id != sliderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SliderItem slider = _context.SliderItems.Find(id);
                    if (ImgFile != null)
                    {

                        #region Обработка изображения

                        var wwwRootPath = _environment.WebRootPath; // URL - для сайта
                        var fileName = Path.GetRandomFileName().Replace('.', '_')
                             + Path.GetExtension(ImgFile.FileName);
                        var filePath = Path.Combine(wwwRootPath + "\\storage\\sliders\\", fileName); // Реальный путь 

                        sliderItem.ImgUrl = "/storage/sliders/" + fileName; // ссылка на картинку

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await ImgFile.CopyToAsync(stream);
                        }

                        #endregion

                    }
                    else
                    {

                        sliderItem.ImgUrl = slider.ImgUrl;
                    }
                    _context.Entry(slider).State = EntityState.Detached;
                    _context.Update(sliderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderItemExists(sliderItem.Id))
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
            ViewData["SliderGroupId"] = new SelectList(_context.SliderGroups, "Id", "Name", sliderItem.SliderGroupId);
            return View(sliderItem);
        }

        // GET: SliderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderItem = await _context.SliderItems
                .Include(s => s.SliderGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderItem == null)
            {
                return NotFound();
            }

            return View(sliderItem);
        }

        // POST: SliderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sliderItem = await _context.SliderItems.FindAsync(id);
            _context.SliderItems.Remove(sliderItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderItemExists(int id)
        {
            return _context.SliderItems.Any(e => e.Id == id);
        }
    }
}
