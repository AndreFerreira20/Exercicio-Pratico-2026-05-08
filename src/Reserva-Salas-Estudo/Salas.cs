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
        private bool silenciosa;
        private int mesas;

        public SalaIndividual(string codigo, int capacidade, List<string> recursos, bool silenciosa, int mesas)
        {
            SetCodigo(codigo);
            SetCapacidade(capacidade);
            SetRecursos(recursos);

            this.silenciosa = silenciosa;
            this.mesas = mesas;
        }

        public override bool Disponivel(int inicio, int fim)
        {
            //    return true;
        }
    }

    // Sala para estudo com computadores.
    public class Laboratorio : Sala
    {
        private int computadores;
        private List<string> softwares;

        public Laboratorio(string codigo, int capacidade, List<string> recursos, int computadores, List<string> softwares)
        {
            SetCodigo(codigo);
            SetCapacidade(capacidade);
            SetRecursos(recursos);

            this.computadores = computadores;
            this.softwares = softwares;
        }

        public override bool Disponivel(int inicio, int fim)
        {
            //    return true;
        }
    }

    // Sala para estudos em grupo.
    internal class SalaGrupo : Sala
    {
        private bool quadro;
        private int maxGrupos;

        public SalaGrupo(string codigo, int capacidade, List<string> recursos, bool quadro, int maxGrupos)
        {
            SetCodigo(codigo);
            SetCapacidade(capacidade);
            SetRecursos(recursos);

            this.quadro = quadro;
            this.maxGrupos = maxGrupos;
        }

        public override bool Disponivel(int inicio, int fim)
        {
            //    return true;
        }
    }
}
