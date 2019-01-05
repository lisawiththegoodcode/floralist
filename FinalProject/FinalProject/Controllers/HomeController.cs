using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using FinalProject.ViewModels;
using FinalProject.Services;
using Shared.Web.MvcExtensions;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        public IActionResult Index()
        {
            var vm = new DesignerHome
            {
                Images = _repository.GetImagesForDesigner(User.GetUserId()),
                ProposalsInProgress = _repository.GetProposalsInProgressForDesigner(User.GetUserId()),
                //need to add a designer id to customer and run a migration... or maybe customers are just global based on the fact that a customer has been created
                Customers = _repository.Customers.ToList(),
                Designer = _repository.Designers.FirstOrDefault(m => m.Id == _repository.GetDesignerIdForUserId(User.GetUserId()))

            };
            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
