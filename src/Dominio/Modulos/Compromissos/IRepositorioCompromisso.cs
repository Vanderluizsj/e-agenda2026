
using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Compromissos;

public interface IRepositorioCompromisso : IRepositorio<Compromisso>
{    
    bool ExisteCompromissoNaDataEHora(DateOnly data, TimeOnly horaInicio, TimeOnly horaTermino, Guid? idIgnorado = null);
}

