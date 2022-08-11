using Cadastro.Models.Enums;


namespace Cadastro.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
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
