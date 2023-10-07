using BarCodeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        ScudCrmContext db;
        public RoomController(ScudCrmContext db)
        {
            this.db = db;
        }

        [HttpPost("/RoomInitializer")]
        public IActionResult RoomInitializer()//cоздание комнат и инициализация их позиции
        {
            try
            {
                int m = 1;
                Random random = new Random();
                for (int i = 2; i < 5; i++)
                {
                    if (i == 3)
                        continue;
                    else
                    {
                        for (int j = 1; j < 9; j++)
                        {

                            db.Rooms.Add(new Room
                            {
                                IdRoom = m,
                                RoomNumber = random.Next(0, 1000000),
                                X = j,
                                Y = i
                            });
                            m++;
                            db.SaveChanges();
                        }
                    }
                                  
                }
                return Ok();
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("/ReturnAllRooms")]
        public IActionResult ReturnAllRooms() 
        {
            try
            {
                return Ok(db.Rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
    }
}
