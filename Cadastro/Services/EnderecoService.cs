using Cadastro.Data;
using Cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cadastro.Services
{
    public class EnderecoService
    {
        private readonly CadastroContext _context;

        public EnderecoService(CadastroContext context)
        {
            _context = context;
        }

        public async Task<Endereco> PostEndereco(Endereco endereco)
        {
            try
            {
                _context.Endereco.Add(endereco);
                await _context.SaveChangesAsync();
                return endereco;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
       
        }

        public async Task<Endereco> PutEndereco(Endereco endereco)
        {
            bool hasAny = await _context.Endereco.AnyAsync(x => x.Id == endereco.Id);
            if (!hasAny)
            {
                throw new Exception("Id NotFound");
            }
            try
            {
                _context.Entry(endereco).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return endereco;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task Delete(Endereco endereco)
        {
            try
            {
                _context.Endereco.Remove(endereco);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
