using Microsoft.AspNetCore.Mvc;
using webapifoto.Models;

namespace webapifoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FotoController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // POST: api/foto
        [HttpPost]
        public async Task<ActionResult<Foto>> PostIndex([FromForm] Foto foto)
        {
            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string rutaArchivos = Path.Combine(webRootPath, "files");

                if (foto.Archivo.Length > 0)
                {
                    if (!Directory.Exists(rutaArchivos))
                    {
                        Directory.CreateDirectory(rutaArchivos);
                    }
                    using (FileStream filestream = System.IO.File.Create(Path.Combine(rutaArchivos, foto.Archivo.FileName)))
                    {
                        await foto.Archivo.CopyToAsync(filestream);
                        filestream.Flush();
                    }
                    foto.Url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/files/" + foto.Archivo.FileName;
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message + _webHostEnvironment.WebRootPath);
            }
            return CreatedAtAction(nameof(PostIndex), new { foto.Nombre, foto.Url });
        }
    }
}
