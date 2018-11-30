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
            //need help thinking this through
            modelBuilder.Entity<Proposal>().HasKey(x => x.Id).ForSqlServerIsClustered();
            modelBuilder.Entity<Proposal>().Property(x => x.Id).UseSqlServerIdentityColumn();
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
