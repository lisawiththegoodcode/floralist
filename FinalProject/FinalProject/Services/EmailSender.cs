using FinalProject.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class EmailSender
    {
        private IFluentEmail _email;

        public EmailSender([FromServices]IFluentEmail email)
        {
            _email = email;
        }

        public void sendProposalEmail(Proposal proposal)
        {
            var email = _email
                .SetFrom(proposal.Designer.Email)
                .To(proposal.Customer.Email)
                .Subject($"Floral Design Proposal for {proposal.Title} 💐")
                .UsingTemplateFromEmbedded("FinalProject.Views.Proposals.ProposalEmail.cshtml",
            proposal,
            Assembly.Load("FinalProject"));

           email.Send();
        }
    }
}
