﻿using Journey.Application.UseCases.Trips.Register;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Communication.Requests;
using Journey.Exception;
using Microsoft.AspNetCore.Mvc;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Application.UseCases.Trips.Delete;
using Journey.Communication;
using Journey.Application.UseCases.Activities.Register;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
            var useCase = new RegisterTripUseCase();
            // ou RegisterTripUseCase useCase = new RegisterTripUseCase();
            // usar 'var' é apenas uma maneira mais fácil de criar uma instancia da variável.

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
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
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute]Guid Id)
    {
            var useCase = new GetByIdTripUseCase();

            var response = useCase.Execute(Id);

            return Ok(response);
    }

    [HttpDelete]
    [Route("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute]Guid Id)
    {
            var useCase = new DeleteTripByIdUseCase();

            useCase.Execute(Id);

            return NoContent();
    }

    [HttpPost]
    [Route("{tripId}/activity")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult RegisterActivity(
        [FromRoute] Guid tripId, 
        [FromBody] RequestRegisterActivityJson request)
    {
        var useCase = new RegisterActivityForTripUseCase();

        var response = useCase.Execute(tripId, request);

        return Created(string.Empty, response);
    }

}
