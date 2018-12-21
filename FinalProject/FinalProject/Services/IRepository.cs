using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public interface IRepository : IDisposable
    {
        IQueryable<Image> Images { get; }
        IQueryable<Proposal> Proposals{ get; }
        IQueryable<ProposalItem> ProposalItems { get; }
        IQueryable<Customer> Customers { get; }
        IQueryable<Designer> Designers { get; }
        IQueryable<Tag> Tags { get; }


        Task AddDesignerAsync(Designer designer);
        int GetDesignerIdForUserId(string userId);

        Task AddProposalItemAsync(int proposalId, ProposalItem proposalItem);
        Task<ProposalItem> GetProposalItemAsync(int? id);
        Task DeleteProposalItemAsync(int id);

        Task AddProposalAsync(Proposal proposal);
        Task<Proposal> GetProposalAsync(int? id);
        Task UpdateProposalAsync(int id, Proposal proposal);
        Task ShareProposalAsync(int id);
        Task DeleteProposalAsync(int id);
        List<ProposalItem> GetProposalItemsForProposal(int id);
        Task<List<Proposal>> GetProposalsForDesignerAsync(string userId);

        Task AddImageAsync(Image image);
        Task DeleteImageAsync(int id);
        Task<List<Image>> GetImagesForDesignerAsync(string userId);

        Task CreateImageTagsAsync(int imageId, int tagId);
        Task AddTagAsync(Tag tag);
        Task DeleteTagAsync(int id);
    }
}
