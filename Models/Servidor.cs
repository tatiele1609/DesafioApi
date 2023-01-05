
namespace DesafioApi.Models
{
    public class Servidor
    {
        public Servidor(Guid id, string nome, string enderecoIP, int portaIP)
        {
            Id = id;
            Nome = nome;
            EnderecoIP = enderecoIP;
            PortaIP = portaIP;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string EnderecoIP { get; set; }
        public int PortaIP { get; set; }

    }
}