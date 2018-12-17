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
        private readonly IRepository _repository;

        public ProposalItemsController(IRepository repository)
        {
            _repository = repository;
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
                return RedirectToAction("Edit", "Proposals", new { id = vm.ProposalId });
            }
            return View(vm);
        }


        // GET: ProposalItems/Delete/5
        public async Task<IActionResult> Delete(int? id, int proposalId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposalItem = await _repository.GetProposalItemAsync(id);

            if (proposalItem == null)
            {
                return NotFound();
            }

            return View(proposalItem);
        }

        // POST: ProposalItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, ProposalItem proposalItem)
        {
            await _repository.DeleteProposalItemAsync(id);
            return RedirectToAction("Edit", "Proposals", new { id = proposalItem.ProposalId });
        }

        private bool ProposalItemExists(int id)
        {
            return _repository.ProposalItems.Any(e => e.Id == id);
        }
    }
}
