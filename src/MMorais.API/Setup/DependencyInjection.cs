using Npgsql;
using MediatR;
using System.Data;
using MMorais.Domain;
using MMorais.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MMorais.Application.AutoMapper;
using MMorais.Infrastructure.Repository;
using MMorais.Application.Usuarios.Events;
using MMorais.Core.Communication.Mediator;
using MMorais.Application.Usuarios.Commands;
using MMorais.Core.Messages.CommonMessages.Notifications;

namespace MMorais.API.Setup;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<UsuarioContext>(options =>
            options.UseNpgsql(connectionString, builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        //Dapper
        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        });


        var myhandlers = AppDomain.CurrentDomain.Load("MMorais.Application");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(myhandlers);
        });

        services.AddAutoMapper(typeof(AutoMapperConfig));
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioDapperRepository, UsuarioDapperRepository>();
        // Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        //Notifications
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        //Commands
        services.AddScoped<IRequestHandler<CadastrarUsuarioCommand, bool>, UsuarioCommandHandler>();
        services.AddScoped<IRequestHandler<AlterarUsuarioCommand, bool>, UsuarioCommandHandler>();
        services.AddScoped<IRequestHandler<ExcluirUsuarioCommand, bool>, UsuarioCommandHandler>();

        //Events
        services.AddScoped<INotificationHandler<UsuarioCadastradoEvent>, UsuarioEventHandler>();
        services.AddScoped<INotificationHandler<UsuarioAlteradoEvent>, UsuarioEventHandler>();
        services.AddScoped<INotificationHandler<UsuarioExcluidoEvent>, UsuarioEventHandler>();
    }
}