using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    internal interface IPoliticaReserva
    {
        string Nome { get; }

        bool Validar(Reserva candidata, List<Reserva> reservasExistentes);
        List<Reserva> ObterReservasParaCancelar(Reserva candidata, List<Reserva> reservasExistentes);

    }
}
