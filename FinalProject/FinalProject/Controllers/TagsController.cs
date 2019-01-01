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
using FinalProject.ViewModels;

namespace FinalProject.Controllers
{
    public class TagsController : Controller
    {
        private readonly IRepository _repository;

        public TagsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(await _repository.Tags.ToListAsync());
        }


        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Name")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddTagAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }


        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _repository.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteTagAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _repository.Tags.Any(e => e.Id == id);
        }
    }
}
