using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using league.Models;
using league.Factory;

namespace league.Controllers
{
    public class HomeController : Controller
    {
        private readonly DojoFactory dojoFactory;
        private readonly NinjaFactory ninjaFactory;
        public HomeController()
        {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            ninjaFactory = new NinjaFactory();
            dojoFactory = new DojoFactory();
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return RedirectToAction("ninjas");
        }
        // GET: /Home/
        [HttpGet]
        [Route("ninjas")]
        public IActionResult Ninjas()
        {
            ViewBag.Ninjas = ninjaFactory.GetNinjas();
            ViewBag.Dojos = dojoFactory.GetDojos();
            return View();
        }
        [HttpPost]
        [Route("AddNinja")]
        public IActionResult AddNinja(Ninja newNinja)
        {
            ninjaFactory.AddNinja(newNinja);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("ninjas/{id}")]
        public IActionResult ShowNinja(int id){
            ViewBag.ninja = ninjaFactory.GetOne(id);
            return View("NinjaDisplay");
        }
    }
}
