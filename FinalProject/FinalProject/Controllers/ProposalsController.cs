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

namespace FinalProject.Controllers
{
    public class ProposalsController : Controller
    {
        private readonly IRepository _repository;

        public ProposalsController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> ImageSearch(string searchString)
        {
            var searchResults = _repository.Images;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchResults = searchResults.Where(x => x.FileName == searchString);
            }
            return View(await searchResults.ToListAsync());
        }

        // GET: Proposals
        public async Task<IActionResult> Index()
        {
            return View(await _repository.Proposals.ToListAsync());
        }

        // GET: Proposals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _repository.Proposals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proposal == null)
            {
                return NotFound();
            }

            return View(proposal);
        }

        // GET: Proposals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proposals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddProposalAsync(proposal);
                return RedirectToAction(nameof(Index));
            }
            return View(proposal);
        }

        //// GET: Proposals/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var proposal = await _repository.Proposals.FindAsync(id);
        //    if (proposal == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(proposal);
        //}

        //    // POST: Proposals/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Proposal proposal)
        //    {
        //        if (id != proposal.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _repository.Update(proposal);
        //                await _repository.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ProposalExists(proposal.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(proposal);
        //    }

        //    // GET: Proposals/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var proposal = await _repository.Proposals
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (proposal == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(proposal);
        //    }

        //    // POST: Proposals/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var proposal = await _repository.Proposals.FindAsync(id);
        //        _repository.Proposals.Remove(proposal);
        //        await _repository.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool ProposalExists(int id)
        //    {
        //        return _repository.Proposals.Any(e => e.Id == id);
        //    }
        //}
    }
}
