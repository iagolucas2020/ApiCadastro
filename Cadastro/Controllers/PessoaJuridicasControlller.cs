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
    public class PessoaJuridicasController : ControllerBase
    {
        private readonly CadastroContext _context;
        private readonly PessoaJuridicaService _pessoaJuridica;
        private readonly EnderecoService _endereco;
        private readonly TelefoneService _telefone;

        public PessoaJuridicasController(CadastroContext context, PessoaJuridicaService pessoaJuridica, EnderecoService endereco, TelefoneService telefone)
        {
            _context = context;
            _pessoaJuridica = pessoaJuridica;
            _endereco = endereco;
            _telefone = telefone;

        }

        // GET: api/PessoaJuridicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaJuridica>>> GetPessoaJuridica()
        {
            try
            {
                var dados = await _pessoaJuridica.GetAllAsync();
                if (dados.Count == 0) return NotFound("Dados não encontrados");
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        // GET: api/PessoaJuridicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaJuridica>> GetPessoaJuridica(int id)
        {
            try
            {
                var pessoaJuridica = await _pessoaJuridica.GetByIdAsync(id);
                if (pessoaJuridica == null)
                {
                    return NotFound("Pessoa não existe!");
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }

        }

        // POST: api/PessoaJuridicas
        [HttpPost]
        public async Task<ActionResult<PessoaJuridica>> PostPessoaJuridica(PessoaJuridica pessoaJuridica)
        {
            try
            {
                PessoaJuridica pessoa = new PessoaJuridica();

                //Validar Cpf
                bool cpf = pessoa.ValidaCpf(pessoaJuridica.Cnpj);
                if (!cpf) return BadRequest("Cpf Inválido");

                //Consultar endereço Via CEP
                await pessoa.ConsultarEndereco(pessoaJuridica.Endereco);

                //Save Endereco
                await _endereco.PostEndereco(pessoaJuridica.Endereco);
                pessoaJuridica.EnderecoId = pessoaJuridica.Endereco.Id;

                //Save Pessoa
                await _pessoaJuridica.Post(pessoaJuridica);

                return CreatedAtAction("GetPessoaJuridica", new { id = pessoaJuridica.Id }, pessoaJuridica);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }

        }

        // PUT: api/PessoaJuridicas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoaJuridica(int id, PessoaJuridica pessoaJuridica)
        {
            if (id != pessoaJuridica.Id)
            {
                return BadRequest();
            }

            try
            {
                //Consultar endereço Via CEP
                if (pessoaJuridica.Endereco.Cep != null) await pessoaJuridica.ConsultarEndereco(pessoaJuridica.Endereco);

                //Update Endereco
                await _endereco.PutEndereco(pessoaJuridica.Endereco);

                //UpDateTelefone
                await _telefone.PutTelefone(pessoaJuridica.Telefones);

                //UpDatePessoa
                await _pessoaJuridica.Put(pessoaJuridica);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaJuridicaExists(id))
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

        // DELETE: api/PessoaJuridicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoaJuridica(int id)
        {
            var pessoaJuridica = await _context.PessoaJuridica.FindAsync(id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            _context.PessoaJuridica.Remove(pessoaJuridica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaJuridicaExists(int id)
        {
            return _context.PessoaJuridica.Any(e => e.Id == id);
        }
    }
}
