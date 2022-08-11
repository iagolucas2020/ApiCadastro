using Cadastro.Data;
using Cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            _context.Endereco.Add(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<Endereco> PutEndereco(Endereco endereco)
        {
            _context.Entry(endereco).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return endereco;
        }
    }
}
