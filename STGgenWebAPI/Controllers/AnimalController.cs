using System.Collections.Generic;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STGgenWebAPI.Data;
using STGgenWebAPI.Model;
using STGgenWebAPI.Services;

namespace STGgenWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : Controller
    {

        readonly IAnimalService animalService;

        public AnimalController(IAnimalService service )
        {
           animalService = service; 
 
        }
 

        [HttpPost(Name = "CreateAnimal")]
        public async Task< IActionResult> CreateAnimal([FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           await  animalService.Save(animal);
 
            return CreatedAtAction("CreateAnimal", new { animalId = animal.AnimalId }, animal);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Animal animal)
        {
           await  animalService.Update(id, animal);
            return Ok();
        }


        [HttpGet("filter")]
        public IActionResult FilterAnimals(int animalId, string Name, string sex, string status)
        { 
            
            return Ok(animalService.FilterAnimals(animalId, Name, sex,status) );
        }
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await animalService.Delete(id);
            return Ok();
        }

        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromBody] List<Animal> animalsOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (animalService.ValidateOrder(animalsOrder))
            {
                var orderNumber = animalService.CreateOrder(animalsOrder);
                return CreatedAtAction("CreateOrder", new { orderId = orderNumber }, animalsOrder);
            }
            else
            {
                return BadRequest("An animal cannot be repeated in the order list.");
            }
        }

    }
}
