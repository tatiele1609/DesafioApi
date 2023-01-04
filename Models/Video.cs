
namespace DesafioApi.Models.Entities
{
    public class Video
    {
        public Video(Guid id, string descricao, Guid idServidor)
        {
            Id = id;
            Descricao = descricao;
            IdServidor = idServidor;
        }

        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Guid IdServidor { get; set; }
    }
}