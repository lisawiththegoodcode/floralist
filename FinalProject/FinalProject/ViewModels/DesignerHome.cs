using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class DesignerHome
    {
        public Designer Designer { get; set; }
        public int DesignerId { get; set; }

        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<Proposal> ProposalsInProgress { get; set; }
        public IEnumerable<Customer> Customers { get; set; }

    }
}
