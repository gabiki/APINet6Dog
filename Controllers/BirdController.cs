using firstAPI.Domain;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace firstAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BirdController : ControllerBase
{
    private static List<Bird> birdList = new List<Bird>
    {
        new Bird {name = "Antonio", age = 12, color = "blue", image = "http://t2.gstatic.com/licensed-image?q=tbn:ANd9GcRhD8fYpet1XVKfsYxPN3mMHJOH64eJBkDIIrsHKLxtQqGtD_KRbA-Wt3wc01e1gPwCK5TwGPjgCoFTKPt37l8"},
        new Bird {name = "Juan", age = 11, color = "green"},
        new Bird {name = "Rafael", age = 14, color = "yellow"},
        new Bird {name = "Rodrigo", age = 3, color = "black"},
    };

    [HttpGet] 
    public ActionResult<List<Bird>> GetBirds() 
    {
        return Ok(birdList);
    }

    [HttpGet("pajaro")]
    public ActionResult<List<Bird>> GetBirdOlderThanAge(int age)
    {
        List<Bird> birds = birdList.FindAll(e => e.age > age);
        if (birds.Count == 0)
        {
            return NotFound("Birds not found.");
        } 
        else
        {
            return Ok(birds);
        }
    }

    [HttpPost] 
    public ActionResult AddBird(Bird bird) 
    {
        var existingBird = birdList.Find(e => e.name == bird.name);
        if (existingBird != null)
        {
            return BadRequest("There is an existing bird with this name.");
        }
        else
        {
            birdList.Add(bird);
            var resourceUrl = Request.Path.ToString() + "/" + bird.name;
            return Created(resourceUrl, bird);
        }
    }

    [HttpPut] 
    public ActionResult UpdateBird(string name, Bird bird)
    {
        var existingBird = birdList.Find(e => e.name == name);
        if (existingBird == null)
        {
            return NotFound("Bird not found.");
        }
        else
        {
            existingBird.name = bird.name;
            existingBird.age = bird.age;
            existingBird.color = bird.color;
            existingBird.image = bird.image;

            return Ok("Bird modified");
        }
    }

    [HttpDelete]
    [Route("{Name}")]
    public ActionResult DeleteBird(string Name) 
    {
        var existingBird = birdList.Find(e => e.name == Name);
        if (existingBird == null)
        {
            return NotFound("Bird not found.");
        }
        else
        {
            birdList.Remove(existingBird);
            return Ok("Bird removed.");
        }
    }
}

