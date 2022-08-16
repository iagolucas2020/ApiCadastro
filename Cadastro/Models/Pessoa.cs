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

        public async Task ConsultarEndereco(Endereco endereco)
        {
            string cep = endereco.Cep.Replace(".", "").Replace("-", "");
            string link = $"https://viacep.com.br/ws/{cep}/json/";
            var result = await link.GetJsonAsync<PessoaViewModels>();
            endereco.Logradouro = result.logradouro;
            endereco.Bairro = result.bairro;
            endereco.Cidade = result.localidade;
            endereco.UF = result.uf;
        }

    }
}
