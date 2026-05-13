using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva_Salas_Estudo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Reserva de Salas de Estudo ===\n");

            // Limpar reservas anteriores (para teste limpo)
            RepositorioReservas.GetInstance().Limpar();

            // 1. Configuração do Repositório (Singleton)
            var repo = RepositorioReservas.GetInstance();

            // 2. Criação de Salas via Factory (Requisito Factory Method)
            Sala salaIndividual = SalaFactory.CriarSala("individual", "S101", 1);
            Sala salaGrupo = SalaFactory.CriarSala("grupo", "G202", 6);
            Sala salaLab = SalaFactory.CriarSala("laboratorio", "L303", 20);

            // 3. Criação de Usuários (Observadores)
            Estudante aluno1 = new Estudante("João Silva", "joao@univ.pt", "2024001", "estudante");
            Estudante aluno2 = new Estudante("Maria Souza", "maria@univ.pt", "2024002", "estudante");
            Professor prof1 = new Professor("Dr. Newton", "newton@univ.pt", "998877", "professor");

            // 4. Teste da Política FIFO (First In, First Out)
            Console.WriteLine("--- Teste 1: Política FIFO ---");
            var servicoFIFO = new ServicoDeReserva(new PoliticaFIFO());

            DateTime hoje = DateTime.Now;
            Reserva res1 = new Reserva(aluno1, salaGrupo, hoje.AddHours(1), hoje.AddHours(2));
            servicoFIFO.RealizarReserva(res1);

            // Tentativa de colisão no mesmo horário (deve ser negada)
            Reserva res2 = new Reserva(aluno2, salaGrupo, hoje.AddHours(1), hoje.AddHours(2));
            servicoFIFO.RealizarReserva(res2);

             res2 = new Reserva(aluno2, salaGrupo, hoje.AddHours(3), hoje.AddHours(4));
            servicoFIFO.RealizarReserva(res2);

            Console.WriteLine("\n--- Teste 2: Política Prioridade Docente ---");
            var servicoPrioridade = new ServicoDeReserva(new PoliticaPrioridadeDocente());

            // Professor tenta reservar no mesmo horário do Aluno 1
            Reserva resProf = new Reserva(prof1, salaGrupo, hoje.AddHours(1), hoje.AddHours(2));
            servicoPrioridade.RealizarReserva(resProf);

            Console.WriteLine("\n--- Teste 3: Relatório Diário (RF-05) ---");
            GerarRelatorioDiario();

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        static void GerarRelatorioDiario()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine($"RELATÓRIO DE RESERVAS - {DateTime.Now:dd/MM/yyyy}");
            Console.WriteLine("==========================================");

            var todas = RepositorioReservas.GetInstance().ListarTodas();
            bool encontrou = false;

            foreach (var r in todas)
            {
                if (r.GetStatus() == StatusReserva.Confirmada)
                {
                    Console.WriteLine(r.ToString());
                    encontrou = true;
                }
            }

            if (!encontrou) Console.WriteLine("Nenhuma reserva confirmada para hoje.");
            Console.WriteLine("==========================================\n");
        }
    }
}
