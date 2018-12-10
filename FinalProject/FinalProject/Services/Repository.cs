using FinalProject.Data;
using FinalProject.Models;
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

        #region Proposals Methods
        public async Task AddProposalAsync(Proposal proposal)
        {
            await _flowerAppContext.Proposals.AddAsync(proposal);
            await _flowerAppContext.SaveChangesAsync();
        }
        #endregion

        public void Dispose()
        {
            _flowerAppContext?.Dispose();
        }
    }
}
