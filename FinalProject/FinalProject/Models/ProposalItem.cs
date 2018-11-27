using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ProposalItem
    {
        public int Id { get; set; }
        public Image Image { get; set; }
        public string Caption { get; set; }
        public Proposal Proposal { get; set; }
    }
}
