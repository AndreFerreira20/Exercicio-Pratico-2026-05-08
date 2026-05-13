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
                // Ignorar reservas canceladas
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

            // PRIMEIRO: validar pela política (que pode autorizar o cancelamento de conflitos)
            if (!_politica.Validar(nova, existentes))
            {
                return; // Política negou a reserva
            }

            // SEGUNDO: obter reservas que devem ser canceladas (se a política permitir)
            var paraCancelar = _politica.ObterReservasParaCancelar(nova, existentes);

            // Cancelar as reservas conflitantes
            foreach (var r in paraCancelar)
            {
                Console.WriteLine($"  Cancelando reserva de {r.GetUsuario().GetNome()} para abrir espaço.");
                r.Cancelar();
                r.GetUsuario().Update($"Sua reserva na sala {r.GetSala().GetCodigo()} foi cancelada devido a uma prioridade.");
            }

            // Atualizar a lista após cancelamentos
            existentes = repo.ListarTodas();

            // TERCEIRO: verificar disponibilidade considerando reservas já canceladas
            if (!SalaDisponivel(nova, existentes))
            {
                Console.WriteLine($"  [Erro] Sala {nova.GetSala().GetCodigo()} não está disponível neste horário!");
                return;
            }

            // QUARTO: confirmar e adicionar a nova reserva
            repo.Adicionar(nova);
            nova.Confirmar();
            nova.GetUsuario().Update("Sua reserva foi confirmada com sucesso!");
        }
    }
}
