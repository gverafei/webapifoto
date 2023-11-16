namespace webapifoto.Models
{
    public class Foto
    {
        public IFormFile Archivo { get; set; }
        public string Nombre { get; set; }

#nullable enable
        // El signo de interrogación, quiere decir que puede ser null
        public string? Url { get; set; }
    }
}
