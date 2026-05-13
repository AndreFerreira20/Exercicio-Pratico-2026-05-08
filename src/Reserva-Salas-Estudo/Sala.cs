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

        public abstract bool Disponivel(DateTime inicio, DateTime fim);

        public void SetCodigo(string codigo) { this.codigo = codigo; }
        public string GetCodigo() { return this.codigo; }
        public void SetCapacidade(int capacidade) { this.capacidade = capacidade; }
        public int GetCapacidade() { return this.capacidade; }
    }
}