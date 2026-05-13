using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    // Sala para estudo individual. 
    public class SalaIndividual : Sala
    {

        public SalaIndividual(string codigo, int capacidade)
        {
            SetCodigo(codigo);
            SetCapacidade(1);
            
        }

        public override bool Disponivel(DateTime inicio, DateTime fim)
        {
            var repo = RepositorioReservas.GetInstance();
            foreach (var reserva in repo.ListarTodas())
            {
                if (reserva.GetSala().GetCodigo() == this.GetCodigo() &&
                    reserva.GetStatus() == StatusReserva.Confirmada &&
                    reserva.ConflitaCom(inicio, fim))
                {
                    return false;
                }
            }
            return true;
        }
    }

    // Sala para estudo com computadores.
    public class Laboratorio : Sala
    {
        private int computadores;
        private List<string> softwares;

        public Laboratorio(string codigo, int capacidade)
        {
            SetCodigo(codigo);
            SetCapacidade(capacidade);
            
        }

        public override bool Disponivel(DateTime inicio, DateTime fim)
        {
            var repo = RepositorioReservas.GetInstance();
            foreach (var reserva in repo.ListarTodas())
            {
                if (reserva.GetSala().GetCodigo() == this.GetCodigo() &&
                    reserva.GetStatus() == StatusReserva.Confirmada &&
                    reserva.ConflitaCom(inicio, fim))
                {
                    return false;
                }
            }
            return true;
        }
    }

    // Sala para estudos em grupo.
    public class SalaGrupo : Sala
    {
        private bool quadro;
        private int maxGrupos;

        public SalaGrupo(string codigo, int capacidade)
        {
            SetCodigo(codigo);
            SetCapacidade(capacidade);
             
        }

        public override bool Disponivel(DateTime inicio, DateTime fim)
        {
            var repo = RepositorioReservas.GetInstance();
            foreach (var reserva in repo.ListarTodas())
            {
                if (reserva.GetSala().GetCodigo() == this.GetCodigo() &&
                    reserva.GetStatus() == StatusReserva.Confirmada &&
                    reserva.ConflitaCom(inicio, fim))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
