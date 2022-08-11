using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Models
{
    public class PessoaJuridica : Pessoa
    {

        public string Razao { get; set; }
        public string Cnpj { get; set; }

        public PessoaJuridica()
        {
        }

        public PessoaJuridica(int id, string email, string cep, string logradouro, string bairro, Cidade cidade, string razao, string cnpj)
            : base(id, email, cep, logradouro, bairro, cidade)
        {
            Razao = razao;
            Cnpj = cnpj;
        }
    }
}
