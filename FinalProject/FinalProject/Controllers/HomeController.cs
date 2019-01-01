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

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var vm = new DesignerHome
            {
                Images = _repository.Images.ToList(),
                Proposals = _repository.Proposals.ToList(),
                //in the repo, make a get proposals for userid returns a list of proposals for current user
                Customers = _repository.Customers.ToList(),
                //maybe customers are just global based on the fact that a customer has been created
                //TODO: THIS IS HARDCODED RIGHT NOW, NEED TO FIGURE OUT LOGIC TO POPULATE NAME BASED ON WHO IS LOGGED IN
                Designer = _repository.Designers.FirstOrDefault(m => m.Id == _repository.GetDesignerIdForUserId(User.GetUserId()))

            };
            return View(vm);
            //return View();
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
