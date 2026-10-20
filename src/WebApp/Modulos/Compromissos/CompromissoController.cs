using eAgenda.Aplicacao.Modulos.Compromissos;
using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Dominio.Modulos.Compromissos;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.Compromissos;

public sealed class CompromissoController(
    ServicoCompromisso servicoCompromisso,
    ServicoContato servicoContato
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<CompromissoDto> dtos = servicoCompromisso.SelecionarTodos();

        return View(dtos);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        List<ContatoDto> contatosDtos = servicoContato.SelecionarTodos();

        CadastrarCompromissoViewModel viewModel = new(
            string.Empty,
            DateOnly.FromDateTime(DateTime.Now),
            new TimeOnly(9, 0),
            new TimeOnly(10, 0),
            TipoCompromisso.Presencial,
            string.Empty,
            null
        );

        return View(viewModel with { ContatosDisponiveis = contatosDtos });
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCompromissoViewModel viewModel)
    {
        CadastrarCompromissoDto dto = new(
            viewModel.Assunto ?? string.Empty,
            viewModel.DataDeOcorrencia,
            viewModel.HoraDeInicio,
            viewModel.HoraDeTermino,
            viewModel.Tipo,
            viewModel.Local ?? string.Empty,
            viewModel.IdContato
        );

        Result<Guid> resultado = servicoCompromisso.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo = erro.Metadata["Campo"].ToString() ?? string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            List<ContatoDto> contatosDtos = servicoContato.SelecionarTodos();

            return View(viewModel with { ContatosDisponiveis = contatosDtos });
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<CompromissoDto> resultado = servicoCompromisso.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        CompromissoDto dto = resultado.Value;

        EditarCompromissoViewModel viewModel = new(
            id,
            dto.Assunto,
            dto.DataDeOcorrencia,
            dto.HoraDeInicio,
            dto.HoraDeTermino,
            dto.Tipo,
            dto.Local,
            dto.Contato?.Id
        );

        List<ContatoDto> contatosDtos = servicoContato.SelecionarTodos();

        return View(viewModel with { ContatosDisponiveis = contatosDtos });
    }

    [HttpPost]
    public ActionResult Editar(EditarCompromissoViewModel viewModel)
    {
        EditarCompromissoDto dto = new(
            viewModel.Id,
            viewModel.Assunto ?? string.Empty,
            viewModel.DataDeOcorrencia,
            viewModel.HoraDeInicio,
            viewModel.HoraDeTermino,
            viewModel.Tipo,
            viewModel.Local ?? string.Empty,
            viewModel.IdContato
        );

        Result resultado = servicoCompromisso.Editar(dto);

        if (resultado.IsFailed)
        {
            if (!resultado.Errors.Any(e => e.HasMetadataKey("Campo")))
            {
                TempData["MensagemErro"] = resultado.Errors.First().Message;

                return RedirectToAction(nameof(Listar));
            }

            foreach (IError erro in resultado.Errors)
            {
                string campo = erro.Metadata["Campo"].ToString() ?? string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            List<ContatoDto> contatosDtos = servicoContato.SelecionarTodos();

            return View(viewModel with { ContatosDisponiveis = contatosDtos });
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<CompromissoDto> resultado = servicoCompromisso.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        CompromissoDto dto = resultado.Value;

        ExcluirCompromissoViewModel viewModel = new(
            dto.Id,
            dto.Assunto,
            dto.DataDeOcorrencia,
            dto.HoraDeInicio,
            dto.HoraDeTermino,
            dto.Tipo,
            dto.Local,
            dto.Contato?.Nome
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirCompromissoViewModel excluirVm)
    {
        Result resultado = servicoCompromisso.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(Listar));
    }
}

