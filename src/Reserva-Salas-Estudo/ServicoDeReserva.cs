using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    internal class ServicoDeReserva
    {
        private IPoliticaReserva _politica;

        public ServicoDeReserva(IPoliticaReserva politica) { _politica = politica; }

        private bool SalaDisponivel(Reserva reserva, List<Reserva> reservasExistentes)
        {
            foreach (var existente in reservasExistentes)
            {
                
                if (existente.GetStatus() == StatusReserva.Cancelada)
                    continue;

                if (existente.GetSala().GetCodigo() == reserva.GetSala().GetCodigo() &&
                    existente.GetStatus() == StatusReserva.Confirmada &&
                    existente.ConflitaCom(reserva.GetInicio(), reserva.GetFim()))
                {
                    return false;
                }
            }
            return true;
        }

        public void RealizarReserva(Reserva nova)
        {
            var repo = RepositorioReservas.GetInstance();
            var existentes = repo.ListarTodas();

            
            if (!_politica.Validar(nova, existentes))
            {
                return; 
            }

            
            var paraCancelar = _politica.ObterReservasParaCancelar(nova, existentes);

           
            foreach (var r in paraCancelar)
            {
                Console.WriteLine($"  Cancelando reserva de {r.GetUsuario().GetNome()} para abrir espaço.");
                r.Cancelar();
                r.GetUsuario().Update($"{r.GetUsuario().GetNome()}, Sua reserva na sala {r.GetSala().GetCodigo()} foi cancelada devido a uma prioridade.");
            }

            
            existentes = repo.ListarTodas();

            
            if (!SalaDisponivel(nova, existentes))
            {
                Console.WriteLine($"  [Erro] Sala {nova.GetSala().GetCodigo()} não está disponível neste horário!");
                return;
            }

            
            repo.Adicionar(nova);
            nova.Confirmar();
            nova.GetUsuario().Update($"{nova.GetUsuario().GetNome()} Sua reserva foi confirmada com sucesso!");
        }
    }
}
