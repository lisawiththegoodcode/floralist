using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class FlowerAppContext : DbContext
    {
        public FlowerAppContext(DbContextOptions<FlowerAppContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //can call below to have base DbContext class implement it's modelcreating method
            base.OnModelCreating(modelBuilder);
            //this applys the default/convention mapping ie helps with foriegn keys, creating entity columns correctly
            

            //need help thinking this through
            modelBuilder.Entity<Proposal>().HasKey(x => x.Id)
                .ForSqlServerIsClustered();
            modelBuilder.Entity<Proposal>().Property(x => x.Id)
                .UseSqlServerIdentityColumn();
            //want to do this same pair for every model ^
            //need to figure out the relationships between all these things
            //a proposal is going to have proposal items
            //todo context lesson 4 --> an example of todo to statuses
            //we are going to have a lot of those statements due to our large models
            //A PROPOSAL HAS MANY PROPOSAL ITEMS
            //A PROPOSAL ITEM HAS ONE PROPOSAL
            //A DESIGNER HAS MANY PROPOSALS
            //A PROPOSAL HAS ONE DESIGNER
            //A DESIGNER HAS MANY CUSTOMERs
            //going to have a lot in here
            //this is where we tell the database what the foriegn key relationships are
        }
        //do we need a dbset for each model?
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<ProposalItem> ProposalItems { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
