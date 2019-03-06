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
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace FinalProject.Controllers
{
    [Authorize]
    public class ProposalsController : Controller
    {
        private readonly IRepository _repository;
        private readonly EmailSender _emailSender;

        public ProposalsController(IRepository repository, EmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> ImageSearch(int proposalId, string searchString)
        {
            var vm = new ImageSearch
            {
                ProposalId = proposalId,
                SearchString = searchString,
                // search for images if there is search text
                Images = String.IsNullOrEmpty(searchString)
                    ? await _repository.GetImagesForDesignerAsync(User.GetUserId())
                    : await _repository.Images
                    .Where(i => i.DesignerId == _repository.GetDesignerIdForUserId(User.GetUserId()))
                    .Where(i => i.ImageTags.Any(x=> x.Tag.Name == searchString))
                    .ToListAsync()
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

        // POST: Proposals/Share/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Share(int id, Proposal proposal)
        {
            Trace.WriteLine("In share task of proposals controller");

            if (id != proposal.Id)
            {
                return NotFound();
            }

            proposal = await _repository.GetProposalAsync(id);
 
            if (ModelState.IsValid)
            {
                try
                {
                    Trace.WriteLine(proposal);
                    _emailSender.sendProposalEmail(proposal);
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
  
            return View(await _repository.GetProposalsForDesignerAsync(User.GetUserId()));
       
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
            var vm = new ProposalCustomers
            {
                Customers = _repository.GetCustomersForDesigner(User.GetUserId())
            };
            return View(vm);
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

            var vm = new ProposalCustomers
            {
                Proposal = await _repository.GetProposalAsync(id),
                Customers = _repository.GetCustomersForDesigner(User.GetUserId())
            };

            if (vm.Proposal == null)
            {
                return NotFound();
            }
            
            return View(vm);
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

        private bool UserUnauthorizedToView(string id)
        {
            return id != User.GetUserId();
        }

    }
}
