using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HomeWork.Entities;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Süd",
                Description = "10% Yağlı",
                Price = (decimal)2.5,
            },
            new Product
            {
                Id = 2,
                Name = "Ət",
                Description = "Dana Sümüksüz",
                Price = (decimal)16,
            },
            new Product
            {
                Id = 3,
                Name = "Yağ Kərə",
                Description = "Finland",
                Price = (decimal)23,
                Discount = 5,
            },
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Product");
        }

        public IActionResult Product()
        {
            return View(products);
        }

        public IActionResult Delete(int id)
        {
            var item = products.FirstOrDefault(e => e.Id == id);
            if (item != null)
            {
                products.Remove(item);
                TempData["Message"] = $"{item.Name} deleted successfully";
            }
            return RedirectToAction("Product");
        }

        [HttpGet]
        public IActionResult Add()
        {
            var vm = new ProductAddViewModel
            {
                Product = new Product()
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = products.FirstOrDefault(e => e.Id == id);
            if (item != null)
            {
                var vm = new ProductAddViewModel
                {
                    Product = item
                };

                Console.WriteLine(vm.Product.Id);

                return View(vm);
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddElement(ProductAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Product.Id = (new Random()).Next(1, 10000);
                products.Add(vm.Product);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public IActionResult EditElement(ProductAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                products[vm.Product.Id] = (vm.Product);
                return RedirectToAction("Index");
            }
            return View(vm);
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
