using DesafioApi.Data;
using DesafioApi.DTO.Servidor;
using DesafioApi.DTO.Video;
using DesafioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Handlers
{
    public class ServerHandler
    {
        private readonly DesafioContext _desafioContext;

        public ServerHandler(DesafioContext desafioContext)
        {
            _desafioContext = desafioContext;
        }


        public async Task Handle(CreateServidorDTO createServidor)
        {
            try
            {
               var servidor = new Servidor(Guid.NewGuid(), 
                                            createServidor.Nome, 
                                            createServidor.EnderecoIP, 
                                            createServidor.PortaIP);
               
                _desafioContext.Servidor.Add(servidor);
                await _desafioContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task Handle(UpdateServidorDTO updateServidor)
        {
            try
            {
                var servidorAtual = await _desafioContext.Servidor.FindAsync(updateServidor.Id);  

                servidorAtual.Nome = updateServidor.Nome;
                servidorAtual.EnderecoIP = updateServidor.EnderecoIP;
                servidorAtual.PortaIP = updateServidor.PortaIP;

                await _desafioContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task Handle(DeleteServidorDTO deleteServidor)
        {
            try
            {
                var servidor = await _desafioContext.Servidor.FindAsync(deleteServidor.Id);

                _desafioContext.Remove(servidor);
                await _desafioContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task Handle(CreateVideoDTO createVideo)
        {
            try
            {
                var video = new Video(Guid.NewGuid(), createVideo.Descricao, createVideo.IdServidor);
                _desafioContext.Video.Add(video);
                await _desafioContext.SaveChangesAsync(); 

                var arquivoVideo = new Arquivo(Guid.NewGuid(), createVideo.VideoBase64, video.Id);
                _desafioContext.Arquivo.Add(arquivoVideo);
                await _desafioContext.SaveChangesAsync(); 
                
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }

        public async Task<string> Handle(GetVideoBinaryDTO getVideoBinaryDTO)
        {
            var arquivo = await _desafioContext.Arquivo
                        .Where(x => x.IdVideo == getVideoBinaryDTO.IdVideo)
                        .FirstOrDefaultAsync();

            return arquivo.CodigoBase64;
        }

        public async Task Handle(DeleteVideoDTO deleteVideo)
        {
            try
            {
                var video = await _desafioContext.Video
                            .Where(x => x.IdServidor == deleteVideo.IdServidor && x.Id == deleteVideo.IdVideo)
                            .FirstOrDefaultAsync();

                _desafioContext.Remove(video);
                await _desafioContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}