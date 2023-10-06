using BarCodeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveHumanController : ControllerBase
    {
        ScudCrmContext db;
        public MoveHumanController(ScudCrmContext db)
        {
            this.db = db;
        }
        [HttpGet("/MoveHuman")]
        public IActionResult MoveHuman(int count)
        {
            try
            {
                List<HumanMovement> commands = new List<HumanMovement>();
                for(int x = 0; x<count; x++)
                {
                    Random rnd = new Random(); //int id,bool stustain, int room
                    commands.Add(new HumanMovement
                    {
                        
                    });

                }
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
