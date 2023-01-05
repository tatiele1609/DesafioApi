
namespace DesafioApi.Models
{
    public class Arquivo
    {
        public Arquivo(Guid id, string codigoBase64, Guid idVideo)
        {
            Id = id;
            CodigoBase64 = codigoBase64;
            IdVideo = idVideo;
        }

        public Guid Id { get; set; }
        public string CodigoBase64 { get; set; }
        public Guid IdVideo { get; set; }
    }
}