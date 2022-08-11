using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cadastro.Data;
using Cadastro.Models;
using Cadastro.Services;

namespace Cadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaFisicasController : ControllerBase
    {
        private readonly CadastroContext _context;
        private readonly PessoaFisicaService _pessoaFisica;
        private readonly CidadeService _cidade;
        private readonly TelefoneService _telefone;

        public PessoaFisicasController(CadastroContext context, PessoaFisicaService pessoaFisica, CidadeService cidade, TelefoneService telefone)
        {
            _context = context;
            _pessoaFisica = pessoaFisica;
            _cidade = cidade;
            _telefone = telefone;

        }

        // GET: api/PessoaFisicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaFisica>>> GetPessoaFisica()
        {
            return await _pessoaFisica.FindAllAsync();
        }

        // GET: api/PessoaFisicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaFisica>> GetPessoaFisica(int id)
        {
            var pessoaFisica = await _pessoaFisica.FindByIdAsync(id);

            if (pessoaFisica == null)
            {
                return NotFound();
            }

            return pessoaFisica;
        }

        // POST: api/PessoaFisicas
        [HttpPost]
        public async Task<ActionResult<PessoaFisica>> PostPessoaFisica(PessoaFisica pessoaFisica)
        {
            //Save Cidades
            await _cidade.PostCidade(pessoaFisica.Cidade);
            pessoaFisica.CidadeId = pessoaFisica.Cidade.Id;

            //Save Pessoa
            await _pessoaFisica.Post(pessoaFisica);

            //Save Telefones
            foreach (var telefone in pessoaFisica.Telefones)
            {
                telefone.PessoaId = pessoaFisica.Id;
                await _telefone.PostTelefone(telefone);
            }

            return CreatedAtAction("GetPessoaFisica", new { id = pessoaFisica.Id }, pessoaFisica);
        }

        // PUT: api/PessoaFisicas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoaFisica(int id, PessoaFisica pessoaFisica)
        {
            if (id != pessoaFisica.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoaFisica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaFisicaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // DELETE: api/PessoaFisicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoaFisica(int id)
        {
            var pessoaFisica = await _context.PessoaFisica.FindAsync(id);
            if (pessoaFisica == null)
            {
                return NotFound();
            }

            _context.PessoaFisica.Remove(pessoaFisica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaFisicaExists(int id)
        {
            return _context.PessoaFisica.Any(e => e.Id == id);
        }
    }
}
