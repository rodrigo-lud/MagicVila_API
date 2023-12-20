using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.logging;
using MagicVila_VilaAPI.Models.Dto;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace MagicVila_VilaAPI.Controllers 
{
    //[Route("api/[controller]")] //To automatic change the controller name
    [Route("api/VillaAPI")]
    [ApiController]

    public class VillaAPIController : ControllerBase
    {

        // **** To log with Ilogger
        //private readonly ILogger<VillaAPIController> _logger;
        //public VillaAPIController(ILogger<VillaAPIController> logger)
        //{
        //    _logger = logger;
        //}

        // **** To log with a custom Log Class - Configure in Program.cs
        private readonly ILogging _logger;
        public VillaAPIController(ILogging logger) { 
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting all Villas","info");
            return Ok(VillaStore.villaList);
        }


        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0) {
                _logger.Log("Get Villa Error - Id: " + id, "error");
                return BadRequest(); 
            }
            var v = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (v == null) {
                _logger.Log("Get Villa Error: Not Found! Id: " + id, "error");
                return NotFound(); 
            }

            _logger.Log("Getting Villa with code: " + id, "info");
            return Ok(v);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO newVilla)
        {
            if (newVilla == null) { return BadRequest(newVilla); }
            if ( VillaStore.villaList.FirstOrDefault(u=>u.Name.ToLower() == newVilla.Name.ToLower()) != null )
            {
                ModelState.AddModelError("CustomError", "Villa "+ newVilla.Name + " already exists!");
                return BadRequest(ModelState);
            }
            newVilla.Id = NextId();
            VillaStore.villaList.Add(newVilla);
            return CreatedAtAction(nameof(GetVilla), new { id = newVilla.Id }, newVilla);

        }


        [HttpDelete("{id:int}", Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteVilla(int id)
        {
            if (id == 0) { return BadRequest(); }
            var Villa = VillaStore.villaList.SingleOrDefault(u => u.Id == id);
            if (Villa == null) { return NotFound(); }
            VillaStore.villaList.Remove(Villa);
            return NoContent();

        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id) { return BadRequest();}
            var Villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (Villa == null) { return BadRequest(); }
            Villa.Name = villaDTO.Name;
            Villa.Occupancy = villaDTO.Occupancy;
            Villa.Sqft = villaDTO.Sqft;
            return NoContent();
        }



        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        /*       
        [
          {
            "path": "/name",
            "op": "replace",
            "value": "New Name of the Villa"
          }
        ]
        */
        public ActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0) { return BadRequest(); }
            var Villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (Villa == null) { return BadRequest(); }
            patchDTO.ApplyTo(Villa, ModelState);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return NoContent();
        }



        // method to generate a unique ID
        private int NextId()
        {
            // increment the maximum existing ID by 1
            return VillaStore.villaList.Max(x => x.Id) + 1;
        }

    }
}
