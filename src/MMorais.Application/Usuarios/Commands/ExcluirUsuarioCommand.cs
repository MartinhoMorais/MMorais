using FluentValidation;
using MMorais.Core.Messages;

namespace MMorais.Application.Usuarios.Commands;

public class ExcluirUsuarioCommand : Command
{
    public Guid Id { get; private set; }

    public ExcluirUsuarioCommand(Guid id)
    {
        Id = id;
    }
    public override bool EhValido()
    {
        ValidationResult = new ExcluirUsuarioValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
public class ExcluirUsuarioValidator : AbstractValidator<ExcluirUsuarioCommand>
{
    public static string IdUsuarioErroMsg => "Id do usuário inválido";

    public ExcluirUsuarioValidator()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage(IdUsuarioErroMsg);
    }
}