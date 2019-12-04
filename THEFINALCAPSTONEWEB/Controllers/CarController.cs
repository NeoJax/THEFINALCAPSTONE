using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using THEFINALCAPSTONE.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace THEFINALCAPSTONE.Controllers
{
    public class CarController : Controller
    {
        private readonly HttpClient _client;

        public CarController(IHttpClientFactory client)
        {
            _client = client.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:5001/");
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult SearchCars()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchCars(Cars car)
        {
            var carCar = $"api/car/search?make={car.Make}&model={car.Model}&year={car.Year}&color={car.Color}";

            var response = await _client.GetAsync(carCar);
            var results = await response.Content.ReadAsAsync<List<Cars>>();

            return View(results);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(Cars car)
        {
            var somethingOrAnother = await _client.PutAsJsonAsync("api/Car", car);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditCar(int id)
        {
            var response = await _client.GetAsync($"api/Car/{id}");
            var car = await response.Content.ReadAsAsync<Cars>();

            return View(car);
        }

        [HttpPut]
        public async Task<IActionResult> EditCar(Cars car)
        {
            var somethingOrAnother = await _client.PutAsJsonAsync("api/Car", car);
            return RedirectToAction("Index", "Home");
        }
    }
}
