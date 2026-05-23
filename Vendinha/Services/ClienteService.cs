using System;
using System.Collections.Generic;
using System.Text;
using Vendinha.Models;

namespace Vendinha.Services
{
    public class ClienteService
    {
        private List<Cliente> lista = new List<Cliente>();

        public bool Criar(Cliente cliente)
        {
            var existe = lista.Any(item =>
            {
                return item.Cpf == cliente.Cpf;
            });

            if (existe)
            {
                return false;
            }

            lista.Add(cliente);
            return true;
        }

        public List<Cliente> Listar()
        {
            return lista.ToList();
        }

        public Cliente BuscarPorCpf(string cpf)
        {
            var cliente = lista.FirstOrDefault(item =>
            {
                return item.Cpf == cpf;
            });

            return cliente;
        }

        public bool Atualizar(Cliente cliente)
        {
            var clienteExistente = BuscarPorCpf(cliente.Cpf);

            if (clienteExistente == null)
            {
                return false;
            }

            clienteExistente.Nome = cliente.Nome;
            clienteExistente.DataNascimento = cliente.DataNascimento;
            clienteExistente.Email = cliente.Email;

            return true;
        }

        public bool Remover(string cpf)
        {
        var cliente = BuscarPorCpf(cpf);

        if (cliente == null)
            {
                            return false;
            }

        lista.Remove(cliente);
        return true;
        }

    }
}
