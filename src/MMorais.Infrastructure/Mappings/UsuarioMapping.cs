using MMorais.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMorais.Infrastructure.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome).IsRequired().HasColumnType("varchar(1000)");
        builder.HasIndex(c => c.Nome ).HasDatabaseName("IX_Usuario_Nome");

        builder.Property(e => e.Cpf).IsRequired().HasColumnType("varchar(14)");
        builder.Property(e => e.Telefone).IsRequired().HasColumnType("varchar(50)");
        
        builder.Property(x => x.DataCadastro)
            .HasColumnName("DataCadastro").HasColumnType("timestamp without time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.HasIndex(c => c.DataCadastro).HasDatabaseName("IX_Usuario_DataCadastro");

        builder.Property(x => x.DataAlteracao).
            HasColumnType("timestamp without time zone");

        builder.Property(x => x.Ativo)
            .HasColumnName("Ativo")
            .IsRequired();
    }
}