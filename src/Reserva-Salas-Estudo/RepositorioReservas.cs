using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    public class RepositorioReservas
    {
        private static RepositorioReservas instance;
        //private static List<Reserva> a;

        public static RepositorioReservas GetInstance()
        {
            if (instance == null)
            {
                instance = new RepositorioReservas();
            }

            return instance;
        }

        private RepositorioReservas() { }
    }
}
