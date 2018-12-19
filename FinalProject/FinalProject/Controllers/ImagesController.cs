using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FinalProject.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IRepository _repository;

        public ImagesController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            return View(await _repository.Images.ToListAsync());
        }

        // GET: Images/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Images
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(image);
        //}

        // GET: Images/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()]Image image, IFormFile files)
        {
            if (files == null || files.Length == 0)
                return Content("file not selected");

            if (ModelState.IsValid)
            {
                using (var stream = new MemoryStream())
                {
                    await files.CopyToAsync(stream);
                    image.FileImage = stream.ToArray();
                }

                image.FileName = Path.GetFileName(files.FileName);

                await _repository.AddImageAsync(image);
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        // GET: Images/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Images.FindAsync(id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(image);
        //}

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,FileName")] Image image)
        //{
        //    if (id != image.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(image);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ImageExists(image.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(image);
        //}

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _repository.Images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteImageAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _repository.Images.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult Raw(int id)
        {
            var image = _repository.Images.FirstOrDefault(f => f.Id == id);
            var extension = new FileInfo(image.FileName).Extension.Replace(".", "");
            return File(image?.FileImage, $"image/{extension}");
        }
    }
}
