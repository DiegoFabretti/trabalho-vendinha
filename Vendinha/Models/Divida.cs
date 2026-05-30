using System;
using System.Collections.Generic;
using System.Text;
using Vendinha.Enums;
using System.ComponentModel.DataAnnotations;

namespace Vendinha.Models
{
    public class Divida
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O valor da dívida é obrigatório.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A situação da dívida é obrigatória.")]
        public SituacaoDivida Situacao { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime DataCriacao { get; set; }

        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "O CPF do cliente é obrigatório.")]
        [StringLength(11, MinimumLength = 11,
            ErrorMessage = "O CPF deve conter 11 dígitos.")]
        public string CpfCliente { get; set; }
    }
}
