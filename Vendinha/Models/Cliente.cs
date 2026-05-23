using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vendinha.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength =8)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O cpf é obrigatório.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        [EmailAddress(ErrorMessage = "O email é inválido.")]
        public string Email { get; set; }

        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;

                if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
                return idade;
            }
        }
    }
}
