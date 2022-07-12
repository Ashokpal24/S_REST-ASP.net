using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
namespace Superhero.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperheroController:ControllerBase
    {
        private static List<SuperheroItem> Superheroes=new List<SuperheroItem>{
            new SuperheroItem{Id=1,Name="Batman"},
            new SuperheroItem{Id=2,Name="Superman"},
            new SuperheroItem{Id=3,Name="IronMan"}
        };
        [HttpGet]
        public ActionResult<List<SuperheroItem>> Get()
        {
            return Ok(Superheroes);
        }
        [HttpGet]
        [Route("{Id}")]
        public ActionResult<List<SuperheroItem>> Get(int Id)
        {
            var superheroItem=Superheroes.Find(x=>x.Id==Id);
            return superheroItem==null?NotFound():Ok(superheroItem);
        }
        [HttpPost]
        public ActionResult Post(SuperheroItem superheroItem){
            var exists=Superheroes.Find(x=>x.Id==superheroItem.Id);
            if(exists!=null){
                return Conflict("Cannot create the Id because it already exists");
            }
            else{
                Superheroes.Add(superheroItem);
                var resourceUrl=Request.Path.ToString()+"/"+superheroItem.Id;
                return Created(resourceUrl,superheroItem);
            }
        }
        [HttpPut]
        public ActionResult Put(SuperheroItem superheroItem){
            var exists=Superheroes.Find(x=>x.Id==superheroItem.Id);
            if(exists!=null){
                exists.Name=superheroItem.Name;
                return Ok();
            }
            else{
                return BadRequest("Cannot update a non-existing term");
            }
        }
        [HttpDelete]
        [Route("{Id}")]
        public ActionResult Delete(int Id)
        {
            var superheroItem=Superheroes.Find(x=>x.Id==Id);
            if(superheroItem==null)
            {
                return NotFound();
            }
            else
            {
                Superheroes.Remove(superheroItem);
                return NoContent();
            }
        }
    }
}