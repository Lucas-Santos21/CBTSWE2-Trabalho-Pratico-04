//Nome e prontuário da dupla:
//Lucas da Silva Santos CB3030598
//Kaueh Farias Ferreira dos Santos CB3031438

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using TP04.Domain.Enums;


namespace TP04.Domain.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O título deve ter entre 3 e 100 caracteres.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500,
            ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Data de criação")]
        [DataType(DataType.DateTime)]
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        [Display(Name = "Data de vencimento")]
        [DataType(DataType.DateTime)]
        public DateTime? DataVencimento { get; set; }

        [EnumDataType(typeof(StatusTarefa),
            ErrorMessage = "O status informado é inválido.")]
        [Display(Name = "Status")]
        public StatusTarefa Status { get; set; }

        [EnumDataType(typeof(PrioridadeTarefa),
            ErrorMessage = "A prioridade informada é inválida.")]
        [Display(Name = "Prioridade")]
        public PrioridadeTarefa Prioridade { get; set; }
    }
}