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
using Shared.Web.MvcExtensions;

namespace FinalProject.Controllers
{
    public class ProposalsController : Controller
    {
        private readonly IRepository _repository;

        public ProposalsController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> ImageSearch(int proposalId, string searchString)
        {
            //queryable query the image table
            //deferred execution when using linq. building up the query, calling to list is telling linq im done and to build the results
            var vm = new ImageSearch
            {
                ProposalId = proposalId,
                SearchString = searchString,
                // search for images if there is search text
                Images = String.IsNullOrEmpty(searchString)
                    ? await _repository.Images.ToListAsync()
                    : await _repository.Images.Where(i => i.FileName.Contains(searchString)).ToListAsync()
            };
            return View(vm);
        }

        //GET: Proposals/Share/5
        public async Task<IActionResult> Share(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _repository.GetProposalAsync(id);

            if (proposal == null)
            {
                return NotFound();
            }
            return View(proposal);
        }

        // POST: Proposals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Share(int id, Proposal proposal)
        {
            if (id != proposal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.ShareProposalAsync(id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProposalExists(proposal.Id))
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
            return View(proposal);
        }

        // GET: Proposals
        public async Task<IActionResult> Index()
        {
            //gonna need something like this to control who sees what data
            //var userId = userManager.getUserId();

            return View(await _repository.Proposals
                .Include(x => x.Customer)
                .Include(x => x.Designer)
                .Where(x => x.Designer.UserId == User.GetUserId())
                .ToListAsync());
        }

        // GET: Proposals/Details/5
        public async Task<IActionResult> Preview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _repository.GetProposalAsync(id);

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
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CustomerId,ProposalItems")] Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                //TODO: THIS IS A WORKAROUND. NEED TO GET THIS TO DEFAULT TO THE CURRENTLY LOGGED IN DESIGNER
                proposal.DesignerId = _repository.GetDesignerIdForUserId(User.GetUserId());
                await _repository.AddProposalAsync(proposal);

                return RedirectToAction(nameof(ImageSearch), new { proposalId = proposal.Id, searchString = "" });
            }
            return View(proposal);
        }

        //GET: Proposals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _repository.GetProposalAsync(id);

            if (proposal == null)
            {
                return NotFound();
            }
            return View(proposal);
        }

        // POST: Proposals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proposal proposal)
        {
            if (id != proposal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateProposalAsync(id, proposal);
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!ProposalExists(proposal.Id))
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
            return View(proposal);
        }

        // GET: Proposals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _repository.GetProposalAsync(id);

            if (proposal == null)
            {
                return NotFound();
            }

            return View(proposal);
        }

        //POST: Proposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteProposalAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProposalExists(int id)
        {
            return _repository.Proposals.Any(e => e.Id == id);
        }

    }
}
