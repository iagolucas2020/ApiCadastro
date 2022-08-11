using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Models
{
    public class PessoaJuridica : Pessoa
    {
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(100, ErrorMessage = "{0} Deve conter no máximo {1}")]
        public string Razao { get; set; }

        [Required(ErrorMessage = "{0} Required")]
        public string Cnpj { get; set; }

        public PessoaJuridica()
        {
        }

        public PessoaJuridica(int id, string email, Endereco endereco, string razao, string cnpj)
            : base(id, email, endereco)
        {
            Razao = razao;
            Cnpj = cnpj;
        }
    }
}
