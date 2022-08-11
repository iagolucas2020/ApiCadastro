using Cadastro.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public Cidade Cidade { get; set; }
        public int CidadeId { get; set; }
        public List<Telefone> Telefones { get; set; } = new List<Telefone>();

        public Pessoa()
        {
        }

        public Pessoa(int id, string email, string cep, string logradouro, string bairro, Cidade cidade)
        {
            Id = id;
            Email = email;
            Cep = cep;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
        }

        public void AddTelefone(Telefone telefone)
        {
            Telefones.Add(telefone);
        }
    }
}
