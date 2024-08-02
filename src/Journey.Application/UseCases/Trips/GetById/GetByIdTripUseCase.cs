using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById;
public class GetByIdTripUseCase
{
    public ResponseTripJson Execute(Guid Id)
    {
        JourneyDbContext dbContext = new JourneyDbContext();

        var trip = dbContext.Trips.Include(trip => trip.Activities).FirstOrDefault(trip => trip.Id == Id);
                                    // FirstOrDeafault garante que se por algum acaso 
                                    // o Id da resquest não corresponder com nenhum Id
                                    // no banco de dados o valor retornado será null
                                    // caso contrário geraria uma exceção
                                                    // aqui temos uma função lambda
                                                    // antes do sinal "=>" temos o parametro da função e depois do sinal "=>" temos o que a função vai realizar
                                                    // nesse caso essa função vai comparar se trip.Id é igual ao Id que recebemos como parametro na função execute

        return new ResponseTripJson
        {
            Id = trip.Id,
            Name = trip.Name,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            Activities = trip.Activities.Select(activity => new ResponseActivityJson
            {
                Id = activity.Id,
                Name = activity.Name,
                Date = activity.Date,
                Status = (Communication.Enums.ActivityStatus)activity.Status
            }).ToList()
        };
    }
}
