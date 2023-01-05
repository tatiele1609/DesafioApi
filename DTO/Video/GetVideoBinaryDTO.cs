
namespace DesafioApi.DTO.Video
{
    public class GetVideoBinaryDTO
    {
        public Guid IdVideo { get; set; }
        public Guid IdServidor { get; set; }
        public string ConteudoBase64 { get; set; }
    }
}