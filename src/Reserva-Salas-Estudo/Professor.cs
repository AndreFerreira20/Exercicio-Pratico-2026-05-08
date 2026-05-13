using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    public class Professor : Usuario
    {
        private string siape;

        public Professor(string nome, string email, string siape, string tipo) : base(nome, email, tipo)
        {
            this.siape = siape;
        }

        public string GetSiape() { return siape; }
    }
}