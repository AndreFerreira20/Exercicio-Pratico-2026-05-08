using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    internal class SalaFactory
    {
        public static Sala CriarSala(string tipo,string cod, int cap)
        {
            switch (tipo.ToLower())
            {
                case "laboratorio":
                    return new Laboratorio(cod,cap);
                case "individual":
                    return new SalaIndividual(cod, 1);     
                case "grupo":
                    return new SalaGrupo(cod, cap);
                default:
                    throw new ArgumentException("não existe esse tipo de sala");
                    
            }
        }
    }
}
