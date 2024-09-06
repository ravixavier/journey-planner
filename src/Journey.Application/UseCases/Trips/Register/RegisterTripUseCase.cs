using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register;
public class RegisterTripUseCase
{
    public ResponseShortTripJson Execute(RequestRegisterTripJson request)
    {
        Validate(request);

        var dbContext = new JourneyDbContext();

        var entity = new Trip
        // seria interessnte devolver essa entidade criada
        // então eu adiciono uma resposta de Journey.Communication
        // ResponseShortTripJson.cs para retornar na função uma versão
        // resumida da nova entidade (trip) criada 
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        dbContext.Trips.Add(entity);
        // prepara o banco de dados para inserir essa entidade 
        // como se ele escrevesse a querie aqui

        dbContext.SaveChanges();
        // aqui eu comando ele que persista na querie
        // e salve as modificações

        return new ResponseShortTripJson
        {
            Name = entity.Name,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Id = entity.Id,
        };
    }

    private void Validate(RequestRegisterTripJson request)
    {
        // quais são as validações que precisamos?
        // o nome não pode ser vazio, nulo ou espaços em branco.
        // o .Net já possui um função que verifica isso, string.IsNullOrWhiteSpace
        // essa função devolve 'true' se por algum acaso a string for vazia ou nula, então lançamos um exeção

        var validator = new RegisterTripValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false) {
            var errorMessages = result.Errors.Select(erro => erro.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
