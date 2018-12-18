using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class Repository : IRepository
    {
        private readonly FlowerAppContext _flowerAppContext;

        public Repository(FlowerAppContext flowerAppContext)
        {
            _flowerAppContext = flowerAppContext;
        }

        public IQueryable<Image> Images => _flowerAppContext.Images;
        public IQueryable<Proposal> Proposals => _flowerAppContext.Proposals;
        public IQueryable<ProposalItem> ProposalItems => _flowerAppContext.ProposalItems;
        public IQueryable<Customer> Customers => _flowerAppContext.Customers;
        public IQueryable<Designer> Designers => _flowerAppContext.Designers;
        
        #region ProposalItem Methods
        public Task AddProposalItemAsync(int proposalId, ProposalItem proposalItem)
        {
            _flowerAppContext.ProposalItems.Add(proposalItem);
            return _flowerAppContext.SaveChangesAsync();
        }

        public Task<ProposalItem> GetProposalItemAsync(int? id)
        {
            return _flowerAppContext.ProposalItems
                .Include(x => x.Image)
                .Include(x => x.Proposal)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task DeleteProposalItemAsync(int id)
        {
            var proposalItem = _flowerAppContext.ProposalItems
                .Include(x => x.Image)
                .Include(x => x.Proposal)
                .FirstOrDefault(m => m.Id == id);

            _flowerAppContext.ProposalItems.Remove(proposalItem);
            return _flowerAppContext.SaveChangesAsync();
        }
        #endregion

        #region Proposals Methods
        public Task AddProposalAsync(Proposal proposal)
        {
            proposal.IsShared = false;
            _flowerAppContext.Proposals.Add(proposal);
            return _flowerAppContext.SaveChangesAsync();
        }

        public Task<Proposal> GetProposalAsync(int? id)
        {
            return _flowerAppContext.Proposals
                .Include(x => x.Customer)
                .Include(x => x.Designer)
                .Include(x => x.ProposalItems)
                    .ThenInclude(x => x.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateProposalAsync(int id)
        {
            var proposal = await GetProposalAsync(id);
            _flowerAppContext.Proposals.Update(proposal);
            await _flowerAppContext.SaveChangesAsync();
        }

        public async Task ShareProposalAsync(int id)
        {
            var proposal = await GetProposalAsync(id);
            proposal.IsShared = proposal.IsShared ? false : true;
            _flowerAppContext.Proposals.Update(proposal);
            await _flowerAppContext.SaveChangesAsync();
        }

        public Task DeleteProposalAsync(int id)
        {
            var proposal = _flowerAppContext.Proposals
                .Include(x => x.Customer)
                .Include(x => x.Designer)
                .Include(x => x.ProposalItems)
                .FirstOrDefault(m => m.Id == id);
            _flowerAppContext.Proposals.Remove(proposal);
            return _flowerAppContext.SaveChangesAsync();
        }

        public List<ProposalItem> GetProposalItemsForProposal(int id)
        {
            //return _flowerAppContext.Proposals.FirstOrDefault(m => m.Id == id).ProposalItems;
            return _flowerAppContext.ProposalItems
            .Where(p => p.ProposalId == id)
            .ToList();
            //maybe add an .orderby or .include?

        }
        #endregion



        public void Dispose()
        {
            _flowerAppContext?.Dispose();
        }
    }
}
