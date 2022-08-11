using Cadastro.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Models
{
    public class Telefone
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Required")]
        [Phone(ErrorMessage = "Telefone precisa ser válido")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "{0} Required")]
        public StatusCadastro Status { get; set; }
        public int PessoaId { get; set; }

        public Telefone()
        {
        }

        public Telefone(int id, string descricao, StatusCadastro status)
        {
            Id = id;
            Descricao = descricao;
            Status = status;
        }
    }
}
