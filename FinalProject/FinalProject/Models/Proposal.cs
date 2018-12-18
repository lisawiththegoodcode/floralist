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
        //table has this id
        public int CustomerId { get; set; }
        //proposal entity has this object
        public Customer Customer { get; set; }

        //for each has one relationship will have this pattern
        public int DesignerId { get; set; }
        public Designer Designer { get; set; }

        //for each has many, requires a list
        public List<ProposalItem> ProposalItems { get; set; }

        public bool IsShared { get; set; }
    }
}
