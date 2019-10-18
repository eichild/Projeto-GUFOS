using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("EVENTO")]
    public partial class Evento
    {
        public Evento()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        [Column("EVENTO_ID")]
        public int EventoId { get; set; }
        [Required]
        [Column("TITULO")]
        [StringLength(50)]
        public string Titulo { get; set; }
        [Column("DATA_EVENTO", TypeName = "datetime")]
        public DateTime DataEvento { get; set; }
        [Required]
        [Column("ACESSO_LIVRE")]
        public bool? AcessoLivre { get; set; }
        [Column("CATEGORIA_ID")]
        public int? CategoriaId { get; set; }
        [Column("LOCALIZACAO_ID")]
        public int? LocalizacaoId { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        [InverseProperty("Evento")]
        public virtual Categoria Categoria { get; set; }
        [ForeignKey(nameof(LocalizacaoId))]
        [InverseProperty("Evento")]
        public virtual Localizacao Localizacao { get; set; }
        [InverseProperty("Evento")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
