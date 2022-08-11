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
        private readonly EnderecoService _endereco;
        private readonly TelefoneService _telefone;

        public PessoaFisicasController(CadastroContext context, PessoaFisicaService pessoaFisica, EnderecoService endereco, TelefoneService telefone)
        {
            _context = context;
            _pessoaFisica = pessoaFisica;
            _endereco = endereco;
            _telefone = telefone;

        }

        // GET: api/PessoaFisicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaFisica>>> GetPessoaFisica()
        {
            try
            {
                var dados = await _pessoaFisica.FindAllAsync();
                if (dados.Count == 0) return NotFound("Dados não encontrados");
                return Ok(dados);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        // GET: api/PessoaFisicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaFisica>> GetPessoaFisica(int id)
        {
            try
            {
                var pessoaFisica = await _pessoaFisica.FindByIdAsync(id);
                if (pessoaFisica == null)
                {
                    return NotFound("Pessoa não existe!");
                }
                return pessoaFisica;
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }

        }

        // POST: api/PessoaFisicas
        [HttpPost]
        public async Task<ActionResult<PessoaFisica>> PostPessoaFisica(PessoaFisica pessoaFisica)
        {
            try
            {
                PessoaFisica pessoa = new PessoaFisica();

                //Validar Cpf
                bool cpf = pessoa.ValidaCpf(pessoaFisica.Cpf);
                if (!cpf) return BadRequest("Cpf Inválido");

                //Consultar endereço Via CEP
                await pessoa.ConsultarEndereco(pessoaFisica);

                //Save Endereco
                await _endereco.PostEndereco(pessoaFisica.Endereco);
                pessoaFisica.EnderecoId = pessoaFisica.Endereco.Id;

                //Save Pessoa
                await _pessoaFisica.Post(pessoaFisica);

                return CreatedAtAction("GetPessoaFisica", new { id = pessoaFisica.Id }, pessoaFisica);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}" );
            }

        }

        // PUT: api/PessoaFisicas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoaFisica(int id, PessoaFisica pessoaFisica)
        {
            if (id != pessoaFisica.Id)
            {
                return BadRequest();
            }

            try
            {
                //Consultar endereço Via CEP
                if(pessoaFisica.Endereco.Cep != null) await pessoaFisica.ConsultarEndereco(pessoaFisica);

                //Update Endereco
                await _endereco.PutEndereco(pessoaFisica.Endereco);

                //UpDateTelefone
                await _telefone.PutTelefone(pessoaFisica.Telefones);

                //UpDatePessoa
                await _pessoaFisica.Put(pessoaFisica);
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
