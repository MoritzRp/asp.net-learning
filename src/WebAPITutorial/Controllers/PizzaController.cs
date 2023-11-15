using Microsoft.AspNetCore.Mvc;
using WebAPITutorial.Models;
using WebAPITutorial.Services;

namespace WebAPITutorial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        public PizzaController() { }

        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza is null) return NotFound();

            return pizza;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Create(Pizza pizza)
        {
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id) return BadRequest();

            var existingPizza = PizzaService.Get(id);
            if (existingPizza is null) return NotFound();

            PizzaService.Update(pizza);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza is null) return NotFound();

            PizzaService.Delete(id);

            return NoContent();
        }
    }
}
