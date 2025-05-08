using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase

{
    private readonly PizzaService _pizzaService;
    public PizzaController(PizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
      _pizzaService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza == null)
            return NotFound();

        return pizza;
    }
    // POST action
    [HttpPost]
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        if (pizza == null)
        {
            return BadRequest("Pizza cannot be null.");
        }

        _pizzaService.AddPizza(pizza); // Assuming Add is a method in PizzaService to save the pizza

        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }








    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (pizza == null || pizza.Id != id)
        {
            return BadRequest("Pizza data is invalid.");
        }

        var existingPizza = PizzaService.Get(id);
        if (existingPizza == null)
        {
            return NotFound();
        }

        _pizzaService.Update(pizza); // Assuming Update is a method in PizzaService to update the pizza

        return NoContent(); // Return 204 No Content to indicate the update was successful
    }
    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
       var pizza = PizzaService.Get(id);
    if (pizza == null)
    {
        return NotFound(); // Return 404 if the pizza does not exist
    }

    _pizzaService.DeletePizza(id); // Assuming Delete is a method in PizzaService to remove the pizza

    return NoContent(); // Return 204 No Content to indicate the deletion was successful
    }
}