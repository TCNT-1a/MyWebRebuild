﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Infra.Data;

namespace MyApp.Web.Controllers
{
    public class HeadingTagsController : Controller
    {
        private readonly BloggingContext _context;

        public HeadingTagsController(BloggingContext context)
        {
            _context = context;
        }

        // GET: HeadingTags
        public async Task<IActionResult> Index()
        {
              return _context.HeadingTags != null ? 
                          View(await _context.HeadingTags.Where(p=>p.IsDeleted==false).ToListAsync()) :
                          Problem("Entity set 'BloggingContext.HeadingTags'  is null.");
        }

        // GET: HeadingTags/Details/5
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

        // GET: HeadingTags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HeadingTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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

        // GET: HeadingTags/Edit/5
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

        // POST: HeadingTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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

        // GET: HeadingTags/Delete/5
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

        // POST: HeadingTags/Delete/5
        [HttpPost, ActionName("Delete")]
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
