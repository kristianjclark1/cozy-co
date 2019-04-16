using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyCo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CozyCo.WebUI.Controllers
{
    public class PropertyController : Controller
    {
        private List<Property> Properties = new List<Property>();

        //property/Index
        public IActionResult Index()
        {
            return View(Properties);
        }

        //property/add
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Property newProperty)
        {
            Properties.Add(newProperty);
            return View(nameof(Index), Properties);
          
            

        }
    }
}