using System;
using System.Collections.Generic;
using System.Text;
using Vendinha.Enums;

namespace Vendinha.Models
{
    public class Divida
    {
        public int Id { get; set; }

        public decimal Valor { get; set; }

        public SituacaoDivida Situacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataPagamento { get; set; }

        public int ClienteId { get; set; }
    }
}
