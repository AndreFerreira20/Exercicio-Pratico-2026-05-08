using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    public class Estudante : Usuario
    {
        private string matricula;

        public Estudante(string nome, string email, string tipo, string matricula) : base(nome, email, tipo)
        {
            this.matricula = matricula;
        }

        public string GetMatricula() { return matricula; }
    }
}
