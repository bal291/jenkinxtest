using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using subjectmicroservice.Models;

namespace subjectmicroservice.Controllers
{
    public class HomeController : Controller
    {
        private RedisDictionary<Guid, Subject> subjects;

        public HomeController()
        {
            subjects = new RedisDictionary<Guid, Subject>("subjects");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.data = GetRedisData();

            return View();
        }

        public IActionResult Add(string Name)
        {
            AddRedisData(Name);

            ViewBag.data = GetRedisData();

            return View("About");
        }

        private List<Subject> GetRedisData()
        {
            return subjects.Values.ToList();
        }

        private void AddRedisData(string value)
        {
            var id = Guid.NewGuid();
            subjects.Add(id, new Subject() {Id= id, Name = value });
          
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
