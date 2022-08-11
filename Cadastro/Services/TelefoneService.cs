using Cadastro.Data;
using Cadastro.Models;
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

        public async Task<Telefone> PostTelefone(Telefone telefone)
        {
            _context.Telefone.Add(telefone);
            await _context.SaveChangesAsync();
            return telefone;
        }
    }
}
