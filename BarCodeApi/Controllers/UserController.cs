using IronBarCode;
using IronSoftware;
using IronSoftware.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection.Metadata;

namespace BarCodeApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        DataClass dataClass = new DataClass();
        UsersdbContext db;
        public UserController(UsersdbContext db) 
        {
            this.db = db;
        }
        
        [HttpGet("/GenerateAllBarCodes")]
        public IActionResult GenerateAllBarCodes(int countOfUsers)
        {
            try
            {
                ChromePdfRenderer renderer = new ChromePdfRenderer();
                PdfDocument pdf = renderer.RenderHtmlAsPdf("<h1>TestPDF</h1>");
                Generator(countOfUsers);
                int iter = 0;
                foreach (Models.User i in db.Users)
                {
                    var myBarCode = BarcodeWriter.CreateBarcode(Convert.ToString(i.IdUser), BarcodeWriterEncoding.QRCode);
                    Bitmap bmp = myBarCode.ToBitmap();
                    pdf.DrawBitmap(bmp, 0, 50, iter * 50, 50, 50); 
                    iter++;
                }
                pdf.SaveAs("drawText.pdf");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }           
        }


        [HttpGet("/ReturnBarCode")]
        public IActionResult ReturnBarCode(long id)
        {
            try
            {
                Models.User? user = db.Users.FirstOrDefault(x => x.IdUser == id);
                if ( user != null){
                    var myBarcode = BarcodeWriter.CreateBarcode(user.IdUser.ToString(), BarcodeEncoding.QRCode);
                    Bitmap bitmap = myBarcode.ToBitmap();
                    return File((ConvertToByteArray(bitmap)),"image/jpeg");
                }
                else
                {
                    return NotFound();
                }                                                                            
                
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
       
        private void Generator(int times)
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
        private byte[] ConvertToByteArray(Bitmap bmp)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();


            }
        }

    }
}
