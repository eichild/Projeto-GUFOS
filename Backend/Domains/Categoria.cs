using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("CATEGORIA")]
    public partial class Categoria
    {
        public Categoria()
        {
            Evento = new HashSet<Evento>();
        }

        [Key]
        [Column("CATEGORIA_ID")]
        public int CategoriaId { get; set; }
        [Required]
        [Column("TITULO")]
        [StringLength(50)]
        public string Titulo { get; set; }

        [InverseProperty("Categoria")]
        public virtual ICollection<Evento> Evento { get; set; }
    }
}
