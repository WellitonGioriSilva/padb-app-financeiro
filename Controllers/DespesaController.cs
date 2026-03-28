using ApiFinanceiro.Dtos;
using ApiFinanceiro.Exceptions;
using ApiFinanceiro.Models;
using ApiFinanceiro.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ApiFinanceiro.Controllers
{
    [Route("/despesas")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly DespesaService _despesaService;

        public DespesaController(DespesaService despesaService)
        {
            _despesaService = despesaService;
        }

        [HttpGet()]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var listaDespesas = await _despesaService.FindAll();
                return Ok(listaDespesas);
            }
            catch (ErrorServiceException ex)
            {
                return ex.ToActionResult(this);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] DespesaDto novaDespesa)
        {
            try
            {
                var despesa = await _despesaService.Create(novaDespesa);

                return Created("", despesa);
            }
            catch (ErrorServiceException ex)
            {
                return ex.ToActionResult(this);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            try
            {
                var despesa = await _despesaService.FindById(id);

                return Ok(despesa);
            }
            catch (ErrorServiceException ex)
            {
                return ex.ToActionResult(this);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DespesaUpdateDto despesaDto)
        {
            try
            {
                var despesa = await _despesaService.Update(id, despesaDto);
                return Ok(despesa);
            }
            catch (ErrorServiceException ex)
            {
                return ex.ToActionResult(this);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                _despesaService?.Remove(id);

                return NoContent();
            }
            catch (ErrorServiceException ex)
            {
                return ex.ToActionResult(this);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[HttpPost("{id}/pagamento")]
        //public ActionResult Pay([FromBody] DespesaDto novaDespesa)
        //{

        //    var despesa = new Despesa
        //    {
        //        Descricao = novaDespesa.Descricao,
        //        Valor = novaDespesa.Valor,
        //        Categoria = novaDespesa.Categoria,
        //        DataVencimento = novaDespesa.DataVencimento,
        //        Situacao = "Pendente"
        //    };

        //    listaDespesas.Add(despesa);

        //    return Created("", despesa);
        //}
    }
}
