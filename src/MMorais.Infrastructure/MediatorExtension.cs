﻿using MMorais.Core.DomainObjects;
using MMorais.Core.Communication.Mediator;

namespace MMorais.Infrastructure;

public static class MediatorExtension
{
    public static async Task PublicarEventos(this IMediatorHandler mediator, UsuarioContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents
            .Select(async (domainEvent) => {
                await mediator.PublicarEvento(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}