namespace MMorais.Application.Usuarios.Commands.ViewModel;

public class AlteraUsuarioViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get;  set; }
    public string Telefone { get; set; }
    public bool Ativo { get; set; }
}