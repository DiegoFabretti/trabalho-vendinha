using System;
using System.Collections.Generic;
using System.Text;
using Vendinha.Data;
using Vendinha.Models;
using System.ComponentModel.DataAnnotations;

namespace Vendinha.Services
{
    public class ClienteService
    {
        private VendinhaDbContext contexto = new VendinhaDbContext();

        public bool Validar(Cliente cliente, out List<ValidationResult> listaErros)
        {
            var contexto = new ValidationContext(cliente);

            listaErros = new List<ValidationResult>();

            var valido = Validator.TryValidateObject(
                cliente,
                contexto,
                listaErros,
                true);

            return valido;
        }

        public bool Criar(
    Cliente cliente,
    out List<ValidationResult> listaErros)
        {
            if (!Validar(cliente, out listaErros))
            {
                return false;
            }

            var existe = contexto.Clientes
                .Any(item => item.Cpf == cliente.Cpf);

            if (existe)
            {
                listaErros.Add(
                    new ValidationResult(
                        "Já existe um cliente com esse CPF."));

                return false;
            }

            contexto.Clientes.Add(cliente);
            contexto.SaveChanges();

            return true;
        }

        public List<Cliente> Listar()
        {
            return contexto.Clientes.ToList();
        }

        public Cliente BuscarPorCpf(string cpf)
        {
            return contexto.Clientes
                .FirstOrDefault(item => item.Cpf == cpf);
        }

        public List<Cliente> BuscarPorNome(string nome)
        {
            return contexto.Clientes
                .Where(item => item.Nome.Contains(nome))
                .ToList();
        }

        public bool Atualizar(
            Cliente cliente,
            out List<ValidationResult> listaErros)
        {
            if (!Validar(cliente, out listaErros))
            {
                return false;
            }

            var clienteExistente = BuscarPorCpf(cliente.Cpf);

            if (clienteExistente == null)
            {
                listaErros.Add(
                    new ValidationResult(
                        "Cliente não encontrado."));

                return false;
            }

            clienteExistente.Nome = cliente.Nome;
            clienteExistente.DataNascimento = cliente.DataNascimento;
            clienteExistente.Email = cliente.Email;

            contexto.SaveChanges();

            return true;
        }

        public bool Remover(
            string cpf,
            out List<ValidationResult> listaErros)
        {
            listaErros = new List<ValidationResult>();

            var cliente = BuscarPorCpf(cpf);

            if (cliente == null)
            {
                listaErros.Add(
                    new ValidationResult(
                        "Cliente não encontrado."));

                return false;
            }

            contexto.Clientes.Remove(cliente);
            contexto.SaveChanges();

            return true;
        }
    }
}
