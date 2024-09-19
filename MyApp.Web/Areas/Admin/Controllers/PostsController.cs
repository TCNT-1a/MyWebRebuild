using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Infra.Data;
using NuGet.Protocol;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Route("admin/[controller]")]
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly BloggingContext _context;

        public PostsController(BloggingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .Include(p => p.HeadingTag)
            .ToListAsync();
            return _context.Posts != null ?
                        View(posts) :
                        Problem("Entity set 'BloggingContext.Posts'  is null.");
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var result = await getCategoryTag();
            ViewBag.categories = result.CategorySelectList;
            ViewBag.tags = result.TagSelectList;
            return View();
        }
        private async Task<ResultData> getCategoryTag()
        {
            var categories = await _context.Categories.Where(p => p.IsDeleted == false).ToListAsync();
            var tags = await _context.Tags.Where(p => p.IsDeleted == false).ToListAsync();


            var categorySelectList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            categorySelectList.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "-- Select Category --"
            });

            var tagSelectList = tags.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            var result = new ResultData
            {
                CategorySelectList = categorySelectList,
                TagSelectList = tagSelectList
            };
            return result;
        }
        class ResultData
        {
            public List<SelectListItem> CategorySelectList { get; set; }
            public List<SelectListItem> TagSelectList { get; set; }
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,Id")] Post post, string[] tags)
        {
            var result = await getCategoryTag();
            ViewBag.categories = result.CategorySelectList;
            ViewBag.tags = result.TagSelectList;

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,PostView,Content,Id,CreatedDate,UpdatedDate,IsDeleted")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost("delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'BloggingContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
