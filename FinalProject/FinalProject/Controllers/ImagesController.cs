using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using FinalProject.Services;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Primitives;

namespace FinalProject.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IRepository _repository;

        // private readonly IHostingEnvironment _hostingEnvironment;

        public ImagesController(IRepository repository)
        {
            _repository = repository;


        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            return View(await _repository.Images.ToListAsync());
        }

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
        public async Task<IActionResult> Create([Bind()] Image image, IFormFile files)
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

        
       // GET: Images/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _repository.Images
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(image);
        //}

        //// GET: Images/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _repository.Images.FindAsync(id);
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
        //            _repository.Update(image);
        //            await _repository.SaveChangesAsync();
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
        public async Task<IActionResult> Raw(int id)
        {
            var image = _repository.Images.FirstOrDefault(f => f.Id == id);
            var extension = new FileInfo(image.FileName).Extension.Replace(".", "");
            return File(image?.FileImage, $"image/{extension}");
        }

        
    }

}
