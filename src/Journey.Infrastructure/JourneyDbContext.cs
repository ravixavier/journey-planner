using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure;

/*
 * JourneyDbContext vai ser a classe responsável por traduzir
 * para a gente uma entidade em queries para inserir no banco de dados
 * e também o contrário, traduzir uma querie que
 * vai recuperar valores em uma entidade
 */

public class JourneyDbContext : DbContext
//trocar a classe para public e adicionar uma herança com dbcontext
{
    public DbSet<Trip> Trips { get; set; }
    /*
     * em "Trips" normalmente tem que ser o nome da tabela
     * 
     * dentro das chaves eu passo minha entidade (Trip)
     * essa entidade (classe), Trip é exatamente um espelho
     * do que há no meu banco de dados na tabela Trip, com todos os atributos
     * (id, name, startdate, enddate...)
     */

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\Ravi\\source\\workspace\\JourneyDatabase.db");
    }
}
