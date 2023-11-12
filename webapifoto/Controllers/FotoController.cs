using Microsoft.AspNetCore.Mvc;
using webapifoto.Models;

namespace webapifoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoController : Controller
    {
        public static IWebHostEnvironment _environment;
        public FotoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // POST: api/foto
        [HttpPost]
        public async Task<ActionResult<Foto>> PostIndex([FromForm] Foto foto)
        {
            string rutaArchivos = "\\files\\";
            try
            {
                if (foto.Archivo.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + rutaArchivos))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + rutaArchivos);
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + rutaArchivos + foto.Archivo.FileName))
                    {
                        await foto.Archivo.CopyToAsync(filestream);
                        filestream.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return CreatedAtAction(nameof(PostIndex), foto.Nombre);
        }
    }
}
