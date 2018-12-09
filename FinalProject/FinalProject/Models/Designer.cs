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
        public List<Proposal> Proposals { get; set; }


        //might want a list of customers, but it's not necessaru
        //come back and add list of customers if I want to use this data
        //if I wanted to have a list of customers on the designers home page
        //every time you set up one of these relationships, entity framework has to do more work
        //have to think about the payoff... is it worth it to do this extra work?
    }
}
