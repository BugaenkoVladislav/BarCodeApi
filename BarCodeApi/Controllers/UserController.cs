using BarCodeApi.Models;
using IronBarCode;
using IronSoftware;
using IronSoftware.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace BarCodeApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        

        DataClass dataClass = new DataClass();
        ScudCrmContext db;
        public UserController(ScudCrmContext db)
        {
            this.db = db;
        }

        [HttpGet("/GenerateAllBarCodes")]
        public IActionResult GenerateAllBarCodes(int countOfUsers)
        {
            try
            {
                ChromePdfRenderer renderer = new ChromePdfRenderer();
                PdfDocument pdf = renderer.RenderHtmlAsPdf("<h1>Список всех сотрудниеов</h1>");
                Generator(countOfUsers);
                int iter = 0;
                foreach (Models.User i in db.Users)//цикл отрисовки всех QR
                {
                    var myBarCode = BarcodeWriter.CreateBarcode(Convert.ToString(i.IdUser), BarcodeWriterEncoding.QRCode);//cоздаем QR code
                    Bitmap bmp = myBarCode.ToBitmap();//указываем в битмапе наш QR
                    pdf.DrawBitmap(bmp, 0, 50, iter * 50, 50, 50);//отрисовыавем QR
                    iter++;
                }
                string name = "UsersQRCode.pdf";
                pdf.SaveAs(name);
                var bytes = System.IO.File.ReadAllBytes(name);
                return File(bytes, "application/pdf");//возвращаем файл массивом байтов
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("/ReturnAllUsersStatus")]
        public IActionResult ReturnAllUsersStatus()
        {
            try
            {
                return Ok(db.Users.Select(m => new
                {
                    m.IdUser,
                    m.IdStatus
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("/ReturnBarCode")]
        public IActionResult ReturnBarCode(long id)
        {
            try
            {
                Models.User? user = db.Users.FirstOrDefault(x => x.IdUser == id);
                if (user != null) {
                    var myBarcode = BarcodeWriter.CreateBarcode(user.IdUser.ToString(), BarcodeEncoding.QRCode);
                    Bitmap bitmap = myBarcode.ToBitmap();
                    return File((ConvertToByteArray(bitmap)), "image/jpeg");
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        

        private void Generator(int times)//создает пользователей в бд 
        {
            if (db.Users.Count() == 0)
            {
                while (times > 0)
                {
                    Random random = new Random();
                    int res = random.Next(0, 100000);

                    db.Users.Add(new Models.User
                    {
                        IdUser = res,
                        Name = dataClass.GetRandomName(),
                        Surname = dataClass.GetRandomSurname(),
                        Midname = dataClass.GetRandomMidname(),
                    });
                    db.SaveChanges();
                    times--;
                }

            }
        }
        private byte[] ConvertToByteArray(Bitmap bmp)//конвертация битмапа в ByteArray
        {
            using (MemoryStream ms = new MemoryStream())//создаем новый поток данных
            {
                bmp.Save(ms, ImageFormat.Png);//cохраняем битмап в формате пнг
                return ms.ToArray();//возвращаем поток  массивом байтов


            }
        }

    }

}
