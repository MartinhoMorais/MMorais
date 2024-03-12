namespace MMorais.Application.Usuarios.Queries.ViewModel;

public class UsuarioViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get;  set; }
    public string Telefone { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get;  set; }
    public bool Ativo { get; set; }

}