using Cadastro.Data;
using Cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cadastro.Services
{
    public class CidadeService
    {
        private readonly CadastroContext _context;

        public CidadeService(CadastroContext context)
        {
            _context = context;
        }

        public async Task<Cidade> PostCidade(Cidade cidade)
        {
            _context.Cidade.Add(cidade);
            await _context.SaveChangesAsync();
            return cidade;
        }
    }
}
