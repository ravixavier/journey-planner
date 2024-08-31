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

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidationException(ResourceErrorMessages.NAME_EMPTY);
        }

        // na próxima regra de negócio, eu não quero aceitar um start-date uma data menor que a data atual.
        // nem um end-date menor que a start-date.

        if (request.StartDate.Date < DateTime.UtcNow.Date)
        {
            throw new ErrorOnValidationException(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);
        }

        if (request.EndDate.Date < request.StartDate.Date)
        {
            throw new ErrorOnValidationException(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
        }
    }
}
