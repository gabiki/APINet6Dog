using firstAPI.Domain;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using firstAPI.DogService;

namespace firstAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DogController : ControllerBase
{
    public static List<Dog> dogList = new List<Dog>
    {
        new Dog {name = "Alfredo", age = 20, color = "coffee", image = "https://www.omlet.com.pl/images/cache/1024/680/Dog-Pug-A_pug_sprinting_at_full_pace_with_it's_tongue_out.jpg"},
        new Dog {name = "Armando", age = 19, color = "grey", image = "https://st.depositphotos.com/2869437/3739/i/950/depositphotos_37392643-stock-photo-close-up-of-pug.jpg"},
        new Dog {name = "Francisco", age = 7, color = "black", image = "https://cdn.britannica.com/35/233235-050-8DED07E3/Pug-dog.jpg"},
        new Dog {name = "Felipe", age = 12, color = "white", image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRymzbFHcbQDki-bbYDuTFfPh4pGp6Teh3-LQ&usqp=CAU"},
    };

    private readonly IDogService dogService;
    public DogController(IDogService dogService)
    {
        this.dogService = dogService;
    }

    [HttpGet]
    public ActionResult<List<Dog>> GetDogs()
    {
        return Ok(dogList);
    }

    [HttpGet("oddDogName")]
    public ActionResult<List<Dog>> GetDogsWithOddName()
    {
        List<Dog> dogs = dogList.FindAll(e => e.name.Length % 2 != 0);
        if (dogs.Count == 0)
        {
            return NotFound("Dogs not found.");
        }
        else
        {
            return Ok(dogs);
        }
    }

    [HttpPost]
    public ActionResult AddSingleDog(Dog dog)
    {
        var existingDog = dogList.Find(e => e.name == dog.name);
        if (existingDog != null)
        {
            return BadRequest("There is an existing dog with this name.");
        }
        else
        {
            dogList.Add(dog);
            var resourceUrl = Request.Path.ToString() + "/" + dog.name;
            return Created(resourceUrl, dog);
        }
    }

    [HttpGet("dogAgeSum")]
    public ActionResult<string> SumDogAge()
    {
        string dogsAge = dogList.Sum(x => x.age).ToString();
        return Ok("The sum of all the dog's age is: " + dogsAge + ".");
    }

    [HttpGet("getSingleDog")]
    public ActionResult<List<Dog>> GetSingleDog(string name)
    {
        List<Dog> singleDog = dogList.FindAll(e => e.name == name);
        if (singleDog.Count == 0)
        {
            return NotFound("Dog not found.");
        }
        else
        {
            return Ok(singleDog);
        }
    }

    [HttpPost("getNewAge")]
    public ActionResult<string> GetNewAgeDog(int age, Dog dog)
    {
        var existingDog = dogList.Find(e => e.name == dog.name);
        if (existingDog == null)
        {
            return NotFound("Dog not found.");
        }
        else
        {
            var estimated = dogService.EstimatedAge(age, existingDog);
            return Ok(estimated);
        }
    }

    [HttpPost("addDogList")]
    public ActionResult<List<Dog>> AddMultipleDogs(List<Dog> dogs)
    {
        for (int i = 0; i < dogs.Count; i++)
        {
            var existingDog = dogList.Find(d => d.name == dogs[i].name);
            if (existingDog != null)
            {
                return BadRequest("There is an existing dog with this name.");
            }
            else
            {
                dogList.Add(dogs[i]);
            }
        }
        return Ok("Dogs added.");
    }

}