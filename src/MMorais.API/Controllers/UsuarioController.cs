using MediatR;
using MMorais.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using MMorais.Application.Usuarios.Queries;
using MMorais.Application.Usuarios.Commands;
using MMorais.Application.Usuarios.Commands.ViewModel;
using MMorais.Application.Usuarios.Queries.ViewModel;
using MMorais.Core.Messages.CommonMessages.Notifications;


namespace MMorais.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IMediator _mediator;
        public UsuarioController(
            INotificationHandler<DomainNotification> notifications,
            IMediator mediator) : base(notifications)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("obter-todos")]
        public async Task<ActionResult<BaseRequestResult<IEnumerable<UsuarioViewModel>>>> ObterTodos()
        {
            var query = new ObterTodosUsuariosQuery();
            var usuarios = await _mediator.Send(query);
            return Ok(new BaseRequestResult<IEnumerable<UsuarioViewModel>>
            {
                DataResult = usuarios,
                Error = false,
                ErrorMessage = "",
                ValidationMessages = new string[] { }
            });
        }

        [HttpPost]
        public async Task<ActionResult<string>> CadastrarUsuario(CadastraUsuarioViewModel viewModel)
        {
            try
            {
                var command = new CadastrarUsuarioCommand(Guid.NewGuid(), viewModel.Nome, viewModel.Cpf, viewModel.Telefone, true);

                await _mediator.Send(command);

                if (OperacaoValida())
                    return Ok(new BaseRequestResult<string>
                    {
                        DataResult = "Usuário cadastrado com sucesso!",
                        Error = false,
                        ErrorMessage = "",
                        ValidationMessages = new string[] { }
                    });

                var resultValidation = new BaseRequestResult<string>
                {
                    Error = true,
                    ValidationMessages = ObterMensagensErro()
                };
                return BadRequest(resultValidation);
            }
            catch (Exception e)
            {
                var result = new BaseRequestResult<string>
                {
                    Error = true,
                    ErrorMessage = e.Message,
                    DataResult = null!,
                    ValidationMessages = new string[] { }
                };

                return BadRequest(result);
            }
        }
        [HttpPut]
        public async Task<ActionResult<string>> AlterarUsuario(AlteraUsuarioViewModel viewModel)
        {
            try
            {
                var command = new AlterarUsuarioCommand(viewModel.Id, viewModel.Nome, viewModel.Cpf, viewModel.Telefone, DateTime.Now, viewModel.Ativo);

                await _mediator.Send(command);

                if (OperacaoValida())
                    return Ok(new BaseRequestResult<string>
                    {
                        DataResult = "Usuário alterado com seucesso!",
                        Error = false,
                        ErrorMessage = "",
                        ValidationMessages = new string[] { }
                    });

                var resultValidation = new BaseRequestResult<string>
                {
                    Error = true,
                    ValidationMessages = ObterMensagensErro()
                };
                return BadRequest(resultValidation);
            }
            catch (Exception e)
            {
                var result = new BaseRequestResult<string>
                {
                    Error = true,
                    ErrorMessage = e.Message,
                    DataResult = null!,
                    ValidationMessages = new string[] { }
                };

                return BadRequest(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> ExcluirUsuario(Guid id)
        {
            try
            {
                var command = new ExcluirUsuarioCommand(id);

                await _mediator.Send(command);

                if (OperacaoValida())
                    return Ok(new BaseRequestResult<string>
                    {
                        DataResult = "Usuário excluído com sucesso!",
                        Error = false,
                        ErrorMessage = "",
                        ValidationMessages = new string[] { }
                    });

                var resultValidation = new BaseRequestResult<string>
                {
                    Error = true,
                    ValidationMessages = ObterMensagensErro()
                };
                return BadRequest(resultValidation);
            }
            catch (Exception e)
            {
                var result = new BaseRequestResult<string>
                {
                    Error = true,
                    ErrorMessage = e.Message,
                    DataResult = null!,
                    ValidationMessages = new string[] { }
                };

                return BadRequest(result);
            }
        }
    }
}
