using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class FlowerAppContext : IdentityDbContext<IdentityUser>
    {
        public FlowerAppContext(DbContextOptions<FlowerAppContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //can call below to have base DbContext class implement it's modelcreating method
            base.OnModelCreating(modelBuilder);
            //this applys the default/convention mapping ie helps with foriegn keys, creating entity columns correctly


            //PROPOSAL KEY WITH IDENTITY SETUP
            modelBuilder.Entity<Proposal>().HasKey(x => x.Id)
                .ForSqlServerIsClustered();
            modelBuilder.Entity<Proposal>().Property(x => x.Id)
                .UseSqlServerIdentityColumn();
            //PROPOSAL CUSTOMER RELATIONSHIP
            modelBuilder.Entity<Proposal>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Proposals)
                .HasForeignKey(x => x.CustomerId);
            modelBuilder.Entity<Proposal>()
                .HasIndex(x => x.CustomerId)
                .HasName($"IX_{nameof(Proposal)}_{nameof(Proposal.Customer)}");
            //PROPOSAL DESIGNER RELATIONSHIP
            modelBuilder.Entity<Proposal>()
                .HasOne(x => x.Designer)
                .WithMany(x => x.Proposals)
                .HasForeignKey(x => x.DesignerId);
            modelBuilder.Entity<Proposal>()
                .HasIndex(x => x.DesignerId)
                .HasName($"IX_{nameof(Proposal)}_{nameof(Proposal.Designer)}");

            //CUSTOMER KEY WITH IDENTITY SETUP
            modelBuilder.Entity<Customer>().HasKey(x => x.Id)
                .ForSqlServerIsClustered();
            modelBuilder.Entity<Customer>().Property(x => x.Id)
                .UseSqlServerIdentityColumn();
            //modelBuilder.Entity<Customer>()
            //    .HasMany(x => x.Proposals)
            //    .WithOne(x => x.Designer);


            //DESIGNER KEY WITH PROPOSAL SETUP
            modelBuilder.Entity<Designer>().HasKey(x => x.Id)
                .ForSqlServerIsClustered();
            modelBuilder.Entity<Designer>().Property(x => x.Id)
                .UseSqlServerIdentityColumn();
            modelBuilder.Entity<Designer>()
                .HasMany(x => x.Proposals)
                .WithOne(x => x.Designer);

            //PROPOSALITEM KEY WITH IDENTITY SETUP
            modelBuilder.Entity<ProposalItem>().HasKey(x => x.Id)
                .ForSqlServerIsClustered();
            modelBuilder.Entity<ProposalItem>().Property(x => x.Id)
                .UseSqlServerIdentityColumn();
            //PROPOSALITEM PROPOSAL RELATIONSHIP
            modelBuilder.Entity<ProposalItem>()
                .HasOne(x => x.Proposal)
                .WithMany(x => x.ProposalItems)
                .HasForeignKey(x => x.ProposalId);
            modelBuilder.Entity<ProposalItem>()
                .HasIndex(x => x.ProposalId)
                .HasName($"IX_{nameof(ProposalItem)}_{nameof(ProposalItem.Proposal)}");
            //PROPOSALITEM IMAGE RELATIONSHIP
            modelBuilder.Entity<ProposalItem>()
                .HasOne(x => x.Image)
                .WithMany(x => x.ProposalItems)
                .HasForeignKey(x => x.ImageId);
            modelBuilder.Entity<ProposalItem>()
                .HasIndex(x => x.ImageId)
                .HasName($"IX_{nameof(ProposalItem)}_{nameof(ProposalItem.Image)}");

            //IMAGE KEY WITH IDENTITY SETUP
            modelBuilder.Entity<Image>().HasKey(x => x.Id)
                .ForSqlServerIsClustered();
            modelBuilder.Entity<Image>().Property(x => x.Id)
                .UseSqlServerIdentityColumn();

            //TAG KEY WITH IDENTITY SETUP
            modelBuilder.Entity<Tag>().HasKey(x => x.Id)
                .ForSqlServerIsClustered();
            modelBuilder.Entity<Tag>().Property(x => x.Id)
                .UseSqlServerIdentityColumn();

            //IMAGETAG KEY SETUP
            modelBuilder.Entity<ImageTag>().HasKey(x => new { x.ImageId, x.TagId });

            //IMAGETAG TAG RELATIONSHIP
            modelBuilder.Entity<ImageTag>()
                .HasOne<Tag>(x => x.Tag)
                .WithMany(x => x.ImageTags)
                .HasForeignKey(x => x.TagId);

            //IMAGETAG IMAGE RELATIONSHIP
            modelBuilder.Entity<ImageTag>()
                .HasOne<Image>(x => x.Image)
                .WithMany(x => x.ImageTags)
                .HasForeignKey(x => x.ImageId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<ProposalItem> ProposalItems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }



    }
}
