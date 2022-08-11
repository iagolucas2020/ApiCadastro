using Cadastro.Data;
using Cadastro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cadastro.Services
{
    public class TelefoneService
    {
        private readonly CadastroContext _context;

        public TelefoneService(CadastroContext context)
        {
            _context = context;
        }

        //public async Task<List<Telefone>> PostTelefone(ICollection<Telefone> telefones)
        //{

        //    _context.Telefone.Add((Telefone)telefones);
        //    await _context.SaveChangesAsync();
        //    return (List<Telefone>)telefones;
        //}

        public async Task<List<Telefone>> PutTelefone(List<Telefone> telefones)
        {
            foreach (var item in telefones)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return telefones;
        }
    }
}
