using eAgenda.Aplicacao.Modulos.Categorias;
using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Aplicacao.Modulos.Despesas;
using eAgenda.Dominio.Modulos.Contatos;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.Contatos;

public sealed class ContatoController(ServicoContato servicoContato) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ContatoDto> dtos = servicoContato.SelecionarTodos();

        return View(dtos);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {        
        CadastrarContatoViewModel cadastrarVm = new(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );
        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarContatoViewModel cadastrarVm)
    {
        CadastrarContatoDto dto = new(
                    cadastrarVm.Nome ?? string.Empty,
                    cadastrarVm.Telefone ?? string.Empty,
                    cadastrarVm.Email ?? string.Empty,
                    cadastrarVm.Cargo ?? string.Empty,
                    cadastrarVm.Empresa ?? string.Empty
                );

        Result<Guid> resultado = servicoContato.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo = erro.Metadata["Campo"].ToString() ?? string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<ContatoDto> resultado = servicoContato.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        ContatoDto dto = resultado.Value;

        EditarContatoViewModel viewModel = new(
            id,
            dto.Nome,
            dto.Telefone,
            dto.Email,
            dto.Cargo,
            dto.Empresa
        );



        return View(viewModel);

    }

    [HttpPost]
    public ActionResult Editar(EditarContatoViewModel editarVm)
    {
        EditarContatoDto dto = new(
            editarVm.Id,
            editarVm.Nome ?? string.Empty,
            editarVm.Telefone ?? string.Empty,
            editarVm.Email ?? string.Empty,
            editarVm.Cargo ?? string.Empty,
            editarVm.Empresa ?? string.Empty
        );

        Result resultado = servicoContato.Editar(dto);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo = erro.Metadata["Campo"].ToString() ?? string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<ContatoDto> resultado = servicoContato.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        ContatoDto dto = resultado.Value;

        ExcluirContatoViewModel viewModel = new(dto.Id, dto.Nome, dto.Email, dto.Telefone, dto.Cargo, dto.Empresa);

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirContatoViewModel excluirVm)
    {
        servicoContato.Excluir(excluirVm.Id);

        return RedirectToAction(nameof(Listar));
    }
}
