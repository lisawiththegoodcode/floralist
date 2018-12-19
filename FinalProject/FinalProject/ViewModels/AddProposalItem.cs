using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class AddProposalItem
    {
        public int ProposalId { get; set; }

        public Proposal Proposal { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }

        public string Caption { get; set; }
    }
}
