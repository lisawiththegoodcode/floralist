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
        public IQueryable<Customer> Customers => _flowerAppContext.Customers;
        public IQueryable<Designer> Designers => _flowerAppContext.Designers;

        #region Proposals Methods
        public async Task AddProposalAsync(Proposal proposal)
        {
            await _flowerAppContext.Proposals.AddAsync(proposal);
            await _flowerAppContext.SaveChangesAsync();
        }
        public Proposal GetProposal(int? id)
        {
            return _flowerAppContext.Proposals
                .Include(x => x.Customer)
                .Include(x => x.Designer)
                .FirstOrDefault(m => m.Id == id);
        }

        public async Task UpdateProposalAsync(int id, Proposal proposal)
        {
            proposal.Id = id;
            _flowerAppContext.Proposals.Update(proposal);
            await _flowerAppContext.SaveChangesAsync();
        }
        public async Task DeleteProposalAsync(int id)
        {
            var proposal = _flowerAppContext.Proposals.FirstOrDefault(m => m.Id == id);
            _flowerAppContext.Proposals.Remove(proposal);
            await _flowerAppContext.SaveChangesAsync();
        }
        #endregion

        public void Dispose()
        {
            _flowerAppContext?.Dispose();
        }
    }
}
