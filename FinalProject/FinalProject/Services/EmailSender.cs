using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void sendProposalViewEmail()
        {
            var email = _email
                //.From("floralisttheapp@gmail.com")
                .To("nashid77@yahoo.com")
                .Subject("hi from floralist")
                //.Body("hi again");
           .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/Mytemplate.cshtml", new { Name = "Rad Dude" });

            email.Send();
        }
    }
}
