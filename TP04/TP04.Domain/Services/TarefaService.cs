//Nome e prontuário da dupla:
//Lucas da Silva Santos CB3030598
//Kaueh Farias Ferreira dos Santos CB3031438

using System;
using System.Collections.Generic;
using System.Text;
using TP04.Domain.Enums;
using TP04.Domain.Models;

namespace TP04.Domain.Services
{
    public class TarefaService
    {
        public bool EstaConcluida(Tarefa tarefa)
        {
            return tarefa.Status == StatusTarefa.Concluida;
        }

        public bool EstaAtrasada(Tarefa tarefa, DateTime dataReferencia)
        {
            return tarefa.Status != StatusTarefa.Concluida
                && tarefa.DataVencimento.HasValue
                && tarefa.DataVencimento.Value < dataReferencia;
        }

        public int ContarPendentes(IEnumerable<Tarefa> tarefas)
        {
            return tarefas.Count(tarefa =>
                tarefa.Status == StatusTarefa.Pendente);
        }

        public int ContarEmAndamento(IEnumerable<Tarefa> tarefas)
        {
            return tarefas.Count(tarefa =>
                tarefa.Status == StatusTarefa.EmAndamento);
        }

        public int ContarConcluidas(IEnumerable<Tarefa> tarefas)
        {
            return tarefas.Count(tarefa =>
                tarefa.Status == StatusTarefa.Concluida);
        }

        public int ContarAtrasadas(
            IEnumerable<Tarefa> tarefas,
            DateTime dataReferencia)
        {
            return tarefas.Count(tarefa =>
                EstaAtrasada(tarefa, dataReferencia));
        }
    }
}
