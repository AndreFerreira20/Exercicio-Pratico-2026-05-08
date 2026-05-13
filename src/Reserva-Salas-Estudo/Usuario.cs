using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    public abstract class Usuario : IReservaObserver
    {
        private Guid id = Guid.NewGuid();
        private string nome;
        private string email;
        private string tipoUsuario;

        protected Usuario(string nome, string email, string tipo)
        {
            this.nome = nome;
            this.email = email;
            this.tipoUsuario = tipo;
        }

        public void Update(string mensagem)
        {
            Console.WriteLine($"[Notificação para {GetNome()}]: {mensagem}");
        }


        // Sets e Gets
        public void SetNome(string nome) { this.nome = nome; }
        public void SetEmail(string email) { this.email = email; }
        public void SetTipoUsuario(string tipo) { this.tipoUsuario = tipo; }

        public string GetNome() { return nome; }
        public string GetEmail() { return email; }
        public string GetTipoUsuario() { return tipoUsuario; }
    }
}
