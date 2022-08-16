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

        public bool ValidaCpf(string cnpj)
        {
			int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int soma;
			int resto;
			string digito;
			string tempCnpj;
			cnpj = cnpj.Trim();
			cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
			if (cnpj.Length != 14)
				return false;
			tempCnpj = cnpj.Substring(0, 12);
			soma = 0;
			for (int i = 0; i < 12; i++)
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString();
			tempCnpj = tempCnpj + digito;
			soma = 0;
			for (int i = 0; i < 13; i++)
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString();
			return cnpj.EndsWith(digito);
		}
    }
}
