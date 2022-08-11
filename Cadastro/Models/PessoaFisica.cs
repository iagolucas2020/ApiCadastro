using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Models
{
    public class PessoaFisica : Pessoa
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public PessoaFisica()
        {
        }

        public PessoaFisica(int id, string email, string cep, string logradouro, string bairro, Cidade cidade, string nome, string cpf) 
            : base(id, email, cep, logradouro, bairro, cidade)
        {
            Nome = nome;
            Cpf = cpf;
        }
    }
}
