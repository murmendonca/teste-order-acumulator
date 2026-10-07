using Microsoft.AspNetCore.Mvc;
using OrderAccumulator.Application.DTOs.v1;
using OrderAccumulator.Application.DTOs.v1.Ordens;
using OrderAccumulator.Application.Interfaces.v1.Services;

namespace OrderAccumulator.Api.Controllers.v1;

[ApiController]
[Route("api/v1/ordem-accumulators")]
public class OrderAccumulatorController(IOrdemService ordemService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Resultado>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] OrdemRequest ordemRequest)
    {
        try
        {
            var resultado = await ordemService.ProcessarOrdemAsync(ordemRequest);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }
            
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(Resultado.Erro(0, $"Ocorreu um erro ao processar a ordem: {ex.Message}"));
        }
    }
}
