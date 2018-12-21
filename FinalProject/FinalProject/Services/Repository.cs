using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
        public IQueryable<Tag> Tags => _flowerAppContext.Tags;

        #region Designer Methods
        public Task AddDesignerAsync(Designer designer)
        {
            _flowerAppContext.Designers.Add(designer);
            return _flowerAppContext.SaveChangesAsync();
        }
        public int GetDesignerIdForUserId(string userId)
        {
            var designer = _flowerAppContext.Designers.FirstOrDefault(m => m.UserId == userId);
            return designer.Id;
        }
        #endregion

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

        public Task UpdateProposalAsync(int id, Proposal proposal)
        {
            _flowerAppContext.Proposals.Update(proposal);
            return _flowerAppContext.SaveChangesAsync();
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
            return _flowerAppContext.ProposalItems
            .Where(p => p.ProposalId == id)
            .ToList();
        }

        public Task<List<Proposal>> GetProposalsForDesignerAsync(string userId)
        {
            return _flowerAppContext.Proposals
                .Include(x => x.Customer)
                .Include(x => x.Designer)
                .Where(x => x.Designer.UserId == userId)
                .ToListAsync();
        }
        #endregion

        #region Image Methods
        public Task AddImageAsync(Image image)
        {
            //image.DesignerId = 1;
            _flowerAppContext.Images.Add(image);
            return _flowerAppContext.SaveChangesAsync();
        }

        public Task DeleteImageAsync(int id)
        {
            var image = _flowerAppContext.Images.FirstOrDefault(m => m.Id == id);
            _flowerAppContext.Images.Remove(image);
            return _flowerAppContext.SaveChangesAsync();
        }

        public Task<List<Image>> GetImagesForDesignerAsync(string userId)
        {
            return _flowerAppContext.Images
                .Include(x => x.ImageTags)
                    .ThenInclude(x => x.Tag)
                .Include(x => x.Designer)
                .Where(x => x.Designer.UserId == userId)
                .ToListAsync();
        }
        #endregion

        #region Tag Methods
        public Task CreateImageTagsAsync(int imageId, int tagId)
        {
            //List<ImageTag> imageTags = new List<ImageTag>();
            //foreach(var item in tags)
            //{
            //    ImageTag imageTag = new ImageTag{ ImageId = imageId, TagId = item.Id };
            //    imageTags.Add(imageTag);
            //}

            //var image = _flowerAppContext.Images.FirstOrDefault(i => i.Id == imageId);
            //image.ImageTags = imageTags;

            var image = _flowerAppContext.Images
                .Include(x=>x.ImageTags)
                .FirstOrDefault(i => i.Id == imageId);

            ImageTag imageTag = new ImageTag
            {
                ImageId = imageId,
                TagId = tagId,
            };

            image.ImageTags.Add(imageTag);

            _flowerAppContext.Images.Update(image);
            return _flowerAppContext.SaveChangesAsync();

        }

        public Task AddTagAsync(Tag tag)
        {
            _flowerAppContext.Add(tag);
            return _flowerAppContext.SaveChangesAsync();
        }

        public Task DeleteTagAsync(int id)
        {
            var tag = _flowerAppContext.Tags.FirstOrDefault(m => m.Id == id);
            _flowerAppContext.Tags.Remove(tag);
            return _flowerAppContext.SaveChangesAsync();
        }
        #endregion


        public void Dispose()
        {
            _flowerAppContext?.Dispose();
        }

    }
}
