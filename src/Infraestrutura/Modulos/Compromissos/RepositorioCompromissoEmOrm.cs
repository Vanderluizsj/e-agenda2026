using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Modulos.Compromissos;

public sealed class RepositorioCompromissoEmOrm(EAgendaDbContext dbContext) :
    RepositorioBaseEmOrm<Compromisso>(dbContext), IRepositorioCompromisso
{
    public override Compromisso? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(c => c.Contato)
            .SingleOrDefault(c => c.Id == idSelecionado);
    }

    public override List<Compromisso> SelecionarTodos()
    {
        return registros
            .OrderBy(c => c.DataDeOcorrencia)
            .ThenBy(c => c.HoraDeInicio)
            .Include(c => c.Contato)
            .ToList();
    }

    public bool ExisteCompromissoNaDataEHora(
        DateOnly data,
        TimeOnly horaInicio,
        TimeOnly horaTermino,
        Guid? idIgnorado = null
    )
    {
        return registros.Any(c =>
            c.Id != idIgnorado &&
            c.DataDeOcorrencia == data &&
            horaInicio < c.HoraDeTermino &&
            horaTermino > c.HoraDeInicio
        );
    }
}

