using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    public interface IReservaObserver
    {
        void Update(string mensagem);
    }
}