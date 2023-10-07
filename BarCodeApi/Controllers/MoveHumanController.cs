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
                List<long> ids = db.Users.Select(b => b.IdUser).ToList();//собирает все айдишники из таблицы юзерсов
                for (int x = 0; x<count; x++)
                {
                    
                    Random rnd = new Random(); //int id,bool stustain, int room
                    int userId = rnd.Next(0, ids.Count);
                    int roomId = rnd.Next(1, db.Rooms.Count());
                    commands.Add(new HumanMovement
                    {
                        Room = db.Rooms.First(x=> x.IdRoom == roomId),
                        User = db.Users.First(x=>x.IdUser == ids[userId]),
                        Sustain = rnd.Next(2) == 1
                    });
                    ids.RemoveAt(userId);
                }
                return Ok(commands);//отправлет запросы
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
