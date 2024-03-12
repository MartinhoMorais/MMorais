using AutoMapper;
using MMorais.Domain;
using MMorais.Application.Usuarios.Queries.ViewModel;

namespace MMorais.Application.AutoMapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
    }
}