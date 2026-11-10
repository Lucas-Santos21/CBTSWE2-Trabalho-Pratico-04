//Nome e prontuário da dupla:
//Lucas da Silva Santos CB3030598
//Kaueh Farias Ferreira dos Santos CB3031438

using TP04.Domain.Enums;
using TP04.Domain.Models;
using TP04.Domain.Services;

namespace TP04.Tests.Domain
{
    public class TarefaServiceTests
    {
        private readonly TarefaService _servico;

        public TarefaServiceTests()
        {
            _servico = new TarefaService();
        }

        [Fact]
        public void EstaConcluida_DeveRetornarVerdadeiro_QuandoTarefaEstiverConcluida()
        {
            Tarefa tarefa = new Tarefa
            {
                Titulo = "Estudar C#",
                Status = StatusTarefa.Concluida
            };

            bool resultado = _servico.EstaConcluida(tarefa);

            Assert.True(resultado);
        }

        [Fact]
        public void EstaConcluida_DeveRetornarFalso_QuandoTarefaNaoEstiverConcluida()
        {
            Tarefa tarefa = new Tarefa
            {
                Titulo = "Estudar C#",
                Status = StatusTarefa.Pendente
            };

            bool resultado = _servico.EstaConcluida(tarefa);

            Assert.False(resultado);
        }

        [Fact]
        public void EstaAtrasada_DeveRetornarVerdadeiro_QuandoPrazoEstiverVencido()
        {
            Tarefa tarefa = new Tarefa
            {
                Titulo = "Entregar trabalho",
                Status = StatusTarefa.Pendente,
                DataVencimento = new DateTime(2026, 11, 9)
            };

            DateTime dataReferencia = new DateTime(2026, 11, 10);

            bool resultado = _servico.EstaAtrasada(tarefa, dataReferencia);

            Assert.True(resultado);
        }

        [Fact]
        public void EstaAtrasada_DeveRetornarFalso_QuandoTarefaEstiverConcluida()
        {
            Tarefa tarefa = new Tarefa
            {
                Titulo = "Entregar trabalho",
                Status = StatusTarefa.Concluida,
                DataVencimento = new DateTime(2026, 11, 9)
            };

            DateTime dataReferencia = new DateTime(2026, 11, 10);

            bool resultado = _servico.EstaAtrasada(tarefa, dataReferencia);

            Assert.False(resultado);
        }

        [Fact]
        public void ContarPendentes_DeveRetornarQuantidadeCorreta()
        {
            List<Tarefa> tarefas = new List<Tarefa>
            {
                new Tarefa { Status = StatusTarefa.Pendente },
                new Tarefa { Status = StatusTarefa.Pendente },
                new Tarefa { Status = StatusTarefa.Concluida }
            };

            int resultado = _servico.ContarPendentes(tarefas);

            Assert.Equal(2, resultado);
        }

        [Fact]
        public void ContarEmAndamento_DeveRetornarQuantidadeCorreta()
        {
            List<Tarefa> tarefas = new List<Tarefa>
            {
                new Tarefa { Status = StatusTarefa.EmAndamento },
                new Tarefa { Status = StatusTarefa.Pendente },
                new Tarefa { Status = StatusTarefa.EmAndamento }
            };

            int resultado = _servico.ContarEmAndamento(tarefas);

            Assert.Equal(2, resultado);
        }

        [Fact]
        public void ContarConcluidas_DeveRetornarQuantidadeCorreta()
        {
            List<Tarefa> tarefas = new List<Tarefa>
            {
                new Tarefa { Status = StatusTarefa.Concluida },
                new Tarefa { Status = StatusTarefa.Pendente },
                new Tarefa { Status = StatusTarefa.Concluida }
            };

            int resultado = _servico.ContarConcluidas(tarefas);

            Assert.Equal(2, resultado);
        }
    }
}