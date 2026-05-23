using System;
using System.Collections.Generic;
using System.Text;
using Vendinha.Models;

namespace Vendinha.Services
{
    public class DividaSerivce
    { 
        private List<Divida> lista = new List<Divida>();

        public bool Criar(Divida divida)
        {
            lista.Add(divida);
            return true;
        }

        public List<Divida> Listar() 
        {
            return lista.ToList();
        }

        public Divida BuscarPorId(int id) 
        {
            var divida = lista.FirstOrDefault(item => {  return item.Id == id; });
            return divida;
        }

        public bool Atualizar(Divida divida) 
        
        { 
            var dividaExistente = BuscarPorId(divida.Id);

            if (dividaExistente == null) 
            {
                return false;
            }

            dividaExistente.Valor = divida.Valor;
            dividaExistente.Situacao = divida.Situacao;
            dividaExistente.DataPagamento = divida.DataPagamento;

            return true;
        }

        public bool Remover(int id) 
        {
            var dividaExistente = BuscarPorId(id);

            if (dividaExistente == null) 
            {
                return false;
            }

            lista.Remove(dividaExistente);
            return true;
        }
    }

}
