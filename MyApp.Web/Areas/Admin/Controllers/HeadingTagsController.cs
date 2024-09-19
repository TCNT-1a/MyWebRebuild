using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Infra.Data;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Route("admin/[controller]")]
    [Area("Admin")]
    public class HeadingTagsController : Controller
    {
        private readonly BloggingContext _context;

        public HeadingTagsController(BloggingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.HeadingTags != null ?
                        View(await _context.HeadingTags.Where(p => p.IsDeleted == false).ToListAsync()) :
                        Problem("Entity set 'BloggingContext.HeadingTags'  is null.");
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HeadingTags == null)
            {
                return NotFound();
            }

            var headingTag = await _context.HeadingTags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (headingTag == null)
            {
                return NotFound();
            }

            return View(headingTag);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,NoIndex,Canonical,MetaDescription")] HeadingTag headingTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(headingTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(headingTag);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HeadingTags == null)
            {
                return NotFound();
            }

            var headingTag = await _context.HeadingTags.FindAsync(id);
            if (headingTag == null)
            {
                return NotFound();
            }
            return View(headingTag);
        }


        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,NoIndex,Canonical,MetaDescription")] HeadingTag headingTag)
        {
            if (id != headingTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(headingTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeadingTagExists(headingTag.Id))
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
            return View(headingTag);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HeadingTags == null)
            {
                return NotFound();
            }

            var headingTag = await _context.HeadingTags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (headingTag == null)
            {
                return NotFound();
            }

            return View(headingTag);
        }

        [HttpPost("delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HeadingTags == null)
            {
                return Problem("Entity set 'BloggingContext.HeadingTags'  is null.");
            }
            var headingTag = await _context.HeadingTags.FindAsync(id);
            if (headingTag != null)
            {
                headingTag.IsDeleted = true;
                _context.HeadingTags.Update(headingTag);
                //_context.HeadingTags.Remove(headingTag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeadingTagExists(int id)
        {
            return (_context.HeadingTags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
