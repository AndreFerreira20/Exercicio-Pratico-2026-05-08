using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    public abstract class Sala
    {
        private string codigo;
        private int capacidade;
        private List<string> recursos;

        public abstract bool Disponivel(int inicio, int fim);

        public void SetCodigo(string codigo) { this.codigo = codigo; }
        public void SetCapacidade(int capacidade) { this.capacidade = capacidade; }
        public void SetRecursos(List<string> recursos) { this.recursos = recursos; }
    }
}
