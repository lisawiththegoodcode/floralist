using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModels;
using FinalProject.Services;

namespace FinalProject.Controllers
{
    public class ProposalItemsController : Controller
    {
        private readonly FlowerAppContext _context;
        private readonly IRepository _repository;

        public ProposalItemsController(IRepository repository)
        {
            _repository = repository;
        }


        // GET: ProposalItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProposalItems.ToListAsync());
        }

        // GET: ProposalItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposalItem = await _context.ProposalItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proposalItem == null)
            {
                return NotFound();
            }

            return View(proposalItem);
        }

        // GET: ProposalItems/Create
        public IActionResult Create(int imageId, int proposalId)
        {
            // create an AddProposalItem view model to hold info for the view
            var vm = new AddProposalItem
            {
                ProposalId = proposalId,
                ImageId = imageId,
                // get the image object by id
                Image = _repository.Images.FirstOrDefault(i => i.Id == imageId),
                Caption = ""
            };
            // pass the view model to the view
            return View(vm);
        }

        // POST: ProposalItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Caption,ImageId,ProposalId")] AddProposalItem vm)
        {
            if (ModelState.IsValid)
            {
                // create a new proposalItem model and fill in the image id, caption, etc
                var newProposalItem = new ProposalItem
                {
                    ImageId = vm.ImageId,
                    ProposalId = vm.ProposalId,
                    Caption = vm.Caption
                };
                // add it to the proposal
                await _repository.AddProposalItemAsync(vm.ProposalId, newProposalItem);

                // redirect to the Proposal Edit page
                return RedirectToAction(nameof(Edit), "Proposals", new { id = vm.ProposalId });
            }
            return View(vm);
        }

        // GET: ProposalItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposalItem = await _context.ProposalItems.FindAsync(id);
            if (proposalItem == null)
            {
                return NotFound();
            }
            return View(proposalItem);
        }

        // POST: ProposalItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Caption")] ProposalItem proposalItem)
        {
            if (id != proposalItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proposalItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProposalItemExists(proposalItem.Id))
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
            return View(proposalItem);
        }

        // GET: ProposalItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposalItem = await _context.ProposalItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proposalItem == null)
            {
                return NotFound();
            }

            return View(proposalItem);
        }

        // POST: ProposalItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proposalItem = await _context.ProposalItems.FindAsync(id);
            _context.ProposalItems.Remove(proposalItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProposalItemExists(int id)
        {
            return _context.ProposalItems.Any(e => e.Id == id);
        }
    }
}
