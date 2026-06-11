using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Vendinha.Data;
using Vendinha.Enums;
using Vendinha.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Vendinha.Services
{
    public class DividaSerivce
    {
        private VendinhaDbContext contexto = new VendinhaDbContext();

        public bool Validar(Divida divida,out List<ValidationResult> listaErros)
        {
            var contextoValidacao = new ValidationContext(divida);

            listaErros = new List<ValidationResult>();

            var valido = Validator.TryValidateObject(
                divida,
                contextoValidacao,
                listaErros,
                true);

            return valido;
        }

        public bool Criar(
            Divida divida,
            out List<ValidationResult> listaErros)
        {
            if (!Validar(divida, out listaErros))
            {
                return false;
            }

            bool possuiDividaAberta = contexto.Dividas.Any(item =>
                item.CpfCliente == divida.CpfCliente &&
                item.Situacao == SituacaoDivida.Aberta);

            if (possuiDividaAberta)
            {
                listaErros.Add(
                    new ValidationResult(
                        "O cliente já possui uma dívida em aberto."));

                return false;
            }

            contexto.Dividas.Add(divida);
            contexto.SaveChanges();

            return true;
        }

        public List<Divida> Listar()
        {
            return contexto.Dividas.ToList();
        }

        public Divida BuscarPorId(int id)
        {
            return contexto.Dividas
                .FirstOrDefault(item => item.Id == id);
        }

        public List<Divida> BuscarPorCpf(string cpf)
        {
            return contexto.Dividas
                .Where(item => item.CpfCliente == cpf)
                .ToList();
        }

        public bool Atualizar(Divida divida, out List<ValidationResult> listaErros)
        {
            if (!Validar(divida, out listaErros))
            {
                return false;
            }

            var dividaExistente = BuscarPorId(divida.Id);

            if (dividaExistente == null)
            {
                listaErros.Add(
                    new ValidationResult(
                        "Dívida não encontrada."));

                return false;
            }

            dividaExistente.Valor = divida.Valor;
            dividaExistente.Situacao = divida.Situacao;
            dividaExistente.DataPagamento = divida.DataPagamento;

            contexto.SaveChanges();

            return true;
        }

        public bool Remover(int id,out List<ValidationResult> listaErros)
        {
            listaErros = new List<ValidationResult>();

            var dividaExistente = BuscarPorId(id);

            if (dividaExistente == null)
            {
                listaErros.Add(
                    new ValidationResult(
                        "Dívida não encontrada."));

                return false;
            }

            contexto.Dividas.Remove(dividaExistente);
            contexto.SaveChanges();

            return true;
        }

        public decimal TotalDividas(string cpf)
        {
            return contexto.Dividas
                .Where(item => item.CpfCliente == cpf)
                .Sum(item => item.Valor);
        }
    }
}