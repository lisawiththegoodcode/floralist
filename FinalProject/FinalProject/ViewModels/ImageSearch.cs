using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class ImageSearch
    {
        public int ProposalId { get; set; }
        public string SearchString { get; set; }

        public IEnumerable<Image> Images { get; set; }

    }
}
