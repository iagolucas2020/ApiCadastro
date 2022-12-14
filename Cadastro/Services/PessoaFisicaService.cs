using Cadastro.Data;
using Cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<List<PessoaFisica>> GetAllAsync()
        {
            try
            {
                return await _context.PessoaFisica
                    .Include(x => x.Endereco)
                    .Include(x => x.Telefones)
                    .ToListAsync(); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<PessoaFisica> GetByIdAsync(int id)
        {
            try
            {
                return await _context.PessoaFisica
                    .Include(x => x.Endereco)
                    .Include(x => x.Telefones)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<PessoaFisica> Post(PessoaFisica pessoaFisica)
        {
            try
            {
                _context.PessoaFisica.Add(pessoaFisica);
                await _context.SaveChangesAsync();
                return pessoaFisica;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<PessoaFisica> Put(PessoaFisica pessoaFisica)
        {
            bool hasAny = await _context.PessoaFisica.AnyAsync(x => x.Id == pessoaFisica.Id);
            if (!hasAny)
            {
                throw new Exception("Id NotFound");
            }
            try
            {
                _context.Entry(pessoaFisica).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return pessoaFisica;
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
                var pessoaFisica = await _context.PessoaFisica
                    .Include(x => x.Endereco)
                    .Include(x => x.Telefones)
                    .FirstOrDefaultAsync(x => x.Id == id);
                _context.PessoaFisica.Remove(pessoaFisica);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
