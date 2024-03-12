using MediatR;
using Microsoft.AspNetCore.Mvc;
using MMorais.Core.Messages.CommonMessages.Notifications;

namespace MMorais.API.Controllers;

public abstract class BaseController : Controller
{
    private readonly DomainNotificationHandler _notifications;
    protected BaseController(INotificationHandler<DomainNotification> notifications)
    {
        _notifications = (DomainNotificationHandler)notifications;
    }

    protected bool OperacaoValida()
    {
        return !_notifications.TemNotificacao();
    }

    protected IEnumerable<string> ObterMensagensErro()
    {
        return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
    }
}