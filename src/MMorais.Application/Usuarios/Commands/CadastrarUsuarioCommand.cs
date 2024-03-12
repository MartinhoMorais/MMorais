using FluentValidation;
using MMorais.Core.Util;
using MMorais.Core.Messages;

namespace MMorais.Application.Usuarios.Commands;

public class CadastrarUsuarioCommand : Command
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Telefone { get; private set; }
    public bool Ativo { get; private set; }

    public CadastrarUsuarioCommand(Guid id, string nome, string cpf, string telefone, bool ativo)
    {
        Id = id;
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        Ativo = ativo;
    }
    public override bool EhValido()
    {
        ValidationResult = new CadastrarUsuarioValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class CadastrarUsuarioValidator : AbstractValidator<CadastrarUsuarioCommand>
{

    public static string IdUsuarioErroMsg => "Id do usuário inválido.";
    public static string NomeErroMsg => "Nome do usuário é obrigatório.";
    public static string CpfErroMsg => "O Cpf do usuário é obrigatório.";
    public static string CpfInvalidoMsg => "CPF inválido.";
    public static string TelefoneErroMsg => "Telefone é obrigatório.";

    public CadastrarUsuarioValidator()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage(IdUsuarioErroMsg);

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage(NomeErroMsg);

        RuleFor(c => c.Cpf)
            .NotEmpty()
            .WithMessage(CpfErroMsg);

        RuleFor(c => c.Cpf)
            .Must(Validacao.ValidarCPF)
            .WithMessage(CpfInvalidoMsg);

        RuleFor(c => c.Telefone)
            .NotEmpty()
            .WithMessage(TelefoneErroMsg);
    }
}