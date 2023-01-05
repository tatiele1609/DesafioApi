using DesafioApi.Data;
using DesafioApi.DTO.Servidor;
using DesafioApi.DTO.Video;
using DesafioApi.Handlers;
using DesafioApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase 
    { 
        private readonly DesafioContext _desafioContext;
        private readonly ServerHandler _serverHandler;

        public ServersController(DesafioContext desafioContext, ServerHandler serverHandler)
        {
            _desafioContext = desafioContext;
            _serverHandler = serverHandler;
        }

        
        [HttpPost]
        public async Task<ActionResult<CreateServidorDTO>> PostServidor(CreateServidorDTO servidorDTO)
        {
            try
            {
                await _serverHandler.Handle(servidorDTO);
                return Ok("Servidor adicionado com sucesso!");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível adicionar o servidor!");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Servidor>> PutServidor(UpdateServidorDTO servidorDTO)
        {
            var servidorAtual = await _desafioContext.Servidor.FindAsync(servidorDTO.Id);  
            if (servidorAtual == null)
                return NotFound("O id do Servidor informado não foi encontrado!");
            
            try
            {
                await _serverHandler.Handle(servidorDTO);
                return Ok("Servidor atualizado com sucesso!");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível atualizar o servidor!");
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servidor>>> GetServidores()
        {
            var servidores = await _desafioContext.Servidor.ToListAsync();
            return servidores;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Servidor>> GetServidor(Guid id)
        {
            var servidor = await _desafioContext.Servidor.FindAsync(id);

            if (servidor == null)
                return NotFound("O id do Servidor informado não foi encontrado!");

            return servidor;
        }


        [HttpGet("endereco/{enderecoIP}/porta/{portaIP}")]
        public async Task<ActionResult<Servidor>> GetServidorConfigurado(string enderecoIP, int portaIP)
        {
            var servidor = await _desafioContext.Servidor
                                .Where(x => x.EnderecoIP == enderecoIP && x.PortaIP == portaIP)
                                .FirstOrDefaultAsync();

            if (servidor == null)
                return NotFound("Não foi encontrado servidor para as configurações de endereço e porta informadas!");

            return servidor;
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<Servidor>> DeleteServidor(DeleteServidorDTO servidorDTO)
        {
            var servidor = await _desafioContext.Servidor.FindAsync(servidorDTO.Id);
            if (servidor == null)
                return NotFound("O id do Servidor informado não foi encontrado!");

            try
            {
                await _serverHandler.Handle(servidorDTO);
                return Ok("Servidor excluído com sucesso!");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível excluir o servidor!");
            }
        }


        
        [HttpPost("{serverId}/videos​")]
        public async Task<ActionResult<Video>> PostVideoServidor(CreateVideoDTO videoDTO)
        {
            var servidor = await _desafioContext.Servidor.FindAsync(videoDTO.IdServidor);
            if (servidor == null)
                return NotFound("O id do Servidor informado não foi encontrado!");

            try
            {
                await _serverHandler.Handle(videoDTO);
                return Ok("Vídeo adicionado ao servidor com sucesso!");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível adicionar o vídeo ao servidor!");
            }
        }


        [HttpGet("{serverId}/videos/{videoId}")]
        public async Task<ActionResult<Video>> GetVideoServidor(Guid serverId, Guid videoId)
        {
            var video = await _desafioContext.Video
                            .Where(x => x.IdServidor == serverId && x.Id == videoId)
                            .FirstOrDefaultAsync();

            if (video == null)
            {
                return NotFound();
            }

            return video;
        }

        [HttpGet("{serverId}/videos/{videoId}/binary​")]
        public async Task<ActionResult<string>> GetConteudoVideo(Guid serverId, Guid videoId)
        {
            try
            {
                var video = new GetVideoBinaryDTO
                {
                    IdServidor = serverId,
                    IdVideo = videoId
                };

                var result = await _serverHandler.Handle(video);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível adicionar o vídeo ao servidor!");
            }
        }



        [HttpGet("{serverId}/videos")]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideosServidor(Guid serverId)
        {
            var videos = await _desafioContext.Video
                            .Where(x => x.IdServidor == serverId)
                            .ToListAsync();

            if (videos == null)
            {
                return NotFound();
            }

            return videos;
        }


        [HttpDelete("{serverId}/videos/{videoId}")]
        public async Task<ActionResult<DeleteVideoDTO>> DeleteVideoServidor(DeleteVideoDTO videoDTO)
        {
            var video = await _desafioContext.Video
                            .Where(x => x.IdServidor == videoDTO.IdServidor && x.Id == videoDTO.IdVideo)
                            .FirstOrDefaultAsync();

            if (video == null)
                return NotFound("O vídeo informado não foi encontrado!");

            try
            {
                _desafioContext.Remove(video);
                await _desafioContext.SaveChangesAsync();
                return Ok("Vídeo excluído com sucesso!");
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível excluir o vídeo!");
            }
        }

    }
}