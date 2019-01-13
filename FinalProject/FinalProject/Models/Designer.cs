using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Designer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public string Company { get; set; }
        public List<Proposal> Proposals { get; set; }
        public string UserId { get; set; }

    }
}
