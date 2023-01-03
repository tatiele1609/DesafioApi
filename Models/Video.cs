using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Models
{
    public class Video
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        //public byte ConteudoVideo { get; set; }
        public string ConteudoVideo { get; set; }
        public Guid IdServidor { get; set; }
    }
}