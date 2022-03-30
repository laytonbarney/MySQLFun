using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySQLFun.Models;

namespace MySQLFun.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository _repo { get; set; }
        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
            var blah = _repo.Bowlers.ToList();
            return View(blah);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("AddEdit");
        }

        [HttpPost]
        public IActionResult Create(Bowler blah)
        {
            _repo.AddBowler(blah);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            var application = _repo.Bowlers.Single(x => x.BowlerID == id);

            return View("AddEdit", application);
        }

        [HttpPost]
        public IActionResult Edit(Bowler editBowler)
        {
            _repo.SaveChanges(editBowler);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var application = _repo.Bowlers.Single(x => x.BowlerID == id);

            return View(application);
        }

        [HttpPost]
        public IActionResult Delete(Bowler stuff)
        {

            _repo.DeleteBowler(stuff);

            return RedirectToAction("Index");
        }
    }
}
