using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    internal class PoliticaFIFO : IPoliticaReserva
    {
        public string Nome => "FIFO (primeiro a reservar tem prioridade)";

        public bool Validar(Reserva candidata, List<Reserva> reservasExistentes)
        {
           
            foreach (var reserva in reservasExistentes)
            {
                bool estaAtiva = reserva.GetStatus() == StatusReserva.Confirmada
                                || reserva.GetStatus() == StatusReserva.Pendente;
                bool temConflito = reserva.ConflitaCom(candidata.GetInicio(), candidata.GetFim());

                if (estaAtiva && temConflito)
                {
                    Console.WriteLine($"  [FIFO] Horário ocupado por {reserva.GetUsuario().GetNome()} " +
                                        $"({reserva.GetInicio():HH:mm}-{reserva.GetFim():HH:mm}). Reserva negada.");
                    return false; 
                }
            }

            return true; 
        }

        
        public List<Reserva> ObterReservasParaCancelar(Reserva candidata, List<Reserva> reservasExistentes)
        {
            return new List<Reserva>();
        }
    }
    internal class PoliticaPrioridadeDocente : IPoliticaReserva
    {
        public string Nome => "Prioridade Docente";

        public bool Validar(Reserva candidata, List<Reserva> reservasExistentes)
        {
           
            var conflitos = GetConflitos(candidata, reservasExistentes);

          
            if (conflitos.Count == 0)
                return true;

           
            if (candidata.GetUsuario() is Professor)
            {
                bool todosAlunos = conflitos.All(r => r.GetUsuario() is Estudante);

                if (todosAlunos)
                {
                    Console.WriteLine($"  [PrioridadeDocente] Professor tem prioridade. " +
                                        $"{conflitos.Count} reserva(s) de aluno(s) serão canceladas.");
                    return true; 
                }
            }

            // Qualquer outro caso (aluno vs aluno, aluno vs professor, professor vs professor) → nega
            var primeiro = conflitos[0];
            Console.WriteLine($"  [PrioridadeDocente] Conflito com {primeiro.GetUsuario().GetTipoUsuario()} " +
                                $"{primeiro.GetUsuario().GetNome()}. Reserva negada.");
            return false;
        }

        // Devolve as reservas de alunos que devem ser canceladas quando um professor tem prioridade.
        // O ServicoDeReserva chama este método e cuida de cancelar + notificar cada uma.
        public List<Reserva> ObterReservasParaCancelar(Reserva candidata, List<Reserva> reservasExistentes)
        {
            // Só faz sentido cancelar algo se quem está reservando é professor
            if (candidata.GetUsuario().GetTipoUsuario() != "professor")
                return new List<Reserva>();

            return GetConflitos(candidata, reservasExistentes)
                .Where(r => r.GetUsuario() is Estudante)
                .ToList();
        }

        // Método auxiliar: retorna as reservas ativas que batem no horário da candidata
        private static List<Reserva> GetConflitos(Reserva candidata, List<Reserva> reservasExistentes)
        {
            var resultado = new List<Reserva>();

            foreach (var reserva in reservasExistentes)
            {
                bool estaAtiva = reserva.GetStatus() == StatusReserva.Confirmada
                                || reserva.GetStatus() == StatusReserva.Pendente;
                bool temConflito = reserva.ConflitaCom(candidata.GetInicio(), candidata.GetFim());
                bool ehOutra = reserva.GetId() != candidata.GetId();

                if (estaAtiva && temConflito && ehOutra)
                    resultado.Add(reserva);
            }

            return resultado;
        }
    }
}
