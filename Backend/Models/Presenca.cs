using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("PRESENCA")]
    public partial class Presenca
    {
        [Key]
        [Column("PRESENCA_ID")]
        public int PresencaId { get; set; }
        [Required]
        [Column("STATUS_PRESENCA")]
        public bool? StatusPresenca { get; set; }
        [Column("USUARIO_ID")]
        public int? UsuarioId { get; set; }
        [Column("EVENTO_ID")]
        public int? EventoId { get; set; }

        [ForeignKey(nameof(EventoId))]
        [InverseProperty("Presenca")]
        public virtual Evento Evento { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty("Presenca")]
        public virtual Usuario Usuario { get; set; }
    }
}
