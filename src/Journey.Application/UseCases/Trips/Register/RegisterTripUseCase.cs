using Journey.Communication.Requests;

namespace Journey.Application.UseCases.Trips.Register;
public class RegisterTripUseCase
{
    public void Execute(RequestRegisterTripJson request)
    {
        Validate(request);
    }

    private void Validate(RequestRegisterTripJson request)
    {
        // quais são as validações que precisamos?
        // o nome não pode ser vazio, nulo ou espaços em branco.
        // o .Net já possui um função que verifica isso, string.IsNullOrWhiteSpace
        // essa função devolve 'true' se por algum acaso a string for vazia ou nula, então lançamos um exeção

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Nome não pode ser vazio!");
        }

        // na próxima regra de negócio, eu não quero aceitar um startdate uma data menor que a data atual.

        if (request.StartDate.Date < DateTime.UtcNow.Date)
        {
            throw new ArgumentException("Data de início inválida!");
        }

        if (request.EndDate < request.StartDate)
        {
            throw new ArgumentException("Data de retorno inválida!");
        }
    }
}
