using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Domains
{
    public partial class BDGUFOSContext : DbContext
    {
        public BDGUFOSContext()
        {
        }

        public BDGUFOSContext(DbContextOptions<BDGUFOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Localizacao> Localizacao { get; set; }
        public virtual DbSet<Presenca> Presenca { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server= DESKTOP-444V57F\\SQLEXPRESS; Database=BDGUFOS; User Id= sa; Password=132");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.Property(e => e.AcessoLivre).HasDefaultValueSql("((1))");

                entity.Property(e => e.Titulo).IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK__EVENTO__CATEGORI__4222D4EF");

                entity.HasOne(d => d.Localizacao)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.LocalizacaoId)
                    .HasConstraintName("FK__EVENTO__LOCALIZA__4316F928");
            });

            modelBuilder.Entity<Localizacao>(entity =>
            {
                entity.HasIndex(e => e.Cnpj)
                    .HasName("UQ__LOCALIZA__AA57D6B4247F2FF9")
                    .IsUnique();

                entity.Property(e => e.Cnpj)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Endereco).IsUnicode(false);

                entity.Property(e => e.RazaoSocial).IsUnicode(false);
            });

            modelBuilder.Entity<Presenca>(entity =>
            {
                entity.Property(e => e.StatusPresenca).IsUnicode(false);

                entity.HasOne(d => d.Evento)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.EventoId)
                    .HasConstraintName("FK__PRESENCA__EVENTO__571DF1D5");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__PRESENCA__USUARI__5629CD9C");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);

                entity.HasOne(d => d.TipoUsuario)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.TipoUsuarioId)
                    .HasConstraintName("FK__USUARIO__TIPO_US__3E52440B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
