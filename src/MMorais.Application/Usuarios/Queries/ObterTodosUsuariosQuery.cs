using MediatR;
using AutoMapper;
using MMorais.Domain;
using MMorais.Application.Usuarios.Queries.ViewModel;

namespace MMorais.Application.Usuarios.Queries;

public class ObterTodosUsuariosQuery : IRequest<IEnumerable<UsuarioViewModel>>
{

    public class ObterTodosUsuariosQueryHandler : IRequestHandler<ObterTodosUsuariosQuery, IEnumerable<UsuarioViewModel>>
    {
        private readonly IUsuarioDapperRepository _usuarioDapperRepository;
        private readonly IMapper _mapper;
        

        public ObterTodosUsuariosQueryHandler(IUsuarioDapperRepository usuarioDapperRepository, IMapper mapper)
        {
            _mapper = mapper;
            _usuarioDapperRepository = usuarioDapperRepository;
        }

        public async Task<IEnumerable<UsuarioViewModel>> Handle(ObterTodosUsuariosQuery request, CancellationToken cancellationToken)
        {
            var query = await _usuarioDapperRepository.ObterTodos();
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(query);
        }
    }
}