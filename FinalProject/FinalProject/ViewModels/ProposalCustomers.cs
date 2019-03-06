using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class ProposalCustomers
    {
        public Proposal Proposal { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
