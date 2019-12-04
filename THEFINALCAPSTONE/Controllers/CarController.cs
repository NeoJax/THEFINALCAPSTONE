using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using THEFINALCAPSTONE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace THEFINALCAPSTONE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarsDbContext _context;

        public CarController(CarsDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<List<Cars>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Cars/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Cars>> GetCarById(int id)
        {
            var cars = await _context.Cars.ToListAsync();
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }

        //GET: api/Cars?terms=
       [HttpGet("search")]
        public async Task<ActionResult<List<Cars>>> GetCarsBySearch(string ? make, string ? model, int ? year, string ? color)
        {
            return Ok(await _context.Cars.Where(x => x.Make.Contains(make) || x.Model.Contains(model) || x.Color.Contains(color) || x.Year == year).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Cars>> PostCar(Cars car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarById), new { id = car.CarId }, car);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCar(int id, Cars car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }
            _context.Entry(car).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            _context.Cars.Remove(car);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
