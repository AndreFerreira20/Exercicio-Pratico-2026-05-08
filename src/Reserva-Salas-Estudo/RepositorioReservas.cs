using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    internal class RepositorioReservas
    {
        private static RepositorioReservas instance;
        private static readonly object _padlock = new object();
        private List<Reserva> _reservas = new List<Reserva>();

        public static RepositorioReservas GetInstance()
        {
            lock (_padlock) // Garante thread-safety conforme o PDF
            {
                if (instance == null) instance = new RepositorioReservas();
                return instance;
            }
        }

        private RepositorioReservas() { }

        public void Limpar() => _reservas.Clear();
        public void Adicionar(Reserva r) => _reservas.Add(r);
        public List<Reserva> ListarTodas() => _reservas;
    }
}
