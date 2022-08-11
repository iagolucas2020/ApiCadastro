using Cadastro.Data;
using Cadastro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cadastro.Services
{
    public class PessoaFisicaService
    {
        private readonly CadastroContext _context;

        public PessoaFisicaService(CadastroContext context)
        {
            _context = context;
        }
        public async Task<List<PessoaFisica>> FindAllAsync()
        {
            return await _context.PessoaFisica.ToListAsync();
        }

        public async Task<PessoaFisica> FindByIdAsync(int id)
        {
            return await _context.PessoaFisica.FindAsync(id);
        }

        public async Task<PessoaFisica> Post(PessoaFisica pessoa)
        {
            _context.PessoaFisica.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

    }
}
