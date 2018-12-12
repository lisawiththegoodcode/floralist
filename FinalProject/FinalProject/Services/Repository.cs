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

        #region Proposals Methods
        public Task AddProposalAsync(Proposal proposal)
        {
            _flowerAppContext.Proposals.Add(proposal);
            return _flowerAppContext.SaveChangesAsync();
        }

        public Task<Proposal> GetProposalAsync(int? id)
        {
            return _flowerAppContext.Proposals
                .Include(x => x.Customer)
                .Include(x => x.Designer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //public Proposal GetProposal(int? id)
        //{
        //    return _flowerAppContext.Proposals
        //        .Include(x => x.Customer)
        //        .Include(x => x.Designer)
        //        .FirstOrDefault(m => m.Id == id);
        //}

        public Task UpdateProposalAsync(int id, Proposal proposal)
        {
            //proposal.Id = id;
            _flowerAppContext.Proposals.Update(proposal);
            return _flowerAppContext.SaveChangesAsync();
        }
        public Task DeleteProposalAsync(int id)
        {
            var proposal = _flowerAppContext.Proposals.FirstOrDefault(m => m.Id == id);
            _flowerAppContext.Proposals.Remove(proposal);
            return _flowerAppContext.SaveChangesAsync();
        }

        public List<ProposalItem> GetProposalItemsForProposal(int id)
        {
            return _flowerAppContext.Proposals.FirstOrDefault(m => m.Id == id).ProposalItems;
                //ProposalItems
                //.Where(p => p.ProposalId == id)
                //.ToList();
            //maybe add an .orderby or .include?

        }
        #endregion



        public void Dispose()
        {
            _flowerAppContext?.Dispose();
        }
    }
}
