using Cadastro.Data;
using Cadastro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Services
{
    public class PessoaJuridicaService
    {
        private readonly CadastroContext _context;

        public PessoaJuridicaService(CadastroContext context)
        {
            _context = context;
        }
        public async Task<List<PessoaJuridica>> GetAllAsync()
        {
            try
            {
                return await _context.PessoaJuridica
                    .Include(x => x.Endereco)
                    .Include(x => x.Telefones)
                    .ToListAsync(); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<PessoaJuridica> GetByIdAsync(int id)
        {
            try
            {
                return await _context.PessoaJuridica
                    .Include(x => x.Endereco)
                    .Include(x => x.Telefones)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<PessoaJuridica> Post(PessoaJuridica pessoaJuridica)
        {
            try
            {
                _context.PessoaJuridica.Add(pessoaJuridica);
                await _context.SaveChangesAsync();
                return pessoaJuridica;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<PessoaJuridica> Put(PessoaJuridica pessoaJuridica)
        {
            bool hasAny = await _context.PessoaJuridica.AnyAsync(x => x.Id == pessoaJuridica.Id);
            if (!hasAny)
            {
                throw new Exception("Id NotFound");
            }
            try
            {
                _context.Entry(pessoaJuridica).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return pessoaJuridica;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                var pessoaJuridica = await _context.PessoaJuridica
                    .Include(x => x.Endereco)
                    .Include(x => x.Telefones)
                    .FirstOrDefaultAsync(x => x.Id == id);
                _context.PessoaJuridica.Remove(pessoaJuridica);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
