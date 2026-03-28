using ApiFinanceiro.Dtos;
using ApiFinanceiro.Exceptions;
using ApiFinanceiro.Models;
using ApiFinanceiro.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/receitas")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly ReceitaService _receitaService;
        public ReceitaController(ReceitaService receitaService)
        {
            _receitaService = receitaService;
        }

        [HttpGet()]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var listaReceitas = await _receitaService.FindAll();
                return Ok(listaReceitas);
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
        public async Task<IActionResult> Create([FromBody]ReceitaDto novaReceita)
        {
            try
            {
                var receita = await _receitaService.Create(novaReceita);
                return Created("", receita);
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
                var receita = await _receitaService.FindById(id);
                return Ok(receita);
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
        public async Task<IActionResult> Update(int id, [FromBody] ReceitaUpdateDto receitaDto)
        {
            try
            {
                var receita = await _receitaService.Update(id, receitaDto);
                return Ok(receita);
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
                await _receitaService.Remove(id);
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
    }
}
