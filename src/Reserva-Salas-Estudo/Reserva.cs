using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    public enum StatusReserva
    {
        Pendente,
        Confirmada,
        Cancelada,
        Modificada
    }
    enum TipoEvento { ReservaCriada, ReservaModificada, ReservaCancelada, ReservaConfirmada }
    internal class Reserva
    {
        private Guid _id;
        private DateTime _inicio;
        private DateTime _fim;
        private Usuario _usuario;
        private Sala _sala;
        private StatusReserva _status;

        public Reserva(Usuario usuario, Sala sala, DateTime inicio, DateTime fim)
        {
            if (fim <= inicio)
            {
                Console.WriteLine("  [Erro] O horário de fim deve ser depois do início.");
                throw new ArgumentException("Fim deve ser depois do inicio.");
            }

            _id = Guid.NewGuid();
            _usuario = usuario;
            _sala = sala;
            _inicio = inicio;
            _fim = fim;
            _status = StatusReserva.Pendente;
        }

        public Guid GetId() { return _id; }
        public DateTime GetInicio() { return _inicio; }
        public DateTime GetFim() { return _fim; }
        public Usuario GetUsuario() { return _usuario; }
        public Sala GetSala() { return _sala; }
        public StatusReserva GetStatus() { return _status; }

        // Muda o status para Confirmada
        public void Confirmar()
        {
            if (_status == StatusReserva.Cancelada)
            {
                Console.WriteLine("  [Erro] Não é possível confirmar uma reserva cancelada.");
                return;
            }
            _status = StatusReserva.Confirmada;
        }

        // Muda o status para Cancelada
        public void Cancelar()
        {
            if (_status == StatusReserva.Cancelada)
            {
                Console.WriteLine("  [Erro] Esta reserva já está cancelada.");
                return;
            }
            _status = StatusReserva.Cancelada;
        }

        // Atualiza o horário e muda o status para Modificada
        public void Modificar(DateTime novoInicio, DateTime novoFim)
        {
            if (_status == StatusReserva.Cancelada)
            {
                Console.WriteLine("  [Erro] Não é possível modificar uma reserva cancelada.");
                return;
            }
            if (novoFim <= novoInicio)
            {
                Console.WriteLine("  [Erro] O horário de fim deve ser depois do início.");
                return;
            }

            _inicio = novoInicio;
            _fim = novoFim;
            _status = StatusReserva.Modificada;
        }

        // Verifica se esta reserva bate no horário informado (início e fim).
        // Duas reservas conflitam quando os intervalos se sobrepõem.
        public bool ConflitaCom(DateTime inicio, DateTime fim)
        {
            bool comecaAntesDeFimOutra = _inicio < fim;
            bool terminaDepoisDeInicioOutra = _fim > inicio;

            return comecaAntesDeFimOutra && terminaDepoisDeInicioOutra;
        }

        public override string ToString()
        {
            string idCurto = _id.ToString().Substring(0, 8);
            return "Reserva " + idCurto + "... | "
                + _sala.GetCodigo() + " | "
                + _inicio.ToString("dd/MM HH:mm") + "-" + _fim.ToString("HH:mm") + " | "
                + _usuario.GetNome() + " | "
                + _status;
        }
    }

    // Carrega as informações de um evento para enviar aos observadores (push)
    class EventoReserva
    {
        private TipoEvento _tipo;
        private Reserva _reserva;
        private DateTime _ocorridoEm;

        public EventoReserva(TipoEvento tipo, Reserva reserva)
        {
            _tipo = tipo;
            _reserva = reserva;
            _ocorridoEm = DateTime.Now;
        }

        public TipoEvento GetTipo() { return _tipo; }
        public Reserva GetReserva() { return _reserva; }
        public DateTime GetOcorridoEm() { return _ocorridoEm; }

        public override string ToString()
        {
            return "[" + _ocorridoEm.ToString("HH:mm:ss") + "] " + _tipo + " → " + _reserva;
        }
    }
}
