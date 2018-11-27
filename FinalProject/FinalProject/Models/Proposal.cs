using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Customer Customer { get; set; }
        public Designer Designer { get; set; }
        public List<ProposalItem> ProposalItems { get; set; }
    }
}
