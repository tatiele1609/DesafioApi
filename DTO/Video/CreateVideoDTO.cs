
namespace DesafioApi.DTO.Video
{
    public class CreateVideoDTO
    {
        public string Descricao { get; set; }
        public Guid IdServidor { get; set; }
        public string VideoBase64 { get; set; }
    }
}