using Cadastro.Models.Enums;
using Cadastro.Models.ViewModels;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Required")]
        [EmailAddress(ErrorMessage = "Email precisa ser válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }
        public List<Telefone> Telefones { get; set; } = new List<Telefone>();

        public Pessoa()
        {
        }

        public Pessoa(int id, string email, Endereco endereco)
        {
            Id = id;
            Email = email;
            Endereco = endereco;

        }

        public void AddTelefone(Telefone telefone)
        {
            Telefones.Add(telefone);
        }

        public async Task ConsultarEndereco(PessoaFisica pessoa)
        {
            string cep = pessoa.Endereco.Cep.Replace(".", "").Replace("-", "");
            string link = $"https://viacep.com.br/ws/{cep}/json/";
            var result = await link.GetJsonAsync<PessoaViewModels>();
            pessoa.Endereco.Logradouro = result.logradouro;
            pessoa.Endereco.Bairro = result.bairro;
            pessoa.Endereco.Cidade = result.localidade;
            pessoa.Endereco.UF = result.uf;
        }

    }
}
