using Cadastro.Data;
using Cadastro.Models;
using Microsoft.AspNetCore.Mvc;
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
            return await _context.PessoaFisica
                .Include(x => x.Endereco)
                .Include(x => x.Telefones)
                .ToListAsync(); ;
        }

        public async Task<PessoaFisica> FindByIdAsync(int id)
        {
            return await _context.PessoaFisica
                .Include(x => x.Endereco)
                .Include(x => x.Telefones)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PessoaFisica> Post(PessoaFisica pessoaFisica)
        {
            _context.PessoaFisica.Add(pessoaFisica);
            await _context.SaveChangesAsync();
            return pessoaFisica;
        }

        public async Task<PessoaFisica> Put(PessoaFisica pessoaFisica)
        {
            _context.Entry(pessoaFisica).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return pessoaFisica;
        }

    }
}
