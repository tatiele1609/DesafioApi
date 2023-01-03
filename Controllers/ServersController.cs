using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ServersController(DesafioContext desafioContext)
        {
            _desafioContext = desafioContext;
        }

        [HttpPost]
        public async Task<ActionResult<Servidor>> PostServidor(Servidor servidor)
        {
            _desafioContext.Servidor.Add(servidor);
            await _desafioContext.SaveChangesAsync();

            return Ok();
            
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Servidor>> PutServidor(Servidor servidor)
        {
            var servidorAtual = await _desafioContext.Servidor.FindAsync(servidor.Id);  
            if (servidor == null)
            {
                return NotFound();
            }

            servidorAtual.Nome = servidor.Nome;
            servidorAtual.EnderecoIP = servidor.EnderecoIP;
            servidorAtual.PortaIP = servidor.PortaIP;
            
            try
            {
                await _desafioContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return NoContent();
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
            {
                return NotFound();
            }

            return servidor;
        }


        [HttpGet("endereco/{enderecoIP}/porta/{portaIP}")]
        public async Task<ActionResult<Servidor>> GetServidorConfigurado(string enderecoIP, int portaIP)
        {
            var servidor = await _desafioContext.Servidor
                                .Where(x => x.EnderecoIP == enderecoIP && x.PortaIP == portaIP)
                                .FirstOrDefaultAsync();

            if (servidor == null)
            {
                return NotFound();
            }

            return servidor;
        }


        //disponibilidade

        [HttpDelete("{id}")]
        public async Task<ActionResult<Servidor>> DeleteServidor(Guid id)
        {
            var servidor = await _desafioContext.Servidor.FindAsync(id);

            if (servidor == null)
            {
                return NotFound();
            }

            _desafioContext.Remove(servidor);
            await _desafioContext.SaveChangesAsync();

            return NoContent();
        }


        //videos
        [HttpPost("{serverId}/videos​")]
        public async Task<ActionResult<Video>> PostVideoServidor(Video video, Guid serverId)
        {
            video.IdServidor = serverId;

            _desafioContext.Video.Add(video);
            await _desafioContext.SaveChangesAsync();

            return Ok();
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
        public async Task<ActionResult<Video>> DeleteVideoServidor(Guid serverId, Guid videoId)
        {
            var video = await _desafioContext.Video
                            .Where(x => x.IdServidor == serverId && x.Id == videoId)
                            .FirstOrDefaultAsync();

            if (video == null)
            {
                return NotFound();
            }

            _desafioContext.Remove(video);
            await _desafioContext.SaveChangesAsync();

            return NoContent();
        }

    }
}