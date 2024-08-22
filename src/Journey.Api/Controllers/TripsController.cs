using Journey.Application.UseCases.Trips.Register;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Communication.Requests;
using Journey.Exception;
using Microsoft.AspNetCore.Mvc;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        try
        {
            var useCase = new RegisterTripUseCase();
            // ou RegisterTripUseCase useCase = new RegisterTripUseCase();
            // usar 'var' é apenas uma maneira mais fácil de criar uma instancia da variável.

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
        catch (JourneyException ex)
        {
            return BadRequest(ex.Message);
        }
       catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ResourceErrorMessages.UNKNOWN_ERROR);
        }
    }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status201Created)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripUseCase();

        var response = useCase.Execute();

        return Ok(response);
    }

    [HttpGet]
    [Route("{Id}")]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute]Guid Id)
    {
        try
        {
            var useCase = new GetByIdTripUseCase();

            var response = useCase.Execute(Id);

            return Ok(response);
        }
        catch (JourneyException ex)
        {
            return NotFound(ex.Message);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro Desconhecido"); 
        }
    }
}
